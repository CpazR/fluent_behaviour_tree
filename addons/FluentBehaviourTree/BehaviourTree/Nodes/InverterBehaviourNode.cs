using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTDecoratorNot.svg")]
[GlobalClass]
public partial class InverterBehaviourNode : BehaviourNode {

    public override void
        BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Invert(Name);
    }
}