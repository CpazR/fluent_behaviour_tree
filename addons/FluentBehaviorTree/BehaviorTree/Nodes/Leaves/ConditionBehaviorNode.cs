using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves;

/**
 * A generic leaf behavior node that checks for a given condition and will return an appropriate <see cref="BehaviourStatus"/> to reflect a true/false
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTLeafCondition.svg")]
[GlobalClass]
public abstract partial class ConditionBehaviorNode : LeafBehaviorNode {

    /**
     * Build an action using the fluent builder
     * <code>builder.Condition(Name, data => { return true; });</code>
     * <see cref="BehaviorNode.BuildNode"/>
     */
    public abstract override void BuildNode(FluentBuilder<GodotBehaviorContext> builder);
}
