using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonActions;

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
