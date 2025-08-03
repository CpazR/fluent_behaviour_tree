# Fluent Behaviour Tree

A godot addon for C# behaviour trees. Makes use
of [Eraclys' Behaviour Tree Nuget package](https://github.com/Eraclys/BehaviourTree) under the hood to create node
wrappers for the Fluent builder.

<img width="595" height="400" alt="image" src="https://github.com/user-attachments/assets/48f7354f-051d-4059-bfe6-20c730f07533" />

- Utilize common behaviour nodes for things like animations and timers
- Extend these nodes for custom functionality
- Uses a custom `GodotBehaviourContext`

Also has a very crude debugging menu.

<img width="2025" height="1160" alt="image" src="https://github.com/user-attachments/assets/6e0d7c47-5e89-4e1b-9c41-23beb74635a6" />

<img width="1985" height="1088" alt="image" src="https://github.com/user-attachments/assets/ffa4d3f9-b916-4cf5-8ff8-06cb7aff5928" />


This is all very proof of concept. Any contributions are welcome.

## Known issues

- Temporarily using icons from [BehaviourToolKit](https://github.com/ThePat02/BehaviourToolkit) for the sake of visual
  clarity. Need to replace these at some point.
- Debugger is very WIP. Needs a lot of visual cleanup.
