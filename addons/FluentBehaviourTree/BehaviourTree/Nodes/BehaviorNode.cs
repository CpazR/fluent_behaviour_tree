using BehaviourTree.FluentBuilder;
using Godot;

namespace Cpaz.FluentBehaviourTree.Nodes;

/**
 * The base node for <see cref="BehaviourTree"/>. All relevant nodes will extend off of this.
 */
[GlobalClass]
public abstract partial class BehaviourNode : Node {

    public abstract void BuildNode(FluentBuilder<GodotBehaviourContext> builder);
}
