using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using System.Reflection;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase.Helpers;
using System.IO;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Grid.TreeList;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace TreeListDemo {
    [XmlRoot("Countries")]
    public class CountriesData : List<Country> {
        static IList dataSource = null;
        public static IList DataSource  {
            get {
                if(dataSource != null)
                    return dataSource;
                Assembly assembly = typeof(CountriesData).Assembly;
                Stream stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) + "Countries.xml", true);
                XmlSerializer s = new XmlSerializer(typeof(CountriesData), new XmlRootAttribute("Countries"));
                dataSource = (IList)s.Deserialize(stream);
                return dataSource;
            }
        }
    }
    public class Country {
        public string Name { get; set; }
        public byte[] Flag { get; set; }
    }

    public class SpaceObjectData : List<SpaceObjects> {
        public static IList<SpaceObjects> DataSource
        {
            get
            {
                Assembly assembly = typeof(SpaceObjectData).Assembly;
                Stream stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) + "SpaceObjects.xml", true);
                XmlSerializer s = new XmlSerializer(typeof(SpaceObjectData), new XmlRootAttribute("NewDataSet"));
                return (List<SpaceObjects>)s.Deserialize(stream);
            }
        }
    }
    public class SpaceObjects {
        public int ObjectId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string WikiPage { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageHint { get; set; }
        public float MeanRadiusInKM { get; set; }
        public string MeanRadiusByEarth { get; set; }
        public string Volume10pow9KM3 { get; set; }
        public float VolumeRByEarth { get; set; }
        public float Mass10pow21kg { get; set; }
        public float MassByEarth { get; set; }
        public float DensitygBycm3 { get; set; }
        public float SurfaceGravitymBys2 { get; set; }
        public float SurfaceGravityByEarth { get; set; }
        public string TypeOfObject { get; set; }
        public bool IsExpanded { get; set; }
    }

    public class SalesData {
        public SalesData(int id, int regionId, string region, decimal marchSales, decimal septemberSales, decimal marchSalesPrev, decimal septermberSalesPrev, double marketShare) {
            ID = id;
            RegionID = regionId;
            Region = region;
            MarchSales = marchSales;
            SeptemberSales = septemberSales;
            MarchSalesPrev = marchSalesPrev;
            SeptemberSalesPrev = septermberSalesPrev;
            MarketShare = marketShare;
        }
        public int ID { get; set; }
        public int RegionID { get; set; }
        public string Region { get; set; }
        public decimal MarchSales { get; set; }
        public decimal SeptemberSales { get; set; }
        public decimal MarchSalesPrev { get; set; }
        public decimal SeptemberSalesPrev { get; set; }
        public double MarketShare { get; set; }
    }
    public class SalesDataGenerator {
        public static List<SalesData> CreateData() {
            List<SalesData> sales = new List<SalesData>();
            sales.Add(new SalesData(0, -1, "Western Europe", 30540, 33000, 32220, 35500, .70));
            sales.Add(new SalesData(1, 0, "Austria", 22000, 28000, 26120, 28500, .92));
            sales.Add(new SalesData(2, 0, "Belgium", 13000, 9640, 14500, 11200, .16));
            sales.Add(new SalesData(3, 0, "Denmark", 21000, 18100, 17120, 15500, .56));
            sales.Add(new SalesData(4, 0, "Finland", 17000, 17420, 18120, 19200, .44));
            sales.Add(new SalesData(5, 0, "France", 23020, 27000, 20120, 24200, .51));
            sales.Add(new SalesData(6, 0, "Germany", 30540, 33000, 32220, 35500, .93));
            sales.Add(new SalesData(7, 0, "Greece", 15600, 13200, 11500, 11000, .11));
            sales.Add(new SalesData(8, 0, "Ireland", 9530, 10939, 12620, 12990, .34));
            sales.Add(new SalesData(9, 0, "Italy", 17299, 19321, 14120, 15945, .22));
            sales.Add(new SalesData(10, 0, "Liechtenstein", 1650, 1820, 1410, 1710, .25));
            sales.Add(new SalesData(11, 0, "Luxembourg", 1920, 1710, 2010, 1620, .18));
            sales.Add(new SalesData(12, 0, "Monaco", 1280, 1350, 1100, 1400, .6));
            sales.Add(new SalesData(13, 0, "Netherlands", 8902, 9214, 7400, 9600, .85));
            sales.Add(new SalesData(14, 0, "Norway", 5400, 7310, 5200, 6880, .7));
            sales.Add(new SalesData(15, 0, "Portugal", 9220, 4271, 4100, 3880, .5));
            sales.Add(new SalesData(16, 0, "Spain", 12900, 10300, 14300, 14900, .82));
            sales.Add(new SalesData(17, 0, "Switzerland", 9323, 10730, 7244, 9400, .14));
            sales.Add(new SalesData(18, 0, "United Kingdom", 14580, 13967, 15200, 16900, .91));

            sales.Add(new SalesData(19, -1, "Eastern Europe", 22500, 24580, 21225, 22698, .62));
            sales.Add(new SalesData(20, 19, "Albania", 2400, 2725, 2140, 2610, .42));
            sales.Add(new SalesData(21, 19, "Belarus", 7315, 18800, 8240, 17480, .34));
            sales.Add(new SalesData(22, 19, "Bosnia and Herzegovina", 6030, 8010, 6120, 7900, .29));
            sales.Add(new SalesData(23, 19, "Bulgaria", 6300, 2821, 5200, 4880, .8));
            sales.Add(new SalesData(24, 19, "Croatia", 4200, 3890, 3880, 4430, .29));
            sales.Add(new SalesData(25, 19, "Czech Republic", 19500, 15340, 16230, 14980, .13));
            sales.Add(new SalesData(26, 19, "Estonia", 3100, 2950, 3300, 3050, .55));
            sales.Add(new SalesData(27, 19, "Hungary", 13495, 13900, 10245, 9560, .14));
            sales.Add(new SalesData(28, 19, "Latvia", 3250, 3400, 3330, 3650, .7));
            sales.Add(new SalesData(29, 19, "Lithuania", 8250, 6400, 8330, 6350, .65));
            sales.Add(new SalesData(30, 19, "Macedonia", 1900, 2100, 1750, 2050, .45));
            sales.Add(new SalesData(31, 19, "Poland", 8930, 9440, 12200, 12150, .52));
            sales.Add(new SalesData(32, 19, "Romania", 4900, 5100, 5241, 6284, .30));
            sales.Add(new SalesData(33, 19, "Russia", 22500, 24580, 21225, 22698, .85));
            sales.Add(new SalesData(34, 19, "Serbia", 12730, 12420, 12935, 12800, .8));
            sales.Add(new SalesData(35, 19, "Slovakia", 11420, 11365, 12225, 12520, .74));
            sales.Add(new SalesData(36, 19, "Slovenia", 7300, 6950, 7280, 7010, .51));

            sales.Add(new SalesData(37, -1, "North America", 31400, 32800, 30300, 31940, .84));
            sales.Add(new SalesData(38, 37, "Antigua and Barbuda", 950, 840, 800, 825, .68));
            sales.Add(new SalesData(39, 37, "Bahamas", 2850, 2740, 3000, 2925, .75));
            sales.Add(new SalesData(40, 37, "Barbados", 2100, 2225, 2250, 2480, .83));
            sales.Add(new SalesData(41, 37, "Belize", 3200, 3225, 3250, 3480, .77));
            sales.Add(new SalesData(42, 37, "Canada", 25390, 27000, 5200, 6880, .64));
            sales.Add(new SalesData(43, 37, "Costa Rica", 4100, 4350, 4050, 4150, .80));
            sales.Add(new SalesData(44, 37, "Cuba", 5600, 5880, 5410, 8900, .84));
            sales.Add(new SalesData(45, 37, "Dominica", 900, 840, 910, 900, .9));
            sales.Add(new SalesData(46, 37, "Dominican Republic", 5450, 5800, 5600, 5200, .2));
            sales.Add(new SalesData(47, 37, "Mexico", 12400, 12650, 12700, 12850, .65));
            sales.Add(new SalesData(48, 37, "USA", 31400, 32800, 30300, 31940, .87));


            sales.Add(new SalesData(49, -1, "South America", 16380, 17590, 15400, 16680, .32));
            sales.Add(new SalesData(50, 49, "Argentina", 16380, 17590, 15400, 16680, .88));
            sales.Add(new SalesData(51, 49, "Bolivia", 4380, 5590, 5400, 5680, .92));
            sales.Add(new SalesData(52, 49, "Brazil", 4560, 9480, 3900, 6100, .10));
            sales.Add(new SalesData(53, 49, "Chile", 7500, 7680, 8100, 8555, .7));
            sales.Add(new SalesData(54, 49, "Colombia", 24400, 25780, 25300, 26750, .72));
            sales.Add(new SalesData(55, 49, "Peru", 19300, 18980, 19400, 19550, .81));
            sales.Add(new SalesData(56, 49, "Venezuela", 6300, 6980, 6400, 6550, .2));

            sales.Add(new SalesData(57, -1, "Asia", 20388, 22547, 22500, 25756, .52));
            sales.Add(new SalesData(58, 57, "India", 4642, 5320, 4200, 6470, .44));
            sales.Add(new SalesData(59, 57, "Japan", 9457, 12859, 8300, 8733, .70));
            sales.Add(new SalesData(60, 57, "China", 20388, 22547, 22500, 25756, .82));
            return sales;
        }
    }

    [XmlRoot("EmployeeTasks")]
    public class EmployeedTasks : List<EmployeeTask> {
        static IList<EmployeeTask> dataSource = null;
        static ObservableCollection<EmployeeTask> editableDataSource;
        static List<string> employeeNames;
        public static IList<EmployeeTask> DataSource {
            get {
                if(dataSource != null)
                    return dataSource;
                dataSource = GetEmployeeTasks();
                return dataSource;
            }

        }
        static IList<EmployeeTask> GetEmployeeTasks() {
            Assembly assembly = typeof(EmployeedTasks).Assembly;
            Stream stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) + "EmployeeTasks.xml", true);
            XmlSerializer s = new XmlSerializer(typeof(EmployeedTasks), new XmlRootAttribute("EmployeeTasks"));
            return (IList<EmployeeTask>)s.Deserialize(stream);
        }
        public static ObservableCollection<EmployeeTask> EditableDataSource {
            get {
                if(editableDataSource != null)
                    return editableDataSource;
                editableDataSource = new ObservableCollection<EmployeeTask>(GetEmployeeTasks().Take(28));
                return editableDataSource;
            }
        }
        public static List<string> EmployeeNames {
            get {
                if(employeeNames != null)
                    return employeeNames;
                employeeNames = DataSource.Select(e => e.Employee).ToList();
                return employeeNames;
            }
        }
    }

    public class EmployeeTask : IEditableObject {
        public EmployeeTask() {
            Priority = Status = -1;
        }
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }
        public bool IsCompleted { get { return Status == 100; } }
        public int Status { get; set; }
        public bool IsUpdated { get; set; }

        void IEditableObject.BeginEdit() { IsUpdated = false;  }
        void IEditableObject.CancelEdit() { IsUpdated = false; }
        void IEditableObject.EndEdit() { IsUpdated = true; }
    }


    public class PriorityIconConverter : IValueConverter {
        static List<ImageSource> PriorityImages;
        static PriorityIconConverter() {
            PriorityImages = new List<ImageSource>();
            PriorityImages.Add(ImageHelper.GetSvgImage("Flag_Yellow"));
            PriorityImages.Add(ImageHelper.GetSvgImage("Flag_Red"));
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int status = (int)value;
            if(status < 0)
                return null;
            return PriorityImages[status];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class EmployeeTaskImageSelector : DevExpress.Xpf.Grid.TreeListNodeImageSelector {
        static List<ImageSource> TaskImages;
        static EmployeeTaskImageSelector() {
            TaskImages = new List<ImageSource>();
            TaskImages.Add(ImageHelper.GetSvgImage("Task"));
            TaskImages.Add(ImageHelper.GetSvgImage("Note"));
        }
        public override ImageSource Select(TreeListRowData rowData) {
            if(rowData.Level == 0) return TaskImages[0];
            return TaskImages[1];
        }
    }
}
