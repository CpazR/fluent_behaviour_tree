using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTCompositeSequence.svg")]
[GlobalClass]
public partial class SequenceBehaviourNode : BehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Sequence(Name);
    }
}