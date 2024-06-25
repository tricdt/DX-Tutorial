using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace ChartsDemo {

    public class ExchangeRatesModel {
        List<FinancialDataPoint> gbpUsdRate;
        List<FinancialDataPoint> eurUsdRate;

        public List<FinancialDataPoint> GbpUsdRate {
            get { return this.gbpUsdRate ?? (this.gbpUsdRate = CsvReader.ReadFinancialData("GBPUSDDaily.csv")); }
        }
        public List<FinancialDataPoint> EurUsdRate {
            get { return this.eurUsdRate ?? (this.eurUsdRate = CsvReader.ReadFinancialData("EURUSDDaily.csv")); }
        }
    }
}
