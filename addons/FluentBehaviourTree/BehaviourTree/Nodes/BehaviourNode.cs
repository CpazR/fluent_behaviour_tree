using BehaviourTree.FluentBuilder;
using Godot;

namespace Cpaz.FluentBehaviourTree.Nodes;

/**
 * The base node for <see cref="BehaviourTree"/>. All relevant nodes will extend off of this.
 */
[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTBehaviour.svg")]
[GlobalClass]
public abstract partial class BehaviourNode : Node {

    /**
     * The basic building block of the <see cref="FluentBuilder"/> wrapper. Is called recursively by the root <see cref="FluentBehaviourTree.BehaviourTree"/> to build the <see cref="FluentBehaviourTree"/>
     */
    public abstract void BuildNode(FluentBuilder<GodotBehaviourContext> builder);
}
