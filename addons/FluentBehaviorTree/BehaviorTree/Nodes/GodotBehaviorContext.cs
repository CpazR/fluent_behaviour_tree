using BehaviourTree;
using Godot;
using Godot.Collections;

namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Nodes {
    /**
 * The behavior tree context
 * <param name="DeltaTime">Delta time in the contet of a godot tick</param>
 * <param name="Owner">The node that "owns" or is controlled by the behavior tree. As provided by <see cref="BehaviorTree"/></param>
 * <param name="Blackboard">The blackboard dictionary that builder nodes can read/write from, to maintain data across nodes. Useful for custom logic. As provided by <see cref="BehaviorTree"/></param>
 */
    public record GodotBehaviorContext(
        double DeltaTime,
        Node3D Owner,
        Dictionary<string, Variant> Blackboard) : IClock {

        public long GetTimeStampInMilliseconds() {
            // TODO: This cast may be problematic... Need to verify.
            return (long)Time.GetTicksMsec();
        }
    }
}
