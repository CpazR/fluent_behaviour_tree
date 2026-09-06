using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTDecoratorSucceed.svg")]
[Tool]
[GlobalClass]
public partial class SucceederDecoratorBehaviorNode : DecoratorBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.AlwaysSucceed(Name);
    }
}
