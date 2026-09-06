using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * After node is composite or leaf is successful, wait N milliseconds before trying again
 */
[Tool]
[GlobalClass]
public partial class CooldownDecoratorBehaviorNode : DecoratorBehaviorNode {

    [Export]
    public int cooldownTimeInMilliseconds;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Cooldown(Name, cooldownTimeInMilliseconds);
    }
}