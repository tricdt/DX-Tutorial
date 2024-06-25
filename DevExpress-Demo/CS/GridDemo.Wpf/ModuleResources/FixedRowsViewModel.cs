using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    public class FixedRowsViewModel : StockViewModelBase {
        string[] fixedTopRows = { "MSFT", "AAPL", "ORCL" };
        public List<StockItem> FixedTopRows { get; set; }
        string[] fixedBottomRows = { "IBM", "CSCO", "MSI", "DELL", "XRX" };
        public List<StockItem> FixedBottomRows { get; set; }
        public StockItem CurrentItem { get; set; }

        public FixedRowsViewModel() {
            FixedTopRows = Data.Where(x => fixedTopRows.Contains(x.CompanyName)).ToList();
            FixedBottomRows = Data.Where(x => fixedBottomRows.Contains(x.CompanyName)).ToList();
            CurrentItem = Data.FirstOrDefault(x => x.CompanyName == "EBAY");
        }

        protected override double GetDeltaPrice(double currentPrice, bool dataGeneration) {
            return Random.NextDouble() * 0.5 - 0.25;
        }
    }
}
