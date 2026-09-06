using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTCompositeRandomSelector.svg")]
[GlobalClass]
public partial class RandomSelectorBehaviorNode : CompositeBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.RandomSelector(Name, new RandomProvider());
    }
}
