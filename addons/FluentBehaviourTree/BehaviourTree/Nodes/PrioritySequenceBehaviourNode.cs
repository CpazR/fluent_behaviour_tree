using BehaviourTree.FluentBuilder;
using Cpaz.FluentBehaviourTree.Nodes;
using Godot;
namespace Cpaz.FluentBehaviourTree;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTCompositeSequence.svg")]
[GlobalClass]
public partial class PrioritySequenceBehaviourNode : BehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.PrioritySequence(Name);
    }
}
