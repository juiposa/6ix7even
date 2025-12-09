using System;
using System.Numerics;
using _6ix7even.Agent;
using _6ix7even.Agent.Actions;
using _6ix7even.Agent.Enums;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using Serilog;

namespace _6ix7even.Windows;

public class MainWindow : Window, IDisposable
{
    private readonly IPlayerState playerState;
    private readonly IObjectTable objectTable;
    private readonly IPartyList partyList;
    private readonly ActionAgent agent;
    private readonly Plugin plugin;

    // We give this window a hidden ID using ##.
    // The user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin, IPlayerState playerState,  IObjectTable objectTable, IPartyList partyList, ActionAgent agent)
        : base("My Amazing Window##With a hidden ID", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    
        this.playerState = playerState;
        this.objectTable = objectTable;
        this.partyList = partyList;
        this.agent = agent;
        this.plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        // Normally a BeginChild() would have to be followed by an unconditional EndChild(),
        // ImRaii takes care of this after the scope ends.
        // This works for all ImGui functions that require specific handling, examples are BeginTable() or Indent().
        using (var child = ImRaii.Child("SomeChildWithAScrollbar", Vector2.Zero, true))
        {
            // Check if this child is drawing
            if (child.Success)
            {
                foreach (var partyMember in partyList)
                {   
                    ImGui.Text(partyMember.Name.ToString());
                    if (ImGui.Button("6-7 " + partyMember.Name.ToString()))
                    {
                        ulong obj = partyMember.GameObject.GameObjectId;
                        Log.Information("PLAYER: {0}", partyMember.Name);
                        agent.DoSixSeven(obj);
                    }
                }
            }
        }
    }
}