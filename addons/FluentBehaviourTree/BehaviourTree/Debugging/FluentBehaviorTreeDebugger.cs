using Godot;
using Godot.Collections;
namespace Cpaz.fluentBehaviourTree;

[Tool]
public partial class FluentBehaviourTreeDebugger : EditorDebuggerPlugin {

    private static string MESSAGE_PREFIX = "FluentBehaviourTree";

    public static string MESSAGE_REGISTER_TREE = "FluentBehaviourTree:RegisterTree";

    public static string MESSAGE_UNREGISTER_TREE = "FluentBehaviourTree:UnregisterTree";

    public static string MESSAGE_UPDATE_TREE = "FluentBehaviourTree:UpdateTree";


    private BehaviourTreeDebuggerPanel debuggerPanel = new BehaviourTreeDebuggerPanel();

    private EditorDebuggerSession session;

    public override void _SetupSession(int sessionId) {
        session = GetSession(sessionId);
        session.Started += () => debuggerPanel.Start();
        session.Stopped += () => debuggerPanel.Stop();

        GD.Print("Adding debugger session tab");

        debuggerPanel.Name = "Behaviour Tree Live View";
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
            var behaviourTree = data[0].AsGodotDictionary();
            debuggerPanel.TreeRegistered(behaviourTree);
            return true;
        }
        if (message == MESSAGE_UNREGISTER_TREE) {
            var behaviourTree = data[0].AsGodotDictionary();
            debuggerPanel.TreeUnregistered(behaviourTree);
            return true;
        }
        if (message == MESSAGE_UPDATE_TREE) {
            var behaviourTree = data[0].AsGodotDictionary();
            if (behaviourTree["name"].AsString() == debuggerPanel.GetTreeName()) {
                debuggerPanel.UpdateTree(behaviourTree);
            }
        }

        return false;
    }
}
