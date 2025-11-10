using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;
namespace fluent_behaviour_tree.addons.FluentBehaviourTree.BehaviourTree.Nodes.Resources;

[Tool]
[GlobalClass]
public partial class SignalExportResource : Resource {

    private NodePath _parentNodeToSignal;

    [Export]
    public NodePath parentNodeToSignal {
        get => _parentNodeToSignal;
        set {
            _parentNodeToSignal = value;
            if (_toolOwningNode != null && parentNodeToSignal != null && parentNodeToSignal != "") {
                var signalingNode = _toolOwningNode.GetNode(parentNodeToSignal);
                var signals = signalingNode?.GetSignalList()
                    .Select(dictionary => dictionary["name"].AsStringName());
                signalList.Clear();
                signalList.AddRange(signals?.ToList() ?? []);
            }
            NotifyPropertyListChanged();
        }
    }

    private StringName signalName;

    private readonly List<StringName> signalList = [];

    public Node _toolOwningNode;

    public void Emit(Node owningResourceNode) {
        if (Engine.IsEditorHint()) {
            return;
        }

        owningResourceNode.GetNode(parentNodeToSignal).EmitSignal(signalName);
    }

    public NodePath GetParentNodePath() {
        return _parentNodeToSignal;
    }

    public StringName GetSignal() {
        return signalName;
    }

    public override Array<Dictionary> _GetPropertyList() {
        Array<Dictionary> propertyList = new Array<Dictionary>();

        var propertySignalList = new Dictionary();
        propertySignalList["name"] = "Signals";
        propertySignalList["type"] = (int)Variant.Type.StringName;
        propertySignalList["hint"] = (int)PropertyHint.Enum;
        propertySignalList["hint_string"] = string.Join(",", signalList);
        propertySignalList["usage"] = (long)PropertyUsageFlags.Storage | (long)PropertyUsageFlags.Editor;

        propertyList.Add(propertySignalList);

        return propertyList;
    }

    public override Variant _Get(StringName property) {

        if (property == "Signals") {
            if (_toolOwningNode != null && parentNodeToSignal != null && parentNodeToSignal != "") {
                if (signalName == null) {
                    signalName = signalList[0];
                }
                return Variant.From(signalName);
            }
        }

        return default;
    }

    public override bool _Set(StringName property, Variant value) {

        if (property == "Signals") {
            signalName = value.ToString();
            return true;
        }

        return false;
    }
}
