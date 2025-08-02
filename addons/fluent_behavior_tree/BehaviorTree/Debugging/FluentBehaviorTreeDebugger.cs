using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FDluentBehaviorTree;

[Tool]
public partial class FluentBehaviorTreeDebugger : EditorDebuggerPlugin {

    private static string MESSAGE_PREFIX = "FluentBehaviorTree";

    public static string MESSAGE_REGISTER_TREE = "FluentBehaviorTree:RegisterTree";

    public static string MESSAGE_UNREGISTER_TREE = "FluentBehaviorTree:UnregisterTree";

    public static string MESSAGE_UPDATE_TREE = "FluentBehaviorTree:UpdateTree";

    private readonly static ILog LOGGER = new Log(nameof(FluentBehaviorTreeDebugger));

    private BehaviorTreeDebuggerPanel debuggerPanel = new BehaviorTreeDebuggerPanel();

    private EditorDebuggerSession session;

    public override void _SetupSession(int sessionId) {
        session = GetSession(sessionId);
        session.Started += () => debuggerPanel.Start();
        session.Stopped += () => debuggerPanel.Stop();

        LOGGER.Print("Adding debugger session tab");

        debuggerPanel.Name = "FBT Live View";
        debuggerPanel.session = session;
        session.AddSessionTab(debuggerPanel);
    }

    public override bool _HasCapture(string capture) {
        return capture == MESSAGE_PREFIX;
    }

    public override bool _Capture(string message, Array data, int sessionId) {
        // LOGGER.Print($"message: {message}, sessionId: {sessionId}, data: {data}");
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
            debuggerPanel.TreeUnregistered(behaviorTree);
            return true;
        }
        // TODO: Track such that only currently view able tree is updated
        if (message == MESSAGE_UPDATE_TREE) {
            var behaviorTree = data[0].AsGodotDictionary();
            debuggerPanel.UpdateTree(behaviorTree);
        }

        return false;
    }
}