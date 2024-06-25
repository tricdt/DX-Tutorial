using System;
using System.Data;

namespace ChartsDemo {
    public class PopulationGenerator {
        const int PointsCount = 150;

        public DataTable Data {
            get { return GetData(); }
        }

        DataTable GetData() {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("Population", typeof(string)), new DataColumn("Argument", typeof(int)), new DataColumn("Value", typeof(int)) });
            Random random = new Random(0);
            Generate(table, "Population 1", random, 140, 1280, 100, 1240, PointsCount);
            Generate(table, "Population 2", random, 500, 1600, 1000, 2100, PointsCount);
            Generate(table, "Population 3", random, 450, 950, 1550, 2050, PointsCount);
            Generate(table, "Population 4", random, 800, 1700, 300, 1200, PointsCount);
            return table;
        }
        void Generate(DataTable table, string name, Random random, int xPlus, int xMinus, int yPlus, int yMinus, int count) {
            foreach (NumericPoint point in PointGenerator.GenerateCluster(random, xPlus, xMinus, yPlus, yMinus, count))
                table.Rows.Add(name, point.Argument, point.Value);
        }
    }
}
