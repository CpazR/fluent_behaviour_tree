using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

/**
 * Has a n% chance to execute the child nodes
 */
[Icon("res://addons/fluent_behaviour_tree/BehaviourTree/Nodes/icons/BTCompositeRandomSelector.svg")]
[GlobalClass]
public partial class RandomBehaviourNode : BehaviourNode {

    [Export]
    public float randomChance;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Random(Name, randomChance);
    }
}