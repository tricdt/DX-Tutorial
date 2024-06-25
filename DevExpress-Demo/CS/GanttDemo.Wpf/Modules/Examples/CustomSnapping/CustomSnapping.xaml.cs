using DevExpress.Xpf.Gantt;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace GanttDemo.Examples {
    public partial class CustomSnapping : UserControl {
        public CustomSnapping() {
            InitializeComponent();
        }

        #region !
        void TaskMoving(object sender, TaskMovingEventArgs e) {
            e.StartDate = Round(e.StartDate);
        }
        void TaskFinishDateMoving(object sender, TaskFinishDateMovingEventArgs e) {
            e.FinishDate = Round(e.FinishDate, ((GanttNode)e.Node).StartDate.Value);
        }
        void TaskMoved(object sender, TaskMovedEventArgs e) {
            e.StartDate = Round(e.StartDate);
        }
        void TaskFinishDateMoved(object sender, TaskFinishDateMovedEventArgs e) {
            e.FinishDate = Round(e.FinishDate, ((GanttNode)e.Node).StartDate.Value);
        }
        void BeginNewTaskDraw(object sender, BeginNewTaskDrawEventArgs e) {
            e.StartDate = Round(e.StartDate);
        }
        void NewTaskDrawing(object sender, NewTaskDrawingEventArgs e) {
            e.FinishDate = Round(e.FinishDate, e.StartDate);
        }
        void NewTaskDrawn(object sender, NewTaskDrawnEventArgs e) {
            e.FinishDate = Round(e.FinishDate, e.StartDate);
        }

        double UnitCount(DateTime value) {
            return (value - DateTime.MinValue).TotalMilliseconds / CustomSnappingViewModel.SnapUnit.TotalMilliseconds;
        }
        DateTime Round(DateTime value) {
            return DateTimeByUnitCount(UnitCount(value));
        }
        DateTime DateTimeByUnitCount(double unitCount) {
            return DateTime.MinValue + TimeSpan.FromMilliseconds(Math.Round(Math.Round(unitCount) * CustomSnappingViewModel.SnapUnit.TotalMilliseconds));
        }
        DateTime Round(DateTime value, DateTime limit) {
            return DateTimeByUnitCount(Math.Max(1.0 + UnitCount(limit), UnitCount(value)));
        }
        #endregion
        void RequestTimescaleRulers(object sender, RequestTimescaleRulersEventArgs e) {
            e.TimescaleRulers = new List<TimescaleRuler>() { new TimescaleRuler(TimescaleUnit.Day, "D"), new TimescaleRuler(TimescaleUnit.Hour) };
        }
    }
}
