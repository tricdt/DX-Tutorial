using System.Collections.Generic;

namespace ChartsDemo {

    public class ErrorBarDataItem {
        public string Argument { get; private set; }
        public int Value { get; private set; }
        public int Negative { get; private set; }
        public int Positive { get; private set; }

        public ErrorBarDataItem(string argument, int value, int negative, int positive) {
            Argument = argument;
            Value = value;
            Negative = negative;
            Positive = positive;
        }
    }


    class ErrorBarsData {
        public List<ErrorBarDataItem> Data {
            get {
                return new List<ErrorBarDataItem>() {
                    new ErrorBarDataItem("A", 20, 5, 8),
                    new ErrorBarDataItem("B", 50, 3, 5),
                    new ErrorBarDataItem("C", 40, 20, 10),
                    new ErrorBarDataItem("D", 22, 15, 5),
                    new ErrorBarDataItem("E", 30, 5, 8),
                    new ErrorBarDataItem("F", 45, 5, 4),
                    new ErrorBarDataItem("G", 35, 5, 3),
                    new ErrorBarDataItem("H", 28, 4, 2),
                    new ErrorBarDataItem("I", 46, 6, 4),
                    new ErrorBarDataItem("J", 27, 8, 20),
                    new ErrorBarDataItem("K", 20, 5, 8),
                    new ErrorBarDataItem("L", 50, 3, 5),
                    new ErrorBarDataItem("M", 40, 20, 10),
                    new ErrorBarDataItem("N", 22, 15, 5),
                    new ErrorBarDataItem("O", 30, 5, 8),
                    new ErrorBarDataItem("P", 45, 5, 2),
                    new ErrorBarDataItem("Q", 35, 5, 5),
                    new ErrorBarDataItem("R", 28, 4, 4),
                    new ErrorBarDataItem("S", 46, 6, 5),
                    new ErrorBarDataItem("T", 27, 8, 8),
                };
            }
        }
    }

}
