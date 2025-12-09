using _6ix7even.Agent.Enums;
using _6ix7even.Signature;
using FFXIVClientStructs;
using FFXIVClientStructs.FFXIV.Component.SteamApi;
using Serilog;

namespace _6ix7even.Agent.Actions;

public class MarkEntity
{
    private readonly State state;

    public MarkEntity(State state)
    {
        this.state = state;
    }

    private class MarkJob
    {
        public MarkJob(PlayerMarker.SignEnum[] markers, ulong target, ushort intervalMs = 500)
        {
            Markers = markers;
            Target = target;
            IntervalMs = intervalMs;
        }
        internal PlayerMarker.SignEnum[] Markers { get; set; }
        internal ulong Target { get; set; }
        internal ushort IntervalMs { get; set; }
    }

    public void Invoke(PlayerMarker.SignEnum marker, ulong target)
    {
        Log.Information("Running Mark For {target}: {marker}", marker, target);
        object[] param = new object[] { state.MarkingCtrlPtr, (byte)PlayerMarker.GetSignIndex(marker), (uint)target };
        state._markingFuncPtr.DynamicInvoke(param);
    }
    
    private static void Sig()
    {
        
    }

    public void InvokeSeries(PlayerMarker.SignEnum[] markers, ulong target, ushort intervalMs = 500)
    {   
        MarkJob job = new MarkJob(markers, target, intervalMs);
        ThreadPool.QueueUserWorkItem(RunSeries, job);
    }

    private void RunSeries(Object job)
    {   
        MarkJob jobState = (MarkJob)job;
        foreach (var marker in jobState.Markers)
        {
            Invoke(marker, jobState.Target);
            Thread.Sleep(jobState.IntervalMs);
        }
    }
}