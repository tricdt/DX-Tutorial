using System;
using System.Linq;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {

    class StandardDeviationCustomAggregateFunction : CustomAggregateFunction {
        public override string ToString() {
            return "StdDev (Custom)";
        }

        public override double[] Calculate(GroupInfo groupInfo) {
            double sum = 0.0;
            foreach (double value in groupInfo.Values1) {
                sum += value;
            }
            int len = groupInfo.Values1.Count();
            double averageAmount = sum / len;
            double standardDeviationSquareSum = 0.0;
            foreach (double value in groupInfo.Values1) {
                double deviation = value - averageAmount;
                standardDeviationSquareSum += deviation * deviation;
            }
            double stdDev = Math.Sqrt(standardDeviationSquareSum / len);
            return new double[1] { stdDev };
        }
    }

}
