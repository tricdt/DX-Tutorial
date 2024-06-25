using System;
using System.Collections.Generic;

namespace ChartsDemo {

    public static class RealEstateData {
        static List<RealEstateDataMonthlyPoint> GenerateMonthlyData(int randomSeek, int min, int max) {
            Random rnd = new Random(randomSeek);
            List<RealEstateDataMonthlyPoint> list = new List<RealEstateDataMonthlyPoint>();
            for (int i = 0; i < 12; i++) {
                list.Add(new RealEstateDataMonthlyPoint() {
                    Month = ((Months)i).ToString(),
                    Income = rnd.Next(min, max)
                });
            }
            return list;
        }

        public static List<RealEstateDataAnnualPoint> GetAnnualData() {
            Random rnd = new Random(1);
            List<RealEstateDataAnnualPoint> list = new List<RealEstateDataAnnualPoint>();
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 3; year < currentYear; year++) {
                list.Add(new RealEstateDataAnnualPoint() {
                    Employee = "Andrew Fuller",
                    MonthlyData = GenerateMonthlyData(year + 4, rnd.Next(5000, 50000), rnd.Next(50000, 500000)),
                    Year = year.ToString()
                });
                list.Add(new RealEstateDataAnnualPoint() {
                    Employee = "Anne Dodsworth",
                    MonthlyData = GenerateMonthlyData(year + 5, rnd.Next(5000, 50000), rnd.Next(50000, 500000)),
                    Year = year.ToString()
                });
                list.Add(new RealEstateDataAnnualPoint() {
                    Employee = "Margaret Peacock",
                    MonthlyData = GenerateMonthlyData(year + 6, rnd.Next(5000, 50000), rnd.Next(50000, 500000)),
                    Year = year.ToString()
                });
                list.Add(new RealEstateDataAnnualPoint() {
                    Employee = "Michael Suyama",
                    MonthlyData = GenerateMonthlyData(year + 7, rnd.Next(5000, 50000), rnd.Next(50000, 500000)),
                    Year = year.ToString()
                });
                list.Add(new RealEstateDataAnnualPoint() {
                    Employee = "Nancy Davolio",
                    MonthlyData = GenerateMonthlyData(year + 8, rnd.Next(5000, 50000), rnd.Next(50000, 500000)),
                    Year = year.ToString()
                });
                list.Add(new RealEstateDataAnnualPoint() {
                    Employee = "Robert King",
                    MonthlyData = GenerateMonthlyData(year + 9, rnd.Next(5000, 50000), rnd.Next(50000, 500000)),
                    Year = year.ToString()
                });
            }
            return list;
        }
    }


    public class RealEstateDataAnnualPoint {
        public string Employee { get; set; }
        public string Year { get; set; }
        public double Income {
            get {
                double sum = 0.0;
                foreach (RealEstateDataMonthlyPoint point in MonthlyData) {
                    sum += point.Income;
                }
                return sum;
            }
        }
        public List<RealEstateDataMonthlyPoint> MonthlyData { get; set; }
    }


    public class RealEstateDataMonthlyPoint {
        public string Month { get; set; }
        public double Income { get; set; }
    }

}
