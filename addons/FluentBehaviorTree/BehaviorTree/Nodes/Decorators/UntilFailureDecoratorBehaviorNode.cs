using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Repeat until child nodes succeeds
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTDecoratorFail.svg")]
[Tool]
[GlobalClass]
public partial class UntilFailureDecoratorBehaviorNode : DecoratorBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.UntilSuccess(Name);
    }
}
