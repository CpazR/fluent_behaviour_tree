using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviorTree.Nodes.CommonActions;

[GlobalClass]
public partial class WaitForActionBehaviorNode : ActionBehaviorNode {

    private readonly static ILog LOGGER = new Log(nameof(WaitForActionBehaviorNode));

    [Export]
    public double waitForSeconds = 3f;

    private Timer timer = new Timer();

    private bool timerComplete;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        timer.OneShot = true;
        AddChild(timer);

        timer.Timeout += () => {
            LOGGER.Print($"Timer completed for {Name}");
            timerComplete = true;
        };

        builder.Do(Name, context => {
            if (!timerComplete) {
                if (timer.IsStopped()) {
                    LOGGER.Print($"Timer set for {Name}");
                    timer.SetWaitTime(waitForSeconds);
                    timer.Start();
                }

                return BehaviourStatus.Running;
            }

            timerComplete = false;
            return BehaviourStatus.Succeeded;
        });
    }
}