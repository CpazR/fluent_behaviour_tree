﻿using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonConditions;

[GlobalClass]
public partial class BlackboardValueCheckBehaviourNode : ConditionBehaviourNode {

    [Export]
    public string blackboardPropertyName;

    [Export]
    public Variant expectedValue;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Condition(Name, context => {
            if (!context.blackboard.TryGetValue(blackboardPropertyName, out var value)) {
                GD.PrintErr($"Missing blackboard property {blackboardPropertyName}");
                return false;
            }
            return expectedValue.Equals(value);
        });
    }
}
