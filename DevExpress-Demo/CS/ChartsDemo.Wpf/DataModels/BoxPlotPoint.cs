using System;
using System.Collections.Generic;
using System.Linq;

namespace ChartsDemo {
    public class BoxPlotPoint {
        public int ExperimentNumber { get; private set; }
        public double Min { get; private set; }
        public double Quartile1 { get; private set; }
        public double Median { get; private set; }
        public double Quartile3 { get; private set; }
        public double Max { get; private set; }
        public double Mean { get; private set; }
        public List<double> Outliers { get; private set; }

        public BoxPlotPoint(int experimentNumber, List<double> randomSequence) {
            ExperimentNumber = experimentNumber;
            CalculateBoxPlotValues(randomSequence);
        }

        void CalculateBoxPlotValues(List<double> randomSequence) {
            Tuple<double, double, double> quartiles = CalculateQuartiles(randomSequence);
            Quartile1 = quartiles.Item1;
            Quartile3 = quartiles.Item3;
            Median = quartiles.Item2;
            Tuple<double, double, double> averageAndMinMax = CalculateAverageAndMinMax(randomSequence);
            Mean = averageAndMinMax.Item1;
            Min = Math.Max(averageAndMinMax.Item2, Quartile1 - 1.5 * (Quartile3 - Quartile1));
            Max = Math.Min(averageAndMinMax.Item3, Quartile3 + 1.5 * (Quartile3 - Quartile1));
            Outliers = randomSequence.Where(d => d > Max || d < Min).ToList();
        }
        Tuple<double, double, double> CalculateAverageAndMinMax(List<double> randomSequence) {
            double average = 0;
            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (double d in randomSequence) {
                average += d;
                if (d > max)
                    max = d;
                if (d < min)
                    min = d;
            }
            average = average / randomSequence.Count;
            return new Tuple<double, double, double>(average, min, max);
        }
        Tuple<double, double, double> CalculateQuartiles(List<double> randomSequence) {
            randomSequence.Sort();
            int middleIndex = (int)(randomSequence.Count / 2); 
            double quartile1 = 0;
            double quartile2;
            double quartile3 = 0;
            if (randomSequence.Count % 2 == 0) {
                quartile2 = (randomSequence[middleIndex - 1] + randomSequence[middleIndex]) / 2;
                int middleIndexOfHalf = middleIndex / 2;
                if (middleIndex % 2 == 0) {
                    quartile1 = (randomSequence[middleIndexOfHalf - 1] + randomSequence[middleIndexOfHalf]) / 2;
                    quartile3 = (randomSequence[middleIndex + middleIndexOfHalf - 1] + randomSequence[middleIndex + middleIndexOfHalf]) / 2;
                }
                else {
                    quartile1 = randomSequence[middleIndexOfHalf];
                    quartile3 = randomSequence[middleIndexOfHalf + middleIndex];
                }
            }
            else if (randomSequence.Count == 1) {
                quartile1 = randomSequence[0];
                quartile2 = randomSequence[0];
                quartile3 = randomSequence[0];
            }
            else {
                quartile2 = randomSequence[middleIndex];
                if ((randomSequence.Count - 1) % 4 == 0) {
                    int quarterIndex = (int)((randomSequence.Count - 1) / 4); 
                    quartile1 = (randomSequence[quarterIndex - 1] * .25) + (randomSequence[quarterIndex] * .75);
                    quartile3 = (randomSequence[3 * quarterIndex] * .75) + (randomSequence[3 * quarterIndex + 1] * .25);
                }
                else if ((randomSequence.Count - 3) % 4 == 0) {
                    int quarterIndex = (int)((randomSequence.Count - 3) / 4); 
                    quartile1 = (randomSequence[quarterIndex] * .75) + (randomSequence[quarterIndex + 1] * .25);
                    quartile3 = (randomSequence[3 * quarterIndex + 1] * .25) + (randomSequence[3 * quarterIndex + 2] * .75);
                }
            }
            return new Tuple<double, double, double>(quartile1, quartile2, quartile3);
        }
    }


    public class PointData {
        public double Argument { get; private set; }
        public double Value { get; private set; }

        public PointData(double val, Random rnd, double argumentOffset = 0) {
            Argument = rnd.NextDouble() + argumentOffset;
            Value = val;
        }
    }
}
