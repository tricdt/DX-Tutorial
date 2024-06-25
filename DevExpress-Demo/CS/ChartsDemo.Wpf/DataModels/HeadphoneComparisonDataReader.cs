using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace ChartsDemo {
    class HeadphoneComparisonDataReader {
        const string FileName = "HeadphoneComparison.dat";

        static public List<HeadphoneComparisonPoint> ReadDataFromFile() {
            List<HeadphoneComparisonPoint> dataSource = new List<HeadphoneComparisonPoint>();
            Uri uri = new Uri("/ChartsDemo;component/Data/" + FileName, UriKind.RelativeOrAbsolute);
            using (Stream stream = Application.GetResourceStream(uri).Stream) {
                StreamReader reader;
                try {
                    reader = new StreamReader(stream);
                    while (!reader.EndOfStream) {
                        string line = reader.ReadLine();
                        if (line.Length == 0 || line.StartsWith("//"))
                            continue;
                        string[] cells = line.Split(new string[] { "," }, StringSplitOptions.None);

                        string name = cells[0];
                        double frequency = double.Parse(cells[1], CultureInfo.InvariantCulture);
                        double spl90Db = double.Parse(cells[2], CultureInfo.InvariantCulture);
                        double spl100Db = double.Parse(cells[3], CultureInfo.InvariantCulture);
                        dataSource.Add(new HeadphoneComparisonPoint(name, frequency, spl90Db, spl100Db));
                    }
                }
                catch {
                    throw new Exception("It's impossible to load " + FileName);
                }
                return dataSource;
            }
        }

        public class HeadphoneComparisonPoint {
            public string HeadphonesName { get; private set; }
            public double Frequency { get; private set; }
            public double Spl90Db { get; private set; }
            public double Spl100Db { get; private set; }

            public HeadphoneComparisonPoint(string headphoneName, double frequency, double spl90Db, double spl100Db) {
                HeadphonesName = headphoneName;
                Frequency = frequency;
                Spl90Db = spl90Db;
                Spl100Db = spl100Db;
            }
        }
    }
}
