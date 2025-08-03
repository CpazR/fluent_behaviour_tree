#if TOOLS
using Cpaz.fluentBehaviourTree;
using Godot;
namespace cpaz.fluentBehaviourTree;

[Tool]
public partial class FluentBehaviourTree : EditorPlugin {

    private FluentBehaviourTreeDebugger attatchedDebugger;

    private const string RegistrarAutoloadPath =
        "res://addons/FluentBehaviourTree/BehaviourTree/Debugging/BehaviourTreeDebugRegistrar.tscn";

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
