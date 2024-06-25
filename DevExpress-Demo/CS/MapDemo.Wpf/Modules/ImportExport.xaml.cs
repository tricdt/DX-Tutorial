using System;
using System.ComponentModel;
using DevExpress.Xpf.Map;
using DevExpress.Map;

namespace MapDemo {
    public partial class ImportExport : MapDemoModule, INotifyPropertyChanged {
        ShapefileWorldResources shapefileWorldResources;

        Uri fileUri;
        int zoomLevel;
        CoordPoint centerPoint;

        public event PropertyChangedEventHandler PropertyChanged;

        public Uri FileUri {
            get { return fileUri; }
            set {
                fileUri = value;
                NotifyPropertyChanged("FileUri");
            }
        }
        public int ZoomLevel {
            get { return zoomLevel; }
            set {
                zoomLevel = value;
                NotifyPropertyChanged("ZoomLevel");
            }
        }
        public CoordPoint CenterPoint {
            get { return centerPoint; }
            set {
                centerPoint = value;
                NotifyPropertyChanged("CenterPoint");
            }
        }

        public ImportExport() {
            InitializeComponent();
            DataContext = this;
            shapefileWorldResources = new ShapefileWorldResources();
            ZoomLevel = 1;
            CenterPoint = new GeoPoint(0, 0);
            FileUri = shapefileWorldResources.CountriesFileUri;
        }

        void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        void lbMapType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            switch (lbMapType.SelectedItem.ToString()) {
                case "World":
                    FileUri = shapefileWorldResources.CountriesFileUri;
                    ZoomLevel = 1;
                    CenterPoint = new GeoPoint(0, 0);
                    break;
                case "Africa":
                    FileUri = shapefileWorldResources.AfricaFileUri;
                    CenterPoint = new GeoPoint(0, 20);
                    ZoomLevel = 3;
                    break;
                case "South America":
                    FileUri = shapefileWorldResources.SouthAmericaFileUri;
                    CenterPoint = new GeoPoint(-26.2538, -61.8752);
                    ZoomLevel = 3;
                    break;
                case "North America":
                    FileUri = shapefileWorldResources.NorthAmericaFileUri;
                    CenterPoint = new GeoPoint(60.572, -100.635);
                    ZoomLevel = 2;
                    break;
                case "Australia":
                    FileUri = shapefileWorldResources.AustraliaFileUri;
                    CenterPoint = new GeoPoint(-25.0856, 141.7675);
                    ZoomLevel = 3;
                    break;
                case "Eurasia":
                    FileUri = shapefileWorldResources.EurasiaFileUri;
                    CenterPoint = new GeoPoint(52.8027, 84.8143);
                    ZoomLevel = 2;
                    break;
            }
        }
    }
}
