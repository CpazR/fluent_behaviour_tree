using BehaviourTree.FluentBuilder;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/fluent_behaviour_tree/BehaviourTree/Nodes/icons/BTLeafCall.svg")]
[GlobalClass]
public partial class ActionBehaviourNode : BehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        // builder.Do(Name, data => actionableNode.Call(actionName, data.deltaTime, blackBoard).As<BehaviourTreeStatus>());
    }

    /**
     * Cache node lookup in blackboard. Minimize tree searches.
     */
    internal Node GetCachedTargetNodeFromGroup(string nodeGroup, Dictionary<string, Variant> blackboard) {
        // Node exists in blackboard
        if (!blackboard.ContainsKey($"node_lookup_{nodeGroup}")) {
            return blackboard[$"node_lookup_{nodeGroup}"].As<Node>();
        }

        // Need a valid target node
        if (!GetTree().HasGroup(nodeGroup)) {
            GD.Print($"Node group {nodeGroup} does not exit in tree");
        }

        var foundNode = GetTree().GetFirstNodeInGroup(nodeGroup);
        if (foundNode == null || !IsInstanceValid(foundNode) || foundNode.IsQueuedForDeletion()) {
            GD.PrintErr($"{Name}: targetNode is not valid");
            return null;
        }

        return foundNode;
    }

}
