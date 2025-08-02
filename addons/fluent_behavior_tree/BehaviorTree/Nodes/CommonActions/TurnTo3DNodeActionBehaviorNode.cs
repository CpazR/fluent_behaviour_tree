using bbgodotprototype.System;
using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviorTree.Nodes.CommonActions;

[GlobalClass]
public partial class TurnTo3DNodeActionBehaviorNode : ActionBehaviorNode {

    private readonly static ILog LOGGER = new Log(nameof(TurnTo3DNodeActionBehaviorNode));

    [Export]
    public required Node3D rotatingNode;

    [Export]
    public required string targetNodePath;

    [Export]
    public Vector3 forwardDirection = Vector3.Forward;

    [Export]
    public int maxDistance;

    [Export]
    public int minDistance;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Do(Name, context => {

            // Need owner to be a character3d node
            if (context.owner is not CharacterBody3D characterBodyOwner) {
                GD.PrintErr($"{Name}: owner is not a CharacterBody3D");
                return BehaviourStatus.Failed;
            }

            // Need a valid target node
            // TODO: How would we parameterize this???
            var targetNode = WorldManager.Player;
            // owner.GetTree().Root.GetNode<Node3D>(targetNodePath);
            if (targetNode == null || !IsInstanceValid(targetNode) || targetNode.IsQueuedForDeletion()) {
                LOGGER.Err($"{Name}: targetNode is not valid");
                if (targetNodePath.StartsWith('%')) {
                    LOGGER.Print(
                        $"Given node path: {targetNodePath} starts with '%'. Does your node exist or have unique path?");
                }
                return BehaviourStatus.Failed;
            }

            var directionTo = characterBodyOwner.GlobalPosition.DirectionTo(targetNode.GlobalPosition);
            var directionRotation = Mathf.Atan2(directionTo.X, directionTo.Z);

            // TODO: Slerp this?
            rotatingNode.Rotation = new Vector3(0.0f, directionRotation, 0.0f);
            return BehaviourStatus.Succeeded;
        });
    }
}