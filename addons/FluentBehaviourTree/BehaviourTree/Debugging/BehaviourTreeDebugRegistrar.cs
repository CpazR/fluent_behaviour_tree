using Godot;
using Godot.Collections;
using System.Linq;
namespace Cpaz.FluentBehaviourTree;

public partial class BehaviourTreeDebugRegistrar : Node {

    /**
     * A map containing behaviour trees mapped by their owner's name
     */
    private Dictionary<string, FluentBehaviourTree.BehaviourTree> registeredTrees = [];

    /**
     * Instance of singleton
     */
    private static BehaviourTreeDebugRegistrar _instance;

    public static BehaviourTreeDebugRegistrar Instance {
        get {
            if (_instance != null) return _instance;
            _instance =
                ((SceneTree)Engine.GetMainLoop()).Root.GetNodeOrNull<BehaviourTreeDebugRegistrar>(
                    nameof(BehaviourTreeDebugRegistrar));

            if (_instance != null) return _instance;

            _instance = new BehaviourTreeDebugRegistrar();
            _instance.Name = nameof(BehaviourTreeDebugRegistrar);
            return _instance;
        }
    }

    public static void RegisterTree(Node owner, FluentBehaviourTree.BehaviourTree tree) {
        // Use both the node name and it's session instanceID
        Instance.registeredTrees[$"{owner.Name}-{owner.GetInstanceId()}"] = tree;
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData()]);
            EngineDebugger.SendMessage(FluentBehaviourTreeDebugger.MESSAGE_REGISTER_TREE, messageParams);
        }
    }

    public static void UpdateTree(Node owner, FluentBehaviourTree.BehaviourTree tree) {
        // Use both the node name and it's session instanceID
        Instance.registeredTrees[$"{owner.Name}-{owner.GetInstanceId()}"] = tree;
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData()]);
            EngineDebugger.SendMessage(FluentBehaviourTreeDebugger.MESSAGE_UPDATE_TREE, messageParams);
        }
    }

    public static void UnregisterTree(Node owner, FluentBehaviourTree.BehaviourTree tree) {
        // Use both the node name and it's session instanceID
        Instance.registeredTrees.Remove($"{owner.Name}-{owner.GetInstanceId()}");
        if (CanSendMessage()) {
            var messageParams = new Array([tree.GetTreeDebuggerData()]);
            EngineDebugger.SendMessage(FluentBehaviourTreeDebugger.MESSAGE_UNREGISTER_TREE, messageParams);
        }
    }

    public static Array<FluentBehaviourTree.BehaviourTree> AvailableTreesAsList() {
        var list = new Array<FluentBehaviourTree.BehaviourTree>();
        foreach (var behaviourTree in Instance.registeredTrees.Select(pair => pair.Value)) {
            list.Add(behaviourTree);
        }
        return list;
    }

    public static bool CanSendMessage() {
        // Only send message if using editor debugger and is supported
        return EngineDebugger.IsActive() && !Engine.IsEditorHint() && OS.HasFeature("editor");
    }
}
