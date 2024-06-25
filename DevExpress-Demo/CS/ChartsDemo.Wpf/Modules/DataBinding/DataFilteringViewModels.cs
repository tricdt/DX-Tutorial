using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevExpress.Utils.Filtering;

namespace ChartsDemo {
    public class SalesDataViewModel {
        public List<DevAVDataItem> Sales { get; private set; }
        public List<string> Companies { get; private set; }


        public SalesDataViewModel() {
            Sales = new DevAVBranchesSales().GetList();
            Companies = Sales.Select(x => x.Company).Distinct().ToList();
        }
    }
}
