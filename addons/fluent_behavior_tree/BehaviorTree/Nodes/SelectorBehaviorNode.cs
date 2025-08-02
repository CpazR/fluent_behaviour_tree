using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTCompositeSelector.svg")]
[GlobalClass]
public partial class SelectorBehaviorNode : BehaviorNode {

    public override void
        BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Sequence(Name);
    }
}