using Godot;

namespace fluent_behaviour_tree.testing;

public partial class MockScene : Node3D {
    
    [Signal]
    public delegate void TestSignalEventHandler();

    [Signal]
    public delegate void TestSignal2EventHandler();
}