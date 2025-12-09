using System.Runtime.InteropServices;
using Dalamud.Game;
using Dalamud.Hooking;
using Serilog;

namespace _6ix7even.Signature
{

    public sealed class State
    {   
        public ISigScanner SigScanner { get; private set; }
        
        internal SigLocator Sig;
        private Dictionary<string, nint> _sigs = new Dictionary<string, nint>();
        
        internal delegate char MarkingFunctionDelegate(nint ctrl, byte markId, uint actorId);
        internal MarkingFunctionDelegate _markingFuncPtr = null;
        internal Hook<MarkingFunctionDelegate> _markingFuncHook = null;
        internal nint MarkingCtrlPtr;
        
        internal delegate void PostCommandDelegate(IntPtr ui, IntPtr cmd, IntPtr unk1, byte unk2);
        internal PostCommandDelegate _postCmdFuncptr = null;

        public State(ISigScanner sigScanner)
        {
            this.SigScanner = sigScanner;
            Sig = new SigLocator(this);
        }
        
        internal char MarkingHook(nint ctrl, byte markId, uint actorId)
        {
            if (_sigs.ContainsKey("MarkingCtrl") == false)
            {
                SetMarkingController(ctrl);
            }
            return _markingFuncHook.Original(ctrl, markId, actorId);
        }

        internal void SetMarkingController(nint addr)
        {
            _sigs["MarkingCtrl"] = addr;
            MarkingCtrlPtr = addr;
            Log.Debug("Marking controller found at {0}", addr.ToString("X"));
            if (_markingFuncPtr != null)
            {
                if (_markingFuncHook != null)
                {
                    _markingFuncPtr = new MarkingFunctionDelegate(MarkingHook);
                    Log.Information("Marking by function pointer available (hooked)");
                }
                else
                {
                    Log.Information("Marking by function pointer available");
                }
            }
        }

        internal void GetSignatures()
        {
            nint addr1, addr2;
            addr1 = Sig.ScanText("48 89 5C 24 10 48 89 6C 24 18 57 48 83 EC 20 8D 42");
            if (addr1 != IntPtr.Zero)
            {
                _sigs["MarkingFunc"] = addr1;
                MarkingCtrlPtr = addr1;
                Log.Information("Marking function found at {0}", addr1.ToString("X"));
                _markingFuncPtr = Marshal.GetDelegateForFunctionPointer<MarkingFunctionDelegate>(addr1);
            }
            else
            {
                Log.Warning("Marking function not found");
            }

            addr2 = Sig.OldGetStaticAddressFromSig(
                "48 8B D0 48 8D 0D ? ? ? ? E8 ? ? ? ? 3B F8 48 8D 0D ? ? ? ? 8B D7");
            if (addr2 != IntPtr.Zero)
            {
                SetMarkingController(addr2);
            }
            else
            {
                Log.Warning("Marking controller not found");
            }

            if (addr1 == IntPtr.Zero || addr2 == IntPtr.Zero)
            {
                Log.Warning("Marking by function pointer not available, falling back to command injection");
            }

            addr1 = Sig.ScanText("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F2 48 8B F9 45 84 C9");
            if (addr1 != IntPtr.Zero)
            {
                _postCmdFuncptr = Marshal.GetDelegateForFunctionPointer<PostCommandDelegate>(addr1);
                Log.Information("Command post function found at {0}", addr1.ToString("X"));
            }
            else
            {
                Log.Warning("Command post function not found");
            }
        }

    }
}

