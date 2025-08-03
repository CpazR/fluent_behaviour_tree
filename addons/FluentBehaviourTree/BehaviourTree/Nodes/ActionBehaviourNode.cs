using BehaviourTree.FluentBuilder;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviourTree.Nodes;

[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTLeaf.svg")]
[GlobalClass]
public partial class ActionBehaviourNode : BehaviourNode {

    private readonly static string LOOKUP_CACHE_PREFIX = "node_lookup_cache_";

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        // Example of basic syntax using the fluent builder
        // builder.Do(Name, data => { return BehaviourStatus.Succeeded; });
    }

    /**
     * Cache node lookup in blackboard. Minimize tree searches.
     */
    internal Node GetCachedTargetNodeFromGroup(string nodeGroup, Dictionary<string, Variant> blackboard) {
        // Node exists in blackboard
        if (blackboard.ContainsKey($"{LOOKUP_CACHE_PREFIX}{nodeGroup}")) {
            return blackboard[$"{LOOKUP_CACHE_PREFIX}{nodeGroup}"].As<Node>();
        }

        // Need a valid target node group
        if (!GetTree().HasGroup(nodeGroup)) {
            GD.Print($"Node group {nodeGroup} does not exit in tree. Check node group exists in node tab.");
        }

        // Attempt to look up node group in tree and cache first result
        // TODO: Need to find something to better configure this look up. Seems potentially error prone to simply use the first result.
        // At the same time, there should only be one player, if using intended use case...
        var foundNode = GetTree().GetFirstNodeInGroup(nodeGroup);
        if (foundNode == null || !IsInstanceValid(foundNode) || foundNode.IsQueuedForDeletion()) {
            GD.PrintErr($"{Name}: targetNode is not valid. Check that desired node is assigned to group in node tab.");
            return null;
        }

        blackboard[$"{LOOKUP_CACHE_PREFIX}{nodeGroup}"] = foundNode;

        return foundNode;
    }

}
