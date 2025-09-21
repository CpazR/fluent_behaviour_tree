using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonActions;

[GlobalClass]
public partial class QueueFreeSelfBehaviourNode : ActionBehaviourNode {

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Do(Name, context => {
            context.owner.CallDeferred(Node.MethodName.QueueFree);
            // Just keep running node indefinitely until queue free is called.
            return BehaviourStatus.Running;
        });
    }
}
