using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

/**
 * A behavior node that checks for a given condition and will return an appropriate <see cref="BehaviourTreeStatus"/> to reflect a true/false
 */
[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTLeafCondition.svg")]
[GlobalClass]
public partial class ConditionBehaviorNode : BehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        // builder.Condition(Name, data => actionableNode.Call(actionName, data.deltaTime).AsBool());
    }
}