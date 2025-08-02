using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/fluent_behaviour_tree/BehaviourTree/Nodes/icons/BTLeafCall.svg")]
[GlobalClass]
public partial class ActionBehaviourNode : BehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        // builder.Do(Name, data => actionableNode.Call(actionName, data.deltaTime, blackBoard).As<BehaviourTreeStatus>());
    }

}