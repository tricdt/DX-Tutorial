using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace GridDemo {
    public static class SalesByYearData {
        public static List<ExpandoObject> Data {
            get {
                var data = new List<ExpandoObject>();
                Random random = new Random();
                for(int yearIndex = 10; yearIndex > 0; yearIndex--) {
                    int year = DateTime.Now.Year - yearIndex;
                    for(int month = 1; month <= 12; month++) {
                        IDictionary<string, object> row = new ExpandoObject();
                        row["Date"] = new DateTime(year, month, 1);
                        for(int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
                            row[Columns[columnIndex].PropertyName] = random.Next(30000);
                        data.Add((ExpandoObject)row);
                    }
                }
                return data;
            }
        }
        public static List<ColumnDescription> Columns { get; private set; }
        static SalesByYearData() {
            var columns = new List<ColumnDescription>();
            foreach(var employee in NWindContext.Create().Employees) {
                string name = employee.FirstName + " " + employee.LastName;
                columns.Add(new ColumnDescription(name));
            }
            Columns = columns;
        }
    }
}
