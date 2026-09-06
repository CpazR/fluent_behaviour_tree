#if TOOLS
using fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Debugging;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree;

[Tool]
public partial class FluentBehaviorTree : EditorPlugin {

    private FluentBehaviorTreeDebugger attachedDebugger;

    private const string RegistrarAutoloadPath =
        "res://addons/FluentBehaviorTree/BehaviorTree/Debugging/BehaviorTreeDebugRegistrar.tscn";

    public override void _EnterTree() {
        attachedDebugger = new FluentBehaviorTreeDebugger();
        AddAutoloadSingleton("BehaviorTreeDebugRegistrar", RegistrarAutoloadPath);
        AddDebuggerPlugin(attachedDebugger);
    }

    public override void _ExitTree() {
        RemoveDebuggerPlugin(attachedDebugger);
    }
}
#endif
