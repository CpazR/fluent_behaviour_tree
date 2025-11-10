using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Leaves.CommonActions;

[GlobalClass]
public partial class SetBlackboardValueBehaviourNode : ActionBehaviourNode {

    [Export]
    public string blackboardPropertyName;

    [Export]
    public Variant newValue;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Do(Name, context => {
            context.blackboard[blackboardPropertyName] = newValue;
            return BehaviourStatus.Succeeded;
        });
    }
}
