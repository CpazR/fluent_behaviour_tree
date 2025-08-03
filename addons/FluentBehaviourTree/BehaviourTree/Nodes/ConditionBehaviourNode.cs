using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

/**
 * A behaviour node that checks for a given condition and will return an appropriate <see cref="BehaviourTreeStatus"/> to reflect a true/false
 */
[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTLeafCondition.svg")]
[GlobalClass]
public partial class ConditionBehaviourNode : BehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        // builder.Condition(Name, data => actionableNode.Call(actionName, data.deltaTime).AsBool());
    }
}