using BehaviourTree.FluentBuilder;
using Godot;

namespace Cpaz.FluentBehaviorTree.Nodes;

/** 
 * The base node for <see cref="BehaviorTree"/>. All relevant nodes will extend off of this.
 */
[GlobalClass]
public abstract partial class BehaviorNode : Node {

    /**
     * TODO: Move owner and blackboard to <see cref="GodotBehaviorContext"/>
     */
    public abstract void BuildNode(FluentBuilder<GodotBehaviorContext> builder);
}