using DevExpress.Mvvm.Gantt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace GanttDemo.Examples {
    public class TaskColorByResourceViewModel {
        public TaskColorByResourceViewModel() {
            #region initialization
            var startDate = DateTime.Now.Date.AddDays(3);
            Tasks = new List<GanttTask> {
                new GanttTask {
                    Id = 0,
                    StartDate = startDate,
                    FinishDate = startDate + TimeSpan.FromDays(5),
                    Name = "Market Analysis",
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 1 } }
                },
                new GanttTask {
                    Id = 1,
                    Name = "Feature Planning",
                    StartDate = startDate + TimeSpan.FromDays(5),
                    FinishDate = startDate + TimeSpan.FromDays(9),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 1 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 0 } }
                },
                new GanttTask {
                    Id = 2,
                    Name = "Feature 1",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(16)
                },
                new GanttTask {
                    Id = 3,
                    ParentId = 2,
                    Name = "Implementation",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(13),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 2 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 1 } }
                },
                new GanttTask {
                    Id = 4,
                    ParentId = 2,
                    Name = "Demos & Docs",
                    StartDate = startDate + TimeSpan.FromDays(13),
                    FinishDate = startDate + TimeSpan.FromDays(16),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 4 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 3 } }
                },
                new GanttTask {
                    Id = 5,
                    Name = "Feature 2",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(17),
                },
                new GanttTask {
                    Id = 6,
                    ParentId = 5,
                    Name = "Implementation",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(13),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 2 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 1 } }
                },
                new GanttTask {
                    Id = 7,
                    ParentId = 5,
                    Name = "Demos & Docs",
                    StartDate = startDate + TimeSpan.FromDays(13),
                    FinishDate = startDate + TimeSpan.FromDays(17),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 4 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 6 } }
                },
                new GanttTask {
                    Id = 8,
                    Name = "Feature 3",
                    StartDate = startDate + TimeSpan.FromDays(16),
                    FinishDate = startDate + TimeSpan.FromDays(19),
                },
                new GanttTask {
                    Id = 9,
                    ParentId = 8,
                    Name = "Implementation",
                    StartDate = startDate + TimeSpan.FromDays(16),
                    FinishDate = startDate + TimeSpan.FromDays(18),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 2 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 4 } }
                },
                new GanttTask {
                    Id = 10,
                    ParentId = 8,
                    Name = "Demos & Docs",
                    StartDate = startDate + TimeSpan.FromDays(18),
                    FinishDate = startDate + TimeSpan.FromDays(19),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 4 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 9 } }
                },
                new GanttTask {
                    Id = 11,
                    Name = "Testing & Bug Fixing",
                    StartDate = startDate + TimeSpan.FromDays(19),
                    FinishDate = startDate + TimeSpan.FromDays(23),
                    ResourceLinks = new ObservableCollection<GanttResourceLink>() { new GanttResourceLink() { ResourceId = 3 } },
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() {
                        new GanttPredecessorLink() { PredecessorTaskId = 7 },
                        new GanttPredecessorLink() { PredecessorTaskId = 10 }
                    },
                },
                new GanttTask {
                    Id = 12,
                    Name = "Development finished",
                    StartDate = startDate + TimeSpan.FromDays(23),
                    FinishDate = startDate + TimeSpan.FromDays(23),
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink>() { new GanttPredecessorLink() { PredecessorTaskId = 11 } }
                },
            };

            Resources = new List<GanttResource> {
                new GanttResource { Name = "Management", Id = 1, Color = Colors.Green },
                new GanttResource { Name = "Developers", Id = 2, Color = Colors.Red },
                new GanttResource { Name = "Testers", Id = 3, Color = Colors.Purple },
                new GanttResource { Name = "Technical Writers", Id = 4, Color = Colors.Navy }
            };
            #endregion
        }

        public IEnumerable<GanttTask> Tasks { get; private set; }
        public IEnumerable<GanttResource> Resources { get; private set; }
    }
}
