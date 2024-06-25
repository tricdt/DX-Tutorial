using System;
using System.Collections.Generic;

namespace GanttDemo.Examples {
    public class BindResourcesWithoutLinksViewModel {
        public BindResourcesWithoutLinksViewModel() {
            #region initialization
            var startDate = DateTime.Now.Date.AddDays(3);
            Tasks = new List<GanttDataItemWithResources> {
                new GanttDataItemWithResources {
                    StartDate = startDate,
                    FinishDate = startDate + TimeSpan.FromDays(5),
                    Name = "Market Analysis",
                    ResourceIds = new List<int>() { 1 }
                },
                new GanttDataItemWithResources {
                    Name = "Feature Planning",
                    StartDate = startDate + TimeSpan.FromDays(5),
                    FinishDate = startDate + TimeSpan.FromDays(9),
                    ResourceIds = new List<int>() { 1, 2 }
                },
                new GanttDataItemWithResources {
                    Name = "Feature 1",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(16),
                    Children = new[] {
                        new GanttDataItemWithResources {
                            Name = "Implementation",
                            StartDate = startDate + TimeSpan.FromDays(9),
                            FinishDate = startDate + TimeSpan.FromDays(13),
                            ResourceIds = new List<int>() { 2 }
                        },
                        new GanttDataItemWithResources {
                            Name = "Demos & Docs",
                            StartDate = startDate + TimeSpan.FromDays(13),
                            FinishDate = startDate + TimeSpan.FromDays(16),
                            ResourceIds = new List<int>() { 4, 2 }
                        }
                    },
                },
                new GanttDataItemWithResources {
                    Name = "Feature 2",
                    StartDate = startDate + TimeSpan.FromDays(9),
                    FinishDate = startDate + TimeSpan.FromDays(17),
                    Children = new[] {
                        new GanttDataItemWithResources {
                            Name = "Implementation",
                            StartDate = startDate + TimeSpan.FromDays(9),
                            FinishDate = startDate + TimeSpan.FromDays(13),
                            ResourceIds = new List<int>() { 2 }
                        },
                        new GanttDataItemWithResources {
                            Name = "Demos & Docs",
                            StartDate = startDate + TimeSpan.FromDays(13),
                            FinishDate = startDate + TimeSpan.FromDays(17),
                            ResourceIds = new List<int>() { 4, 2 }
                        }
                    },
                },
                new GanttDataItemWithResources {
                    Name = "Feature 3",
                    StartDate = startDate + TimeSpan.FromDays(16),
                    FinishDate = startDate + TimeSpan.FromDays(19),
                    Children = new[] {
                        new GanttDataItemWithResources {
                            Name = "Implementation",
                            StartDate = startDate + TimeSpan.FromDays(16),
                            FinishDate = startDate + TimeSpan.FromDays(18),
                            ResourceIds = new List<int>() { 2 }
                        },
                        new GanttDataItemWithResources {
                            Id = 10,
                            Name = "Demos & Docs",
                            StartDate = startDate + TimeSpan.FromDays(18),
                            FinishDate = startDate + TimeSpan.FromDays(19),
                            ResourceIds = new List<int>() { 4, 2 }
                        }
                    },
                },
                new GanttDataItemWithResources {
                    Name = "Testing & Bug Fixing",
                    StartDate = startDate + TimeSpan.FromDays(19),
                    FinishDate = startDate + TimeSpan.FromDays(23),
                    ResourceIds = new List<int>() { 3, 2 }
                },
                new GanttDataItemWithResources {
                    Name = "Development finished",
                    StartDate = startDate + TimeSpan.FromDays(23),
                    FinishDate = startDate + TimeSpan.FromDays(23)
                },
            };

            Resources = new List<GanttResourceItem> {
                new GanttResourceItem { Name = "Management", Id = 1 },
                new GanttResourceItem { Name = "Developers", Id = 2 },
                new GanttResourceItem { Name = "Testers", Id = 3 },
                new GanttResourceItem { Name = "Technical Writers", Id = 4 }
            };
            #endregion
        }

        public IEnumerable<GanttDataItemWithResources> Tasks { get; private set; }
        public IEnumerable<GanttResourceItem> Resources { get; private set; }
    }

    public class GanttDataItemWithResources : GanttDataItem {
        public List<int> ResourceIds { get; set; }
    }

    public class GanttResourceItem {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
