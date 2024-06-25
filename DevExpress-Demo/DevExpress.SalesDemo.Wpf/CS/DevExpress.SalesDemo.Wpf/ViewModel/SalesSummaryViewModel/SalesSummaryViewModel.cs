using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class SalesSummaryViewModel : DataViewModel {
        public static SalesSummaryViewModel Create() {
            return ViewModelSource.Create(() => new SalesSummaryViewModel());
        }
        public SalesSummaryViewModel() {
            SalesBySectorViewModel = SalesBySectorViewModel.Create();
            ThisYearAnnualSalesPerformanceViewModel = AnnualSalesPerformanceViewModel.Create(DateTime.Now);
            LastYearAnnualSalesPerformanceViewModel = AnnualSalesPerformanceViewModel.Create(DateTime.Now.AddYears(-1));
            SalesForecastViewModel = SalesForecastViewModel.Create();
            SalesForecastHeader = string.Format("SALES FORECAST ({0})", DateTimeUtils.GetCurrentYear());
        }

        public virtual SalesBySectorViewModel SalesBySectorViewModel { get; protected set; }
        public virtual AnnualSalesPerformanceViewModel ThisYearAnnualSalesPerformanceViewModel { get; protected set; }
        public virtual AnnualSalesPerformanceViewModel LastYearAnnualSalesPerformanceViewModel { get; protected set; }
        public virtual SalesForecastViewModel SalesForecastViewModel { get; protected set; }
        public virtual string SalesForecastHeader { get; protected set; }
    }
}
