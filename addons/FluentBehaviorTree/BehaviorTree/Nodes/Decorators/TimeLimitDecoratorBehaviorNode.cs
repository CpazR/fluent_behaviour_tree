using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Decorator has N time in milliseconds for it's child nodes to complete, otherwise, fail the nodes
 */
[Tool]
[GlobalClass]
public partial class TimeLimitDecoratorBehaviorNode : DecoratorBehaviorNode {

    [Export]
    public int timeToCompleteMilliseconds;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.TimeLimit(Name, timeToCompleteMilliseconds);
    }
}
