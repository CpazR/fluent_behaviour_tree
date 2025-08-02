using bbgodotprototype.System;
using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviorTree.Nodes.CommonActions;

[GlobalClass]
public partial class MoveAndTurnTo3DActionBehaviorNode : ActionBehaviorNode {

    private readonly static ILog LOGGER = new Log(nameof(MoveAndTurnTo3DActionBehaviorNode));

    /**
     * What node will handle rotation? Leave null if not doing rotation
     */
    [Export]
    public Node3D? rotatingNode;

    [Export]
    public float speed;

    [Export]
    public Vector3 forwardDirection = Vector3.Forward;

    [Export]
    public required string targetNodePath;

    [Export]
    public int maxDistance;

    [Export]
    public int minDistance;


    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Do(Name, context => {

            // Need owner to be a character3d node
            if (context.owner is not CharacterBody3D characterBodyOwner) {
                LOGGER.Err($"{Name}: owner is not a CharacterBody3D");
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
            var distanceTo = characterBodyOwner.GlobalPosition.DistanceTo(targetNode.GlobalPosition);

            // TODO: Optional timeout?
            if (distanceTo < minDistance) {
                characterBodyOwner.Velocity = Vector3.Zero;
                return BehaviourStatus.Succeeded;
            }

            if (distanceTo > maxDistance) {
                characterBodyOwner.Velocity = Vector3.Zero;
                return BehaviourStatus.Failed;
            }

            var directionTo = characterBodyOwner.GlobalPosition.DirectionTo(targetNode.GlobalPosition);
            var directionRotation = Mathf.Atan2(directionTo.X, directionTo.Z);

            characterBodyOwner.Velocity = (forwardDirection * speed).Rotated(Vector3.Up, directionRotation);
            characterBodyOwner.MoveAndSlide();

            if (rotatingNode != null) {
                rotatingNode.Rotation = new Vector3(0.0f, directionRotation, 0.0f);
            }

            return BehaviourStatus.Running;
        });
    }
}