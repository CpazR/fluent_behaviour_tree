using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves.CommonActions;

[GlobalClass]
public partial class QueueFreeSelfBehaviorNode : ActionBehaviorNode {

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Do(Name, context => {
            context.Owner.CallDeferred(Node.MethodName.QueueFree);
            // Just keep running node indefinitely until queue free is called.
            return BehaviourStatus.Running;
        });
    }
}
