using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviorTree.Nodes;

/**
 * The behavior tree context
 * <param name="deltaTime">Delta time in the contet of a godot tick</param>
 * <param name="owner">The node that "owns" or is controlled by the behavior tree. As provided by <see cref="BehaviorTree"/></param>
 * <param name="blackboard">The blackboard dictionary that builder nodes can read/write from, to maintain data across nodes. Useful for custom logic. As provided by <see cref="BehaviorTree"/></param>
 */
public record GodotBehaviorContext(
    double deltaTime,
    Node3D owner,
    Dictionary<string, Variant> blackboard);