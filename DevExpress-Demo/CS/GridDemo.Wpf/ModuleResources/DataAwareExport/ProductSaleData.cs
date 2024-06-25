using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class ProductSaleData {
        public string Country { get; set; }

        public string ProductName { get; set; }

        public int Year { get; set; }

        [DisplayFormat(DataFormatString = "#,##0,,M")]
        public double Sales { get; set; }

        [DisplayFormat(DataFormatString = "#,##0,,M")]
        public double Profit { get; set; }

        [DisplayFormat(DataFormatString = "p", ApplyFormatInEditMode = true), Display(Name = "Sales vs Target")]
        public double SalesVsTarget { get; set; }

        [DisplayFormat(DataFormatString = "p0", ApplyFormatInEditMode = true)]
        public double MarketShare { get; set; }

        [Display(Name = "Cust Satisfaction")]
        public double CustomersSatisfaction { get; set; }
    }
}
