using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts.Sankey;
using DevExpress.Xpf.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace SankeyDemo {
    public class InteractionViewModel : BindableBase {
        public static InteractionViewModel Create() {
            return ViewModelSource.Create(() => new InteractionViewModel());
        }

        readonly List<Export> data;
        Dictionary<string, Color> continentColorPairs = new Dictionary<string, Color>();
        Dictionary<string, List<string>> continentCountriesPairs;

        Dictionary<string, List<Export>> countryNameExportPairs = new Dictionary<string, List<Export>>();
        Dictionary<string, MapItem> countryNameMapItemPairs = new Dictionary<string, MapItem>();
        Dictionary<Export, MapPolyline> exportMapArrowPairs = new Dictionary<Export, MapPolyline>();

        ObservableCollection<object> highlightedSankeyItems = new ObservableCollection<object>();
        ObservableCollection<object> selectedExportItems = new ObservableCollection<object>();
        ObservableCollection<MapItem> selectedMapItems = new ObservableCollection<MapItem>();
        bool updateSelectedArrows = true;

        public virtual MapItemStorage MapArrowStorage { get; set; }
        public virtual ShapefileDataAdapter DataAdapter { get; set; }
        public virtual Dictionary<string, Color> ContinentColorPairs {
            get { return continentColorPairs; }
            set { continentColorPairs = value; }
        }
        public virtual List<Export> SankeyData { get { return data; } }
        public virtual SankeyColorizerBase Colorizer { get; set; }
        public virtual IList SelectedMapItems {
            get { return selectedMapItems; }

        }
        public virtual IList HighlightedSankeyItems {
            get { return highlightedSankeyItems; }
        }
        public virtual IList SelectedExportItems {
            get { return selectedExportItems; }
        }
        public virtual Uri MapFileUri { get; set; }

        protected InteractionViewModel() {
            highlightedSankeyItems.CollectionChanged += (sender, eventArgs) => ShowArrowsByHighlightedItems(highlightedSankeyItems);
            selectedExportItems.CollectionChanged += (sender, eventArgs) => ShowArrowsBySelectedItems(selectedExportItems);
            selectedMapItems.CollectionChanged += (sender, eventArgs) => ShowArrowsBySelectedMapItems(selectedMapItems);
            MapArrowStorage = new MapItemStorage();
            DataAdapter = new ShapefileDataAdapter();
            data = OilTradeDataGenerator.GetData();
            DataAdapter.ShapesLoaded += DataAdapter_ShapesLoaded;
            MapFileUri = Utils.GetFileUri("Countries.shp");
            DataAdapter.FileUri = MapFileUri;
            continentColorPairs = ContinentCountries.GetContinentColorPairs_InteractionDemo();
            continentCountriesPairs = ContinentCountries.GetContinentCountriesPairs_InteractionDemo();
            Colorizer = new ContinentColorizer() {
                ContinentCountriesPairs = continentCountriesPairs,
                ContinentColorPairs = continentColorPairs
            };
        }

        void DataAdapter_ShapesLoaded(object sender, ShapesLoadedEventArgs e) {
            Dictionary<string, List<CoordPoint>> territoryNameBorderCoordPairs = GetTerritoryCenterPoints(e.Shapes);
            CreateArrows(territoryNameBorderCoordPairs);
            DisableUnknownItems(e.Shapes);
        }
        void DisableUnknownItems(IList<MapItem> mapItems) {
            foreach(var mapItem in mapItems) {
                string countryName = mapItem.Attributes["NAME"].Value.ToString();
                if(countryNameExportPairs.ContainsKey(countryName))
                    continue;
                countryName = mapItem.Attributes["CONTINENT"].Value.ToString();
                if(!countryNameExportPairs.ContainsKey(countryName))
                    mapItem.Visible = false;
            }
        }
        Dictionary<string, List<CoordPoint>> GetTerritoryCenterPoints(IList<MapItem> mapItems) {
            var territoryNameCenterCoordPairs = new Dictionary<string, List<CoordPoint>>();
            List<string> nodeNames = data.Select(export => export.Exporter)
                                               .Union(data.Select(export => export.Importer))
                                               .ToList();
            foreach(var mapItem in mapItems) {
                string countryName = mapItem.Attributes["NAME"].Value.ToString();
                if(!nodeNames.Contains(countryName))
                    countryName = mapItem.Attributes["CONTINENT"].Value.ToString();
                if(!countryNameMapItemPairs.ContainsKey(countryName))
                    countryNameMapItemPairs.Add(countryName, mapItem);

                List<CoordPoint> buffPoints = null;
                if(!territoryNameCenterCoordPairs.TryGetValue(countryName, out buffPoints)) {
                    buffPoints = new List<CoordPoint>();
                    territoryNameCenterCoordPairs.Add(countryName, buffPoints);
                }
                foreach(var figure in ((MapPathGeometry)((MapPath)mapItem).Data).Figures)
                    foreach(MapPolyLineSegment segment in figure.Segments)
                        buffPoints.AddRange(segment.Points);
            }
            return territoryNameCenterCoordPairs;
        }
        void CreateArrows(Dictionary<string, List<CoordPoint>> territoryNameBorderCoordPairs) {
            exportMapArrowPairs = new Dictionary<Export, MapPolyline>();
            foreach(var export in data) {
                GeoPoint startPoint = GetAveragePoint(territoryNameBorderCoordPairs[export.Exporter]);
                GeoPoint endPoint = GetAveragePoint(territoryNameBorderCoordPairs[export.Importer]);

                var polyline = new MapPolyline();
                polyline.Points.Add(startPoint);
                polyline.Points.Add(endPoint);
                SetPolylineDrawOptions(polyline);
                MapArrowStorage.Items.Add(polyline);

                if(!countryNameExportPairs.ContainsKey(export.Exporter))
                    countryNameExportPairs.Add(export.Exporter, new List<Export>());
                countryNameExportPairs[export.Exporter].Add(export);
                exportMapArrowPairs.Add(export, polyline);
            }
        }
        void SetPolylineDrawOptions(MapPolyline shape) {
            shape.IsGeodesic = true;
            shape.EndLineCap = new MapLineCap() { Visible = true };
            SetShapeDrawOptions(shape);
        }
        void SetShapeDrawOptions(MapShape shape) {
            shape.Fill = new SolidColorBrush(Color.FromRgb(209, 28, 28));
            shape.Stroke = new SolidColorBrush(Color.FromRgb(209, 28, 28));
            shape.StrokeStyle = new StrokeStyle() { Thickness = 1.5 };
            shape.Visible = false;
        }
        GeoPoint GetAveragePoint(List<CoordPoint> points) {
            double longitudeSum = 0, latitudeSum = 0;
            foreach(GeoPoint point in points) {
                longitudeSum += point.Longitude;
                latitudeSum += point.Latitude;
            }
            return new GeoPoint(latitudeSum / points.Count, longitudeSum / points.Count);
        }

        void ShowArrowsByHighlightedItems(ObservableCollection<object> activeItems) {
            HideAllUnselectedArrows();
            if(activeItems == null || activeItems.Count == 0)
                return;
            var linkTags = new List<Export>();
            foreach(object tag in activeItems)
                if(tag is Export)
                    linkTags.Add((Export)tag);

            SetArrowVisibilityByExport(linkTags, true);
        }
        void ShowArrowsBySelectedItems(ObservableCollection<object> selectedItems) {
            if(!updateSelectedArrows)
                return;
            updateSelectedArrows = false;
            selectedMapItems.Clear();
            var linkTags = new List<Export>();
            foreach(object tag in selectedItems)
                if(tag is Export)
                    linkTags.Add((Export)tag);
            foreach(Export export in linkTags) {
                var mapItem = countryNameMapItemPairs[export.Exporter];
                if(!selectedMapItems.Contains(mapItem))
                    selectedMapItems.Add(mapItem);
                mapItem = countryNameMapItemPairs[export.Importer];
                if(!selectedMapItems.Contains(mapItem))
                    selectedMapItems.Add(mapItem);
            }
            updateSelectedArrows = true;

            SetArrowVisibilityByExport(linkTags, true);
            HideAllUnselectedArrows();
        }
        void ShowArrowsBySelectedMapItems(ObservableCollection<MapItem> selectedMapItems) {
            if(!updateSelectedArrows)
                return;

            updateSelectedArrows = false;
            selectedExportItems.Clear();
            foreach(MapItem mapItem in selectedMapItems) {
                string countryName = mapItem.Attributes["NAME"].Value.ToString();
                if(!countryNameExportPairs.ContainsKey(countryName))
                    countryName = mapItem.Attributes["CONTINENT"].Value.ToString();
                if(!countryNameExportPairs.ContainsKey(countryName))
                    continue;
                foreach(var export in countryNameExportPairs[countryName])
                    selectedExportItems.Add(export);
                SetArrowVisibilityByExport(countryNameExportPairs[countryName], true);
            }
            updateSelectedArrows = true;

            HideAllUnselectedArrows();
        }
        void HideAllUnselectedArrows() {
            foreach(var linkTagMapArrowPair in exportMapArrowPairs)
                if(selectedExportItems == null || !selectedExportItems.Contains(linkTagMapArrowPair.Key))
                    linkTagMapArrowPair.Value.Visible = false;
        }
        void SetArrowVisibilityByExport(IEnumerable<Export> items, bool visible) {
            foreach(Export link in items)
                exportMapArrowPairs[link].Visible = visible;
        }
    }

    public class ColorValueDictionaryToColorCollectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Dictionary<string, Color> continentColorPairs = (Dictionary<string, Color>)value;
            var colorCollection = new ColorCollection();
            foreach(var color in continentColorPairs.Values)
                colorCollection.Add(color);
            return colorCollection;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class StringKeyDictionaryToColorizerKeyItemCollectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Dictionary<string, Color> continentColorPairs = (Dictionary<string, Color>)value;
            var keyItemCollection = new ColorizerKeyItemCollection();
            foreach(var key in continentColorPairs.Keys)
                keyItemCollection.Add(new ColorizerKeyItem() { Key = key, Name = key });
            return keyItemCollection;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class InteractionDemoToolTipConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string text = "";
            string totalImportAndExportText = "";
            if(value is SankeyNode) {
                SankeyNode node = (SankeyNode)value;
                if(node.InputLinks != null && node.InputLinks.Count > 0) {
                    totalImportAndExportText += "Total import: " + node.InputLinks.Select(link => link.TotalWeight).Sum() + " million tonnes" + Environment.NewLine;
                    text += "Import:" + Environment.NewLine;
                    foreach(var inputLink in node.InputLinks)
                        text += inputLink.TotalWeight + " million tonnes from " + inputLink.SourceNode.Tag.ToString() + Environment.NewLine;
                }
                if(node.OutputLinks != null && node.OutputLinks.Count > 0) {
                    if(!string.IsNullOrEmpty(text))
                        text += Environment.NewLine;
                    totalImportAndExportText += "Total export: " + node.OutputLinks.Select(link => link.TotalWeight).Sum() + " million tonnes" + Environment.NewLine;
                    text += "Export:" + Environment.NewLine;
                    foreach(var outputLink in node.OutputLinks)
                        text += outputLink.TotalWeight + " million tonnes to " + outputLink.TargetNode.Tag.ToString() + Environment.NewLine;
                }
                text = text.Remove(text.Length - Environment.NewLine.Length, Environment.NewLine.Length);
                if(!string.IsNullOrEmpty(totalImportAndExportText))
                    totalImportAndExportText += Environment.NewLine;
            } else if(value is SankeyLink) {
                SankeyLink link = (SankeyLink)value;
                text = String.Format("${0} million tonnes", link.TotalWeight);
            }
            return totalImportAndExportText + text;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
