﻿using BehaviourTree;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;
using BehaviourTree.FluentBuilder;
using Cpaz.FluentBehaviourTree.Nodes;
using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;
using Node = Godot.Node;

namespace Cpaz.FluentBehaviourTree;

/**
 * The entry point and root node for a behaviour tree.
 *
 * Leverages <see cref="FluentBuilder<GodotBehaviourContext>"/> under the hood the handle all the actual behaviour tree logic.
 */
[Icon("res://addons/FluentBehaviourTree/BehaviourTree/Nodes/icons/BTRoot.svg")]
[GlobalClass]
public partial class BehaviourTree : Node {

    [Export]
    public required bool enabled = true;

    [Export]
    public required Node3D treeOwner;

    /**
     * Properties bound to the behaviour tree
     */
    [Export]
    public Godot.Collections.Dictionary<string, Variant> blackboard =
        new Godot.Collections.Dictionary<string, Variant>();

    public IBehaviour<GodotBehaviourContext> behaviourTree { get; private set; }

    public override void _Ready() {
        base._Ready();
        var builder = new FluentBuilder<GodotBehaviourContext>();
        var behaviourNodes = GetChildren()
            .Where(node => node is BehaviourNode)
            .Cast<BehaviourNode>()
            .ToList();

        // Don't "end" branch since it's the root
        AddBranch(builder, behaviourNodes, false);
        behaviourTree = builder.Build();
        // Once built, register with debugger
        BehaviourTreeDebugRegistrar.RegisterTree(treeOwner, this);
    }

    public override void _Process(double delta) {
        base._Process(delta);

        if (!enabled) {
            return;
        }

        behaviourTree.Tick(new GodotBehaviourContext((float)delta, treeOwner, blackboard));
        BehaviourTreeDebugRegistrar.UpdateTree(treeOwner, this);
    }

    public override void _Notification(int what) {
        if (what == NotificationPredelete) {
            BehaviourTreeDebugRegistrar.UnregisterTree(Owner, this);
        }
    }

    /**
     * From a "new root" BT node with children (IE sequence or composite nodes)
     */
    private void AddBranch(
        FluentBuilder<GodotBehaviourContext> builder,
        List<BehaviourNode> childNodes,
        bool canEndBranch = true) {

        foreach (var behaviourNode in childNodes) {
            behaviourNode.BuildNode(builder);
            var behaviourNodes = behaviourNode.GetChildren()
                .Where(node => node is BehaviourNode)
                .Cast<BehaviourNode>()
                .ToList();

            if (behaviourNodes.Count != 0) {
                AddBranch(builder, behaviourNodes);
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
        return GetNodeDebuggerData(0, behaviourTree);
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
     *      ],
     *      "blackboard": { // Only the root will have the blackboard
     *          ...
     *      }
     *  }
     * </code>
     */
    private Dictionary GetNodeDebuggerData(int depth, IBehaviour<GodotBehaviourContext> behaviourNode) {
        Dictionary nodeDebugMapping = new Dictionary();
        nodeDebugMapping["depth"] = depth;
        nodeDebugMapping["name"] = depth == 0 ? $"{Owner.Name}-{Owner.GetInstanceId()}" : behaviourNode.Name;
        nodeDebugMapping["status"] = (int)behaviourNode.Status;

        // Only the root will have the blackboard 
        if (depth == 0) {
            nodeDebugMapping["blackboard"] = blackboard;
        }

        var childDepth = depth + 1;

        Array<Dictionary> children = new Array<Dictionary>();
        if (behaviourNode is CompositeBehaviour<GodotBehaviourContext> compositeBehaviour) {
            foreach (var child in compositeBehaviour.Children) {
                children.Add(GetNodeDebuggerData(childDepth, child));
            }
        }

        if (behaviourNode is DecoratorBehaviour<GodotBehaviourContext> decoratorBehaviour) {
            children.Add(GetNodeDebuggerData(childDepth, decoratorBehaviour.Child));
        }
        nodeDebugMapping["childNodes"] = children;

        return nodeDebugMapping;
    }

}
