using BehaviourTree;
using BehaviourTree.FluentBuilder;
using fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Resources;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves.CommonActions;

/**
 * From a given node, emit a signal
 */
[Tool]
[GlobalClass]
public partial class SignalEmitterBehaviorNode : ActionBehaviorNode {

    private SignalExportResource _signalExportResource;

    [Export]
    public required SignalExportResource signalExportResource {
        get => _signalExportResource;
        set {
            _signalExportResource = value;
            _signalExportResource._toolOwningNode = this;
        }
    }

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        builder.Do(Name, context => {
            _signalExportResource.Emit(this);
            return BehaviourStatus.Succeeded;
        });
    }
}
