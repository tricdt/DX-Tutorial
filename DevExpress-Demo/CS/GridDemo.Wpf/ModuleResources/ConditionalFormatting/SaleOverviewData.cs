using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class SaleOverviewData {
        public SaleOverviewData(string state, double sales, double salesVsTarget, double profit, double customersSatisfaction, double markerShare) {
            this.State = state;
            this.Sales = sales;
            this.Profit = profit;
            this.SalesVsTarget = salesVsTarget;
            this.CustomersSatisfaction = customersSatisfaction;
            this.MarketShare = markerShare;
        }
        public string State { get; set; }

        [DisplayFormat(DataFormatString = "#,##0,,M")]
        public double Sales { get; set; }

        [DisplayFormat(DataFormatString = "#,##0,,M")]
        public double Profit { get; set; }

        [DisplayFormat(DataFormatString = "p", ApplyFormatInEditMode = true), Display(Name = "Sales vs Target")]
        public double SalesVsTarget { get; set; }

        [DisplayFormat(DataFormatString = "p0", ApplyFormatInEditMode = true)]
        public double MarketShare { get; set; }

        public double CustomersSatisfaction { get; set; }

    }
}
