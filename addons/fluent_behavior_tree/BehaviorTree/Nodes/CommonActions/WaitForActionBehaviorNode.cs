using BehaviourTree;
using BehaviourTree.FluentBuilder;
using Godot;
namespace Cpaz.FluentBehaviourTree.Nodes.CommonActions;

[GlobalClass]
public partial class WaitForActionBehaviourNode : ActionBehaviourNode {


    [Export]
    public double waitForSeconds = 3f;

    private Timer timer = new Timer();

    private bool timerComplete;

    public override void BuildNode(FluentBuilder<GodotBehaviourContext> builder) {
        timer.OneShot = true;
        AddChild(timer);

        timer.Timeout += () => {
            GD.Print($"Timer completed for {Name}");
            timerComplete = true;
        };

        builder.Do(Name, context => {
            if (!timerComplete) {
                if (timer.IsStopped()) {
                    GD.Print($"Timer set for {Name}");
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