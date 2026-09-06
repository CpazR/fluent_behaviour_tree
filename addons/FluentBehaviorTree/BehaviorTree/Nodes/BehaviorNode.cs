using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes;

/**
 * The base node for <see cref="BehaviorTree"/>. All relevant nodes will extend off of this.
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTBehavior.svg")]
[GlobalClass]
public abstract partial class BehaviorNode : Node {

    /**
     * Allow all non-error logging messages. Configured per-node.
     * TODO: Consider an optional blackboard flag to enable this globally for while tree?
     */
    [ExportCategory("Debug")]
    [Export]
    public bool debugLogging;

    /**
     * The basic building block of the <see cref="FluentBuilder"/> wrapper. Is called recursively by the root <see cref="BehaviorTree"/> to build the <see cref="FluentBehaviorTree"/>
     */
    public abstract void BuildNode(FluentBuilder<GodotBehaviorContext> builder);
}
