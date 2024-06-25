using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;

namespace PivotGridDemo {
    [POCOViewModel]
    public class CustomerReportsViewModel {
        const string allString = "(All)";

        public CustomerReportsViewModel() {
            AllYears = CustomerReports
                .Select(x => x.OrderDate.Value.Year)
                .Distinct()
                .OrderBy(x => x)
                .Cast<object>();
            AllQuarters = new object[] { allString }.Concat(
                CustomerReports
                .Select(x => (x.OrderDate.Value.Month - 1) / 3 + 1)
                .Distinct()
                .OrderBy(x => x)
                .Cast<object>());

            SelectedQuarter = AllQuarters.FirstOrDefault();
            SelectedYear = AllYears.FirstOrDefault();
        }

        public IEnumerable<object> AllQuarters { get; set; }
        public IEnumerable<object> AllYears { get; set; }
        public IList<DevExpress.DemoData.Models.CustomerReport> CustomerReports { get { return NWindDataProvider.CustomerReports; } }
        public virtual object QuartersFilter { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnSelectedQuarterChanged")]
        public virtual object SelectedQuarter { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnSelectedYearChanged")]
        public virtual object SelectedYear { get; set; }
        public virtual object YearsFilter { get; set; }

        protected void OnSelectedQuarterChanged() {
            QuartersFilter = Equals(SelectedQuarter, allString) ? AllQuarters.Skip(1) : new [] { SelectedQuarter };
        }
        protected void OnSelectedYearChanged() {
            YearsFilter = new[] { SelectedYear };
        }
    }
}
