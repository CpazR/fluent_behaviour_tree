using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Repeat the child node N times, as given by node input
 */
[Tool]
[GlobalClass]
public partial class RepeatDecoratorBehaviorNode : DecoratorBehaviorNode {

    [Export]
    public required int repeatTimes = 5;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Repeat(Name, repeatTimes);
    }
}
