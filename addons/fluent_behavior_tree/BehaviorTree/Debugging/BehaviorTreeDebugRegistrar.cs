using Cpaz.FluentBehaviorTree;
using Godot;
using Godot.Collections;
using System.Linq;
namespace Cpaz.FDluentBehaviorTree;

public partial class BehaviorTreeDebugRegistrar : Node {

    /**
     * A map containing behavior trees mapped by their owner's name
     */
    private Dictionary<string, BehaviorTree> registeredTrees = [];

    /**
     * Instance of singleton
     */
    private static BehaviorTreeDebugRegistrar _instance;

    public static BehaviorTreeDebugRegistrar Instance {
        get {
            if (_instance != null) return _instance;
            _instance =
                ((SceneTree)Engine.GetMainLoop()).Root.GetNodeOrNull<BehaviorTreeDebugRegistrar>(
                    nameof(BehaviorTreeDebugRegistrar));

            if (_instance != null) return _instance;

            _instance = new BehaviorTreeDebugRegistrar();
            _instance.Name = nameof(BehaviorTreeDebugRegistrar);
            return _instance;
        }
    }

    public static void RegisterTree(Node owner, BehaviorTree tree) {
        // Use both the node name and it's session instanceID
        Instance.registeredTrees[$"{owner.Name}-{owner.GetInstanceId()}"] = tree;
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData()]);
            EngineDebugger.SendMessage(FluentBehaviorTreeDebugger.MESSAGE_REGISTER_TREE, messageParams);
        }
    }

    public static void UpdateTree(Node owner, BehaviorTree tree) {
        // Use both the node name and it's session instanceID
        Instance.registeredTrees[$"{owner.Name}-{owner.GetInstanceId()}"] = tree;
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData()]);
            EngineDebugger.SendMessage(FluentBehaviorTreeDebugger.MESSAGE_UPDATE_TREE, messageParams);
        }
    }

    public static void UnregisterTree(Node owner, BehaviorTree tree) {
        // Use both the node name and it's session instanceID
        Instance.registeredTrees.Remove($"{owner.Name}-{owner.GetInstanceId()}");
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData()]);
            EngineDebugger.SendMessage(FluentBehaviorTreeDebugger.MESSAGE_UNREGISTER_TREE, messageParams);
        }
    }

    public static Array<BehaviorTree> AvailableTreesAsList() {
        var list = new Array<BehaviorTree>();
        foreach (var behaviorTree in Instance.registeredTrees.Select(pair => pair.Value)) {
            list.Add(behaviorTree);
        }
        return list;
    }

    public static bool CanSendMessage() {
        return !Engine.IsEditorHint() && OS.HasFeature("editor");
    }
}