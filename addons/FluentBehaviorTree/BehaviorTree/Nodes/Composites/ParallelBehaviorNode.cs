using BehaviourTree.Composites;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Composites;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTSimpleParallel.svg")]
[GlobalClass]
public partial class ParallelBehaviorNode : CompositeBehaviorNode {

    [Export]
    public SimpleParallelPolicy parallelPolicy;

    public override void
        BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.SimpleParallel(Name, parallelPolicy);
    }
}
