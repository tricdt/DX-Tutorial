using System;
using System.Data;
using DevExpress.Mvvm;

namespace EditorsDemo {
    public class OutlookViewModel : ViewModelBase {

        public OutlookViewModel() {
            Initialize();
        }

        public DateTime? FromDate {
            get { return GetProperty(() => FromDate); }
            set {
                SetProperty(() => FromDate, value);
                UpdateFilter();
            }
        }
        public DateTime? ToDate {
            get { return GetProperty(() => ToDate); }
            set {
                SetProperty(() => ToDate, value);
                UpdateFilter();
            }
        }
        public string FilterString {
            get { return GetProperty(() => FilterString); }
            set { SetProperty(() => FilterString, value); }
        }
        public DataTable Source {
            get { return GetProperty(() => Source); }
            private set { SetProperty(() => Source, value); }
        }

        void Initialize() {
            Source = OutlookDataGenerator.CreateOutlookDataTable(1000);
            DateTime min = DateTime.MaxValue, max = DateTime.MinValue;
            foreach (DataRow row in Source.Rows) {
                var date = (DateTime)row["Sent"];
                if (date < min)
                    min = date;
                if (date > max)
                    max = date;
            }
            FromDate = min;
            ToDate = max;
        }
        void UpdateFilter() {
            if (FromDate != null && ToDate != null && FromDate < ToDate) {
                FilterString = String.Format("([Sent] >= #{0}# AND [Sent] < #{1}#)", FromDate, ToDate);
            }
        }
    }
}
