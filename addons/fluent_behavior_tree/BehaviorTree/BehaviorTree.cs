using BehaviourTree;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;
using BehaviourTree.FluentBuilder;
using Cpaz.FDluentBehaviorTree;
using Cpaz.FluentBehaviorTree.Nodes;
using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;
using Node = Godot.Node;

namespace Cpaz.FluentBehaviorTree;

/**
 * The entry point and root node for a behavior tree.
 *
 * Leverages <see cref="FluentBuilder<GodotBehaviorContext>"/> under the hood the handle all the actual behavior tree logic.
 */
[Icon("res://addons/fluent_behavior_tree/BehaviorTree/Nodes/icons/BTBehaviour.svg")]
[GlobalClass]
public partial class BehaviorTree : Node {

    [Export]
    public bool enabled = true;

    [Export]
    public Node3D treeOwner;

    /**
     * Properties bound to the behavior tree
     */
    [Export]
    public Godot.Collections.Dictionary<string, Variant> blackboard =
        new Godot.Collections.Dictionary<string, Variant>();

    public IBehaviour<GodotBehaviorContext> behaviorTree { get; private set; }

    public override void _Ready() {
        base._Ready();
        var builder = new FluentBuilder<GodotBehaviorContext>();
        var behaviorNodes = GetChildren()
            .Where(node => node is BehaviorNode)
            .Cast<BehaviorNode>()
            .ToList();

        // Don't "end" branch since it's the root
        AddBranch(builder, behaviorNodes, false);
        behaviorTree = builder.Build();
        // Once built, register with debugger
        BehaviorTreeDebugRegistrar.RegisterTree(treeOwner, this);
    }

    public override void _Process(double delta) {
        base._Process(delta);

        if (!enabled) {
            return;
        }

        behaviorTree.Tick(new GodotBehaviorContext((float)delta, treeOwner, blackboard));
        BehaviorTreeDebugRegistrar.UpdateTree(treeOwner, this);
    }

    /**
     * From a "new root" BT node with children (IE sequence or composite nodes)
     */
    private void AddBranch(
        FluentBuilder<GodotBehaviorContext> builder,
        List<BehaviorNode> childNodes,
        bool canEndBranch = true) {

        foreach (var behaviorNode in childNodes) {
            behaviorNode.BuildNode(builder);
            var behaviorNodes = behaviorNode.GetChildren()
                .Where(node => node is BehaviorNode)
                .Cast<BehaviorNode>()
                .ToList();

            if (behaviorNodes.Count != 0) {
                AddBranch(builder, behaviorNodes);
            }
        }

        if (canEndBranch) {
            builder.End();
        }
    }

    /**
     * Build a variant-compatible dictionary for the debugger from the root node. Required since Godot handles
     * editor-application interactions through the networking interface via messaging, which only supports variants.
     * <seealso cref="GetNodeDebuggerData"/>
     */
    public Dictionary GetTreeDebuggerData() {
        return GetNodeDebuggerData(0, behaviorTree);
    }

    /**
     * A payload for the debugger using variant-compatible dictionaries
     * Formatted as such
     * <code>
     *  {
     *      "depth": 0,
     *      "name": "root",
     *      "status": 1, // The status from the `BehaviourStatus` enum
     *      "childNodes" : [
     *          {
     *              "depth": 1,
     *              "name": "Sequence",
     *              "status": 0, // The status from the `BehaviourStatus` enum
     *              "childNodes": [...]
     *          },
     *          {
     *              ...
     *          },
     *          ...
     *      ]
     *  }
     * </code>
     */
    private Dictionary GetNodeDebuggerData(int depth, IBehaviour<GodotBehaviorContext> behaviourNode) {
        Dictionary nodeDebugMapping = new Dictionary();
        nodeDebugMapping["depth"] = depth;
        nodeDebugMapping["name"] = depth == 0 ? $"{Owner.Name}-{Owner.GetInstanceId()}" : behaviourNode.Name;
        nodeDebugMapping["status"] = (int)behaviourNode.Status;

        var childDepth = depth + 1;

        Array<Dictionary> children = new Array<Dictionary>();
        if (behaviourNode is CompositeBehaviour<GodotBehaviorContext> compositeBehaviour) {
            foreach (var child in compositeBehaviour.Children) {
                children.Add(GetNodeDebuggerData(childDepth, child));
            }
        }

        if (behaviourNode is DecoratorBehaviour<GodotBehaviorContext> decoratorBehaviour) {
            children.Add(GetNodeDebuggerData(childDepth, decoratorBehaviour.Child));
        }
        nodeDebugMapping["childNodes"] = children;

        return nodeDebugMapping;
    }

}