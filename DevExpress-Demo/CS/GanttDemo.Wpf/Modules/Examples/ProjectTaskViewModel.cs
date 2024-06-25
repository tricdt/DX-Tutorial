using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Gantt;

namespace GanttDemo.Examples {
    public class ProjectTaskViewModel {
        public ObservableCollection<GanttTask> Tasks { get; set; }

        public ProjectTaskViewModel() {
            var now = DateTime.Today.AddHours(9);
            Tasks = new ObservableCollection<GanttTask> {
                new GanttTask() {
                    Id = 0,
                    Name = "Add a new feature",
                    Progress = .65,
                    StartDate = now.AddDays(-1),
                    FinishDate = now.AddDays(6)
                },
                new GanttTask() {
                    Id =1,
                    ParentId = 0,
                    Name = "Write the code",
                    Progress = 1,
                    StartDate = now.AddDays(-1),
                    FinishDate = now.AddDays(2)
                },
                new GanttTask() {
                    Id = 2,
                    ParentId = 0,
                    Name = "Write the docs",
                    Progress = .3,
                    StartDate = now.AddDays(2),
                    FinishDate = now.AddDays(5),
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink> {
                        new GanttPredecessorLink() { PredecessorTaskId = 1, LinkType = PredecessorLinkType.FinishToStart }
                    }
                },
                new GanttTask() {
                    Id = 3,
                    ParentId = 0,
                    Name = "Test the new feature",
                    Progress = .6,
                    StartDate = now.AddDays(2),
                    FinishDate = now.AddDays(5),
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink> {
                        new GanttPredecessorLink() { PredecessorTaskId = 1, LinkType = PredecessorLinkType.FinishToStart }
                    }
                },
                new GanttTask() {
                    Id = 4,
                    ParentId = 0,
                    Name = "Release the new feature",
                    StartDate = now.AddDays(5),
                    FinishDate = now.AddDays(6),
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink> {
                        new GanttPredecessorLink { PredecessorTaskId = 2, LinkType = PredecessorLinkType.FinishToStart },
                        new GanttPredecessorLink { PredecessorTaskId = 3, LinkType = PredecessorLinkType.FinishToStart },
                    }
                },
                new GanttTask {
                    Id = 5,
                    ParentId = 0,
                    Name = "Feature is released",
                    StartDate = now.AddDays(6),
                    FinishDate = now.AddDays(6),
                    PredecessorLinks = new ObservableCollection<GanttPredecessorLink> {
                        new GanttPredecessorLink { PredecessorTaskId = 4 }
                    }
                }
            };
        }
    }
}
