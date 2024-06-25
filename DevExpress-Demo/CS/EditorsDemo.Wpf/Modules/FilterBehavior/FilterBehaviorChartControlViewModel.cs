using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace EditorsDemo.FilterBehavior {
    public class FilterBehaviorChartControlViewModel : ViewModelBase {
        public List<DevAVDataItem> Sales { get; private set; }
        public List<string> Companies { get; private set; }


        public FilterBehaviorChartControlViewModel() {
            Sales = new DevAVBranchesSales().GetList();
            Companies = Sales.Select(x => x.Company).Distinct().ToList();
        }
    }
}
