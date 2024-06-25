using System;
using System.Collections.Generic;

namespace GanttDemo.Examples {
    public class BindToDataViewModel {
        public BindToDataViewModel() {
            #region initialization
            var startDate = DateTime.Now.Date.AddDays(3);
            Tasks = new List<GanttDataItem> {
                new GanttDataItem {
                    Id = 0,
                    StartDate = startDate,
                    FinishDate = startDate + TimeSpan.FromDays(5),
                    Progress = 1,
                    Name = "Market Analysis"
                },
                new GanttDataItem {
                    Id = 1,
                    Name = "Feature Planning",
                    StartDate = startDate + TimeSpan.FromDays(5),
                    FinishDate = startDate + TimeSpan.FromDays(9),
                    Progress = 1,
                },
                new GanttDataItem {
                    Id = 2,
                    Name = "Feature 1",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(16),
                    Children = new[] {
                        new GanttDataItem {
                            Id = 3,
                            Name = "Implementation",
                            StartDate = startDate + TimeSpan.FromDays(9),
                            FinishDate = startDate + TimeSpan.FromDays(13),
                            Progress = 1,
                        },
                        new GanttDataItem {
                            Id = 4,
                            Name = "Demos & Docs",
                            StartDate = startDate + TimeSpan.FromDays(13),
                            FinishDate = startDate + TimeSpan.FromDays(16),
                            Progress = 1,
                        }
                    },
                },
                new GanttDataItem {
                    Id = 5,
                    Name = "Feature 2",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(17),
                    Progress = 0.85,
                    Children = new[] {
                        new GanttDataItem {
                            Id = 6,
                            Name = "Implementation",
                            StartDate = startDate + TimeSpan.FromDays(9),
                            FinishDate = startDate + TimeSpan.FromDays(13),
                            Progress = 1,
                        },
                        new GanttDataItem {
                            Id = 7,
                            Name = "Demos & Docs",
                            StartDate = startDate + TimeSpan.FromDays(13),
                            FinishDate = startDate + TimeSpan.FromDays(17),
                            Progress = 0.7,
                        }
                    },
                },
                new GanttDataItem {
                    Id = 8,
                    Name = "Feature 3",
                    StartDate = startDate + TimeSpan.FromDays(16),
                    FinishDate = startDate + TimeSpan.FromDays(19),
                    Progress = 0.4,
                    Children = new[] {
                        new GanttDataItem {
                            Id = 9,
                            Name = "Implementation",
                            StartDate = startDate + TimeSpan.FromDays(16),
                            FinishDate = startDate + TimeSpan.FromDays(18),
                            Progress = 0.8,
                        },
                        new GanttDataItem {
                            Id = 10,
                            Name = "Demos & Docs",
                            StartDate = startDate + TimeSpan.FromDays(18),
                            FinishDate = startDate + TimeSpan.FromDays(19),
                            Progress = 0,
                        }
                    },
                },
                new GanttDataItem { Id = 11, Name = "Testing & Bug Fixing", StartDate = startDate + TimeSpan.FromDays(19), FinishDate = startDate + TimeSpan.FromDays(23) },
                new GanttDataItem { Id = 12, Name = "Development finished", StartDate = startDate + TimeSpan.FromDays(23), FinishDate = startDate + TimeSpan.FromDays(23) },
            };
            Links = new List<GanttDataLink> {
                new GanttDataLink(0, 1), new GanttDataLink(1, 3), new GanttDataLink(3, 4), new GanttDataLink(4, 9), new GanttDataLink(9, 10),
                new GanttDataLink(10, 11), new GanttDataLink(1, 6), new GanttDataLink(6, 7), new GanttDataLink(7, 11), new GanttDataLink(11, 12)
            };
            #endregion
        }

        public IEnumerable<GanttDataItem> Tasks { get; private set; }
        public IEnumerable<GanttDataLink> Links { get; private set; }
    }

    public class GanttDataItem {
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Progress { get; set; }
        public IEnumerable<GanttDataItem> Children { get; set; }
    }

    public class GanttDataLink {
        public GanttDataLink(int sourceId, int targetId) {
            SourceTaskId = sourceId;
            TargetTaskId = targetId;
        }
        public int SourceTaskId { get; private set; }
        public int TargetTaskId { get; private set; }
    }
}
