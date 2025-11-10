using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Leaves.CommonConditions;

[GlobalClass]
public partial class DistanceTo3DConditionBehaviourNode : ConditionBehaviourNode {

    [Export]
    public required string targetNodeGroup = "player";

    [Export]
    public int distance;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {

        builder.Condition(Name, context => {

            // Need a valid target node
            if (!GetTree().HasGroup(targetNodeGroup)) {
                GD.PrintErr($"Node group {targetNodeGroup} does not exit in tree");
            }

            if (GetTree().GetFirstNodeInGroup(targetNodeGroup) is not Node3D targetNode ||
                !IsInstanceValid(targetNode) || targetNode.IsQueuedForDeletion()) {
                GD.PrintErr($"{Name}: targetNode is not valid");
                return false;
            }

            var distanceTo = context.owner.GlobalPosition.DistanceTo(targetNode.GlobalPosition);
            return distanceTo < distance;
        });
    }
}
