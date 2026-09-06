using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTCompositeSequence.svg")]
[GlobalClass]
public partial class PrioritySequenceBehaviorNode : CompositeBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.PrioritySequence(Name);
    }
}
