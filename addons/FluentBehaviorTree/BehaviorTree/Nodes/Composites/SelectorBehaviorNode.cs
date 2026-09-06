using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTCompositeSelector.svg")]
[GlobalClass]
partial class SelectorBehaviorNode : CompositeBehaviorNode {

    public override void
        BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Selector(Name);
    }
}
