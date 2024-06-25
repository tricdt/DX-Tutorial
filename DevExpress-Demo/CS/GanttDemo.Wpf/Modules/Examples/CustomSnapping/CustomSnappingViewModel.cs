using DevExpress.Mvvm.Gantt;
using System;
using System.Collections.Generic;

namespace GanttDemo.Examples {
    public class CustomSnappingViewModel {
        public CustomSnappingViewModel() {
            #region initialization
            var startDate = DateTime.Today;
            Tasks = new List<GanttTask> {
                new GanttTask {
                    StartDate = startDate,
                    FinishDate = startDate.AddHours(5).AddMinutes(45),
                    Name = "Night Light"
                },
                new GanttTask {
                    StartDate = startDate.AddHours(6).AddMinutes(45),
                    FinishDate = startDate.AddHours(7),
                    Name = "Teapot"
                },
                new GanttTask {
                    StartDate = startDate.AddHours(8),
                    FinishDate = startDate.AddHours(12),
                    Name = "PC"
                },
                new GanttTask {
                    StartDate = startDate.AddHours(13),
                    FinishDate = startDate.AddHours(17),
                    Name = "Notebook"
                },
                new GanttTask {
                    StartDate = startDate.AddHours(18),
                    FinishDate = startDate.AddHours(22),
                    Name = "TV"
                },
                new GanttTask {
                    StartDate = startDate.AddHours(19),
                    FinishDate = startDate.AddDays(1),
                    Name = "Light"
                }
            };
            #endregion

        }

        public IEnumerable<GanttTask> Tasks { get; private set; }
        public static TimeSpan SnapUnit { get { return TimeSpan.FromMinutes(15); } }
    }
}
