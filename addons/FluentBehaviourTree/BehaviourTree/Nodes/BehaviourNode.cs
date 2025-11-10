using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes;

/**
 * The base node for <see cref="BehaviourTree"/>. All relevant nodes will extend off of this.
 */
[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTBehaviour.svg")]
[GlobalClass]
public abstract partial class BehaviourNode : Node {

    /**
     * Allow all non-error logging messages. Configured per-node.
     * TODO: Consider an optional blackboard flag to enable this globally for while tree?
     */
    [ExportCategory("Debug")]
    [Export]
    public bool debugLogging;

    /**
     * The basic building block of the <see cref="FluentBuilder"/> wrapper. Is called recursively by the root <see cref="BehaviourTree"/> to build the <see cref="FluentBehaviourTree"/>
     */
    public abstract void BuildNode(FluentBuilder<GodotBehaviourContext> builder);
}
