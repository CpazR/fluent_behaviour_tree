# Fluent Behaviour Tree

A godot addon for C# behaviour trees. Makes use
of [Eraclys' Behaviour Tree Nuget package](https://github.com/Eraclys/BehaviourTree) under the hood to create node
wrappers for the Fluent builder.

- Utilize common behaviour nodes for things like animations and timers
- Extend these nodes for custom functionality
- Uses a custom `GodotBehaviourContext`

Also has a very crude debugging menu.

This is all very proof of concept. Any contributions are welcome.

## Known issues

- The common behaviour nodes are incomplete. Need some way to identity the player character independent of the behaviour
  tree's scene tree.
- Temporarily using icons from [BehaviourToolKit](https://github.com/ThePat02/BehaviourToolkit) for the sake of visual
  clarity. Need to replace these at some point.
- Debugger is very WIP. Needs a lot of visual cleanup.
- Debugger needs to only accept updates from currently viewed Behaviour tree