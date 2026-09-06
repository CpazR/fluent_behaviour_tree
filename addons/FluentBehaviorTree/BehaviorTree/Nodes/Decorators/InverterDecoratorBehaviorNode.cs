using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Decorators;

/**
 * Invert the output status of the child node
 * IE:
 *  SUCCESS -> FAILED
 *  RUNNING -> RUNNING (no change if in progress)
 *  FAILED -> SUCCESS
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTDecoratorNot.svg")]
[Tool]
[GlobalClass]
public partial class InverterDecoratorBehaviorNode : DecoratorBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Invert(Name);
    }
}