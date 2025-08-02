using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTCompositeRandomSelector.svg")]
[GlobalClass]
public partial class RandomSelectorBehaviorNode : BehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.RandomSelector(Name, new RandomProvider());
    }
}