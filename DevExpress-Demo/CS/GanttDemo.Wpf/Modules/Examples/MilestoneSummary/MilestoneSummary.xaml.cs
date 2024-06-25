using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using DevExpress.Mvvm.Gantt;

namespace GanttDemo.Examples {
    public partial class MilestoneSummary : UserControl {
        public MilestoneSummary() {
            #region Initialization
            InitializeComponent();
            var projectStartDate = DateTime.Now.Date.AddDays(3);
            var tasks = new List<GanttTask> {
                new GanttTask {
                    Id = 1,
                    Name = "Finalize the work"
                },
                new GanttTask {
                    Id = 2,
                    ParentId = 1,
                    Name = "Beta testing",
                    StartDate = projectStartDate,
                    FinishDate = projectStartDate + TimeSpan.FromDays(5),
                },
                new GanttTask {
                    Id = 3,
                    ParentId = 1,
                    Name = "Create a demo",
                    StartDate = projectStartDate,
                    FinishDate = projectStartDate + TimeSpan.FromDays(5),
                },
            };
            #endregion
            gantt.View.CalculateSummaryTaskBounds += (sender, e) => {
                var childTasks = e.Node.Nodes.Select(x => (GanttTask)x.Content).Where(x => x.StartDate.HasValue);
                e.StartDate = childTasks.Aggregate(DateTime.MaxValue, (startDate, task) => startDate < task.StartDate.Value ? startDate : task.StartDate.Value);
                e.FinishDate = e.StartDate;
                e.Handled = true;
            };
            #region Scheduling
            gantt.View.AllowRecreateNodesOnEndDataUpdate = false;
            gantt.BeginDataUpdate();
            gantt.ItemsSource = tasks;
            gantt.View.ScheduleAll();
            gantt.EndDataUpdate();
            #endregion
        }
    }
}
