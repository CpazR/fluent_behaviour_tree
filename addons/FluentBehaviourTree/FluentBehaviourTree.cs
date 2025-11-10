#if TOOLS
using fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Debugging;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree;

[Tool]
public partial class FluentBehaviourTree : EditorPlugin {

    private FluentBehaviourTreeDebugger attachedDebugger;

    private const string RegistrarAutoloadPath =
        "res://addons/FluentBehaviourTree/BehaviourTree/Debugging/BehaviourTreeDebugRegistrar.tscn";

    public override void _EnterTree() {
        attachedDebugger = new FluentBehaviourTreeDebugger();
        AddAutoloadSingleton("BehaviourTreeDebugRegistrar", RegistrarAutoloadPath);
        AddDebuggerPlugin(attachedDebugger);
    }

    public override void _ExitTree() {
        RemoveDebuggerPlugin(attachedDebugger);
    }
}
#endif
