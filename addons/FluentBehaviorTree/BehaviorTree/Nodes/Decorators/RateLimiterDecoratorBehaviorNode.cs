using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Cache the output of a completed call for N milliseconds. Where the cache is dirties and the composite or leaf will be called again
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTDecoratorLimiter.svg")]
[Tool]
[GlobalClass]
public partial class RateLimiterDecoratorBehaviorNode : DecoratorBehaviorNode {

    [Export]
    public int cacheResultsEveryMilliseconds;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.LimitCallRate(Name, cacheResultsEveryMilliseconds);
    }
}
