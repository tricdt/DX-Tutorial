using System;
using System.Collections.Generic;

namespace GanttDemo.Examples {
    public class BindToSelfReferenceDataViewModel {
        public BindToSelfReferenceDataViewModel() {
            #region initialization
            var startDate = DateTime.Now.Date.AddDays(3);
            Tasks = new List<GanttTaskItem> {
                new GanttTaskItem {
                    Id = 0,
                    StartDate = startDate,
                    FinishDate = startDate + TimeSpan.FromDays(5),
                    Progress = 1,
                    Name = "Market Analysis",
                },
                new GanttTaskItem {
                    Id = 1,
                    Name = "Feature Planning",
                    StartDate = startDate + TimeSpan.FromDays(5),
                    FinishDate = startDate + TimeSpan.FromDays(9),
                    Progress = 1,
                    Links = new[] { new GanttTaskLink(0) }
                },
                new GanttTaskItem {
                    Id = 2,
                    Name = "Feature 1",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(16),
                },
                new GanttTaskItem {
                    Id = 3,
                    ParentId = 2,
                    Name = "Implementation",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(13),
                    Progress = 1,
                    Links = new[] { new GanttTaskLink(1), }
                },
                new GanttTaskItem {
                    Id = 4,
                    ParentId = 2,
                    Name = "Demos & Docs",
                    StartDate = startDate + TimeSpan.FromDays(13),
                    FinishDate = startDate + TimeSpan.FromDays(16),
                    Progress = 1,
                    Links = new[] { new GanttTaskLink(3), }
                },
                new GanttTaskItem {
                    Id = 5,
                    Name = "Feature 2",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(17),
                    Progress = 0.85,
                },
                new GanttTaskItem {
                    Id = 6,
                    ParentId = 5,
                    Name = "Implementation",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(13),
                    Progress = 1,
                    Links = new[] { new GanttTaskLink(1), }
                },
                new GanttTaskItem {
                    Id = 7,
                    ParentId = 5,
                    Name = "Demos & Docs",
                    StartDate = startDate + TimeSpan.FromDays(13),
                    FinishDate = startDate + TimeSpan.FromDays(17),
                    Progress = 0.7,
                    Links = new[] { new GanttTaskLink(6), }
                },
                new GanttTaskItem {
                    Id = 8,
                    Name = "Feature 3",
                    StartDate = startDate + TimeSpan.FromDays(16),
                    FinishDate = startDate + TimeSpan.FromDays(19),
                    Progress = 0.4,
                },
                new GanttTaskItem {
                    Id = 9,
                    ParentId = 8,
                    Name = "Implementation",
                    StartDate = startDate + TimeSpan.FromDays(16),
                    FinishDate = startDate + TimeSpan.FromDays(18),
                    Progress = 0.8,
                    Links = new[] { new GanttTaskLink(4), }
                },
                new GanttTaskItem {
                    Id = 10,
                    ParentId = 8,
                    Name = "Demos & Docs",
                    StartDate = startDate + TimeSpan.FromDays(18),
                    FinishDate = startDate + TimeSpan.FromDays(19),
                    Progress = 0,
                    Links = new[] { new GanttTaskLink(9), }
                },
                new GanttTaskItem {
                    Id = 11,
                    Name = "Testing & Bug Fixing",
                    StartDate =startDate + TimeSpan.FromDays(19),
                    FinishDate = startDate + TimeSpan.FromDays(23),
                    Links = new [] { new GanttTaskLink(7), new GanttTaskLink(10) }
                },
                new GanttTaskItem {
                    Id = 12,
                    Name = "Development finished",
                    StartDate = startDate + TimeSpan.FromDays(23),
                    FinishDate = startDate + TimeSpan.FromDays(23),
                    Links = new[] { new GanttTaskLink(11) }
                },
            };
            #endregion
        }

        public IEnumerable<GanttTaskItem> Tasks { get; private set; }
    }

    public class GanttTaskItem {
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public double Progress { get; set; }
        public IEnumerable<GanttTaskLink> Links { get; set; }
    }

    public class GanttTaskLink {
        public GanttTaskLink(int predecessorId) {
            PredecessorId = predecessorId;
        }
        public int PredecessorId { get; private set; }
    }
}
