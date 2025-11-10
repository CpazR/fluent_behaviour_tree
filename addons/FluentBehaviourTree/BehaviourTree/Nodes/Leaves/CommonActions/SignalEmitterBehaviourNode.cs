using BehaviourTree;
using BehaviourTree.FluentBuilder;
using fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Resources;
using Godot;

namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Leaves.CommonActions;

/**
 * From a given node, emit a signal
 */
[Tool]
[GlobalClass]
public partial class SignalEmitterBehaviourNode : ActionBehaviourNode {

    private SignalExportResource _signalExportResource;

    [Export]
    public required SignalExportResource signalExportResource {
        get => _signalExportResource;
        set {
            _signalExportResource = value;
            _signalExportResource._toolOwningNode = this;
        }
    }

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        builder.Do(Name, context => {
            _signalExportResource.Emit(this);
            return BehaviourStatus.Succeeded;
        });
    }
}
