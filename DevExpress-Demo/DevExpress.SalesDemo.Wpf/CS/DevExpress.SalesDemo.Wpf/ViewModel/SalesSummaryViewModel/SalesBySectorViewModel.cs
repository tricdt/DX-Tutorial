using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;
using System.Collections.Generic;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class SalesBySectorViewModel : DataViewModel {
        public static SalesBySectorViewModel Create() {
            return ViewModelSource.Create(() => new SalesBySectorViewModel());
        }
        protected SalesBySectorViewModel() {
            RequestData("SalesBySector", x => x.GetSalesBySector(new DateTime(), DateTime.Now, GroupingPeriod.All), x => SalesBySector = x);
        }

        public virtual IEnumerable<SalesGroup> SalesBySector { get; protected set; }
    }
}
