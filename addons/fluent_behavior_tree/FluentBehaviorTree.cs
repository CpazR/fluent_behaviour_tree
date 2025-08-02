#if TOOLS
using Cpaz.FDluentBehaviorTree;
using Godot;
namespace cpaz.fluentBehaviorTree;

[Tool]
public partial class FluentBehaviorTree : EditorPlugin {

    private FluentBehaviorTreeDebugger attatchedDebugger;

    private const string RegistrarAutoloadPath = "res://addons/fluent_behavior_tree/BehaviorTree/Debugging/BehaviorTreeDebugRegistrar.tscn";

    public override void _EnterTree() {
        attatchedDebugger = new FluentBehaviorTreeDebugger();
        AddAutoloadSingleton("BehaviorTreeDebugRegistrar", RegistrarAutoloadPath);
        AddDebuggerPlugin(attatchedDebugger);
    }

    public override void _ExitTree() {
        RemoveDebuggerPlugin(attatchedDebugger);
    }

}
#endif