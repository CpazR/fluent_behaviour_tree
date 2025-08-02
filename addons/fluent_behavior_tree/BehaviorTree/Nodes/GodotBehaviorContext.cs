using Godot;
using Godot.Collections;
namespace Cpaz.FluentBehaviourTree.Nodes;

/**
 * The behaviour tree context
 * <param name="deltaTime">Delta time in the contet of a godot tick</param>
 * <param name="owner">The node that "owns" or is controlled by the behaviour tree. As provided by <see cref="BehaviourTree"/></param>
 * <param name="blackboard">The blackboard dictionary that builder nodes can read/write from, to maintain data across nodes. Useful for custom logic. As provided by <see cref="BehaviourTree"/></param>
 */
public record GodotBehaviourContext(
    double deltaTime,
    Node3D owner,
    Dictionary<string, Variant> blackboard);