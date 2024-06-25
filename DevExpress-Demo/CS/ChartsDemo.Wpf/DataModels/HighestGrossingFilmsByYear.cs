using System.Data;

namespace ChartsDemo {

    public class HighestGrossingFilmsByYear {
        public DataTable Data {
            get { return GetData(); }
        }

        public DataTable GetData() {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("Year", typeof(int)), new DataColumn("Budget", typeof(decimal)), new DataColumn("Grosses", typeof(decimal)), new DataColumn("Title", typeof(string)) });
            table.Rows.Add(2007, 300, 0.963, "Pirates of the Caribbean: At World's End");
            table.Rows.Add(2008, 185, 1.004, "The Dark Knight");
            table.Rows.Add(2009, 237, 2.788, "Avatar");
            table.Rows.Add(2010, 200, 1.067, "Toy Story 3");
            table.Rows.Add(2011, 250, 1.341, "Harry Potter and the Deathly Hallows Part 2");
            table.Rows.Add(2012, 220, 1.519, "Marvel's The Avengers");
            table.Rows.Add(2013, 150, 1.276, "Frozen");
            table.Rows.Add(2014, 210, 1.104, "Transformers: Age of Extinction");
            table.Rows.Add(2015, 245, 2.068, "Star Wars: The Force Awakens");
            table.Rows.Add(2016, 250, 1.153, "Captain America: Civil War");
            return table;
        }
    }

}
