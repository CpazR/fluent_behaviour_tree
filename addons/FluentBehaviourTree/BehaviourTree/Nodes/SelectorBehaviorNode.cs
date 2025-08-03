using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTCompositeSelector.svg")]
[GlobalClass]
public partial class SelectorBehaviourNode : BehaviourNode {

    public override void
        BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Sequence(Name);
    }
}