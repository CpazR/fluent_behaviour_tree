#if TOOLS
using Godot;
using System.Collections.Generic;
using Array = Godot.Collections.Array;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Debugging;

[Tool]
public partial class FluentBehaviorTreeDebugger : EditorDebuggerPlugin {

    private static string MESSAGE_PREFIX = "FluentBehaviorTree";

    public static string MESSAGE_REGISTER_TREE = "FluentBehaviorTree:RegisterTree";

    public static string MESSAGE_UNREGISTER_TREE = "FluentBehaviorTree:UnregisterTree";

    public static string MESSAGE_UPDATE_TREE = "FluentBehaviorTree:UpdateTree";


    private BehaviorTreeDebuggerPanel debuggerPanel = new BehaviorTreeDebuggerPanel();

    private EditorDebuggerSession session;

    public override void _SetupSession(int sessionId) {
        session = GetSession(sessionId);
        session.Started += () => debuggerPanel.Start();
        session.Stopped += () => debuggerPanel.Stop();


        GD.Print("Adding debugger session tab");

        debuggerPanel.Name = "Behavior Tree Live View";
        debuggerPanel.session = session;
        session.AddSessionTab(debuggerPanel);
    }

    public override bool _HasCapture(string capture) {
        return capture == MESSAGE_PREFIX;
    }

    public override bool _Capture(string message, Array data, int sessionId) {
        // GD.Print($"message: {message}, sessionId: {sessionId}, data: {data}");
        if (debuggerPanel == null) {
            return false;
        }

        if (message == MESSAGE_REGISTER_TREE) {
            var behaviorTree = data[0].AsGodotDictionary();
            debuggerPanel.TreeRegistered(behaviorTree);
            return true;
        }
        if (message == MESSAGE_UNREGISTER_TREE) {
            var behaviorTree = data[0].AsGodotDictionary();
            if (!debuggerPanel.CanUnregister()) {
                GD.PushWarning("No behavior trees registered");
                return true;
            }
            debuggerPanel.TreeUnregistered(behaviorTree);
            return true;
        }
        if (message == MESSAGE_UPDATE_TREE) {
            var behaviorTree = data[0].AsGodotDictionary();
            var treeName = behaviorTree.GetValueOrDefault("name", "").AsString();
            // The debugger panel tree should never be empty, or missing this field
            // Double check here so we can log a useful message just in case 
            if (treeName != string.Empty) {
                // Only update currently selected tree. Message is valid regardless. But only parse relevant messages.
                if (treeName == debuggerPanel.GetTreeName(debuggerPanel.behavior)) {
                    debuggerPanel.UpdateTree(behaviorTree);
                }
                return true;
            } else {
                GD.Print($"Error parsing update message: {behaviorTree}");
            }
        }

        return false;
    }
}
#endif
