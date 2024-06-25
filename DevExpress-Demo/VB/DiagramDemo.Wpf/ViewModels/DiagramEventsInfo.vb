Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Class DiagramEventsInfo

        Private Shared ReadOnly EAction As EventArgInfo(Of ItemsActionKind) = New EventArgInfo(Of ItemsActionKind)("Action", EnumArgValues({ItemsActionKind.Move, ItemsActionKind.MoveCopy, ItemsActionKind.Resize, ItemsActionKind.Rotate, ItemsActionKind.Copy, ItemsActionKind.Delete}, Function(x) False))

        Private Shared ReadOnly EStage As EventArgInfo(Of DiagramActionStage) = New EventArgInfo(Of DiagramActionStage)("Stage", EnumArgValues({DiagramActionStage.Start, DiagramActionStage.Continue, DiagramActionStage.Finished, DiagramActionStage.Canceled}, Function(x) x <> DiagramActionStage.Continue))

        Private Shared ReadOnly EConnectorPointType As EventArgInfo(Of ConnectorPointType) = New EventArgInfo(Of ConnectorPointType)("ConnectorPointType", EnumArgValues({ConnectorPointType.Begin, ConnectorPointType.End}, Function(x) x = ConnectorPointType.End))

        Private Shared ReadOnly EConnector As EventArgInfo(Of DiagramConnector) = New EventArgInfo(Of DiagramConnector)("Connector", {New EventArgValueInfo(Of DiagramConnector)(Function(x) "e.Connector.GetDiagram() is null", Nothing, Function(x) x.GetDiagram() Is Nothing), New EventArgValueInfo(Of DiagramConnector)(Function(x) "e.Connector.GetDiagram() is not null", True, Function(x) x.GetDiagram() IsNot Nothing)})

        Private Shared ReadOnly EItemUsage As EventArgInfo(Of ItemUsage) = New EventArgInfo(Of ItemUsage)("ItemUsage", EnumArgValues({ItemUsage.Diagram, ItemUsage.ToolboxPreview}, Function(x) x = ItemUsage.Diagram))

        Private eventsOwner As DiagramControl

        Private addToLog As Action(Of String, String, Object())

        Public Sub New(ByVal eventsOwner As DiagramControl, ByVal addToLog As Action(Of String, String, Object()))
            Me.eventsOwner = eventsOwner
            Me.addToLog = addToLog
        End Sub

        Public Function Initialize() As IEnumerable(Of DiagramEventNode)
            Dim groups = New ObservableCollection(Of DiagramEventNode)()
            Dim behaviorGroup = DiagramEventNode.Create("Behavior", Nothing, DiagramEventNodeKind.Group)
            groups.Add(behaviorGroup)
            behaviorGroup.AddChild(Initialize(Of DiagramCustomCursorEventArgs)("CustomCursor", False, ""))
            behaviorGroup.AddChild(Initialize(Of DiagramCustomItemDragEventArgs)("CustomItemDrag", True, ""))
            behaviorGroup.AddChild(Initialize(Of DiagramCustomItemDragResultEventArgs)("CustomItemDragResult", True, "Result = {0}", Function(e) e.Result))
            behaviorGroup.AddChild(Initialize(Of DiagramCustomItemGiveFeedbackEventArgs)("CustomItemGiveFeedback", False, ""))
            behaviorGroup.AddChild(Initialize(Of DiagramCustomItemQueryContinueDragEventArgs)("CustomItemQueryContinueDrag", False, ""))
            behaviorGroup.AddChild(Initialize(Of DiagramActiveToolChangedEventArgs)("ActiveToolChanged", True, "OldTool = {0}, NewTool = {1}", Function(e) e.OldTool, Function(e) e.NewTool))
            Dim diagramGroup = DiagramEventNode.Create("Diagram Document", Nothing, DiagramEventNodeKind.Group)
            groups.Add(diagramGroup)
            diagramGroup.AddChild(Initialize(Of DiagramCanvasBoundsChangedEventArgs)("CanvasBoundsChanged", False, "OldBounds = {{ {0} }}, NewBounds = {{ {1} }}", Function(e) e.OldBounds, Function(e) e.NewBounds))
            diagramGroup.AddChild(Initialize(Of DiagramCustomLoadDocumentEventArgs)("CustomLoadDocument", True, ""))
            diagramGroup.AddChild(Initialize(Of DiagramCustomSaveDocumentEventArgs)("CustomSaveDocument", True, ""))
            diagramGroup.AddChild(Initialize(Of DiagramDocumentLoadedEventArgs)("DocumentLoaded", True, ""))
            diagramGroup.AddChild(Initialize(Of DiagramShowingOpenDialogEventArgs)("ShowingOpenDialog", True, ""))
            diagramGroup.AddChild(Initialize(Of DiagramShowingSaveDialogEventArgs)("ShowingSaveDialog", True, ""))
            Dim itemsGroup = DiagramEventNode.Create("Diagram Items", Nothing, DiagramEventNodeKind.Group)
            groups.Add(itemsGroup)
            itemsGroup.AddChild(Initialize(Of DiagramAddingNewItemEventArgs)("AddingNewItem", True, "Item is {0}, Parent is {1}", Function(e) e.Item.GetType().Name, Function(e) e.Parent.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramBeforeItemsMovingEventArgs)("BeforeItemsMoving", True, "ActionSource = {0}", Function(e) e.ActionSource))
            itemsGroup.AddChild(Initialize(Of DiagramBeforeItemsResizingEventArgs)("BeforeItemsResizing", True, "ActionSource = {0}", Function(e) e.ActionSource))
            itemsGroup.AddChild(Initialize(Of DiagramBeforeItemsRotatingEventArgs)("BeforeItemsRotating", True, "ActionSource = {0}", Function(e) e.ActionSource))
            itemsGroup.AddChild(Initialize(Of DiagramClosedEditorEventArgs)("ClosedEditor", True, "Item is {0}", Function(e) e.Item.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramConnectionChangingEventArgs)("ConnectionChanging", True, "PointType = {0}", Function(e) e.ConnectorPointType))
            itemsGroup.AddChild(Initialize(Of DiagramConnectionChangedEventArgs)("ConnectionChanged", True, "PointType = {0}", Function(e) e.ConnectorPointType))
            itemsGroup.AddChild(Initialize(Of DiagramCustomGetEditableItemPropertiesEventArgs)("CustomGetEditableItemProperties", True, "Item is {0}", Function(e) e.Item.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramCustomGetEditableItemPropertiesCacheKeyEventArgs)("CustomGetEditableItemPropertiesCacheKey", False, "Item is {0}", Function(e) e.Item.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramCustomGetSerializableItemPropertiesEventArgs)("CustomGetSerializableItemProperties", True, "ItemType = {0}", Function(e) e.ItemType.Name))
            itemsGroup.AddChild(Initialize(Of DiagramCustomLoadImageEventArgs)("CustomLoadImage", True, ""))
            itemsGroup.AddChild(Initialize(Of DiagramItemBoundsChangedEventArgs)("ItemBoundsChanged", False, ""))
            itemsGroup.AddChild(Initialize(Of DiagramItemContentChangedEventArgs)("ItemContentChanged", True, "OldValue = {0}, NewValue = {1}", Function(e) e.OldValue, Function(e) e.NewValue))
            itemsGroup.AddChild(Initialize(Of DiagramItemCreatingEventArgs, ItemUsage)("ItemCreating", Nothing, EItemUsage, "ItemType = {0}, ItemUsage = {1}", Function(e) e.ItemType.Name, Function(e) e.ItemUsage))
            itemsGroup.AddChild(Initialize(Of DiagramItemDrawingEventArgs, DiagramActionStage)("ItemDrawing", Nothing, EStage, "Tool = {0}, Stage = {1}", Function(e) e.Tool.ToolName, Function(e) e.Stage))
            itemsGroup.AddChild(Initialize(Of DiagramItemInitializingEventArgs, ItemUsage)("ItemInitializing", Nothing, EItemUsage, "Item is {0}, ItemUsage = {1}", Function(e) e.Item.GetType().Name, Function(e) e.ItemUsage))
            itemsGroup.AddChild(Initialize(Of DiagramItemsChangedEventArgs)("ItemsChanged", True, "Action = {0}", Function(e) e.Action))
            itemsGroup.AddChild(Initialize(Of DiagramItemsDeletingEventArgs)("ItemsDeleting", True, ""))
            itemsGroup.AddChild(Initialize(Of DiagramItemsMovingEventArgs, DiagramActionStage)("ItemsMoving", Nothing, EStage, "Stage = {0}, ActionSource = {1}", Function(e) e.Stage, Function(e) e.ActionSource))
            itemsGroup.AddChild(Initialize(Of DiagramItemsPastingEventArgs)("ItemsPasting", True, ""))
            itemsGroup.AddChild(Initialize(Of DiagramItemsResizingEventArgs, DiagramActionStage)("ItemsResizing", Nothing, EStage, "Stage = {0}, ActionSource = {1}", Function(e) e.Stage, Function(e) e.ActionSource))
            itemsGroup.AddChild(Initialize(Of DiagramItemsRotatingEventArgs, DiagramActionStage)("ItemsRotating", Nothing, EStage, "Stage = {0}, ActionSource = {1}", Function(e) e.Stage, Function(e) e.ActionSource))
            itemsGroup.AddChild(Initialize(Of DiagramQueryConnectionPointsEventArgs, ConnectorPointType, DiagramConnector)("QueryConnectionPoints", Nothing, EConnectorPointType, EConnector, "ConnectorPointType = {0}, HoveredItem is {1}", Function(e) e.ConnectorPointType, Function(e) e.HoveredItem.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramQueryItemDrawActionEventArgs)("QueryItemDrawAction", False, "Tool = {0}", Function(e) e.Tool.ToolName))
            itemsGroup.AddChild(Initialize(Of DiagramQueryItemEditActionEventArgs)("QueryItemEditAction", True, "Item is {0}", Function(e) e.Item.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramQueryItemSnappingEventArgs)("QueryItemSnapping", False, "Item is {0}, SnapTo is {1}", Function(e) e.Item.GetType().Name, Function(e) e.SnapTo.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramQueryItemsActionEventArgs, ItemsActionKind)("QueryItemsAction", False, EAction, "Action = {0}", Function(e) e.Action))
            itemsGroup.AddChild(Initialize(Of DiagramShowingEditorEventArgs)("ShowingEditor", True, "Item is {0}", Function(e) e.Item.GetType().Name))
            itemsGroup.AddChild(Initialize(Of DiagramShowingOpenImageDialogEventArgs)("ShowingOpenImageDialog", True, ""))
            Dim propertiesGroup = DiagramEventNode.Create("Diagram Property Changed", Nothing, DiagramEventNodeKind.Group)
            groups.Add(propertiesGroup)
            propertiesGroup.AddChild(Initialize(Of DiagramSelectionChangedEventArgs)("SelectionChanged", True, ""))
            propertiesGroup.AddChild(Initialize(Of DiagramZoomFactorChangedEventArgs)("ZoomFactorChanged", False, "OldValue = {0:0 %}, NewValue = {1:0 %}", Function(e) e.OldValue, Function(e) e.NewValue))
            Dim subordinatesGroup = DiagramEventNode.Create("Expand And Collapse", True, DiagramEventNodeKind.Group)
            groups.Add(subordinatesGroup)
            subordinatesGroup.AddChild(Initialize(Of DiagramSubordinatesHiddenEventArgs)("SubordinatesHidden", True, ""))
            subordinatesGroup.AddChild(Initialize(Of DiagramSubordinatesHidingEventArgs)("SubordinatesHiding", True, ""))
            subordinatesGroup.AddChild(Initialize(Of DiagramSubordinatesShowingEventArgs)("SubordinatesShowing", True, ""))
            subordinatesGroup.AddChild(Initialize(Of DiagramSubordinatesShownEventArgs)("SubordinatesShown", True, ""))
            Dim layoutGroup = DiagramEventNode.Create("Layout", True, DiagramEventNodeKind.Group)
            groups.Add(layoutGroup)
            layoutGroup.AddChild(Initialize(Of DiagramRelayoutItemsCompletedEventArgs)("RelayoutItemsCompleted", True, ""))
            Return groups
        End Function

        Private Shared Function EnumArgValues(Of TArg)(ByVal values As IEnumerable(Of TArg), ByVal isChecked As Func(Of TArg, Boolean)) As IEnumerable(Of EventArgValueInfo(Of TArg))
            Return values.[Select](Function(value) New EventArgValueInfo(Of TArg)(Function(x) "e." & x & " = " & value.ToString(), isChecked(value), Function(x) Equals(x, value))).ToArray()
        End Function

        Private NotInheritable Class EventArgValueInfo(Of TArg)

            Public Sub New(ByVal title As Func(Of String, String), ByVal isChecked As Boolean?, ByVal isValue As Func(Of TArg, Boolean))
                Me.Title = title
                Me.IsValue = isValue
                Me.IsChecked = isChecked
            End Sub

            Public ReadOnly Title As Func(Of String, String)

            Public IsValue As Func(Of TArg, Boolean)

            Public ReadOnly IsChecked As Boolean?
        End Class

        Private NotInheritable Class EventArgInfo(Of TArg)

            Public Sub New(ByVal title As String, ByVal values As IEnumerable(Of EventArgValueInfo(Of TArg)))
                Me.Title = title
                Me.Values = values
            End Sub

            Public ReadOnly Title As String

            Public Values As IEnumerable(Of EventArgValueInfo(Of TArg))
        End Class

        Private Function Initialize(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As DiagramEventNode
            Dim node = DiagramEventNode.Create(title, isChecked, DiagramEventNodeKind.EventNode)
            eventsOwner.GetType().GetEvent(title).AddEventHandler(eventsOwner, New EventHandler(Of TEventArgs)(Sub(sender, e)
                If node.ActualIsChecked Then addToLog(title, format, formatArgs.[Select](Function(x) x(e)).ToArray())
            End Sub))
            Return node
        End Function

        Private Function Initialize(Of TEventArgs As EventArgs, TArg1)(ByVal title As String, ByVal isChecked As Boolean?, ByVal arg1 As EventArgInfo(Of TArg1), ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As DiagramEventNode
            Dim node = DiagramEventNode.Create(title, isChecked, DiagramEventNodeKind.EventNode)
            Dim children = New List(Of Tuple(Of Func(Of TArg1, Boolean), DiagramEventNode))()
            For Each value1 In arg1.Values
                Dim child = DiagramEventNode.Create(value1.Title(arg1.Title), value1.IsChecked, DiagramEventNodeKind.Parameter)
                children.Add(New Tuple(Of Func(Of TArg1, Boolean), DiagramEventNode)(Function(x) value1.IsValue(x), child))
                node.AddChild(child)
            Next

            eventsOwner.GetType().GetEvent(CStr(title)).AddEventHandler(eventsOwner, New EventHandler(Of TEventArgs)(Sub(sender, e)
                Dim v1 = CType(e.GetType().GetProperty(CStr(arg1.Title)).GetValue(e, Nothing), TArg1)
                If Enumerable.First(Of Tuple(Of Global.System.Func(Of TArg1, Global.System.[Boolean]), Global.DiagramDemo.DiagramEventNode))(children, CType(Function(x) CBool(x.Item1(CType(v1, TArg1))), Func(Of Tuple(Of Func(Of TArg1, Boolean), DiagramEventNode), Boolean))).Item2.ActualIsChecked Then addToLog(title, format, formatArgs.[Select](Function(x) x(e)).ToArray())
            End Sub))
            Return node
        End Function

        Private Function Initialize(Of TEventArgs As EventArgs, TArg1, TArg2)(ByVal title As String, ByVal isChecked As Boolean?, ByVal arg1 As EventArgInfo(Of TArg1), ByVal arg2 As EventArgInfo(Of TArg2), ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As DiagramEventNode
            Dim node = DiagramEventNode.Create(title, isChecked, DiagramEventNodeKind.EventNode)
            Dim children = New List(Of Tuple(Of Func(Of TArg1, TArg2, Boolean), DiagramEventNode))()
            For Each value2 In arg2.Values
                Dim child2 = DiagramEventNode.Create(value2.Title(arg2.Title), value2.IsChecked, DiagramEventNodeKind.Parameter)
                For Each value1 In arg1.Values
                    Dim child1 = DiagramEventNode.Create(value1.Title(arg1.Title), If(value2.IsChecked.HasValue, value2.IsChecked, value1.IsChecked), DiagramEventNodeKind.Parameter)
                    children.Add(New Tuple(Of Func(Of TArg1, TArg2, Boolean), DiagramEventNode)(Function(x1, x2) value1.IsValue(x1) AndAlso value2.IsValue(x2), child1))
                    child2.AddChild(child1)
                Next

                node.AddChild(child2)
            Next

            eventsOwner.GetType().GetEvent(CStr(title)).AddEventHandler(eventsOwner, New EventHandler(Of TEventArgs)(Sub(sender, e)
                Dim v1 = CType(e.GetType().GetProperty(CStr(arg1.Title)).GetValue(e, Nothing), TArg1)
                Dim v2 = CType(e.GetType().GetProperty(CStr(arg2.Title)).GetValue(e, Nothing), TArg2)
                If Enumerable.First(Of Tuple(Of Global.System.Func(Of TArg1, TArg2, Global.System.[Boolean]), Global.DiagramDemo.DiagramEventNode))(children, CType(Function(x) CBool(x.Item1(CType(v1, TArg1), CType(v2, TArg2))), Func(Of Tuple(Of Func(Of TArg1, TArg2, Boolean), DiagramEventNode), Boolean))).Item2.ActualIsChecked Then addToLog(title, format, formatArgs.[Select](Function(x) x(e)).ToArray())
            End Sub))
            Return node
        End Function
    End Class
End Namespace
