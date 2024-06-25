using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace ChartsDemo {

    public class StarsData {
        List<StarDataItem> starsData;
        public List<StarDataItem> Data { get { return this.starsData ?? (this.starsData = GetStarsData()); } }

        List<StarDataItem> GetStarsData() {
            string fileName = "starsdata.csv";
            var dataSource = new List<StarDataItem>();
            try {
                string path = "/ChartsDemo;component/Data/" + fileName;
                StreamResourceInfo info = Application.GetResourceStream(new Uri(path, UriKind.RelativeOrAbsolute));
                using (StreamReader reader = new StreamReader(info.Stream)) {
                    while (!reader.EndOfStream) {
                        string line = reader.ReadLine();
                        var values = line.Split(';');
                        StarDataItem data = new StarDataItem() {
                            HipID = int.Parse(values[0], CultureInfo.InvariantCulture),
                            Spectr = values[1],
                            CI = double.Parse(values[2], CultureInfo.InvariantCulture),
                            X = double.Parse(values[3], CultureInfo.InvariantCulture),
                            Y = double.Parse(values[4], CultureInfo.InvariantCulture),
                            Z = double.Parse(values[5], CultureInfo.InvariantCulture),
                            Lum = double.Parse(values[6], CultureInfo.InvariantCulture)
                        };
                        dataSource.Add(data);
                    }
                }
            }
            catch {
                throw new Exception("It's impossible to load " + fileName);
            }
            return dataSource;
        }
    }


    public struct StarDataItem {
        public int HipID { get; set; }
        public string Spectr { get; set; }
        public double Lum { get; set; }
        public double CI { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

}
