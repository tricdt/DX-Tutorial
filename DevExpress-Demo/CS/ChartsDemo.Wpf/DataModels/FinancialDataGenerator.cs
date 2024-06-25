using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace ChartsDemo {

    class FinancialDataGenerator {
        const int InitialDataPointsCount = 3652;

        public static List<FinancialDataPoint> Generate() {
            List<FinancialDataPoint> list = new List<FinancialDataPoint>();
            DateTime stamp = DateTime.Now;
            Uri uri = new Uri("/ChartsDemo;component/Data/InitialFinData.csv", UriKind.RelativeOrAbsolute);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            StreamReader reader = new StreamReader(info.Stream);
            for (int i = 0; i < InitialDataPointsCount; i++) {
                if (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    FinancialDataPoint point = new FinancialDataPoint();
                    stamp = stamp.AddDays(-1);
                    point.DateTimeStamp = stamp;
                    point.Open = double.Parse(values[0], CultureInfo.InvariantCulture);
                    point.High = double.Parse(values[1], CultureInfo.InvariantCulture);
                    point.Low = double.Parse(values[2], CultureInfo.InvariantCulture);
                    point.Close = double.Parse(values[3], CultureInfo.InvariantCulture);
                    point.Volume = double.Parse(values[4], CultureInfo.InvariantCulture);
                    list.Insert(0, point);
                }
            }
            return list;
        }
    }

}
