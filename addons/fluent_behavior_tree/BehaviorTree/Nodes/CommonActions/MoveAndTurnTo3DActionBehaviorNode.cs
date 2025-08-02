using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonActions;

[GlobalClass]
public partial class MoveAndTurnTo3DActionBehaviourNode : ActionBehaviourNode {


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


    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Do(Name, context => {

            // Need owner to be a character3d node
            if (context.owner is not CharacterBody3D characterBodyOwner) {
                GD.PrintErr($"{Name}: owner is not a CharacterBody3D");
                return BehaviourStatus.Failed;
            }

            // Need a valid target node
            
            // TODO: Hardcoded example from personal project:
            // var targetNode = WorldManager.Player;
            
            // TODO: How would we parameterize this???
            // Doesn't seem to work correctly... 
            // owner.GetTree().Root.GetNode<Node3D>(targetNodePath);
            Node3D targetNode = null;
            if (targetNode == null || !IsInstanceValid(targetNode) || targetNode.IsQueuedForDeletion()) {
                GD.PrintErr($"{Name}: targetNode is not valid");
                if (targetNodePath.StartsWith('%')) {
                    GD.Print(
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