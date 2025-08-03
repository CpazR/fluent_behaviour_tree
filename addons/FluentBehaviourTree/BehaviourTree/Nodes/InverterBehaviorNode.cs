using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/fluent_behaviour_tree/BehaviourTree/Nodes/icons/BTDecoratorNot.svg")]
[GlobalClass]
public partial class InverterBehaviourNode : BehaviourNode {

    public override void
        BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Invert(Name);
    }
}