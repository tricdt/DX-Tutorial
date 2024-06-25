using DevExpress.Mvvm.DataAnnotations;
using EditorsDemo.SCService;
using System;
using System.Linq;
using DevExpress.DemoData;
using DevExpress.Mvvm;

namespace EditorsDemo {
    public class RangeControlFilteringViewModel : ViewModelBase {
        public RangeControlFilteringViewModel() {
            EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            StartDate = EndDate.AddMonths(-6);
            Source = NWindDataProvider.InvoicesUpToDate.Where(x => x.OrderDate >= StartDate && x.OrderDate <= EndDate).ToList();
            SelectedStart = VisibleStartDate = StartDate;
            SelectedEnd = VisibleEndDate = EndDate;
        }
        public object Source { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime VisibleStartDate { get; private set; }
        public DateTime VisibleEndDate { get; private set; }
        public DateTime SelectedStart {
            get { return GetProperty(() => SelectedStart); }
            set {
                SetProperty(() => SelectedStart, value);
                UpdateFilter();
            }
        }
        public DateTime SelectedEnd {
            get { return GetProperty(() => SelectedEnd); }
            set {
                SetProperty(() => SelectedEnd, value);
                UpdateFilter();
            }
        }
        public string FilterString {
            get { return GetProperty(() => FilterString); }
            set { SetProperty(() => FilterString, value); }
        }

        void UpdateFilter() {
            FilterString = String.Format("([OrderDate] >= #{0}# AND [OrderDate] < #{1}#)", SelectedStart, SelectedEnd);
        }
    }
}
