using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDemo.Modules {
    public class Appointment : INotifyPropertyChanged {
        int? eventType;
        public int? EventType {
            get { return eventType; }
            set {
                if (eventType == value)
                    return;
                eventType = value;
                RaisePropertyChanged("EventType");
            }
        }

        DateTime? startDate;
        public DateTime? StartDate {
            get { return startDate; }
            set {
                if (startDate == value)
                    return;
                startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        DateTime? endDate;
        public DateTime? EndDate {
            get { return endDate; }
            set {
                if (endDate == value)
                    return;
                endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        bool? allDay;
        public bool? AllDay {
            get { return allDay; }
            set {
                if (allDay == value)
                    return;
                allDay = value;
                RaisePropertyChanged("AllDay");
            }
        }

        string subject;
        public string Subject {
            get { return subject; }
            set {
                if (subject == value)
                    return;
                subject = value;
                RaisePropertyChanged("Subject");
            }
        }

        string location;
        public string Location {
            get { return location; }
            set {
                if (location == value)
                    return;
                location = value;
                RaisePropertyChanged("Location");
            }
        }

        string description;
        public string Description {
            get { return description; }
            set {
                if (description == value)
                    return;
                description = value;
                RaisePropertyChanged("Description");
            }
        }

        int? status;
        public int? Status {
            get { return status; }
            set {
                if (status == value)
                    return;
                status = value;
                RaisePropertyChanged("Status");
            }
        }

        int? label;
        public int? Label {
            get { return label; }
            set {
                if (label == value)
                    return;
                label = value;
                RaisePropertyChanged("Label");
            }
        }

        string recurrenceInfo;
        public string RecurrenceInfo {
            get { return recurrenceInfo; }
            set {
                if (recurrenceInfo == value)
                    return;
                recurrenceInfo = value;
                RaisePropertyChanged("RecurrenceInfo");
            }
        }

        string reminderInfo;
        public string ReminderInfo {
            get { return reminderInfo; }
            set {
                if (reminderInfo == value)
                    return;
                reminderInfo = value;
                RaisePropertyChanged("ReminderInfo");
            }
        }

        string contactInfo;
        public string ContactInfo {
            get { return contactInfo; }
            set {
                if (contactInfo == value)
                    return;
                contactInfo = value;
                RaisePropertyChanged("ContactInfo");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName) {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) throw new ArgumentException(propertyName + " doesn't exist in " + GetType().Name + " type");
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CalendarModel {
        public ObservableCollection<Appointment> Appointments { get { return DataBaseHelper.Instance.Appointments; } }

        static CalendarModel model = null;
        public static CalendarModel Model {
            get {
                if (model == null)
                    model = new CalendarModel();

                return model;
            }
        }
    }
}
