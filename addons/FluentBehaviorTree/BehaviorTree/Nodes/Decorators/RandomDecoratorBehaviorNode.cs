using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Has a n% chance to execute the child nodes
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTCompositeRandomSelector.svg")]
[Tool]
[GlobalClass]
public partial class RandomDecoratorBehaviorNode : DecoratorBehaviorNode {

    [Export]
    public float randomChance;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Random(Name, randomChance);
    }
}
