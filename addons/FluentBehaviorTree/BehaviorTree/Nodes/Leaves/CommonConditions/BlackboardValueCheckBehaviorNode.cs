using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves.CommonConditions;

[GlobalClass]
public partial class BlackboardValueCheckBehaviorNode : ConditionBehaviorNode {

    [Export]
    public string blackboardPropertyName;

    [Export]
    public Variant expectedValue;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Condition(Name, context => {
            if (!context.Blackboard.TryGetValue(blackboardPropertyName, out var value)) {
                GD.PrintErr($"Missing blackboard property {blackboardPropertyName}");
                return false;
            }
            return expectedValue.Obj != null && expectedValue.Obj.Equals(value.Obj);
        });
    }
}
