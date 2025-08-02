using BehaviourTree;
using Godot;
using Godot.Collections;
using System;
using System.Linq;
namespace Cpaz.FDluentBehaviorTree;

[Tool]
public partial class BehaviorTreeViewContainer : VBoxContainer {

    public Dictionary behaviourNode;

    private readonly int depth;

    private readonly Label nodeLabel = new Label();

    private Array<BehaviorTreeViewContainer> childContainer = [];

    public BehaviorTreeViewContainer(Dictionary behaviourNode) {
        this.behaviourNode = behaviourNode;
        this.depth = behaviourNode["depth"].AsInt32();

        nodeLabel.Text = $"{GetIndentation()}{GetLabelName()}";
        AddChild(nodeLabel);

        var childNodes = behaviourNode["childNodes"].AsGodotArray<Dictionary>();

        if (childNodes is { Count: > 0 }) {
            foreach (var childNode in childNodes) {
                var childLabel = new BehaviorTreeViewContainer(childNode);
                childContainer.Add(childLabel);
                AddChild(childLabel);
            }
        }
    }

    public void UpdateData(Dictionary behaviourNode) {
        this.behaviourNode = behaviourNode;

        nodeLabel.Text = $"{GetIndentation()}{GetLabelName()}";
        var childNodes = behaviourNode["childNodes"].AsGodotArray<Dictionary>();
        for (var i = 0; i < childContainer.Count; i++) {
            childContainer[i].UpdateData(childNodes[i]);
        }
    }

    public override void _Process(double delta) {
        base._Process(delta);
        var statusInt = behaviourNode["status"].AsInt32();
        var color = GetColorFromStatus(statusInt);
        AddThemeColorOverride("font_color", color);
    }

    private string GetIndentation() {
        return string.Join(string.Empty, Enumerable.Repeat("    ", depth));
    }

    private string GetLabelName() {
        if (!string.IsNullOrWhiteSpace(behaviourNode["name"].AsString())) {
            return behaviourNode["name"].AsString();
        }

        var type = behaviourNode.GetType();

        // TODO: check for generic

        return type.Name;
    }

    private static Color GetColorFromStatus(int status) {
        return status switch {
            (int)BehaviourStatus.Ready => Colors.DarkGray,
            (int)BehaviourStatus.Running => Colors.Yellow,
            (int)BehaviourStatus.Succeeded => Colors.Green,
            (int)BehaviourStatus.Failed => Colors.Red,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}