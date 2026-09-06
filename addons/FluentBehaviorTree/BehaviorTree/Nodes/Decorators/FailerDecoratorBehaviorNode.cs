using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Always fail the child node
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTDecoratorFail.svg")]
[Tool]
[GlobalClass]
public partial class FailerDecoratorBehaviorNode : DecoratorBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.AlwaysFail(Name);
    }
}
