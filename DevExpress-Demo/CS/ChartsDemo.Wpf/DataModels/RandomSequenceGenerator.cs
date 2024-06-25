using System;
using System.Collections.Generic;

namespace ChartsDemo {
    static class RandomSequenceGenerator {
        public static List<double> GenerateRandomSequence(Random random, int length) {
            double selector = random.NextDouble();
            if (selector < 0.33)
                return GenerateExponentialDistribution(random, length);
            if (selector < 0.66)
                return GenerateSpecialDistribution(random, length);
            else
                return GenerateNormalDistribution(random, length);
        }

        static List<double> GenerateNormalDistribution(Random random, int length) {
            List<double> list = new List<double>(length);
            
            double mean = random.Next(450, 550);
            double stdDev = random.Next(50, 70);
            for (int i = 0; i < length; i++) {
                double u1 = 1.0 - random.NextDouble();
                double u2 = 1.0 - random.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                list.Add(mean + stdDev * randStdNormal);
            }
            return list;
        }
        static List<double> GenerateExponentialDistribution(Random random, int length) {
            List<double> list = new List<double>(length);
            double minVal = random.Next(250, 300);
            double maxVal = minVal + 300;
            int generatedCount = 0;
            double lambda = random.NextDouble() * 2;
            while (generatedCount < length) {
                double u = random.NextDouble();
                double t = -Math.Log(u) / lambda;
                double increment = (maxVal - minVal) / 6.0;
                double result = minVal + (t * increment);
                if (result < maxVal) {
                    list.Add(result);
                    generatedCount++;
                }
            }
            return list;
        }
        static List<double> GenerateSpecialDistribution(Random random, int length) {
            List<double> list = new List<double>(length);
            int min = random.Next(100, 250);
            int step = random.Next(30, 70);
            for (int i = 0; i < (int)(length * 0.05); i++)
                list.Add(random.Next(min, min + step));
            for (int i = 0; i < (int)(length * 0.025); i++)
                list.Add(random.Next(min + step + 1, min + 2 * step));
            for (int i = 0; i < (int)(length * 0.075); i++)
                list.Add(random.Next(min + 2 * step + 1, min + 3 * step));
            for (int i = 0; i < (int)(length * 0.10); i++)
                list.Add(random.Next(min + 3 * step + 1, min + 4 * step));
            for (int i = 0; i < (int)(length * 0.20); i++)
                list.Add(random.Next(min + 4 * step + 1, min + 5 * step));
            for (int i = 0; i < (int)(length * 0.30); i++)
                list.Add(random.Next(min + 5 * step + 1, min + 6 * step));
            for (int i = 0; i < (int)(length * 0.20); i++)
                list.Add(random.Next(min + 6 * step + 1, min + 7 * step));
            for (int i = 0; i < (int)(length * 0.05) + 1; i++)
                list.Add(random.Next(min + 7 * step + 1, min + 8 * step));
            return list;
        }

    }
}
