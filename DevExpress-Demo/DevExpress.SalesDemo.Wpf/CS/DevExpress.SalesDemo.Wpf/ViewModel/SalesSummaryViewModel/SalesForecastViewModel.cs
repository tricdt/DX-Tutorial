using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class SalesForecastViewModel : DataViewModel {
        public static SalesForecastViewModel Create() {
            return ViewModelSource.Create(() => new SalesForecastViewModel());
        }
        protected SalesForecastViewModel() {
            RequestData("YTDSalesVolume", x => x.GetTotalSalesByRange(DateTimeUtils.GetYtdRange()).TotalCost, x => {
                var YTDSalesVolume = x;
                YTDSalesForecast = SalesForecastMaker.GetYtdForecast(YTDSalesVolume);
            });

            DecimalRange badSalesRange = SalesRangeProvider.GetBadSalesRange();
            DecimalRange normalSalesRange = SalesRangeProvider.GetNormalSalesRange();
            DecimalRange goodSalesRange = SalesRangeProvider.GetGoodSalesRange();
            AnnualSalesFirstRangeEnd = badSalesRange.End;
            AnnualSalesSecondRangeEnd = normalSalesRange.End;
            AnnualSalesThirdRangeEnd = goodSalesRange.End;
        }

        public virtual decimal AnnualSalesFirstRangeEnd { get; protected set; }
        public virtual decimal AnnualSalesSecondRangeEnd { get; protected set; }
        public virtual decimal AnnualSalesThirdRangeEnd { get; protected set; }
        public virtual decimal YTDSalesForecast { get; protected set; }
    }
}
