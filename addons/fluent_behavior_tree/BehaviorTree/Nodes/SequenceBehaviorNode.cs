using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTCompositeSequence.svg")]
[GlobalClass]
public partial class SequenceBehaviorNode : BehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Sequence(Name);
    }
}