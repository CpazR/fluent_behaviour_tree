using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTLeafCall.svg")]
[GlobalClass]
public partial class ActionBehaviorNode : BehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        // builder.Do(Name, data => actionableNode.Call(actionName, data.deltaTime, blackBoard).As<BehaviourTreeStatus>());
    }

}