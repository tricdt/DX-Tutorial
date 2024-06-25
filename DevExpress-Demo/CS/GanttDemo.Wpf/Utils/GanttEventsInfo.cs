using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.Gantt;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Gantt;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;

namespace GanttDemo {
    public class GanttEventsInfo {
        readonly GanttControl eventsOwner;
        readonly Action<string, string, string, object[]> addToLog;

        public GanttEventsInfo(GanttControl eventsOwner, Action<string, string, string, object[]> addToLog) {
            this.eventsOwner = eventsOwner;
            this.addToLog = addToLog;
        }
        public IEnumerable<GanttEventNode> Initialize() {
            var groups = new ObservableCollection<GanttEventNode>();
            var navigationGroup = GanttEventNode.CreateGroup("Navigation", null);
            groups.Add(navigationGroup);
            navigationGroup.AddChild(InitializeGanttViewEvent<RequestTimescaleRulersEventArgs>("RequestTimescaleRulers", true, ""));
            navigationGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeEventArgs>("NodeExpanded", true, e => !string.IsNullOrEmpty(((GanttNode)e.Node).Name), "Task: {0}", e => ((GanttNode)e.Node).Name));
            navigationGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeEventArgs>("NodeCollapsed", true, "Task: {0}", e => ((GanttNode)e.Node).Name));
            navigationGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeAllowEventArgs>("NodeExpanding", false, e => !string.IsNullOrEmpty(((GanttNode)e.Node).Name), "Task: {0}", e => ((GanttNode)e.Node).Name));
            navigationGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeAllowEventArgs>("NodeCollapsing", false, "Task: {0}", e => ((GanttNode)e.Node).Name));
            navigationGroup.AddChild(InitializeDataControlBaseEvent<CurrentItemChangedEventArgs>("CurrentItemChanged", true, "Old Task: {0}\r\nNew Task: {1}", e => NameOrDefault(e.OldItem), e => NameOrDefault(e.NewItem)));
            navigationGroup.AddChild(InitializeDataControlBaseEvent<SelectedItemChangedEventArgs>("SelectedItemChanged", false, "Old Task: {0}\r\nNew Task: {1}", e => NameOrDefault(e.OldItem), e => NameOrDefault(e.NewItem)));
            navigationGroup.AddChild(InitializeDataControlBaseEvent<CurrentColumnChangedEventArgs>("CurrentColumnChanged", true, "Old Column: {0}\r\nNew Column: {1}", e => e.OldColumn != null ? e.OldColumn.Header : null, e => e.NewColumn != null ? e.NewColumn.Header : null));
            navigationGroup.AddChild(InitializeDataViewBaseEvent<FocusedRowHandleChangedEventArgs>("FocusedRowHandleChanged", false, "Task: {0}", e => NameOrDefault(e.RowData.Row)));

            var sortingGroup = GanttEventNode.CreateGroup("Sorting", true);
            groups.Add(sortingGroup);
            sortingGroup.AddChild(InitializeTreeListViewEvent<RoutedEventArgs>("StartSorting", true, ""));
            sortingGroup.AddChild(InitializeTreeListViewEvent<RoutedEventArgs>("EndSorting", true, ""));

            var filteringGroup = GanttEventNode.CreateGroup("Filtering", null);
            groups.Add(filteringGroup);
            filteringGroup.AddChild(InitializeDataViewBaseEvent<FilterPopupEventArgs>("ShowFilterPopup", true, "Column: {0}", e => e.Column.Header));
            filteringGroup.AddChild(InitializeDataControlBaseEvent<RoutedEventArgs>("FilterChanged", true, ""));
            filteringGroup.AddChild(InitializeDataControlBaseEvent<FilterGroupSortChangingEventArgs>("FilterGroupSortChanging", false, ""));

            var copyPasteGroup = GanttEventNode.CreateGroup("Copy/Paste", true);
            groups.Add(copyPasteGroup);
            copyPasteGroup.AddChild(InitializeTreeListControlEvent<TreeListCopyingToClipboardEventArgs>("CopyingToClipboard", true, ""));
            copyPasteGroup.AddChild(InitializeDataControlBaseEvent<PastingFromClipboardEventArgs>("PastingFromClipboard", true, ""));

            var dragDropGroup = GanttEventNode.CreateGroup("DragDrop", null);
            groups.Add(dragDropGroup);
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent<GiveRecordDragFeedbackEventArgs>("GiveRecordDragFeedback", false, "Task: {0}", e =>
                ((GanttTask)((RecordDragDropData)e.Data.GetData(typeof(RecordDragDropData).FullName)).Records[0]).Name
            ));
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent<ContinueRecordDragEventArgs>("ContinueRecordDrag", false, "Task: {0}\r\nAction: {1}",
                e => ((GanttTask)((RecordDragDropData)e.Data.GetData(typeof(RecordDragDropData).FullName)).Records[0]).Name,
                e => e.Action
            ));
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent<CompleteRecordDragDropEventArgs>("CompleteRecordDragDrop", true, "Task: {0}",
                e => ((GanttTask)e.Records[0]).Name
            ));
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent<DragRecordOverEventArgs>("DragRecordOver", false, "Task: {0}\r\nOver Task: {1}\r\nPosition: {2}",
                e => ((GanttTask)((RecordDragDropData)e.Data.GetData(typeof(RecordDragDropData).FullName)).Records[0]).Name,
                e => ((GanttTask)e.TargetRecord).Name,
                e => e.DropPosition
            ));
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent<DropRecordEventArgs>("DropRecord", true, "Task: {0}\r\nDrop To Task: {1}\r\nPosition: {2}",
                e => ((GanttTask)((RecordDragDropData)e.Data.GetData(typeof(RecordDragDropData).FullName)).Records[0]).Name,
                e => ((GanttTask)e.TargetRecord).Name,
                e => e.DropPosition
            ));
            dragDropGroup.AddChild(InitializeDataViewBaseGenericEvent<StartRecordDragEventArgs>("StartRecordDrag", true, "Task: {0}", e =>
                ((GanttTask)((RecordDragDropData)e.Data.GetData(typeof(RecordDragDropData).FullName)).Records[0]).Name
            ));
            dragDropGroup.AddChild(InitializeGanttViewEvent<QueryKeepTasksLinkedEventArgs>("QueryKeepTasksLinked", false, "Predecessor: {0}\r\nSuccessor: {1}", e => ((GanttNode)e.Predecessor).Name, e => ((GanttNode)e.Successor).Name));

            var editingGroup = GanttEventNode.CreateGroup("Editing", null);
            groups.Add(editingGroup);
            editingGroup.AddChild(InitializeGanttViewEvent<QueryAllowedTaskEditActionEventArgs>("QueryAllowedTaskEditAction", false, "Task: {0}", e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeGanttViewEvent<QueryAllowPredecessorEditEventArgs>("QueryAllowPredecessorEdit", true, "Predecessor: {0}\r\nSuccessor: {1}", e => ((GanttNode)e.Predecessor).Name, e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeGanttViewEvent<QueryAllowPredecessorLinkEditEventArgs>("QueryAllowPredecessorLinkEdit", true, "Predecessor: {0}\r\nSuccessor: {1}", e => ((GanttNode)e.Predecessor).Name, e => ((GanttNode)e.Successor).Name));
            
            editingGroup.AddChild(InitializeGanttViewEvent<BeginTaskFinishDateMoveEventArgs>("BeginTaskFinishDateMove", true, "Task: {0}", e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeGanttViewEvent<TaskFinishDateMovingEventArgs>("TaskFinishDateMoving", false, "Task: {0}\r\nFinish Date: {1}", e => ((GanttNode)e.Node).Name, e => e.FinishDate));
            editingGroup.AddChild(InitializeGanttViewEvent<TaskFinishDateMovedEventArgs>("TaskFinishDateMoved", true, "Task: {0}\r\nFinish Date: {1}", e => ((GanttNode)e.Node).Name, e => e.FinishDate));
            editingGroup.AddChild(InitializeGanttViewEvent<TaskFinishDateMoveCanceledEventArgs>("TaskFinishDateMoveCanceled", true, "Task: {0}", e => ((GanttNode)e.Node).Name));

            editingGroup.AddChild(InitializeGanttViewEvent<BeginTaskMoveEventArgs>("BeginTaskMove", true, "Task: {0}", e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeGanttViewEvent<TaskMovingEventArgs>("TaskMoving", false, "Task: {0}\r\nStart Date: {1}", e => ((GanttNode)e.Node).Name, e => e.StartDate));
            editingGroup.AddChild(InitializeGanttViewEvent<TaskMovedEventArgs>("TaskMoved", true, "Task: {0}\r\nStart Date: {1}", e => ((GanttNode)e.Node).Name, e => e.StartDate));
            editingGroup.AddChild(InitializeGanttViewEvent<TaskMoveCanceledEventArgs>("TaskMoveCanceled", true, "Task: {0}", e => ((GanttNode)e.Node).Name));

            editingGroup.AddChild(InitializeGanttViewEvent<BeginProgressMoveEventArgs>("BeginProgressMove", true, "Task: {0}", e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeGanttViewEvent<ProgressMovingEventArgs>("ProgressMoving", false, "Task: {0}\r\nProgress: {1:p0}", e => ((GanttNode)e.Node).Name, e => e.Progress));
            editingGroup.AddChild(InitializeGanttViewEvent<ProgressMovedEventArgs>("ProgressMoved", true, "Task: {0}\r\nProgress: {1:p0}", e => ((GanttNode)e.Node).Name, e => e.Progress));
            editingGroup.AddChild(InitializeGanttViewEvent<ProgressMoveCanceledEventArgs>("ProgressMoveCanceled", true, "Task: {0}", e => ((GanttNode)e.Node).Name));

            editingGroup.AddChild(InitializeGanttViewEvent<BeginNewTaskDrawEventArgs>("BeginNewTaskDraw", true, "Start Date: {0}", e => e.StartDate));
            editingGroup.AddChild(InitializeGanttViewEvent<NewTaskDrawingEventArgs>("NewTaskDrawing", false, "Start Date: {0}\r\nFinish Date: {1}", e => e.StartDate, e => e.FinishDate));
            editingGroup.AddChild(InitializeGanttViewEvent<NewTaskDrawnEventArgs>("NewTaskDrawn", true, "Start Date: {0}\r\nFinish Date: {1}", e => e.StartDate, e => e.FinishDate));
            editingGroup.AddChild(InitializeGanttViewEvent<NewTaskDrawCanceledEventArgs>("NewTaskDrawCanceled", true, ""));

            editingGroup.AddChild(InitializeGanttViewEvent<BeginPredecessorLinkEditEventArgs>("BeginPredecessorLinkEdit", true, "Predecessor: {0}\r\nSuccessor: {1}", e => ((GanttNode)e.Predecessor).Name, e => e.Successor != null ? ((GanttNode)e.Successor).Name : "None"));
            editingGroup.AddChild(InitializeGanttViewEvent<PredecessorLinkEditedEventArgs>("PredecessorLinkEdited", true, "Predecessor: {0}\r\nOld Successor: {1}\r\nNew Successor: {2}\r\nChange Type: {3}",
                e => ((GanttNode)e.Predecessor).Name,
                e => e.OldSuccessor != null ? ((GanttNode)e.OldSuccessor).Name : "None",
                e => e.Successor != null ? ((GanttNode)e.Successor).Name : "None",
                e => e.ChangeType
            ));
            editingGroup.AddChild(InitializeGanttViewEvent<PredecessorLinkEditCanceledEventArgs>("PredecessorLinkEditCanceled", true, "Predecessor: {0}\r\nSuccessor: {1}", e => ((GanttNode)e.Predecessor).Name, e => e.Successor != null ? ((GanttNode)e.Successor).Name : "None"));

            editingGroup.AddChild(InitializeGanttViewEvent<AddingNewPredecessorLinkEventArgs>("AddingNewPredecessorLink", true, ""));
            editingGroup.AddChild(InitializeGanttViewEvent<AddingNewResourceLinkEventArgs>("AddingNewResourceLink", true, "Task: {0}\r\nResource: {1}", e => ((GanttTask)e.Task).Name, e => ((GanttResource)e.Resource).Name));

            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeChangedEventArgs>("NodeChanged", false, e => !string.IsNullOrEmpty(((GanttNode)e.Node).Name), "Task: {0}\r\nChange Type: {1}", e => ((GanttNode)e.Node).Name, e => e.ChangeType));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeEventArgs>("NodeUpdated", true, "Task: {0}", e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeValidationEventArgs>("ValidateNode", true, "Task: {0}", e => ((GanttNode)e.Node).Name));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListCellValidationEventArgs>("ValidateCell", true, "Task: {0}\r\nColumn: {1}", e => ((GanttNode)e.Node).Name, e => e.Column.Header));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListEditorEventArgs>("ShownEditor", false, "Task: {0}\r\nColumn: {1}", e => ((GanttNode)e.Node).Name, e => e.Column.Header));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListShowingEditorEventArgs>("ShowingEditor", false, "Task: {0}\r\nColumn: {1}", e => ((GanttNode)e.Node).Name, e => e.Column.Header));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListEditorEventArgs>("HiddenEditor", false, "Task: {0}\r\nColumn: {1}", e => ((GanttNode)e.Node).Name, e => e.Column.Header));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListCellValueChangedEventArgs>("CellValueChanged", true, "Task: {0}\r\nColumn: {1}\r\nValue: {2}", e => ((GanttNode)e.Node).Name, e => e.Column.Header, e => ((GanttColumn)e.Column).BindTo == GanttColumnBindingSource.ResourceLinks ? "(Collection)" : e.Value));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListCellValueChangedEventArgs>("CellValueChanging", false, "Task: {0}\r\nColumn: {1}\r\nValue: {2}", e => ((GanttNode)e.Node).Name, e => e.Column.Header, e => e.Value));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListNodeEventArgs> ("InitNewNode", true, ""));
            editingGroup.AddChild(InitializeTreeListViewEvent<TreeListAddingNewEventArgs>("AddingNewNode", true, ""));

            var schedulingGroup = GanttEventNode.CreateGroup("Scheduling", null);
            groups.Add(schedulingGroup);
            schedulingGroup.AddChild(InitializeGanttViewEvent<QueryAllowScheduleTaskEventArgs>("QueryAllowScheduleTask", false, "Task: {0}", e => ((GanttNode)e.Node).Name));
            schedulingGroup.AddChild(InitializeGanttViewEvent<TasksScheduledEventArgs>("TasksScheduled", true, ""));
            schedulingGroup.AddChild(InitializeGanttViewEvent<CalculateSummaryTaskProgressEventArgs>("CalculateSummaryTaskProgress", false, "Task: {0}", e => ((GanttNode)e.Node).Name));
            schedulingGroup.AddChild(InitializeGanttViewEvent<CalculateSummaryTaskBoundsEventArgs>("CalculateSummaryTaskBounds", false, "Task: {0}", e => ((GanttNode)e.Node).Name));

            var otherGroup = GanttEventNode.CreateGroup("Other", null);
            groups.Add(otherGroup);
            otherGroup.AddChild(InitializeDataViewBaseEvent<RoutedEventArgs>("ShownColumnChooser", true, ""));
            otherGroup.AddChild(InitializeDataViewBaseEvent<RoutedEventArgs>("HiddenColumnChooser", true, ""));
            otherGroup.AddChild(InitializeDataViewBaseEvent<GridMenuEventArgs>("ShowGridMenu", true, ""));
            otherGroup.AddChild(InitializeDataViewBaseEvent<ColumnHeaderClickEventArgs>("ColumnHeaderClick", false, "Column: {0}", e => e.Column.Header));
            otherGroup.AddChild(InitializeTreeListViewEvent<RowDoubleClickEventArgs>("RowDoubleClick", false, "Task: {0}", e => ((GanttTask)e.Source.DataControl.GetRow(e.HitInfo.RowHandle)).Name));

            return groups;
        }
        string NameOrDefault(object task) {
            return task != null ? ((GanttTask)task).Name : string.Empty;
        }
        GanttEventNode InitializeGanttViewEvent<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeGenericEvent<TEventArgs, GanttView>(title, isChecked, format, x => x.View, formatArgs);
        }
        GanttEventNode InitializeDataViewBaseGenericEvent<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeGenericEvent<TEventArgs, DataViewBase>(title, isChecked, format, x => x.View, formatArgs);
        }
        GanttEventNode InitializeGenericEvent<TEventArgs, TOwner>(string title, bool isChecked, string format, Func<GanttControl, TOwner> getOwner, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            var className = typeof(TOwner).FullName;
            var node = GanttEventNode.Create(title, className, isChecked, GanttEventNodeKind.EventNode);
            typeof(TOwner).GetEvent(title).AddEventHandler(getOwner(eventsOwner), new EventHandler<TEventArgs>((sender, e) => {
                if(node.ActualIsChecked)
                    addToLog(title, className, format, formatArgs.Select(x => x(e)).ToArray());
            }));
            return node;
        }

        GanttEventNode InitializeEvent<TEventArgs, TOwner>(string title, bool isChecked, string format, Func<GanttControl, TOwner> getOwner, Func<TEventArgs, bool> canLog, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            var className = typeof(TOwner).FullName;
            var node = GanttEventNode.Create(title, className, isChecked, GanttEventNodeKind.EventNode);
            var eventInfo = typeof(TOwner).GetEvent(title);
            Action<object, TEventArgs> handler = new Action<object, TEventArgs>((sender, e) => {
                if(node.ActualIsChecked && canLog(e))
                    addToLog(title, className, format, formatArgs.Select(x => x(e)).ToArray());
            });
            eventInfo.AddEventHandler(getOwner(eventsOwner), Delegate.CreateDelegate(eventInfo.EventHandlerType, handler.Target, handler.Method));
            return node;
        }

        GanttEventNode InitializeTreeListViewEvent<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeEvent<TEventArgs, TreeListView>(title, isChecked, format, x => x.View, x => true, formatArgs);
        }
        GanttEventNode InitializeTreeListViewEvent<TEventArgs>(string title, bool isChecked, Func<TEventArgs, bool> canLog, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeEvent<TEventArgs, TreeListView>(title, isChecked, format, x => x.View, canLog, formatArgs);
        }
        GanttEventNode InitializeTreeListControlEvent<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeEvent<TEventArgs, TreeListControlBase>(title, isChecked, format, x => x, x => true, formatArgs);
        }
        GanttEventNode InitializeDataControlBaseEvent<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeEvent<TEventArgs, DataControlBase>(title, isChecked, format, x => x, x => true, formatArgs);
        }

        GanttEventNode InitializeDataViewBaseEvent<TEventArgs>(string title, bool isChecked, string format, params Func<TEventArgs, object>[] formatArgs) where TEventArgs : EventArgs {
            return InitializeEvent<TEventArgs, DataViewBase>(title, isChecked, format, x => x.View, x => true, formatArgs);
        }
    }
}
