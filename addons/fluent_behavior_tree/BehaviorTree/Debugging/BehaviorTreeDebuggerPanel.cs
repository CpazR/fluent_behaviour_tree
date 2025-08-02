using Chickensoft.Log;
using Godot;
using Godot.Collections;
namespace Cpaz.FDluentBehaviorTree;

[Tool]
public partial class BehaviorTreeDebuggerPanel : PanelContainer {

    private static ILog LOGGER = new Log(nameof(FluentBehaviorTreeDebugger));

    internal EditorDebuggerSession session;

    private Dictionary behaviour;

    private int _index;

    private Array<Dictionary> treeArray = [];

    private VSplitContainer splitContainer = new VSplitContainer();

    private ScrollContainer treeContainer = new ScrollContainer();

    private VBoxContainer treeContainerVBox = new VBoxContainer();

    private BehaviorTreeViewContainer rootControl;

    private OptionButton treeList = new OptionButton();

    private Label noTreeText = new Label();


    public override void _Ready() {
        noTreeText.Text = "Run game to populate debug window";
        splitContainer.SetAnchorsPreset(LayoutPreset.FullRect);
        AddChild(splitContainer);
        splitContainer.AddChild(treeList);
        splitContainer.AddChild(treeContainer);
        treeContainerVBox.Alignment = BoxContainer.AlignmentMode.Begin;
        treeContainer.AddChild(treeContainerVBox);
        // treeContainer.AddChild(noTreeText);
    }

    private void SelectTree(Dictionary tree) {
        behaviour = tree;
        rootControl = new BehaviorTreeViewContainer(behaviour);
        treeContainerVBox.AddChild(rootControl);
    }

    public void Start() {
        // Setup signals for new behavior trees post game startup
        treeList.ItemSelected += index => { SelectTree(treeArray[(int)index]); };
    }

    internal void TreeRegistered(Dictionary tree) {
        InsertTree(tree);
    }

    internal void TreeUnregistered(Dictionary tree) {
        // The list and the option control should have synced indices
        var index = treeArray.IndexOf(tree);
        treeList.RemoveItem(index);
        treeArray.RemoveAt(index);
    }

    private void InsertTree(Dictionary tree) {
        if (behaviour == null) {
            SelectTree(tree);
        }
        treeList.AddItem(tree["name"].AsString());
        treeArray.Add(tree);
    }

    public void Stop() {
        treeContainerVBox.RemoveChild(rootControl);
        // treeContainer.AddChild(noTreeText);
        treeList.Clear();
        treeArray.Clear();
    }

    public void UpdateTree(Dictionary behaviorTree) {
        if (rootControl != null) {
            rootControl.UpdateData(behaviorTree);
        }
    }
}