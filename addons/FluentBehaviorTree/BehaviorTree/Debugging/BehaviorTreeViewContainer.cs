using BehaviourTree;
using Godot;
using Godot.Collections;
using System;
using System.Linq;
namespace fluent_behavior_tree.addons.FluentBehaviorTree.BehaviorTree.Debugging;

[Tool]
public partial class BehaviorTreeViewContainer : VBoxContainer {

    public Dictionary behaviorNode;

    private readonly int depth;

    private readonly RichTextLabel nodeLabel = new RichTextLabel();

    private Array<BehaviorTreeViewContainer> childContainer = [];

    /**
     * A param-less constructor is apparently required by godot on subsequent rebuilds but not on launch?
     * Behavior here is not clear (ironically).
     */
    public BehaviorTreeViewContainer() {
        nodeLabel.BbcodeEnabled = true;
        nodeLabel.ScrollActive = false;
        nodeLabel.FitContent = true;
        nodeLabel.AutowrapMode = TextServer.AutowrapMode.Off;
        AddChild(nodeLabel);
    }

    public BehaviorTreeViewContainer(Dictionary behaviorNode) : this() {
        this.behaviorNode = behaviorNode;
        this.depth = behaviorNode["depth"].AsInt32();

        nodeLabel.Text = $"{GetIndentation()}{GetLabelName()}";

        var childNodes = behaviorNode["childNodes"].AsGodotArray<Dictionary>();

        if (childNodes is { Count: > 0 }) {
            foreach (var childNode in childNodes) {
                var childLabel = new BehaviorTreeViewContainer(childNode);
                childContainer.Add(childLabel);
                AddChild(childLabel);
            }
        }
    }

    public void UpdateData(Dictionary behaviorNode) {
        this.behaviorNode = behaviorNode;

        nodeLabel.Text = $"{GetIndentation()}{GetLabelName()}";
        var childNodes = behaviorNode["childNodes"].AsGodotArray<Dictionary>();
        for (var i = 0; i < childContainer.Count; i++) {
            childContainer[i].UpdateData(childNodes[i]);
        }
    }

    public override void _Process(double delta) {
        base._Process(delta);
        var statusInt = behaviorNode["status"].AsInt32();
        nodeLabel.Text = GetColorFromStatus(statusInt);

    }

    private string GetIndentation() {
        return string.Join(string.Empty, Enumerable.Repeat("    ", depth));
    }

    private string GetLabelName() {
        if (!string.IsNullOrWhiteSpace(behaviorNode["name"].AsString())) {
            return behaviorNode["name"].AsString();
        }

        var type = behaviorNode.GetType();

        // TODO: check for generic

        return type.Name;
    }

    private string GetColorFromStatus(int status) {
        var behaviorColor = status switch {
            (int)BehaviourStatus.Ready => "darkgray",
            (int)BehaviourStatus.Running => "yellow",
            (int)BehaviourStatus.Succeeded => "green",
            (int)BehaviourStatus.Failed => "red",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
        var textBuilder = $"{GetIndentation()}[color={behaviorColor}]{GetLabelName()}[/color]";
        return textBuilder;
    }
}
