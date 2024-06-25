using DevExpress.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
#endif

namespace DevExpress.DemoData {
    public class VehiclesData {
        public static List<OrderItem> GetMDBData() {
#if DXCORE3
            return InitOrdersData(null, 10000, 3 * 365);
#else
            return InitOrdersData("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataDirectoryHelper.GetFile("Vehicles.mdb", DataDirectoryHelper.DataFolderName), 10000, 3* 365);
#endif
        }
        public class Trademark {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public class BodyStyle {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public class OrderItem {
            internal Model Model;
            public OrderItem(Model model, int days, Random rnd, int id) {
                Model = model;
                ModelPrice = model.Price;
                Trademark = model.Trademark;
                Name = model.Name;
                Modification = model.Modification;
                Category = model.Category;
                MPGCity = model.MPGCity;
                MPGHighway = model.MPGHighway;
                Doors = model.Doors;
                BodyStyle = model.BodyStyle.ToString();
                Cylinders = model.Cylinders;
                Horsepower = model.Horsepower;
                Torque = model.Torque;
                TransmissionSpeeds = model.TransmissionSpeeds;
                TransmissionType = model.TransmissionType;
                InStock = model.InStock;

                SalesDate = DateTime.Now.AddDays(-rnd.Next(days));
                Discount = Math.Round(0.05 * rnd.Next(4), 2);
                OrderID = id;
            }
            public int OrderID { get; set; }
            public DateTime SalesDate { get; set; }
            public double Discount { get; set; }
            public decimal ModelPrice { get; set; }
            public string Trademark { get; set; }
            public string Name { get; set; }
            public string Modification { get; set; }
            public int Category { get; set; }
            public int? MPGCity { get; set; }
            public int? MPGHighway { get; set; }
            public int Doors { get; set; }
            public string BodyStyle { get; set; }
            public int Cylinders { get; set; }
            public string Horsepower { get; set; }
            public string Torque { get; set; }
            public int TransmissionSpeeds { get; set; }
            public int TransmissionType { get; set; }
            public bool InStock { get; set; }
        }
        public class Model {
            public int ID { get; set; }
            public string Trademark { get; set; }
            public string Name { get; set; }
            public string Modification { get; set; }
            public int Category { get; set; }
            public decimal Price { get; set; }
            public int? MPGCity { get; set; }
            public int? MPGHighway { get; set; }
            public int Doors { get; set; }
            public string BodyStyle { get; set; }
            public int Cylinders { get; set; }
            public string Horsepower { get; set; }
            public string Torque { get; set; }
            public int TransmissionSpeeds { get; set; }
            public int TransmissionType { get; set; }
            public string Description { get; set; }
            public DateTime DeliveryDate { get; set; }
            public bool InStock { get; set; }
        }
        public static List<OrderItem> InitOrdersData(string connectionString, int itemCount, int dataInterval) {
            Random rnd = new Random();
#if DXCORE3
            List<Model> listModels = InitDataCore(1);
#else
            DataSet ds;
            List<Model> listModels = InitMDBDataCore(connectionString, out ds, 1);  
#endif
            List<OrderItem> orders = new List<OrderItem>();
            decimal averagePrice = listModels.Select(x => x.Price).Average();
            int averageOrders = itemCount / listModels.Count;
            foreach(Model model in listModels)
                for(int i = 0; i < averageOrders * averagePrice / model.Price; i++)
                    orders.Add(new OrderItem(model, dataInterval, rnd, i + 1));
            return orders;
        }
#if DXCORE3
        static List<Model> InitDataCore(int dataInterval) {
            var rnd = new Random();
            var listModels = new List<Model>();
            
            var vehiclesContext = new Models.Vehicles.VehiclesContext();
            vehiclesContext.Models.Load();
            vehiclesContext.BodyStyles.Load();
            vehiclesContext.Categories.Load();
            vehiclesContext.Trademarks.Load();
            vehiclesContext.TransmissionTypes.Load();

            vehiclesContext.Models.Local.ToList().ForEach(source => listModels.Add(new Model() {
                ID = (int)source.ID,
                Trademark = source.TrademarkName,
                Name = source.Name,
                Modification = source.Modification,
                Category = (int)source.CategoryID,
                Price = source.Price,
                MPGCity = source.MPGCity,
                MPGHighway = source.MPGHighway,
                Doors = source.Doors,
                BodyStyle = source.BodyStyleName,
                Cylinders = source.Cylinders,
                Horsepower = source.Horsepower,
                Torque = source.Torque,
                TransmissionSpeeds = Convert.ToInt32(source.TransmissionSpeeds),
                TransmissionType = (int)source.TransmissionTypeID,
                Description = source.Description,
                DeliveryDate = DateTime.Now.AddDays(rnd.Next(dataInterval)),
                InStock = rnd.Next(100) < 95
            }));
            return listModels;
        }
#else
        static List<Model> InitMDBDataCore(string connectionString, out DataSet ds, int dataInterval) {
            string Model = "Model";
            string Trademark = "Trademark";
            string Category = "Category";
            string BodyStyle = "BodyStyle";
            string TransmissionType = "TransmissionType";
            ds = new DataSet();
            FillTable(Model, null, connectionString, ds);
            FillTable(Category, null, connectionString, ds);
            FillTable(Trademark, null, connectionString, ds);
            FillTable(BodyStyle, null, connectionString, ds);
            FillTable(TransmissionType, null, connectionString, ds);

            List<Trademark> listTrademarks = new List<Trademark>();
            foreach(DataRow row in ds.Tables[Trademark].Rows)
                listTrademarks.Add(new Trademark {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"]
                });

            List<BodyStyle> listBodyStyles = new List<BodyStyle>();
            foreach(DataRow row in ds.Tables[BodyStyle].Rows)
                listBodyStyles.Add(new BodyStyle {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"]
                });

            List<Model> listModels = new List<Model>();
            Random rnd = new Random();
            foreach(DataRow row in ds.Tables[Model].Rows)
                listModels.Add(new Model() {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"],
                    Trademark = listTrademarks.First(x => x.ID == (int)row["TrademarkID"]).Name,
                    Modification = (string)row["Modification"],
                    Category = (int)row["CategoryID"],
                    Price = (decimal)row["Price"],
                    MPGCity = DBNull.Value.Equals(row["MPG City"]) ? null : (int?)row["MPG City"],
                    MPGHighway = DBNull.Value.Equals(row["MPG City"]) ? null : (int?)row["MPG Highway"],
                    Doors = (int)row["Doors"],
                    BodyStyle = listBodyStyles.First(x => x.ID == (int)row["BodyStyleID"]).Name,
                    Cylinders = (int)row["Cylinders"],
                    Horsepower = (string)row["Horsepower"],
                    Torque = (string)row["Torque"],
                    TransmissionSpeeds = Convert.ToInt32(row["Transmission Speeds"]),
                    TransmissionType = (int)row["Transmission Type"],
                    Description = string.Format("{0}", row["Description"]),
                    DeliveryDate = DateTime.Now.AddDays(rnd.Next(dataInterval)),
                    InStock = rnd.Next(100) < 95
                });
            return listModels;
        }
        static void FillTable(string table, string query, string connectionString, DataSet ds) {
            if(query == null)
                query = string.Format("SELECT * FROM {0}", table);
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(query, connectionString);
            oleDbDataAdapter.Fill(ds, table);
        }
#endif
    }
}
