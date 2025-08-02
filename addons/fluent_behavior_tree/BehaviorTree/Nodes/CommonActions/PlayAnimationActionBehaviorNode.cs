using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviorTree.Nodes.CommonActions;

[GlobalClass]
public partial class PlayAnimationActionBehaviorNode : ActionBehaviorNode {

    private readonly static ILog LOGGER = new Log(nameof(PlayAnimationActionBehaviorNode));

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
                GD.Print($"Animation {animationName} completed for {Name}");
                animationCompleted = true;
            }
        };
    }

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {

        builder.Do(Name, data => {
            if ((!animationPlayer.IsPlaying() || animationPlayer.CurrentAnimation != animationName) &&
                !animationCompleted) {
                if (animationPlayer.HasAnimation(animationName)) {
                    LOGGER.Print($"Animation {animationName} started for {Name}");
                    animationPlayer.Play(animationName);
                } else {
                    LOGGER.Err($"Attempting to play animation that does not exist: {animationName}");
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