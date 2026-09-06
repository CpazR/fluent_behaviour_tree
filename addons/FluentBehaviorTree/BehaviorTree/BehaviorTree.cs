using BehaviourTree;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;
using BehaviourTree.FluentBuilder;
using fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Debugging;
using fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes;
using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;
using Node = Godot.Node;

namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree;

/**
 * The entry point and root node for a behavior tree.
 *
 * Leverages <see cref="FluentBuilder"/> under the hood the handle all the actual behavior tree logic.
 */
[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTRoot.svg")]
[Tool]
[GlobalClass]
public partial class BehaviorTree : Node {

    [Export]
    public required bool enabled = true;

    [Export]
    public required Node3D treeOwner;

    /**
     * A hard coded blackboard value that determines if a behavior tree can be interrupted via <see cref="Interrupt"/>
     */
    public static readonly string BB_PROP_CAN_INTERUPT = "CAN_INTERRUPT";

    /**
     * Properties bound to the behavior tree. Includes defaults.
     */
    [Export]
    public Godot.Collections.Dictionary<string, Variant> blackboard = new() {
        // Default interrupts to true. Allows leaves to conditionally enable/disable interrupts 
        [BB_PROP_CAN_INTERUPT] = true
    };

    public IBehaviour<GodotBehaviorContext> behaviorTree { get; private set; }

    private string debuggerId;

    public override void _Ready() {
        base._Ready();

        if (Engine.IsEditorHint()) {
            return;
        }

        var builder = new FluentBuilder<GodotBehaviorContext>();
        var behaviorNodes = GetChildren()
            .OfType<BehaviorNode>()
            .ToList();

        // Don't "end" branch since it's the root
        AddBranch(builder, behaviorNodes, false);
        behaviorTree = builder.Build();
        debuggerId = $"{Owner.Name}-{Owner.GetInstanceId()}";
        // Once built, register with debugger
        #if TOOLS
        BehaviorTreeDebugRegistrar.RegisterTree(treeOwner, this);
        #endif
    }

    /**
     * Handle basic validation for exported fields
     */
    public override string[] _GetConfigurationWarnings() {
        var warnings = new List<string>();

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        // Disable this check since this is technically "required" in code, but no real way to enforce this otherwise via editor.
        if (treeOwner == null) {
            warnings.Add($"\"Tree Owner\" should be assigned. Behavior tree may not function correctly otherwise.");
        }
        return warnings.ToArray();
    }

    public override void _Process(double delta) {
        base._Process(delta);

        if (Engine.IsEditorHint() || !enabled) {
            return;
        }

        behaviorTree.Tick(new GodotBehaviorContext((float)delta, treeOwner, blackboard));
        #if TOOLS
        BehaviorTreeDebugRegistrar.UpdateTree(treeOwner, this);
        #endif
    }

    public override void _Notification(int what) {
        if (what == NotificationPredelete) {
            #if TOOLS
            BehaviorTreeDebugRegistrar.UnregisterTree(treeOwner, this);
            #endif
        }
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
     * Restart the behavior tree from the top. Useful when, for example Player input demands the BT be recalculated from the start for hit stun/death branches.
     */
    public void Interrupt() {
        if (blackboard[BB_PROP_CAN_INTERUPT].AsBool()) {
            behaviorTree.Reset();
        }
    }

    /**
     * Build a variant-compatible dictionary for the debugger from the root node. Required since Godot handles
     * editor-application interactions through the networking interface via messaging, which only supports variants.
     * <param name="debuggerMessage">Includes debugger message for potential troubleshooting of debug tab</param>
     * <seealso cref="GetNodeDebuggerData"/>
     */
    public Dictionary GetTreeDebuggerData(string debuggerMessage) {
        return GetNodeDebuggerData(debuggerMessage, 0, behaviorTree);
    }

    /**
     * A payload for the debugger using variant-compatible dictionaries
     * Formatted as such
     * <code>
     *  {
     *      "depth": 0,
     *      "name": "root",
     *      "status": 1, // The status from the `BehaviorStatus` enum
     *      "childNodes" : [
     *          {
     *              "depth": 1,
     *              "name": "Sequence",
     *              "status": 0, // The status from the `BehaviorStatus` enum
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
     * <param name="debuggerMessage">Includes debugger message for potential troubleshooting of debug tab</param>
     * <param name="depth"></param>
     * <param name="behaviorNode"></param>
     */
    private Dictionary GetNodeDebuggerData(string debuggerMessage,
        int depth,
        IBehaviour<GodotBehaviorContext> behaviorNode) {
        var nodeDebugMapping = new Dictionary();
        nodeDebugMapping["depth"] = depth;
        nodeDebugMapping["name"] = depth == 0 ? debuggerId : behaviorNode.Name;
        nodeDebugMapping["status"] = (int)behaviorNode.Status;


        // Only the root will have the blackboard 
        if (depth == 0) {
            nodeDebugMapping.Add("blackboard", blackboard);
        }

        var childDepth = depth + 1;

        var children = new Array<Dictionary>();

        switch (behaviorNode) {
            case CompositeBehaviour<GodotBehaviorContext> compositeBehavior:
            {
                foreach (var child in compositeBehavior.Children) {
                    children.Add(GetNodeDebuggerData(debuggerMessage, childDepth, child));
                }
                break;
            }
            case DecoratorBehaviour<GodotBehaviorContext> decoratorBehavior:
            {
                children.Add(GetNodeDebuggerData(debuggerMessage, childDepth, decoratorBehavior.Child));
                break;
            }
        }

        nodeDebugMapping["childNodes"] = children;

        return nodeDebugMapping;
    }

}
