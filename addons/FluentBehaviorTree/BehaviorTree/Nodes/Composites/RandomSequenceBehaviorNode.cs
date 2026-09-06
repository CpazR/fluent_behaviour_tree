using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTCompositeRandomSequence.svg")]
[GlobalClass]
public partial class RandomSequenceBehaviorNode : CompositeBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.RandomSequence(Name, new RandomProvider());
    }
}
