using System;

namespace ChartsDemo {
    public class FinancialDataPoint {
        public DateTime DateTimeStamp { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public bool IsEmpty { get { return DateTimeStamp.Equals(new DateTime()); } }

        public FinancialDataPoint() { }
        public FinancialDataPoint(DateTime date, double open, double high, double low, double close, double volume) {
            DateTimeStamp = date;
            Low = low;
            High = high;
            Open = open;
            Close = close;
            Volume = volume;
        }
    }
}
