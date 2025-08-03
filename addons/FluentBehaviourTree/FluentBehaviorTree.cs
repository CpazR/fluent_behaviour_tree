#if TOOLS
using Cpaz.FDluentBehaviourTree;
using Godot;
namespace cpaz.fluentBehaviourTree;

[Tool]
public partial class FluentBehaviourTree : EditorPlugin {

    private FluentBehaviourTreeDebugger attatchedDebugger;

    private const string RegistrarAutoloadPath = "res://addons/fluent_behaviour_tree/BehaviourTree/Debugging/BehaviourTreeDebugRegistrar.tscn";

    public override void _EnterTree() {
        attatchedDebugger = new FluentBehaviourTreeDebugger();
        AddAutoloadSingleton("BehaviourTreeDebugRegistrar", RegistrarAutoloadPath);
        AddDebuggerPlugin(attatchedDebugger);
    }

    public override void _ExitTree() {
        RemoveDebuggerPlugin(attatchedDebugger);
    }

}
#endif