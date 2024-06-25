using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Scheduling;

namespace SchedulingDemo.ViewModels {
    public class EndUserRestrictionsDemoViewModel {
        protected EndUserRestrictionsDemoViewModel() {
            Start = TeamData.Start;
            Calendars = new ObservableCollection<TeamCalendar>(TeamData.Calendars);
            Appointments = new ObservableCollection<TeamAppointment>(TeamData.AllAppointments);
        }

        public virtual DateTime Start { get; set; }
        public virtual IEnumerable<TeamCalendar> Calendars { get; protected set; }
        public virtual IEnumerable<TeamAppointment> Appointments { get; protected set; }
        public virtual bool IsTeamCalendarReadonly { get; set; }
        public virtual ResourceItem SelectedResource { get; set; }

        public void CustomAllowAppointmentItemOperation(AppointmentItemOperationEventArgs e) {
            if (e.Appointment == null && SelectedResource == null)
                return;
            var resource = e.Appointment == null ? SelectedResource.Id : e.Appointment.ResourceId;
            e.Allow = !IsReadOnly(resource);
        }
        public void StartAppointmentDrag(StartAppointmentDragEventArgs e) {
            e.Cancel = IsCancelDragDrop(e.SourceAppointments);
        }
        public void DropAppointment(DropAppointmentEventArgs e) {
            e.Cancel = IsCancelDragDrop(e.DragAppointments);
        }
        public void DragAppointmentOver(DragAppointmentOverEventArgs e) {
            if (IsCancelDragDrop(e.DragAppointments)) {
                e.Effects = System.Windows.DragDropEffects.None;
                for (int i = 0; i < e.DragAppointments.Count; i++)
                    e.ConflictedAppointments[i].Add(e.DragAppointments[i]);
            }
        }
        public void StartAppointmentResize(StartAppointmentResizeEventArgs e) {
            e.Cancel = IsTeamCalendarReadonly && e.SourceAppointment.ResourceId.Equals(1);
        }

        bool IsCancelDragDrop(IEnumerable<AppointmentItem> appointments) {
            bool cancel = false;
            foreach (AppointmentItem appointment in appointments) {
                cancel = IsReadOnly(appointment.ResourceId);
                if (cancel) break;
            }
            return cancel;
        }
        bool IsReadOnly(object resourceId) {
            return IsTeamCalendarReadonly && resourceId.Equals(1);
        }
    }
}
