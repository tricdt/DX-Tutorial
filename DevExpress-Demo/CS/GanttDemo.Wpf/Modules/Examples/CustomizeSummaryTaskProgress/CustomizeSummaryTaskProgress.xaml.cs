using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using DevExpress.Mvvm.Gantt;

namespace GanttDemo.Examples {
    public partial class CustomizeSummaryTaskProgress : UserControl {
        public CustomizeSummaryTaskProgress() {
            #region Initialization
            InitializeComponent();
            var startDate = DateTime.Now.Date.AddDays(3);
            var tasks = new List<GanttTask> {
                new GanttTask {
                    Id = 2,
                    Name = "Feature 1 (implementation - 80%, demos & docs - 20%)"
                },
                new GanttTask {
                    Id = 3,
                    ParentId = 2,
                    Name = "Implementation (80%)",
                    StartDate = startDate,
                    FinishDate = startDate + TimeSpan.FromDays(4),
                    Progress = 0.8,
                    Tag = 0.8,
                },
                new GanttTask {
                    Id = 4,
                    ParentId = 2,
                    Name = "Demos & Docs (20%)",
                    StartDate = startDate + TimeSpan.FromDays(4),
                    FinishDate = startDate + TimeSpan.FromDays(7),
                    Progress = 0.2,
                    Tag = 0.2
                },
                new GanttTask {
                    Id = 5,
                    Name = "Feature 2 (implementation - 30%, demos & docs - 70%)",
                },
                new GanttTask {
                    Id = 6,
                    ParentId = 5,
                    Name = "Implementation (30%)",
                    StartDate = startDate,
                    FinishDate = startDate + TimeSpan.FromDays(4),
                    Progress = 0.8,
                    Tag = 0.3
                },
                new GanttTask {
                    Id = 7,
                    ParentId = 5,
                    Name = "Demos & Docs (70%)",
                    StartDate = startDate + TimeSpan.FromDays(4),
                    FinishDate = startDate + TimeSpan.FromDays(7),
                    Progress = 0.2,
                    Tag = 0.7
                },
            };
            #endregion
            gantt.View.CalculateSummaryTaskProgress += (sender, e) => {
                e.Progress = 0.0;
                foreach(var task in e.Node.Nodes.Select(x => (GanttTask)x.Content)) {
                    var taskWeight = (double)task.Tag;
                    e.Progress += taskWeight * task.Progress;
                }
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
