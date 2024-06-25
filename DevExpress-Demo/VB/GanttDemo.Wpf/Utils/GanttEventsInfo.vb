Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.Gantt
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Gantt
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList

Namespace GanttDemo

    Public Class GanttEventsInfo

        Private ReadOnly eventsOwner As GanttControl

        Private ReadOnly addToLog As Action(Of String, String, String, Object())

        Public Sub New(ByVal eventsOwner As GanttControl, ByVal addToLog As Action(Of String, String, String, Object()))
            Me.eventsOwner = eventsOwner
            Me.addToLog = addToLog
        End Sub

        Public Function Initialize() As IEnumerable(Of GanttEventNode)
            Dim groups = New ObservableCollection(Of GanttEventNode)()
            Dim navigationGroup = GanttEventNode.CreateGroup("Navigation", Nothing)
            groups.Add(navigationGroup)
            navigationGroup.AddChild(InitializeGanttViewEvent(Of RequestTimescaleRulersEventArgs)("RequestTimescaleRulers", True, ""))
            navigationGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeEventArgs)("NodeExpanded", True, Function(e) Not String.IsNullOrEmpty(CType(e.Node, GanttNode).Name), "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            navigationGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeEventArgs)("NodeCollapsed", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            navigationGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeAllowEventArgs)("NodeExpanding", False, Function(e) Not String.IsNullOrEmpty(CType(e.Node, GanttNode).Name), "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            navigationGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeAllowEventArgs)("NodeCollapsing", False, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            navigationGroup.AddChild(InitializeDataControlBaseEvent(Of CurrentItemChangedEventArgs)("CurrentItemChanged", True, "Old Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "New Task: {1}", Function(e) NameOrDefault(e.OldItem), Function(e) NameOrDefault(e.NewItem)))
            navigationGroup.AddChild(InitializeDataControlBaseEvent(Of SelectedItemChangedEventArgs)("SelectedItemChanged", False, "Old Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "New Task: {1}", Function(e) NameOrDefault(e.OldItem), Function(e) NameOrDefault(e.NewItem)))
            navigationGroup.AddChild(InitializeDataControlBaseEvent(Of CurrentColumnChangedEventArgs)("CurrentColumnChanged", True, "Old Column: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "New Column: {1}", Function(e) If(e.OldColumn IsNot Nothing, e.OldColumn.Header, Nothing), Function(e) If(e.NewColumn IsNot Nothing, e.NewColumn.Header, Nothing)))
            navigationGroup.AddChild(InitializeDataViewBaseEvent(Of FocusedRowHandleChangedEventArgs)("FocusedRowHandleChanged", False, "Task: {0}", Function(e) NameOrDefault(e.RowData.Row)))
            Dim sortingGroup = GanttEventNode.CreateGroup("Sorting", True)
            groups.Add(sortingGroup)
            sortingGroup.AddChild(InitializeTreeListViewEvent(Of RoutedEventArgs)("StartSorting", True, ""))
            sortingGroup.AddChild(InitializeTreeListViewEvent(Of RoutedEventArgs)("EndSorting", True, ""))
            Dim filteringGroup = GanttEventNode.CreateGroup("Filtering", Nothing)
            groups.Add(filteringGroup)
            filteringGroup.AddChild(InitializeDataViewBaseEvent(Of FilterPopupEventArgs)("ShowFilterPopup", True, "Column: {0}", Function(e) e.Column.Header))
            filteringGroup.AddChild(InitializeDataControlBaseEvent(Of RoutedEventArgs)("FilterChanged", True, ""))
            filteringGroup.AddChild(InitializeDataControlBaseEvent(Of FilterGroupSortChangingEventArgs)("FilterGroupSortChanging", False, ""))
            Dim copyPasteGroup = GanttEventNode.CreateGroup("Copy/Paste", True)
            groups.Add(copyPasteGroup)
            copyPasteGroup.AddChild(InitializeTreeListControlEvent(Of TreeListCopyingToClipboardEventArgs)("CopyingToClipboard", True, ""))
            copyPasteGroup.AddChild(InitializeDataControlBaseEvent(Of PastingFromClipboardEventArgs)("PastingFromClipboard", True, ""))
            Dim dragDropGroup = GanttEventNode.CreateGroup("DragDrop", Nothing)
            groups.Add(dragDropGroup)
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent(Of GiveRecordDragFeedbackEventArgs)("GiveRecordDragFeedback", False, "Task: {0}", Function(e) CType(CType(e.Data.GetData(GetType(RecordDragDropData).FullName), RecordDragDropData).Records(0), GanttTask).Name))
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent(Of ContinueRecordDragEventArgs)("ContinueRecordDrag", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Action: {1}", Function(e) CType(CType(e.Data.GetData(CStr(GetType(RecordDragDropData).FullName)), RecordDragDropData).Records(CInt(0)), GanttTask).Name, Function(e) e.Action))
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent(Of CompleteRecordDragDropEventArgs)("CompleteRecordDragDrop", True, "Task: {0}", Function(e) CType(e.Records(0), GanttTask).Name))
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent(Of DragRecordOverEventArgs)("DragRecordOver", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Over Task: {1}" & Microsoft.VisualBasic.Constants.vbCrLf & "Position: {2}", Function(e) CType(CType(e.Data.GetData(CStr(GetType(RecordDragDropData).FullName)), RecordDragDropData).Records(CInt(0)), GanttTask).Name, Function(e) CType(e.TargetRecord, GanttTask).Name, Function(e) e.DropPosition))
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent(Of DropRecordEventArgs)("DropRecord", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Drop To Task: {1}" & Microsoft.VisualBasic.Constants.vbCrLf & "Position: {2}", Function(e) CType(CType(e.Data.GetData(CStr(GetType(RecordDragDropData).FullName)), RecordDragDropData).Records(CInt(0)), GanttTask).Name, Function(e) CType(e.TargetRecord, GanttTask).Name, Function(e) e.DropPosition))
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent(Of StartRecordDragEventArgs)("StartRecordDrag", True, "Task: {0}", Function(e) CType(CType(e.Data.GetData(GetType(RecordDragDropData).FullName), RecordDragDropData).Records(0), GanttTask).Name))
            dragDropGroup.AddChild(InitializeGanttViewEvent(Of QueryKeepTasksLinkedEventArgs)("QueryKeepTasksLinked", False, "Predecessor: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Successor: {1}", Function(e) CType(e.Predecessor, GanttNode).Name, Function(e) CType(e.Successor, GanttNode).Name))
            Dim editingGroup = GanttEventNode.CreateGroup("Editing", Nothing)
            groups.Add(editingGroup)
            editingGroup.AddChild(InitializeGanttViewEvent(Of QueryAllowedTaskEditActionEventArgs)("QueryAllowedTaskEditAction", False, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of QueryAllowPredecessorEditEventArgs)("QueryAllowPredecessorEdit", True, "Predecessor: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Successor: {1}", Function(e) CType(e.Predecessor, GanttNode).Name, Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of QueryAllowPredecessorLinkEditEventArgs)("QueryAllowPredecessorLinkEdit", True, "Predecessor: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Successor: {1}", Function(e) CType(e.Predecessor, GanttNode).Name, Function(e) CType(e.Successor, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of BeginTaskFinishDateMoveEventArgs)("BeginTaskFinishDateMove", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of TaskFinishDateMovingEventArgs)("TaskFinishDateMoving", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Finish Date: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.FinishDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of TaskFinishDateMovedEventArgs)("TaskFinishDateMoved", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Finish Date: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.FinishDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of TaskFinishDateMoveCanceledEventArgs)("TaskFinishDateMoveCanceled", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of BeginTaskMoveEventArgs)("BeginTaskMove", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of TaskMovingEventArgs)("TaskMoving", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Start Date: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.StartDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of TaskMovedEventArgs)("TaskMoved", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Start Date: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.StartDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of TaskMoveCanceledEventArgs)("TaskMoveCanceled", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of BeginProgressMoveEventArgs)("BeginProgressMove", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of ProgressMovingEventArgs)("ProgressMoving", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Progress: {1:p0}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Progress))
            editingGroup.AddChild(InitializeGanttViewEvent(Of ProgressMovedEventArgs)("ProgressMoved", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Progress: {1:p0}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Progress))
            editingGroup.AddChild(InitializeGanttViewEvent(Of ProgressMoveCanceledEventArgs)("ProgressMoveCanceled", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeGanttViewEvent(Of BeginNewTaskDrawEventArgs)("BeginNewTaskDraw", True, "Start Date: {0}", Function(e) e.StartDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of NewTaskDrawingEventArgs)("NewTaskDrawing", False, "Start Date: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Finish Date: {1}", Function(e) e.StartDate, Function(e) e.FinishDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of NewTaskDrawnEventArgs)("NewTaskDrawn", True, "Start Date: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Finish Date: {1}", Function(e) e.StartDate, Function(e) e.FinishDate))
            editingGroup.AddChild(InitializeGanttViewEvent(Of NewTaskDrawCanceledEventArgs)("NewTaskDrawCanceled", True, ""))
            editingGroup.AddChild(InitializeGanttViewEvent(Of BeginPredecessorLinkEditEventArgs)("BeginPredecessorLinkEdit", True, "Predecessor: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Successor: {1}", Function(e) CType(e.Predecessor, GanttNode).Name, Function(e) If(e.Successor IsNot Nothing, CType(e.Successor, GanttNode).Name, "None")))
            editingGroup.AddChild(InitializeGanttViewEvent(Of PredecessorLinkEditedEventArgs)("PredecessorLinkEdited", True, "Predecessor: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Old Successor: {1}" & Microsoft.VisualBasic.Constants.vbCrLf & "New Successor: {2}" & Microsoft.VisualBasic.Constants.vbCrLf & "Change Type: {3}", Function(e) CType(e.Predecessor, GanttNode).Name, Function(e) If(e.OldSuccessor IsNot Nothing, CType(e.OldSuccessor, GanttNode).Name, "None"), Function(e) If(e.Successor IsNot Nothing, CType(e.Successor, GanttNode).Name, "None"), Function(e) e.ChangeType))
            editingGroup.AddChild(InitializeGanttViewEvent(Of PredecessorLinkEditCanceledEventArgs)("PredecessorLinkEditCanceled", True, "Predecessor: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Successor: {1}", Function(e) CType(e.Predecessor, GanttNode).Name, Function(e) If(e.Successor IsNot Nothing, CType(e.Successor, GanttNode).Name, "None")))
            editingGroup.AddChild(InitializeGanttViewEvent(Of AddingNewPredecessorLinkEventArgs)("AddingNewPredecessorLink", True, ""))
            editingGroup.AddChild(InitializeGanttViewEvent(Of AddingNewResourceLinkEventArgs)("AddingNewResourceLink", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Resource: {1}", Function(e) CType(e.Task, GanttTask).Name, Function(e) CType(e.Resource, GanttResource).Name))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeChangedEventArgs)("NodeChanged", False, Function(e) Not String.IsNullOrEmpty(CType(e.Node, GanttNode).Name), "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Change Type: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.ChangeType))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeEventArgs)("NodeUpdated", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeValidationEventArgs)("ValidateNode", True, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListCellValidationEventArgs)("ValidateCell", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Column: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Column.Header))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListEditorEventArgs)("ShownEditor", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Column: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Column.Header))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListShowingEditorEventArgs)("ShowingEditor", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Column: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Column.Header))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListEditorEventArgs)("HiddenEditor", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Column: {1}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Column.Header))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListCellValueChangedEventArgs)("CellValueChanged", True, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Column: {1}" & Microsoft.VisualBasic.Constants.vbCrLf & "Value: {2}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Column.Header, Function(e) If(CType(e.Column, GanttColumn).BindTo = GanttColumnBindingSource.ResourceLinks, "(Collection)", e.Value)))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListCellValueChangedEventArgs)("CellValueChanging", False, "Task: {0}" & Microsoft.VisualBasic.Constants.vbCrLf & "Column: {1}" & Microsoft.VisualBasic.Constants.vbCrLf & "Value: {2}", Function(e) CType(e.Node, GanttNode).Name, Function(e) e.Column.Header, Function(e) e.Value))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListNodeEventArgs)("InitNewNode", True, ""))
            editingGroup.AddChild(InitializeTreeListViewEvent(Of TreeListAddingNewEventArgs)("AddingNewNode", True, ""))
            Dim schedulingGroup = GanttEventNode.CreateGroup("Scheduling", Nothing)
            groups.Add(schedulingGroup)
            schedulingGroup.AddChild(InitializeGanttViewEvent(Of QueryAllowScheduleTaskEventArgs)("QueryAllowScheduleTask", False, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            schedulingGroup.AddChild(InitializeGanttViewEvent(Of TasksScheduledEventArgs)("TasksScheduled", True, ""))
            schedulingGroup.AddChild(InitializeGanttViewEvent(Of CalculateSummaryTaskProgressEventArgs)("CalculateSummaryTaskProgress", False, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            schedulingGroup.AddChild(InitializeGanttViewEvent(Of CalculateSummaryTaskBoundsEventArgs)("CalculateSummaryTaskBounds", False, "Task: {0}", Function(e) CType(e.Node, GanttNode).Name))
            Dim otherGroup = GanttEventNode.CreateGroup("Other", Nothing)
            groups.Add(otherGroup)
            otherGroup.AddChild(InitializeDataViewBaseEvent(Of RoutedEventArgs)("ShownColumnChooser", True, ""))
            otherGroup.AddChild(InitializeDataViewBaseEvent(Of RoutedEventArgs)("HiddenColumnChooser", True, ""))
            otherGroup.AddChild(InitializeDataViewBaseEvent(Of GridMenuEventArgs)("ShowGridMenu", True, ""))
            otherGroup.AddChild(InitializeDataViewBaseEvent(Of ColumnHeaderClickEventArgs)("ColumnHeaderClick", False, "Column: {0}", Function(e) e.Column.Header))
            otherGroup.AddChild(InitializeTreeListViewEvent(Of RowDoubleClickEventArgs)("RowDoubleClick", False, "Task: {0}", Function(e) CType(e.Source.DataControl.GetRow(e.HitInfo.RowHandle), GanttTask).Name))
            Return groups
        End Function

        Private Function NameOrDefault(ByVal task As Object) As String
            Return If(task IsNot Nothing, CType(task, GanttTask).Name, String.Empty)
        End Function

        Private Function InitializeGanttViewEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeGenericEvent(title, isChecked, format, Function(x) x.View, formatArgs)
        End Function

        Private Function InitializeDataViewBaseGenericEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeGenericEvent(Of TEventArgs, DataViewBase)(title, isChecked, format, Function(x) x.View, formatArgs)
        End Function

        Private Function InitializeGenericEvent(Of TEventArgs As EventArgs, TOwner)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ByVal getOwner As Func(Of GanttControl, TOwner), ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Dim className = GetType(TOwner).FullName
            Dim node = GanttEventNode.Create(title, className, isChecked, GanttEventNodeKind.EventNode)
            Call GetType(TOwner).GetEvent(title).AddEventHandler(getOwner(eventsOwner), New EventHandler(Of TEventArgs)(Sub(sender, e)
                If node.ActualIsChecked Then addToLog(title, className, format, formatArgs.[Select](Function(x) x(e)).ToArray())
            End Sub))
            Return node
        End Function

        Private Function InitializeEvent(Of TEventArgs As EventArgs, TOwner)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ByVal getOwner As Func(Of GanttControl, TOwner), ByVal canLog As Func(Of TEventArgs, Boolean), ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Dim className = GetType(TOwner).FullName
            Dim node = GanttEventNode.Create(title, className, isChecked, GanttEventNodeKind.EventNode)
            Dim eventInfo = GetType(TOwner).GetEvent(title)
            Dim handler As Action(Of Object, TEventArgs) = New Action(Of Object, TEventArgs)(Sub(sender, e)
                If node.ActualIsChecked AndAlso canLog(e) Then addToLog(title, className, format, formatArgs.[Select](Function(x) x(e)).ToArray())
            End Sub)
            eventInfo.AddEventHandler(getOwner(eventsOwner), [Delegate].CreateDelegate(eventInfo.EventHandlerType, handler.Target, handler.Method))
            Return node
        End Function

        Private Function InitializeTreeListViewEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeEvent(Of TEventArgs, TreeListView)(title, isChecked, format, Function(x) x.View, Function(x) True, formatArgs)
        End Function

        Private Function InitializeTreeListViewEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal canLog As Func(Of TEventArgs, Boolean), ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeEvent(Of TEventArgs, TreeListView)(title, isChecked, format, Function(x) x.View, canLog, formatArgs)
        End Function

        Private Function InitializeTreeListControlEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeEvent(Of TEventArgs, TreeListControlBase)(title, isChecked, format, Function(x) x, Function(x) True, formatArgs)
        End Function

        Private Function InitializeDataControlBaseEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeEvent(Of TEventArgs, DataControlBase)(title, isChecked, format, Function(x) x, Function(x) True, formatArgs)
        End Function

        Private Function InitializeDataViewBaseEvent(Of TEventArgs As EventArgs)(ByVal title As String, ByVal isChecked As Boolean, ByVal format As String, ParamArray formatArgs As Func(Of TEventArgs, Object)()) As GanttEventNode
            Return InitializeEvent(Of TEventArgs, DataViewBase)(title, isChecked, format, Function(x) x.View, Function(x) True, formatArgs)
        End Function
    End Class
End Namespace
