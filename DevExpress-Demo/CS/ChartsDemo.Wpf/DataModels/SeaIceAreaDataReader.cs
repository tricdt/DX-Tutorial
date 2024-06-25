using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace ChartsDemo {

    static class SeaIceAreaDataReader {
        public static List<SeaIceAreaDataPoint> ReadDataFromFile() {
            List<SeaIceAreaDataPoint> dataSource = new List<SeaIceAreaDataPoint>();
            StreamResourceInfo info = Application.GetResourceStream(new Uri("/ChartsDemo;component/Data/nsidc_global_nt_final_and_nrt.dat", UriKind.RelativeOrAbsolute));
            using (StreamReader reader = new StreamReader(info.Stream)) {
                try {
                    while (!reader.EndOfStream) {
                        string line = reader.ReadLine();
                        if (line[0] != '1' && line[0] != '2')
                            continue;
                        string[] cells = line.Split(new string[] { ", " }, StringSplitOptions.None);
                        if (cells[3].Trim() == "nan")
                            continue;
                        string year = cells[0].Split('-')[0];
                        double dayOfYear = double.Parse(cells[1], CultureInfo.InvariantCulture);
                        double area = double.Parse(cells[3], CultureInfo.InvariantCulture);
                        dataSource.Add(new SeaIceAreaDataPoint(Convert.ToDateTime(cells[0], CultureInfo.InvariantCulture), year, dayOfYear, area));
                    }
                }
                catch {
                    throw new Exception("It's impossible to load " + "nsidc_global_nt_final_and_nrt.dat");
                }
            }
            return dataSource;
        }
    }


    public class SeaIceAreaDataPoint {
        public DateTime FullDate { get; private set; }
        public string Year { get; private set; }
        public double DayOfYear { get; private set; }
        public double IceArea { get; private set; }

        internal SeaIceAreaDataPoint(DateTime fullDate, string year, double dayOfYear, double iceArea) {
            FullDate = fullDate;
            Year = year;
            DayOfYear = dayOfYear;
            IceArea = iceArea;
        }
    }

}
