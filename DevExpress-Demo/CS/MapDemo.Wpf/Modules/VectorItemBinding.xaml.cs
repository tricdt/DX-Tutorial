using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Linq;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class VectorItemBinding : MapDemoModule {
        public VectorItemBinding() {
            InitializeComponent();
            DataContext = LoadDataFromXML();
        }

        ObservableCollection<ShipInfo> LoadDataFromXML() {
            ObservableCollection<ShipInfo> ships = new ObservableCollection<ShipInfo>();
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Ships.xml");
            if (document != null) {
                foreach (XElement element in document.Element("Ships").Elements()) {
                    ShipInfo shipInfo = new ShipInfo(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture),
                        element.Element("Name").Value,
                        element.Element("Description").Value,
                        Convert.ToInt16(element.Element("Year").Value));
                    ships.Add(shipInfo);
                }
            }
            return ships;
        }
    }

    public class ShipInfo {
        public ShipInfo(double latitude, double longitude, string name, string description, int year) {
            this.Location = new GeoPoint(latitude, longitude);
            this.Name = name;
            this.Year = year;
            this.Description = description;
        }
        public GeoPoint Location { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public string Description { get; private set; }
        public string Header { get { return Name + " (" + Year + ")"; } }
    }
}
