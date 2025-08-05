using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

/**
 * A behaviour node that checks for a given condition and will return an appropriate <see cref="BehaviourStatus"/> to reflect a true/false
 */
[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTLeafCondition.svg")]
[GlobalClass]
public abstract partial class ConditionBehaviourNode : BehaviourNode {

    /**
     * Build an action using the fluent builder
     * <code>builder.Condition(Name, data => { return true; });</code>
     * <see cref="BehaviourNode.BuildNode"/>
     */
    public abstract override void BuildNode(FluentBuilder<GodotBehaviourContext> builder);
}
