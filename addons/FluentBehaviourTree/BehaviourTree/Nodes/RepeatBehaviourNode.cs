using BehaviourTree.FluentBuilder;
using Cpaz.FluentBehaviourTree.Nodes;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes;

public partial class RepeatBehaviourNode : BehaviourNode {

    [Export]
    public required int repeatTimes = 5;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Repeat(Name, repeatTimes);
    }
}
