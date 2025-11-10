using BehaviourTree;
using Godot;
using Godot.Collections;
using System;
using System.Linq;
namespace Cpaz.FluentBehaviourTree;

[Tool]
public partial class BehaviourTreeViewContainer : VBoxContainer {

    public Dictionary behaviourNode;

    private readonly int depth;

    private readonly RichTextLabel nodeLabel = new RichTextLabel();

    private Array<BehaviourTreeViewContainer> childContainer = [];

    /**
     * A param-less constructor is apparently required by godot on subsequent rebuilds but not on launch?
     * Behavior here is not clear (ironically).
     */
    public BehaviourTreeViewContainer() {
        nodeLabel.BbcodeEnabled = true;
        nodeLabel.ScrollActive = false;
        nodeLabel.FitContent = true;
        nodeLabel.AutowrapMode = TextServer.AutowrapMode.Off;
        AddChild(nodeLabel);
    }

    public BehaviourTreeViewContainer(Dictionary behaviourNode) : this() {
        this.behaviourNode = behaviourNode;
        this.depth = behaviourNode["depth"].AsInt32();

        nodeLabel.Text = $"{GetIndentation()}{GetLabelName()}";

        var childNodes = behaviourNode["childNodes"].AsGodotArray<Dictionary>();

        if (childNodes is { Count: > 0 }) {
            foreach (var childNode in childNodes) {
                var childLabel = new BehaviourTreeViewContainer(childNode);
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
        nodeLabel.Text = GetColorFromStatus(statusInt);

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

    private string GetColorFromStatus(int status) {
        var textBuilder = $"{GetIndentation()}[color=";
        textBuilder += status switch {
            (int)BehaviourStatus.Ready => "darkgray]",
            (int)BehaviourStatus.Running => "yellow]",
            (int)BehaviourStatus.Succeeded => "green]",
            (int)BehaviourStatus.Failed => "red]",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
        textBuilder += $"{GetLabelName()}[/color]";
        return textBuilder;
    }
}
