using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Gantt;
using DevExpress.Utils;
using DevExpress.Xpf.Gantt;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace GanttDemo.Examples {
    public partial class EditLimits : UserControl {
        public EditLimits() {
            InitializeComponent();
        }
    }

    [POCOViewModel]
    public class EditLimitsViewModel {
        public EditLimitsViewModel() {
            #region initialization
            var startDate = DateTime.Today;
            Tasks = new List<LimitedGanttTask> {
                new LimitedGanttTask {
                    StartDate = startDate,
                    FinishDate = startDate.AddDays(1),
                    Name = "Task 1",
                    GreenRange = new DateTimeRange(startDate.AddDays(-2), startDate.AddDays(3)),
                    YellowRange = new DateTimeRange(startDate.AddDays(-4), startDate.AddDays(5)),
                    RedRange = new DateTimeRange(startDate.AddDays(-5), startDate.AddDays(6))
                },
                new LimitedGanttTask {
                    StartDate = startDate.AddDays(1),
                    FinishDate = startDate.AddDays(2),
                    Name = "Task 2",
                    GreenRange = new DateTimeRange(startDate.AddDays(-1), startDate.AddDays(4)),
                    YellowRange = new DateTimeRange(startDate.AddDays(-3), startDate.AddDays(6)),
                    RedRange = new DateTimeRange(startDate.AddDays(-4), startDate.AddDays(7))
                },
                new LimitedGanttTask {
                    StartDate = startDate.AddDays(2),
                    FinishDate = startDate.AddDays(3),
                    Name = "Task 3",
                    GreenRange = new DateTimeRange(startDate, startDate.AddDays(5)),
                    YellowRange = new DateTimeRange(startDate.AddDays(-2), startDate.AddDays(7)),
                    RedRange = new DateTimeRange(startDate.AddDays(-3), startDate.AddDays(8))
                },
            };
            #endregion
        }

        public IEnumerable<LimitedGanttTask> Tasks { get; private set; }
        public virtual IEnumerable<TaskLimit> Limits { get; set; }

        public virtual void UpdateLimits(LimitedGanttTask selectedTask) {
            Limits = selectedTask == null ? null : new List<TaskLimit> {
                new TaskLimit(selectedTask.GreenRange.Start, Colors.Lime),
                new TaskLimit(selectedTask.GreenRange.End, Colors.Lime),
                new TaskLimit(selectedTask.YellowRange.Start, Colors.Yellow),
                new TaskLimit(selectedTask.YellowRange.End, Colors.Yellow),
                new TaskLimit(selectedTask.RedRange.Start, Colors.Red),
                new TaskLimit(selectedTask.RedRange.End, Colors.Red),
            };
        }
        public DateTime StartDateChanging(DateTime startDate, GanttView view, GanttNode node) {
            return ((LimitedGanttTask)node.Content).StartDateChanging(startDate, view, node.Duration);
        }
        public DateTime StartDateChanged(DateTime startDate, GanttView view, GanttNode node) {
            UpdateLimits(null);
            return ((LimitedGanttTask)node.Content).StartDateChanged(startDate, view, node.Duration);
        }
    }

    public enum TaskState { Red, Yellow, Green };

    public class TaskLimit : ImmutableObject {
        readonly DateTime limit;
        public DateTime Limit { get { return limit; } }
        readonly Color color;
        public Color Color { get { return color; } }

        public TaskLimit(DateTime limit, Color color) {
            this.limit = limit;
            this.color = color;
        }
    }
    #region !
    public class LimitedGanttTask : GanttTask {
        TaskState editState = TaskState.Green;
        public TaskState EditState {
            get { return editState; }
            set {
                if(editState != value) {
                    editState = value;
                    RaisePropertyChanged("EditState");
                }
            }
        }
        TaskState state = TaskState.Green;
        public TaskState State {
            get { return state; }
            set {
                if(state != value) {
                    state = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        public DateTimeRange GreenRange { get; set; }
        public DateTimeRange YellowRange { get; set; }
        public DateTimeRange RedRange { get; set; }

        static DateTime Max(DateTime value1, DateTime value2) {
            return value1 > value2 ? value1 : value2;
        }
        static DateTime Min(DateTime value1, DateTime value2) {
            return value1 < value2 ? value1 : value2;
        }
        TaskState GetTaskState(DateTimeRange range) {
            var rangeStates = new Dictionary<DateTimeRange, TaskState>() {
                { GreenRange, TaskState.Green },
                { YellowRange, TaskState.Yellow },
                { RedRange, TaskState.Red }
            };
            foreach(var rangeState in rangeStates) {
                if(rangeState.Key.Contains(range))
                    return rangeState.Value;
            }
            return TaskState.Red;
        }
        DateTime UpdateState(DateTime startDate, GanttView view, TimeSpan duration, Action<TaskState> setState) {
            var finishDate = view.CalculateFinishDate(startDate, duration);
            var range = new DateTimeRange(startDate, finishDate);
            setState(GetTaskState(range));
            return RedRange.Contains(range) ? startDate : Min(view.CalculateStartDate(RedRange.End, duration), Max(startDate, RedRange.Start));
        }

        public DateTime StartDateChanging(DateTime startDate, GanttView view, TimeSpan duration) {
            return UpdateState(startDate, view, duration, x => EditState = x);
        }
        public DateTime StartDateChanged(DateTime startDate, GanttView view, TimeSpan duration) {
            return UpdateState(startDate, view, duration, x => State = x);
        }
    }
    #endregion
}
