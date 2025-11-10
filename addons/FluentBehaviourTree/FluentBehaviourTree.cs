#if TOOLS
using Cpaz.FluentBehaviourTree;
using Godot;
namespace cpaz.FluentBehaviourTree;

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
