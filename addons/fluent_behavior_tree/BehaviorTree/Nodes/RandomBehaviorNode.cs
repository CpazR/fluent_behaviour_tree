using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

/**
 * Has a n% chance to execute the child nodes
 */
[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTCompositeRandomSelector.svg")]
[GlobalClass]
public partial class RandomBehaviorNode : BehaviorNode {

    [Export]
    public float randomChance;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Random(Name, randomChance);
    }
}