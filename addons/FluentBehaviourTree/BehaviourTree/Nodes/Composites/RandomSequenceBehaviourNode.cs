using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTCompositeRandomSequence.svg")]
[GlobalClass]
public partial class RandomSequenceBehaviourNode : CompositeBehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.RandomSequence(Name, new RandomProvider());
    }
}
