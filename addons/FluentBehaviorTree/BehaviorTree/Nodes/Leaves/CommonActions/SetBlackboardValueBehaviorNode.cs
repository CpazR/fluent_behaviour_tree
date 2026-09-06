using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves.CommonActions;

[GlobalClass]
public partial class SetBlackboardValueBehaviorNode : ActionBehaviorNode {

    [Export]
    public string blackboardPropertyName;

    [Export]
    public Variant newValue;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Do(Name, context => {
            context.Blackboard[blackboardPropertyName] = newValue;
            return BehaviourStatus.Succeeded;
        });
    }
}
