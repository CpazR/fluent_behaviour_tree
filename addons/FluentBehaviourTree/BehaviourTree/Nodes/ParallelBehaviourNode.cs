using BehaviourTree.Composites;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTSimpleParallel.svg")]
[GlobalClass]
public partial class ParallelBehaviourNode : BehaviourNode {

    [Export]
    public SimpleParallelPolicy parallelPolicy;

    public override void
        BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.SimpleParallel(Name, parallelPolicy);
    }
}