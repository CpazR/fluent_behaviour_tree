using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTCompositeSequence.svg")]
[GlobalClass]
public partial class PrioritySequenceBehaviourNode : CompositeBehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.PrioritySequence(Name);
    }
}
