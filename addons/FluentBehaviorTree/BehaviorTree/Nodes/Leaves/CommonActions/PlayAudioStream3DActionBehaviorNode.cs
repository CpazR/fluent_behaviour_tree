using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves.CommonActions;

/**
 * Simply play a provided audio stream and immediately succeed
 */
[GlobalClass]
public partial class PlayAudioStream3DActionBehaviorNode : ActionBehaviorNode {

    // TODO: Are there any other parameters or configuration that should be exposed here?
    [Export]
    public required AudioStreamPlayer3D audioStreamPlayer;

    [Export]
    public required bool succeedOnAudioFinish;

    private bool isFinished;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        if (succeedOnAudioFinish) {
            audioStreamPlayer.Finished += () => isFinished = true;
        }

        builder.Do(Name, context => {
            audioStreamPlayer.Play();

            if (!succeedOnAudioFinish) {
                return BehaviourStatus.Succeeded;
            }

            return isFinished ? BehaviourStatus.Succeeded : BehaviourStatus.Running;
        });
    }
}
