using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTCompositeRandomSequence.svg")]
[GlobalClass]
public partial class RandomSequenceBehaviorNode : BehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.RandomSelector(Name);
    }
}