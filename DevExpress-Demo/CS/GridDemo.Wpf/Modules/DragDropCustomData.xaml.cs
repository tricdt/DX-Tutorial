using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Scheduling;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DragDropCustomDataViewModel.(cs)")]
    public partial class DragDropCustomData : GridDemoModule {
        public DragDropCustomData() {
            InitializeComponent();
            Unloaded += OnDragDropCustomDataUnloaded;
        }
        void OnDragDropCustomDataUnloaded(object sender, RoutedEventArgs e) {
            scheduler.DataSource = null;
        }

        void OnStartRecordDrag(object sender, StartRecordDragEventArgs e) {
            e.Data.SetData(typeof(IEnumerable<SchedulerTask>), e.Records.Cast<SchedulerTask>());
            e.Handled = true;
        }
        void OnDragRecordOver(object sender, DragRecordOverEventArgs e) {
            if(e.IsFromOutside && e.Data.GetDataPresent(typeof(IEnumerable<AppointmentItemData>))) {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }
        void OnDropRecord(object sender, DropRecordEventArgs e) {
            if(e.Data.GetDataPresent(typeof(IEnumerable<AppointmentItemData>))) {
                var appointments = (IEnumerable<AppointmentItemData>)e.Data.GetData(typeof(IEnumerable<AppointmentItemData>));
                var dataObject = new DataObject();
                dataObject.SetData(new RecordDragDropData(appointments.Select(x => CreateTask(x)).ToArray()));
                e.Data = dataObject;
                e.Effects = DragDropEffects.Move;
            }
        }
        void OnStartAppointmentDragFromOutside(object sender, StartAppointmentDragFromOutsideEventArgs e) {
            if (e.Data.GetDataPresent(typeof(IEnumerable<SchedulerTask>)))
                ((IEnumerable<SchedulerTask>)e.Data.GetData(typeof(IEnumerable<SchedulerTask>))).ToList().ForEach(x => e.DragAppointments.Add(CreateAppointment(x)));
        }
        void OnResizeAppointmentOver(object sender, ResizeAppointmentOverEventArgs e) {
            if(e.ResizeAppointment.Duration.TotalHours < 1) {
                if(e.ResizeHandle == ResizeHandle.End) {
                    e.ResizeAppointment.End += TimeSpan.FromHours(1) - e.ResizeAppointment.Duration;
                } else {
                    DateTime end = e.ResizeAppointment.End;
                    e.ResizeAppointment.Start -= TimeSpan.FromHours(1) - e.ResizeAppointment.Duration;
                    e.ResizeAppointment.End = end;
                }
            }
        }
        AppointmentItem CreateAppointment(SchedulerTask task) {
            return new AppointmentItem {
                Subject = task.Subject,
                LabelId = task.Priority,
                Description = task.Description,
                Start = new DateTime(),
                End = new DateTime() + task.Duration,
                AllDay = task.AllDay
            };
        }
        SchedulerTask CreateTask(AppointmentItemData appointment) {
            return new SchedulerTask {
                Subject = appointment.Subject,
                Priority = appointment.LabelId is IssuePriority ? (IssuePriority)appointment.LabelId : IssuePriority.Low,
                Description = appointment.Description,
                Duration = appointment.End - appointment.Start,
                AllDay = appointment.AllDay
            };
        }
    }
}
