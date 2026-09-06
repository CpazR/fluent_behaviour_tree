#if TOOLS
using Godot;
using Godot.Collections;
using System.Linq;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Debugging;

public partial class BehaviorTreeDebugRegistrar : Node {

    /**
     * A map containing behavior trees mapped by their owner's name
     */
    private Dictionary<string, BehaviorTree> registeredTrees = [];

    /**
     * Instance of singleton
     */
    private static BehaviorTreeDebugRegistrar _instance;

    public static BehaviorTreeDebugRegistrar Instance
    {
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
        Instance.registeredTrees[GetReadableTreeKey(owner)] = tree;
        if (CanSendMessage()) {
            var messageParams =
                new Array([tree.GetTreeDebuggerData(FluentBehaviorTreeDebugger.MESSAGE_REGISTER_TREE)]);
            EngineDebugger.SendMessage(FluentBehaviorTreeDebugger.MESSAGE_REGISTER_TREE, messageParams);
        }
    }

    public static void UpdateTree(Node owner, BehaviorTree tree) {
        Instance.registeredTrees[GetReadableTreeKey(owner)] = tree;
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData(FluentBehaviorTreeDebugger.MESSAGE_UPDATE_TREE)]);
            EngineDebugger.SendMessage(FluentBehaviorTreeDebugger.MESSAGE_UPDATE_TREE, messageParams);
        }
    }

    public static void UnregisterTree(Node owner, BehaviorTree tree) {
        Instance.registeredTrees.Remove(GetReadableTreeKey(owner));
        // Send dictionary of current tree to debugger for removal
        if (CanSendMessage()) {
            var messageParams =
                new Array([tree.GetTreeDebuggerData(FluentBehaviorTreeDebugger.MESSAGE_UNREGISTER_TREE)]);
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

    public static string GetReadableTreeKey(Node node) {
        // Use both the node name and it's session instanceID
        return $"{node.Name}-{node.GetInstanceId()}";
    }

    public static bool CanSendMessage() {
        // Only send message if using editor debugger and is supported
        return EngineDebugger.IsActive() && !Engine.IsEditorHint() && OS.HasFeature("editor");
    }
}
#endif
