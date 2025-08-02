using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTDecoratorNot.svg")]
[GlobalClass]
public partial class InverterBehaviorNode : BehaviorNode {

    public override void
        BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Invert(Name);
    }
}