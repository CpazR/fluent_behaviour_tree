using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonActions;

[GlobalClass]
public partial class TurnTo3DNodeActionBehaviourNode : ActionBehaviourNode {

    [Export]
    public required Node3D rotatingNode;

    [Export]
    public required string targetNodeGroup = "player";

    [Export]
    public Vector3 forwardDirection = Vector3.Forward;

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

            if (GetCachedTargetNodeFromGroup(targetNodeGroup, context.blackboard) is not Node3D targetNode) {
                GD.PrintErr($"{Name}: targetNode is not valid");
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
