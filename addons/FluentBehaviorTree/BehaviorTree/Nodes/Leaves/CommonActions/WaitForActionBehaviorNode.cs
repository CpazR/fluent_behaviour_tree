using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes.Leaves.CommonActions;

[Icon("res://addons/FluentBehaviorTree/BehaviorTree/Nodes/icons/BTLeafWait.svg")]
[GlobalClass]
public partial class WaitForActionBehaviorNode : ActionBehaviorNode {

    [Export]
    public double waitForSeconds = 3f;

    private Timer timer = new Timer();

    private bool timerComplete;

    public override void BuildNode(FluentBuilder<GodotBehaviorContext> builder) {
        timer.OneShot = true;
        AddChild(timer);

        timer.Timeout += () => {
            if (debugLogging) {
                GD.Print($"Timer completed for {Name}");
            }
            timerComplete = true;
        };

        builder.Do(Name, context => {
            if (!timerComplete) {
                if (timer.IsStopped()) {
                    if (debugLogging) {
                        GD.Print($"Timer set for {Name}");
                    }
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
