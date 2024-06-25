using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.Map;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public class PuzzleLayoutGenerator {
        const double HeightPadding = 0.01;
        const double WidthPadding = 0.01;

        readonly IList<Tuple<MapPath, Rect>> items;
        readonly Rect availableBounds;
        readonly ProjectionBase projection = new SphericalMercatorProjection();

        public PuzzleLayoutGenerator(IEnumerable<MapItem> items) {
            this.items = new List<Tuple<MapPath, Rect>>();
            foreach(MapPath item in items) {
                Rect itemBoundingBox = CalculateBoundingBox(item);
                this.items.Add(new Tuple<MapPath, Rect>(item, itemBoundingBox));
            }
            MapUnit leftTop = projection.GeoPointToMapUnit(new GeoPoint(15, -180));
            MapUnit rightBottom = projection.GeoPointToMapUnit(new GeoPoint(-62, -90));
            this.availableBounds = new Rect(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
        }

        Rect CalculateBoundingBox(ISupportCoordPoints item) {
            double maxLat = double.NegativeInfinity;
            double minLat = double.PositiveInfinity;
            double maxLon = double.NegativeInfinity;
            double minLon = double.PositiveInfinity;
            foreach(GeoPoint point in item.Points) {
                if(maxLat < point.Latitude)
                    maxLat = point.Latitude;
                if(minLat > point.Latitude)
                    minLat = point.Latitude;
                if(maxLon < point.Longitude)
                    maxLon = point.Longitude;
                if(minLon > point.Longitude)
                    minLon = point.Longitude;
            }
            MapUnit corner1 = this.projection.GeoPointToMapUnit(new GeoPoint(maxLat, minLon));
            MapUnit corner2 = this.projection.GeoPointToMapUnit(new GeoPoint(minLat, maxLon));
            return new Rect(corner1.X, corner1.Y, corner2.X - corner1.X, corner2.Y - corner1.Y);
        }
        public IEnumerable<MapPathInfo> GeneratePathInfos() {
            Random rnd = new Random(DateTime.Now.Millisecond);
            List<Tuple<MapPath, Rect>> unusedItems = new List<Tuple<MapPath, Rect>>(this.items);
            List<MapPathInfo> result = new List<MapPathInfo>();
            double availableHeight = this.availableBounds.Height;
            double y = this.availableBounds.Top;
            double x = this.availableBounds.Left;
            double maxWidth = 0;
            while(unusedItems.Count > 0) {
                List<Tuple<MapPath, Rect>> availableItems = new List<Tuple<MapPath, Rect>>();
                foreach(Tuple<MapPath, Rect> item in unusedItems)
                    if(item.Item2.Height < availableHeight)
                        availableItems.Add(item);
                if(availableItems.Count > 0) {
                    int index = rnd.Next(availableItems.Count);
                    Tuple<MapPath, Rect> pair = availableItems[index];
                    MapUnit gameCenter = new MapUnit(x + pair.Item2.Width / 2.0d, y + pair.Item2.Height / 2.0d);
                    result.Add(new MapPathInfo(pair.Item1, PuzzleViewModel.GetItemLocation(pair.Item1), (GeoPoint)pair.Item1.GetCenter(), projection.MapUnitToGeoPoint(gameCenter)));

                    unusedItems.Remove(pair);
                    availableHeight -= (pair.Item2.Height + HeightPadding);
                    y += pair.Item2.Height + HeightPadding;
                    if(pair.Item2.Width > maxWidth)
                        maxWidth = pair.Item2.Width;
                } else {
                    availableHeight = this.availableBounds.Height;
                    x += maxWidth + WidthPadding;
                    y = this.availableBounds.Top;
                    maxWidth = 0;
                }
            }
            return result;
        }
    }
    public class BindFunctionToSourceBehavior : Behavior<MapControl> {
        public static readonly DependencyProperty FunctionProperty = DependencyProperty.Register("Function", 
            typeof(Func<CoordPoint, Point>), typeof(BindFunctionToSourceBehavior));

        public Func<CoordPoint, Point> Function {
            get { return (Func<CoordPoint, Point>)GetValue(FunctionProperty); }
            set { SetValue(FunctionProperty, value); }
        }
        
        protected override void OnAttached() {
            base.OnAttached();
            Function = (point) => AssociatedObject.CoordPointToScreenPoint(point); 
        }
    }
}
