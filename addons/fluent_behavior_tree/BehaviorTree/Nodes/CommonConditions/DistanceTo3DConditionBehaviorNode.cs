using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonConditions;

[GlobalClass]
public partial class DistanceTo3DConditionBehaviourNode : ConditionBehaviourNode {

    [Export]
    public required string targetNodePath;

    [Export]
    public int distance;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {

        builder.Condition(Name, context => {

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
                return false;
            }

            var distanceTo = context.owner.GlobalPosition.DistanceTo(targetNode.GlobalPosition);
            return distanceTo < distance;
        });
    }
}
