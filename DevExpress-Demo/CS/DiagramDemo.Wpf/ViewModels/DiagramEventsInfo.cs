using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Diagram.Core;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public class DiagramEventsInfo {
        static readonly EventArgInfo<ItemsActionKind> EAction = new EventArgInfo<ItemsActionKind>(
            "Action",
            EnumArgValues(new[] { ItemsActionKind.Move, ItemsActionKind.MoveCopy, ItemsActionKind.Resize, ItemsActionKind.Rotate, ItemsActionKind.Copy, ItemsActionKind.Delete }, x => false)
        );
        static readonly EventArgInfo<DiagramActionStage> EStage = new EventArgInfo<DiagramActionStage>(
            "Stage",
            EnumArgValues(new[] { DiagramActionStage.Start, DiagramActionStage.Continue, DiagramActionStage.Finished, DiagramActionStage.Canceled }, x => x != DiagramActionStage.Continue)
        );
        static readonly EventArgInfo<ConnectorPointType> EConnectorPointType = new EventArgInfo<ConnectorPointType>(
            "ConnectorPointType",
            EnumArgValues(new[] { ConnectorPointType.Begin, ConnectorPointType.End }, x => x == ConnectorPointType.End)
        );
        static readonly EventArgInfo<DiagramConnector> EConnector = new EventArgInfo<DiagramConnector>(
            "Connector",
            new[] { new EventArgValueInfo<DiagramConnector>(x => "e.Connector.GetDiagram() is null", null, x => x.GetDiagram() == null), new EventArgValueInfo<DiagramConnector>(x => "e.Connector.GetDiagram() is not null", true, x => x.GetDiagram() != null) }
        );
        static readonly EventArgInfo<ItemUsage> EItemUsage = new EventArgInfo<ItemUsage>(
            "ItemUsage",
            EnumArgValues(new[] { ItemUsage.Diagram, ItemUsage.ToolboxPreview }, x => x == ItemUsage.Diagram)
        );

        DiagramControl eventsOwner;
        Action<string, string, object[]> addToLog;

        public DiagramEventsInfo(DiagramControl eventsOwner, Action<string, string, object[]> addToLog) {
            this.eventsOwner = eventsOwner;
            this.addToLog = addToLog;
        }
        public IEnumerable<DiagramEventNode> Initialize() {
            var groups = new ObservableCollection<DiagramEventNode>();
            var behaviorGroup = DiagramEventNode.Create("Behavior", null, DiagramEventNodeKind.Group);
            groups.Add(behaviorGroup);
            behaviorGroup.AddChild(Initialize<DiagramCustomCursorEventArgs>("CustomCursor", false, ""));
            behaviorGroup.AddChild(Initialize<DiagramCustomItemDragEventArgs>("CustomItemDrag", true, ""));
            behaviorGroup.AddChild(Initialize<DiagramCustomItemDragResultEventArgs>("CustomItemDragResult", true, "Result = {0}", e => e.Result));
            behaviorGroup.AddChild(Initialize<DiagramCustomItemGiveFeedbackEventArgs>("CustomItemGiveFeedback", false, ""));
            behaviorGroup.AddChild(Initialize<DiagramCustomItemQueryContinueDragEventArgs>("CustomItemQueryContinueDrag", false, ""));
            behaviorGroup.AddChild(Initialize<DiagramActiveToolChangedEventArgs>("ActiveToolChanged", true,"OldTool = {0}, NewTool = {1}", e => e.OldTool, e => e.NewTool));
            var diagramGroup = DiagramEventNode.Create("Diagram Document", null, DiagramEventNodeKind.Group);
            groups.Add(diagramGroup);
            diagramGroup.AddChild(Initialize<DiagramCanvasBoundsChangedEventArgs>("CanvasBoundsChanged", false, "OldBounds = {{ {0} }}, NewBounds = {{ {1} }}", e => e.OldBounds, e => e.NewBounds));
            diagramGroup.AddChild(Initialize<DiagramCustomLoadDocumentEventArgs>("CustomLoadDocument", true, ""));
            diagramGroup.AddChild(Initialize<DiagramCustomSaveDocumentEventArgs>("CustomSaveDocument", true, ""));
            diagramGroup.AddChild(Initialize<DiagramDocumentLoadedEventArgs>("DocumentLoaded", true, ""));
            diagramGroup.AddChild(Initialize<DiagramShowingOpenDialogEventArgs>("ShowingOpenDialog", true, ""));
            diagramGroup.AddChild(Initialize<DiagramShowingSaveDialogEventArgs>("ShowingSaveDialog", true, ""));
            var itemsGroup = DiagramEventNode.Create("Diagram Items", null, DiagramEventNodeKind.Group);
            groups.Add(itemsGroup);
            itemsGroup.AddChild(Initialize<DiagramAddingNewItemEventArgs>("AddingNewItem", true, "Item is {0}, Parent is {1}", e => e.Item.GetType().Name, e => e.Parent.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramBeforeItemsMovingEventArgs>("BeforeItemsMoving", true, "ActionSource = {0}", e => e.ActionSource));
            itemsGroup.AddChild(Initialize<DiagramBeforeItemsResizingEventArgs>("BeforeItemsResizing", true, "ActionSource = {0}", e => e.ActionSource));
            itemsGroup.AddChild(Initialize<DiagramBeforeItemsRotatingEventArgs>("BeforeItemsRotating", true, "ActionSource = {0}", e => e.ActionSource));
            itemsGroup.AddChild(Initialize<DiagramClosedEditorEventArgs>("ClosedEditor", true, "Item is {0}", e => e.Item.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramConnectionChangingEventArgs>("ConnectionChanging", true, "PointType = {0}", e => e.ConnectorPointType));
            itemsGroup.AddChild(Initialize<DiagramConnectionChangedEventArgs>("ConnectionChanged", true, "PointType = {0}", e => e.ConnectorPointType));
            itemsGroup.AddChild(Initialize<DiagramCustomGetEditableItemPropertiesEventArgs>("CustomGetEditableItemProperties", true, "Item is {0}", e => e.Item.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramCustomGetEditableItemPropertiesCacheKeyEventArgs>("CustomGetEditableItemPropertiesCacheKey", false, "Item is {0}", e => e.Item.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramCustomGetSerializableItemPropertiesEventArgs>("CustomGetSerializableItemProperties", true, "ItemType = {0}", e => e.ItemType.Name));
            itemsGroup.AddChild(Initialize<DiagramCustomLoadImageEventArgs>("CustomLoadImage", true, ""));
            itemsGroup.AddChild(Initialize<DiagramItemBoundsChangedEventArgs>("ItemBoundsChanged", false, ""));
            itemsGroup.AddChild(Initialize<DiagramItemContentChangedEventArgs>("ItemContentChanged", true, "OldValue = {0}, NewValue = {1}", e => e.OldValue, e => e.NewValue));
            itemsGroup.AddChild(Initialize<DiagramItemCreatingEventArgs, ItemUsage>("ItemCreating", null, EItemUsage, "ItemType = {0}, ItemUsage = {1}", e => e.ItemType.Name, e => e.ItemUsage));
            itemsGroup.AddChild(Initialize<DiagramItemDrawingEventArgs, DiagramActionStage>("ItemDrawing", null, EStage, "Tool = {0}, Stage = {1}", e => e.Tool.ToolName, e => e.Stage));
            itemsGroup.AddChild(Initialize<DiagramItemInitializingEventArgs, ItemUsage>("ItemInitializing", null, EItemUsage, "Item is {0}, ItemUsage = {1}", e => e.Item.GetType().Name, e => e.ItemUsage));
            itemsGroup.AddChild(Initialize<DiagramItemsChangedEventArgs>("ItemsChanged", true, "Action = {0}", e => e.Action));
            itemsGroup.AddChild(Initialize<DiagramItemsDeletingEventArgs>("ItemsDeleting", true, ""));
            itemsGroup.AddChild(Initialize<DiagramItemsMovingEventArgs, DiagramActionStage>("ItemsMoving", null, EStage, "Stage = {0}, ActionSource = {1}", e => e.Stage, e => e.ActionSource));
            itemsGroup.AddChild(Initialize<DiagramItemsPastingEventArgs>("ItemsPasting", true, ""));
            itemsGroup.AddChild(Initialize<DiagramItemsResizingEventArgs, DiagramActionStage>("ItemsResizing", null, EStage, "Stage = {0}, ActionSource = {1}", e => e.Stage, e => e.ActionSource));
            itemsGroup.AddChild(Initialize<DiagramItemsRotatingEventArgs, DiagramActionStage>("ItemsRotating", null, EStage, "Stage = {0}, ActionSource = {1}", e => e.Stage, e => e.ActionSource));
            itemsGroup.AddChild(Initialize<DiagramQueryConnectionPointsEventArgs, ConnectorPointType, DiagramConnector>("QueryConnectionPoints", null, EConnectorPointType, EConnector, "ConnectorPointType = {0}, HoveredItem is {1}", e => e.ConnectorPointType, e => e.HoveredItem.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramQueryItemDrawActionEventArgs>("QueryItemDrawAction", false, "Tool = {0}", e => e.Tool.ToolName));
            itemsGroup.AddChild(Initialize<DiagramQueryItemEditActionEventArgs>("QueryItemEditAction", true, "Item is {0}", e => e.Item.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramQueryItemSnappingEventArgs>("QueryItemSnapping", false, "Item is {0}, SnapTo is {1}", e => e.Item.GetType().Name, e => e.SnapTo.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramQueryItemsActionEventArgs, ItemsActionKind>("QueryItemsAction", false, EAction, "Action = {0}", e => e.Action));
            itemsGroup.AddChild(Initialize<DiagramShowingEditorEventArgs>("ShowingEditor", true, "Item is {0}", e => e.Item.GetType().Name));
            itemsGroup.AddChild(Initialize<DiagramShowingOpenImageDialogEventArgs>("ShowingOpenImageDialog", true, ""));
            var propertiesGroup = DiagramEventNode.Create("Diagram Property Changed", null, DiagramEventNodeKind.Group);
            groups.Add(propertiesGroup);
            propertiesGroup.AddChild(Initialize<DiagramSelectionChangedEventArgs>("SelectionChanged", true, ""));
            propertiesGroup.AddChild(Initialize<DiagramZoomFactorChangedEventArgs>("ZoomFactorChanged", false, "OldValue = {0:0 %}, NewValue = {1:0 %}", e => e.OldValue, e => e.NewValue));
            var subordinatesGroup = DiagramEventNode.Create("Expand And Collapse", true, DiagramEventNodeKind.Group);
            groups.Add(subordinatesGroup);
            subordinatesGroup.AddChild(Initialize<DiagramSubordinatesHiddenEventArgs>("SubordinatesHidden", true, ""));
            subordinatesGroup.AddChild(Initialize<DiagramSubordinatesHidingEventArgs>("SubordinatesHiding", true, ""));
            subordinatesGroup.AddChild(Initialize<DiagramSubordinatesShowingEventArgs>("SubordinatesShowing", true, ""));
            subordinatesGroup.AddChild(Initialize<DiagramSubordinatesShownEventArgs>("SubordinatesShown", true, ""));
            var layoutGroup = DiagramEventNode.Create("Layout", true, DiagramEventNodeKind.Group);
            groups.Add(layoutGroup);
            layoutGroup.AddChild(Initialize<DiagramRelayoutItemsCompletedEventArgs>("RelayoutItemsCompleted", true, ""));
            return groups;
        }

        static IEnumerable<EventArgValueInfo<TArg>> EnumArgValues<TArg>(IEnumerable<TArg> values, Func<TArg, bool> isChecked) {
            return values.Select(value => new EventArgValueInfo<TArg>(x => "e." + x + " = " + value.ToString(), isChecked(value), x => Equals(x, value))).ToArray();
        }
        sealed class EventArgValueInfo<TArg> {
            public EventArgValueInfo(Func<string, string> title, bool? isChecked, Func<TArg, bool> isValue) {
                Title = title;
                IsValue = isValue;
                IsChecked = isChecked;
            }
            public readonly Func<string, string> Title;
            public Func<TArg, bool> IsValue;
            public readonly bool? IsChecked;
        }
        sealed class EventArgInfo<TArg> {
            public EventArgInfo(string title, IEnumerable<EventArgValueInfo<TArg>> values) {
                Title = title;
                Values = values;
            }
            public readonly string Title;
            public IEnumerable<EventArgValueInfo<TArg>> Values;
        }
        DiagramEventNode Initialize<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            var node = DiagramEventNode.Create(title, isChecked, DiagramEventNodeKind.EventNode);
            eventsOwner.GetType().GetEvent(title).AddEventHandler(eventsOwner, new EventHandler<TEventArgs>((sender, e) => {
                if(node.ActualIsChecked)
                    addToLog(title, format, formatArgs.Select(x => x(e)).ToArray());
            }));
            return node;
        }
        DiagramEventNode Initialize<TEventArgs, TArg1>(string title, bool? isChecked, EventArgInfo<TArg1> arg1, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            var node = DiagramEventNode.Create(title, isChecked, DiagramEventNodeKind.EventNode);
            var children = new List<Tuple<Func<TArg1, bool>, DiagramEventNode>>();
            foreach(var value1 in arg1.Values) {
                var child = DiagramEventNode.Create(value1.Title(arg1.Title), value1.IsChecked, DiagramEventNodeKind.Parameter);
                children.Add(new Tuple<Func<TArg1, bool>, DiagramEventNode>(x => value1.IsValue(x), child));
                node.AddChild(child);
            }
            eventsOwner.GetType().GetEvent(title).AddEventHandler(eventsOwner, new EventHandler<TEventArgs>((sender, e) => {
                var v1 = (TArg1)e.GetType().GetProperty(arg1.Title).GetValue(e, null);
                if(children.First(x => x.Item1(v1)).Item2.ActualIsChecked)
                    addToLog(title, format, formatArgs.Select(x => x(e)).ToArray());
            }));
            return node;
        }
        DiagramEventNode Initialize<TEventArgs, TArg1, TArg2>(string title, bool? isChecked, EventArgInfo<TArg1> arg1, EventArgInfo<TArg2> arg2, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            var node = DiagramEventNode.Create(title, isChecked, DiagramEventNodeKind.EventNode);
            var children = new List<Tuple<Func<TArg1, TArg2, bool>, DiagramEventNode>>();
            foreach(var value2 in arg2.Values) {
                var child2 = DiagramEventNode.Create(value2.Title(arg2.Title), value2.IsChecked, DiagramEventNodeKind.Parameter);
                foreach(var value1 in arg1.Values) {
                    var child1 = DiagramEventNode.Create(value1.Title(arg1.Title), value2.IsChecked.HasValue ? value2.IsChecked : value1.IsChecked, DiagramEventNodeKind.Parameter);
                    children.Add(new Tuple<Func<TArg1, TArg2, bool>, DiagramEventNode>((x1, x2) => value1.IsValue(x1) && value2.IsValue(x2), child1));
                    child2.AddChild(child1);
                }
                node.AddChild(child2);
            }
            eventsOwner.GetType().GetEvent(title).AddEventHandler(eventsOwner, new EventHandler<TEventArgs>((sender, e) => {
                var v1 = (TArg1)e.GetType().GetProperty(arg1.Title).GetValue(e, null);
                var v2 = (TArg2)e.GetType().GetProperty(arg2.Title).GetValue(e, null);
                if(children.First(x => x.Item1(v1, v2)).Item2.ActualIsChecked)
                    addToLog(title, format, formatArgs.Select(x => x(e)).ToArray());
            }));
            return node;
        }
    }
}
