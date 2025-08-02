using BehaviourTree.Composites;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviorTree.Nodes;

[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTSimpleParallel.svg")]
[GlobalClass]
public partial class ParallelBehaviorNode : BehaviorNode {

    [Export]
    public SimpleParallelPolicy parallelPolicy;

    public override void
        BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.SimpleParallel(Name, parallelPolicy);
    }
}