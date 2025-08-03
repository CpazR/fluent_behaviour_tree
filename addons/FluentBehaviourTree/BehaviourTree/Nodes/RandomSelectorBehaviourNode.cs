using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTCompositeRandomSelector.svg")]
[GlobalClass]
public partial class RandomSelectorBehaviourNode : BehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.RandomSelector(Name, new RandomProvider());
    }
}