using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Leaves.CommonActions;

[GlobalClass]
public partial class PlayAnimationActionBehaviourNode : ActionBehaviourNode {

    [Export]
    public required AnimationPlayer animationPlayer;

    [Export]
    public required StringName animationName;

    /**
     * To count as a "succeed" does the node need to wait for the animation to complete?
     */
    [Export]
    public bool waitForCompletion;

    private bool animationCompleted;

    public override void _Ready() {
        base._Ready();

        animationPlayer.AnimationFinished += name => {
            if (waitForCompletion && name == animationName) {
                if (debugLogging) {
                    GD.Print($"Animation {animationName} completed for {Name}");
                }
                animationCompleted = true;
            }
        };
    }

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {

        builder.Do(Name, data => {
            if ((!animationPlayer.IsPlaying() || animationPlayer.CurrentAnimation != animationName) &&
                !animationCompleted) {
                if (animationPlayer.HasAnimation(animationName)) {
                    if (debugLogging) {
                        GD.Print($"Animation {animationName} started for {Name}");
                    }
                    animationPlayer.Play(animationName);
                } else {
                    GD.PrintErr($"Attempting to play animation that does not exist: {animationName}");
                    return BehaviourStatus.Failed;
                }
            }

            if (waitForCompletion && !animationCompleted) {
                return BehaviourStatus.Running;
            }

            // Reset flag when node is replayed
            animationCompleted = false;

            return BehaviourStatus.Succeeded;
        });
    }
}
