using bbgodotprototype.System;
using BehaviourTree.FluentBuilder;
using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviorTree.Nodes.CommonConditions;

[GlobalClass]
public partial class DistanceTo3DConditionBehaviorNode : ConditionBehaviorNode {

    private readonly static ILog LOGGER = new Log(nameof(DistanceTo3DConditionBehaviorNode));

    [Export]
    public required string targetNodePath;

    [Export]
    public int distance;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {

        builder.Condition(Name, context => {

            // Need a valid target node
            // TODO: How would we parameterize this???
            var targetNode = WorldManager.Player;
            // owner.GetTree().Root.GetNode<Node3D>(targetNodePath);
            if (targetNode == null || !IsInstanceValid(targetNode) || targetNode.IsQueuedForDeletion()) {
                GD.PrintErr($"{Name}: targetNode is not valid");
                if (targetNodePath.StartsWith('%')) {
                    LOGGER.Print(
                        $"Given node path: {targetNodePath} starts with '%'. Does your node exist or have unique path?");
                }
                return false;
            }

            var distanceTo = context.owner.GlobalPosition.DistanceTo(targetNode.GlobalPosition);
            return distanceTo < distance;
        });
    }
}