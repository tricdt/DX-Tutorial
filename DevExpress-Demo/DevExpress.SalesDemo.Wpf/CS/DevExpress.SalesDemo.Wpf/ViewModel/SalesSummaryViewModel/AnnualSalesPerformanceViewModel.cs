using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class AnnualSalesPerformanceViewModel : DataViewModel {
        public static AnnualSalesPerformanceViewModel Create(DateTime date) {
            return ViewModelSource.Create(() => new AnnualSalesPerformanceViewModel(date));
        }
        protected AnnualSalesPerformanceViewModel()
            : this(DateTime.Now) {
        }
        protected AnnualSalesPerformanceViewModel(DateTime date) {
            DecimalRange badSalesRange = SalesRangeProvider.GetBadSalesRange();
            DecimalRange normalSalesRange = SalesRangeProvider.GetNormalSalesRange();
            DecimalRange goodSalesRange = SalesRangeProvider.GetGoodSalesRange();
            AnnualSalesFirstRangeEnd = badSalesRange.End;
            AnnualSalesSecondRangeEnd = normalSalesRange.End;
            AnnualSalesThirdRangeEnd = goodSalesRange.End;
            if(DateTimeUtils.IsCurrentYear(date)) {
                VolumeHeader = "YEAR TO DATE";
                RequestData("Volume", x => x.GetTotalSalesByRange(DateTimeUtils.GetYtdRange()).TotalCost, x => Volume = x);
            } else {
                VolumeHeader = "YEAR " + date.Year;
                RequestData("Volume", x => x.GetTotalSalesByRange(DateTimeUtils.GetYearRange(date)).TotalCost, x => Volume = x);
            }
        }
        
        public virtual decimal AnnualSalesFirstRangeEnd { get; protected set; }
        public virtual decimal AnnualSalesSecondRangeEnd { get; protected set; }
        public virtual decimal AnnualSalesThirdRangeEnd { get; protected set; }
        public virtual string VolumeHeader { get; protected set; }
        public virtual decimal Volume { get; protected set; }
    }
}
