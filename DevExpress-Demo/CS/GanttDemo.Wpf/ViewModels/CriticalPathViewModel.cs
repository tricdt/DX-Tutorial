using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Gantt;
using DevExpress.Xpf.Gantt;
using System;
using System.Collections.Generic;

namespace GanttDemo {
    [POCOViewModel]
    public class CriticalPathViewModel : BindableBase {
        public List<GanttTask> SingleSource { get; private set; }
        public List<GanttTask> MultipleSource { get; private set; }
        public virtual DateTime FirstVisibleDate { get; set; }

        CriticalPathHighlightMode criticalPathHighlightMode = CriticalPathHighlightMode.Single;
        public virtual CriticalPathHighlightMode CriticalPathHighlightMode {
            get { return criticalPathHighlightMode; }
            set {
                if(criticalPathHighlightMode.Equals(value))
                    return;
                var firstVisibleDate = FirstVisibleDate;
                criticalPathHighlightMode = value;
                RaisePropertyChanged(() => CriticalPathHighlightMode);
                FirstVisibleDate = firstVisibleDate;
            }
        }

        public CriticalPathViewModel() {
            SingleSource = CreateSingleSource();
            MultipleSource = CreateMultipleSource();
            FirstVisibleDate = new DateTime(2018, 12, 3);
        }
        List<GanttTask> CreateSingleSource() {
            var tasks = CreateTasksSource();
            tasks[3].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 2 });
            tasks[4].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 3 });
            tasks[5].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 4 });
            tasks[6].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 5 });
            tasks[8].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 6 });
            tasks[9].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 8 });
            tasks[10].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 9 });
            tasks[11].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 10 });
            tasks[12].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 11 });
            tasks[13].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 12 });
            tasks[14].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 13 });
            tasks[15].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 14 });
            tasks[16].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 15 });
            tasks[18].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 16 });
            tasks[19].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 18 });
            tasks[20].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 19 });
            tasks[21].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 20 });
            tasks[22].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 21 });
            tasks[23].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 22 });
            tasks[24].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 23 });
            tasks[26].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[27].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 26 });
            tasks[28].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 27 });
            tasks[29].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 28 });
            tasks[30].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 29 });
            tasks[31].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 30 });
            tasks[33].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[34].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[36].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 33 });
            tasks[36].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 31 });
            tasks[37].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 31 });
            tasks[37].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 36 });
            tasks[38].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 37 });
            tasks[39].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 38 });
            tasks[40].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 39 });
            tasks[41].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 40 });
            tasks[43].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 41 });
            tasks[44].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 43 });
            tasks[45].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 44 });
            tasks[46].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 45 });
            tasks[47].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 46 });
            tasks[49].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[50].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[51].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 49 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 31 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 50 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 51 });
            tasks[53].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 52 });
            tasks[54].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 53 });
            tasks[55].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 54 });
            tasks[56].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 55 });
            tasks[58].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[59].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 58 });
            tasks[59].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 29 });
            tasks[60].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 59 });
            tasks[61].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 60 });
            tasks[62].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 24 });
            tasks[63].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 62 });
            tasks[63].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 29 });
            tasks[64].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 63 });
            tasks[65].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 64 });
            tasks[66].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 65 });
            tasks[66].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 61 });
            tasks[68].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 16 });
            tasks[69].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 68 });
            tasks[70].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 47 });
            tasks[70].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 69 });
            tasks[70].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 66 });
            tasks[70].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 56 });
            tasks[71].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 70 });
            tasks[72].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 71 });
            tasks[73].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 72 });
            tasks[75].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 73 });
            tasks[76].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 75 });
            tasks[77].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 76 });
            tasks[78].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 77 });
            tasks[79].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 78 });
            tasks[80].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 79 });
            tasks[82].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 80 });
            tasks[83].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 82 });
            tasks[84].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 83 });
            tasks[85].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 84 });
            tasks[86].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 85 });
            return tasks;
        }

        List<GanttTask> CreateMultipleSource() {
            var tasks = CreateTasksSource();
            tasks[3].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 2 });
            tasks[4].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 3 });
            tasks[5].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 4 });
            tasks[6].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 5 });
            tasks[9].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 8 });
            tasks[10].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 9 });
            tasks[11].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 10 });
            tasks[12].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 11 });
            tasks[13].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 12 });
            tasks[14].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 13 });
            tasks[15].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 14 });
            tasks[16].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 15 });
            tasks[19].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 18 });
            tasks[20].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 19 });
            tasks[21].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 20 });
            tasks[22].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 21 });
            tasks[23].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 22 });
            tasks[24].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 23 });
            tasks[27].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 26 });
            tasks[28].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 27 });
            tasks[29].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 28 });
            tasks[30].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 29 });
            tasks[31].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 30 });
            tasks[36].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 33 });
            tasks[36].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 34 });
            tasks[37].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 36 });
            tasks[38].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 37 });
            tasks[39].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 38 });
            tasks[40].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 39 });
            tasks[41].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 40 });
            tasks[43].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 41 });
            tasks[44].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 43 });
            tasks[45].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 44 });
            tasks[46].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 45 });
            tasks[47].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 46 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 49 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 50 });
            tasks[52].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 51 });
            tasks[53].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 52 });
            tasks[54].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 53 });
            tasks[55].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 54 });
            tasks[56].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 55 });
            tasks[59].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 58 });
            tasks[60].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 59 });
            tasks[61].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 60 });
            tasks[63].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 62 });
            tasks[64].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 63 });
            tasks[65].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 64 });
            tasks[66].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 65 });
            tasks[66].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 61 });
            tasks[69].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 68 });
            tasks[70].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 69 });
            tasks[71].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 70 });
            tasks[72].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 71 });
            tasks[73].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 72 });
            tasks[76].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 75 });
            tasks[77].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 76 });
            tasks[78].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 77 });
            tasks[79].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 78 });
            tasks[80].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 79 });
            tasks[83].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 82 });
            tasks[84].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 83 });
            tasks[85].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 84 });
            return tasks;
        }

        List<GanttTask> CreateTasksSource() {
            return new List<GanttTask>() {
                new GanttTask { Id = 0, Name = "Software Development", StartDate = new DateTime(2018, 10, 25, 8, 0, 0), FinishDate = new DateTime(2019, 3, 7, 15, 0, 0) },
                new GanttTask { Id = 1, ParentId = 0, Name = "Scope", StartDate = new DateTime(2018, 10, 25, 8, 0, 0), FinishDate = new DateTime(2018, 10, 30, 12, 0, 0) },
                new GanttTask { Id = 2, ParentId = 1, Name = "Determine project scope", StartDate = new DateTime(2018, 10, 25, 8, 0, 0), FinishDate = new DateTime(2018, 10, 25, 12, 0, 0) },
                new GanttTask { Id = 3, ParentId = 1, Name = "Secure project sponsorship", StartDate = new DateTime(2018, 10, 25, 13, 0, 0), FinishDate = new DateTime(2018, 10, 26, 12, 0, 0) },
                new GanttTask { Id = 4, ParentId = 1, Name = "Define preliminary resources", StartDate = new DateTime(2018, 10, 26, 13, 0, 0), FinishDate = new DateTime(2018, 10, 29, 12, 0, 0) },
                new GanttTask { Id = 5, ParentId = 1, Name = "Secure core resources", StartDate = new DateTime(2018, 10, 29, 13, 0, 0), FinishDate = new DateTime(2018, 10, 30, 12, 0, 0) },
                new GanttTask { Id = 6, ParentId = 1, Name = "Scope complete", StartDate = new DateTime(2018, 10, 30, 12, 0, 0), FinishDate = new DateTime(2018, 10, 30, 12, 0, 0) },
                new GanttTask { Id = 7, ParentId = 0, Name = "Analysis/Software Requirements", StartDate = new DateTime(2018, 10, 30, 13, 0, 0), FinishDate = new DateTime(2018, 11, 19, 12, 0, 0) },
                new GanttTask { Id = 8, ParentId = 7, Name = "Conduct needs analysis", StartDate = new DateTime(2018, 10, 30, 13, 0, 0), FinishDate = new DateTime(2018, 11, 6, 12, 0, 0) },
                new GanttTask { Id = 9, ParentId = 7, Name = "Draft preliminary software specifications", StartDate = new DateTime(2018, 11, 6, 13, 0, 0), FinishDate = new DateTime(2018, 11, 9, 12, 0, 0) },
                new GanttTask { Id = 10, ParentId = 7, Name = "Develop preliminary budget", StartDate = new DateTime(2018, 11, 9, 13, 0, 0), FinishDate = new DateTime(2018, 11, 13, 12, 0, 0) },
                new GanttTask { Id = 11, ParentId = 7, Name = "Review software specifications/budget with team", StartDate = new DateTime(2018, 11, 13, 13, 0, 0), FinishDate = new DateTime(2018, 11, 13, 17, 0, 0) },
                new GanttTask { Id = 12, ParentId = 7, Name = "Incorporate feedback on software specifications", StartDate = new DateTime(2018, 11, 14, 8, 0, 0), FinishDate = new DateTime(2018, 11, 14, 17, 0, 0) },
                new GanttTask { Id = 13, ParentId = 7, Name = "Develop delivery timeline", StartDate = new DateTime(2018, 11, 15, 8, 0, 0), FinishDate = new DateTime(2018, 11, 15, 17, 0, 0) },
                new GanttTask { Id = 14, ParentId = 7, Name = "Obtain approvals to proceed (concept, timeline, budget)", StartDate = new DateTime(2018, 11, 16, 8, 0, 0), FinishDate = new DateTime(2018, 11, 16, 12, 0, 0) },
                new GanttTask { Id = 15, ParentId = 7, Name = "Secure required resources", StartDate = new DateTime(2018, 11, 16, 13, 0, 0), FinishDate = new DateTime(2018, 11, 19, 12, 0, 0) },
                new GanttTask { Id = 16, ParentId = 7, Name = "Analysis complete", StartDate = new DateTime(2018, 11, 19, 12, 0, 0), FinishDate = new DateTime(2018, 11, 19, 12, 0, 0) },
                new GanttTask { Id = 17, ParentId = 0, Name = "Design", StartDate = new DateTime(2018, 11, 19, 13, 0, 0), FinishDate = new DateTime(2018, 12, 7, 17, 0, 0) },
                new GanttTask { Id = 18, ParentId = 17, Name = "Review preliminary software specifications", StartDate = new DateTime(2018, 11, 19, 13, 0, 0), FinishDate = new DateTime(2018, 11, 21, 12, 0, 0) },
                new GanttTask { Id = 19, ParentId = 17, Name = "Develop functional specifications", StartDate = new DateTime(2018, 11, 21, 13, 0, 0), FinishDate = new DateTime(2018, 11, 28, 12, 0, 0) },
                new GanttTask { Id = 20, ParentId = 17, Name = "Develop prototype based on functional specifications", StartDate = new DateTime(2018, 11, 28, 13, 0, 0), FinishDate = new DateTime(2018, 12, 4, 12, 0, 0) },
                new GanttTask { Id = 21, ParentId = 17, Name = "Review functional specifications", StartDate = new DateTime(2018, 12, 4, 13, 0, 0), FinishDate = new DateTime(2018, 12, 6, 12, 0, 0) },
                new GanttTask { Id = 22, ParentId = 17, Name = "Incorporate feedback into functional specifications", StartDate = new DateTime(2018, 12, 6, 13, 0, 0), FinishDate = new DateTime(2018, 12, 7, 12, 0, 0) },
                new GanttTask { Id = 23, ParentId = 17, Name = "Obtain approval to proceed", StartDate = new DateTime(2018, 12, 7, 13, 0, 0), FinishDate = new DateTime(2018, 12, 7, 17, 0, 0) },
                new GanttTask { Id = 24, ParentId = 17, Name = "Design complete", StartDate = new DateTime(2018, 12, 7, 17, 0, 0), FinishDate = new DateTime(2018, 12, 7, 17, 0, 0) },
                new GanttTask { Id = 25, ParentId = 0, Name = "Development", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2019, 1, 8, 15, 0, 0) },
                new GanttTask { Id = 26, ParentId = 25, Name = "Review functional specifications", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 10, 17, 0, 0) },
                new GanttTask { Id = 27, ParentId = 25, Name = "Identify modular/tiered design parameters", StartDate = new DateTime(2018, 12, 11, 8, 0, 0), FinishDate = new DateTime(2018, 12, 11, 17, 0, 0) },
                new GanttTask { Id = 28, ParentId = 25, Name = "Assign development staff", StartDate = new DateTime(2018, 12, 12, 8, 0, 0), FinishDate = new DateTime(2018, 12, 12, 17, 0, 0) },
                new GanttTask { Id = 29, ParentId = 25, Name = "Develop code", StartDate = new DateTime(2018, 12, 13, 8, 0, 0), FinishDate = new DateTime(2019, 1, 2, 17, 0, 0) },
                new GanttTask { Id = 30, ParentId = 25, Name = "Developer testing (primary debugging)", StartDate = new DateTime(2018, 12, 18, 15, 0, 0), FinishDate = new DateTime(2019, 1, 8, 15, 0, 0) },
                new GanttTask { Id = 31, ParentId = 25, Name = "Development complete", StartDate = new DateTime(2019, 1, 8, 15, 0, 0), FinishDate = new DateTime(2019, 1, 8, 15, 0, 0) },
                new GanttTask { Id = 32, ParentId = 0, Name = "Testing", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2019, 2, 14, 15, 0, 0) },
                new GanttTask { Id = 33, ParentId = 32, Name = "Develop unit test plans using product specifications", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 13, 17, 0, 0) },
                new GanttTask { Id = 34, ParentId = 32, Name = "Develop integration test plans using product specifications", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 13, 17, 0, 0) },
                new GanttTask { Id = 35, ParentId = 32, Name = "Unit Testing", StartDate = new DateTime(2019, 1, 8, 15, 0, 0), FinishDate = new DateTime(2019, 1, 29, 15, 0, 0) },
                new GanttTask { Id = 36, ParentId = 35, Name = "Review modular code", StartDate = new DateTime(2019, 1, 8, 15, 0, 0), FinishDate = new DateTime(2019, 1, 15, 15, 0, 0) },
                new GanttTask { Id = 37, ParentId = 35, Name = "Test component modules to product specifications", StartDate = new DateTime(2019, 1, 15, 15, 0, 0), FinishDate = new DateTime(2019, 1, 17, 15, 0, 0) },
                new GanttTask { Id = 38, ParentId = 35, Name = "Identify anomalies to product specifications", StartDate = new DateTime(2019, 1, 17, 15, 0, 0), FinishDate = new DateTime(2019, 1, 22, 15, 0, 0) },
                new GanttTask { Id = 39, ParentId = 35, Name = "Modify code", StartDate = new DateTime(2019, 1, 22, 15, 0, 0), FinishDate = new DateTime(2019, 1, 25, 15, 0, 0) },
                new GanttTask { Id = 40, ParentId = 35, Name = "Re-test modified code", StartDate = new DateTime(2019, 1, 25, 15, 0, 0), FinishDate = new DateTime(2019, 1, 29, 15, 0, 0) },
                new GanttTask { Id = 41, ParentId = 35, Name = "Unit testing complete", StartDate = new DateTime(2019, 1, 29, 15, 0, 0), FinishDate = new DateTime(2019, 1, 29, 15, 0, 0) },
                new GanttTask { Id = 42, ParentId = 32, Name = "Integration Testing", StartDate = new DateTime(2019, 1, 29, 15, 0, 0), FinishDate = new DateTime(2019, 2, 14, 15, 0, 0) },
                new GanttTask { Id = 43, ParentId = 42, Name = "Test module integration", StartDate = new DateTime(2019, 1, 29, 15, 0, 0), FinishDate = new DateTime(2019, 2, 5, 15, 0, 0) },
                new GanttTask { Id = 44, ParentId = 42, Name = "Identify anomalies to specifications", StartDate = new DateTime(2019, 2, 5, 15, 0, 0), FinishDate = new DateTime(2019, 2, 7, 15, 0, 0) },
                new GanttTask { Id = 45, ParentId = 42, Name = "Modify code", StartDate = new DateTime(2019, 2, 7, 15, 0, 0), FinishDate = new DateTime(2019, 2, 12, 15, 0, 0) },
                new GanttTask { Id = 46, ParentId = 42, Name = "Re-test modified code", StartDate = new DateTime(2019, 2, 12, 15, 0, 0), FinishDate = new DateTime(2019, 2, 14, 15, 0, 0) },
                new GanttTask { Id = 47, ParentId = 42, Name = "Integration testing complete", StartDate = new DateTime(2019, 2, 14, 15, 0, 0), FinishDate = new DateTime(2019, 2, 14, 15, 0, 0) },
                new GanttTask { Id = 48, ParentId = 0, Name = "Training", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2019, 2, 11, 15, 0, 0) },
                new GanttTask { Id = 49, ParentId = 48, Name = "Develop training specifications for end users", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 12, 17, 0, 0) },
                new GanttTask { Id = 50, ParentId = 48, Name = "Develop training specifications for helpdesk support staff", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 12, 17, 0, 0) },
                new GanttTask { Id = 51, ParentId = 48, Name = "Identify training delivery methodology (computer based training, classroom, etc.)", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 11, 17, 0, 0) },
                new GanttTask { Id = 52, ParentId = 48, Name = "Develop training materials", StartDate = new DateTime(2019, 1, 8, 15, 0, 0), FinishDate = new DateTime(2019, 1, 29, 15, 0, 0) },
                new GanttTask { Id = 53, ParentId = 48, Name = "Conduct training usability study", StartDate = new DateTime(2019, 1, 29, 15, 0, 0), FinishDate = new DateTime(2019, 2, 4, 15, 0, 0) },
                new GanttTask { Id = 54, ParentId = 48, Name = "Finalize training materials", StartDate = new DateTime(2019, 2, 4, 15, 0, 0), FinishDate = new DateTime(2019, 2, 7, 15, 0, 0) },
                new GanttTask { Id = 55, ParentId = 48, Name = "Develop training delivery mechanism", StartDate = new DateTime(2019, 2, 7, 15, 0, 0), FinishDate = new DateTime(2019, 2, 11, 15, 0, 0) },
                new GanttTask { Id = 56, ParentId = 48, Name = "Training materials complete", StartDate = new DateTime(2019, 2, 11, 15, 0, 0), FinishDate = new DateTime(2019, 2, 11, 15, 0, 0) },
                new GanttTask { Id = 57, ParentId = 0, Name = "Documentation", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2019, 1, 21, 12, 0, 0) },
                new GanttTask { Id = 58, ParentId = 57, Name = "Develop Help specification", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 10, 17, 0, 0) },
                new GanttTask { Id = 59, ParentId = 57, Name = "Develop Help system", StartDate = new DateTime(2018, 12, 24, 13, 0, 0), FinishDate = new DateTime(2019, 1, 14, 12, 0, 0) },
                new GanttTask { Id = 60, ParentId = 57, Name = "Review Help documentation", StartDate = new DateTime(2019, 1, 14, 13, 0, 0), FinishDate = new DateTime(2019, 1, 17, 12, 0, 0) },
                new GanttTask { Id = 61, ParentId = 57, Name = "Incorporate Help documentation feedback", StartDate = new DateTime(2019, 1, 17, 13, 0, 0), FinishDate = new DateTime(2019, 1, 21, 12, 0, 0) },
                new GanttTask { Id = 62, ParentId = 57, Name = "Develop user manuals specifications", StartDate = new DateTime(2018, 12, 10, 8, 0, 0), FinishDate = new DateTime(2018, 12, 11, 17, 0, 0) },
                new GanttTask { Id = 63, ParentId = 57, Name = "Develop user manuals", StartDate = new DateTime(2018, 12, 24, 13, 0, 0), FinishDate = new DateTime(2019, 1, 14, 12, 0, 0) },
                new GanttTask { Id = 64, ParentId = 57, Name = "Review all user documentation", StartDate = new DateTime(2019, 1, 14, 13, 0, 0), FinishDate = new DateTime(2019, 1, 16, 12, 0, 0) },
                new GanttTask { Id = 65, ParentId = 57, Name = "Incorporate user documentation feedback", StartDate = new DateTime(2019, 1, 16, 13, 0, 0), FinishDate = new DateTime(2019, 1, 18, 12, 0, 0) },
                new GanttTask { Id = 66, ParentId = 57, Name = "Documentation complete", StartDate = new DateTime(2019, 1, 21, 12, 0, 0), FinishDate = new DateTime(2019, 1, 21, 12, 0, 0) },
                new GanttTask { Id = 67, ParentId = 0, Name = "Pilot", StartDate = new DateTime(2018, 11, 19, 13, 0, 0), FinishDate = new DateTime(2019, 2, 25, 15, 0, 0) },
                new GanttTask { Id = 68, ParentId = 67, Name = "Identify test group", StartDate = new DateTime(2018, 11, 19, 13, 0, 0), FinishDate = new DateTime(2018, 11, 20, 12, 0, 0) },
                new GanttTask { Id = 69, ParentId = 67, Name = "Develop software delivery mechanism", StartDate = new DateTime(2018, 11, 20, 13, 0, 0), FinishDate = new DateTime(2018, 11, 21, 12, 0, 0) },
                new GanttTask { Id = 70, ParentId = 67, Name = "Install/deploy software", StartDate = new DateTime(2019, 2, 14, 15, 0, 0), FinishDate = new DateTime(2019, 2, 15, 15, 0, 0) },
                new GanttTask { Id = 71, ParentId = 67, Name = "Obtain user feedback", StartDate = new DateTime(2019, 2, 15, 15, 0, 0), FinishDate = new DateTime(2019, 2, 22, 15, 0, 0) },
                new GanttTask { Id = 72, ParentId = 67, Name = "Evaluate testing information", StartDate = new DateTime(2019, 2, 22, 15, 0, 0), FinishDate = new DateTime(2019, 2, 25, 15, 0, 0) },
                new GanttTask { Id = 73, ParentId = 67, Name = "Pilot complete", StartDate = new DateTime(2019, 2, 25, 15, 0, 0), FinishDate = new DateTime(2019, 2, 25, 15, 0, 0) },
                new GanttTask { Id = 74, ParentId = 0, Name = "Deployment", StartDate = new DateTime(2019, 2, 25, 15, 0, 0), FinishDate = new DateTime(2019, 3, 4, 15, 0, 0) },
                new GanttTask { Id = 75, ParentId = 74, Name = "Determine final deployment strategy", StartDate = new DateTime(2019, 2, 25, 15, 0, 0), FinishDate = new DateTime(2019, 2, 26, 15, 0, 0) },
                new GanttTask { Id = 76, ParentId = 74, Name = "Develop deployment methodology", StartDate = new DateTime(2019, 2, 26, 15, 0, 0), FinishDate = new DateTime(2019, 2, 27, 15, 0, 0) },
                new GanttTask { Id = 77, ParentId = 74, Name = "Secure deployment resources", StartDate = new DateTime(2019, 2, 27, 15, 0, 0), FinishDate = new DateTime(2019, 2, 28, 15, 0, 0) },
                new GanttTask { Id = 78, ParentId = 74, Name = "Train support staff", StartDate = new DateTime(2019, 2, 28, 15, 0, 0), FinishDate = new DateTime(2019, 3, 1, 15, 0, 0) },
                new GanttTask { Id = 79, ParentId = 74, Name = "Deploy software", StartDate = new DateTime(2019, 3, 1, 15, 0, 0), FinishDate = new DateTime(2019, 3, 4, 15, 0, 0) },
                new GanttTask { Id = 80, ParentId = 74, Name = "Deployment complete", StartDate = new DateTime(2019, 3, 4, 15, 0, 0), FinishDate = new DateTime(2019, 3, 4, 15, 0, 0) },
                new GanttTask { Id = 81, ParentId = 0, Name = "Post Implementation Review", StartDate = new DateTime(2019, 3, 4, 15, 0, 0), FinishDate = new DateTime(2019, 3, 7, 15, 0, 0) },
                new GanttTask { Id = 82, ParentId = 81, Name = "Document lessons learned", StartDate = new DateTime(2019, 3, 4, 15, 0, 0), FinishDate = new DateTime(2019, 3, 5, 15, 0, 0) },
                new GanttTask { Id = 83, ParentId = 81, Name = "Distribute to team members", StartDate = new DateTime(2019, 3, 5, 15, 0, 0), FinishDate = new DateTime(2019, 3, 6, 15, 0, 0) },
                new GanttTask { Id = 84, ParentId = 81, Name = "Create software maintenance team", StartDate = new DateTime(2019, 3, 6, 15, 0, 0), FinishDate = new DateTime(2019, 3, 7, 15, 0, 0) },
                new GanttTask { Id = 85, ParentId = 81, Name = "Post implementation review complete", StartDate = new DateTime(2019, 3, 7, 15, 0, 0), FinishDate = new DateTime(2019, 3, 7, 15, 0, 0) },
                new GanttTask { Id = 86, ParentId = 0, Name = "Software development template complete", StartDate = new DateTime(2019, 3, 7, 15, 0, 0), FinishDate = new DateTime(2019, 3, 7, 15, 0, 0) }
            };
        }
    }
}
