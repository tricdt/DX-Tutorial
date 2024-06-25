using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DevExpress.Utils;

namespace ChartsDemo {
    static class CsvReader {
        internal static List<FinancialDataPoint> ReadFinancialData(string fileName) {
            string longFileName = string.Empty;
            StreamReader reader;
            var dataSource = new List<FinancialDataPoint>();
            Stream stream = AssemblyHelper.GetEmbeddedResourceStream(typeof(CsvReader).Assembly, fileName, false);
            try {
                reader = new StreamReader(stream);
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    var values = line.Split(',');
                    values[1] = values[1].Trim();
                    var point = new FinancialDataPoint();
                    point.DateTimeStamp = DateTime.Parse(values[0]);
                    if (values[1].Length <= 3)
                        continue;
                    point.Close = double.Parse(values[1], CultureInfo.InvariantCulture);
                    dataSource.Add(point);
                }
            }
            catch {
                throw new Exception("It's impossible to load " + fileName);
            }
            return dataSource;
        }
        internal static List<CarbonContributionDataPoint> ReadCarbonData(string fileName) {
            string longFileName = string.Empty;
            StreamReader reader;
            var dataSource = new List<CarbonContributionDataPoint>();
            Stream stream = AssemblyHelper.GetEmbeddedResourceStream(typeof(CsvReader).Assembly, fileName, false);
            try {
                reader = new StreamReader(stream);
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    var values = line.Split(';');
                    var point = new CarbonContributionDataPoint();
                    point.Year = values[0];
                    point.Contribution = double.Parse(values[1], CultureInfo.InvariantCulture);
                    point.Factor = values[2];
                    dataSource.Add(point);
                }
            }
            catch {
                throw new Exception("It's impossible to load " + fileName);
            }
            return dataSource;
        }
    }
}
