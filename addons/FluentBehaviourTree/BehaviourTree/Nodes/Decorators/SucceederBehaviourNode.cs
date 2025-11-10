using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Decorators;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTDecoratorSucceed.svg")]
[GlobalClass]
public partial class SucceederBehaviourNode : DecoratorBehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.AlwaysSucceed(Name);
    }
}
