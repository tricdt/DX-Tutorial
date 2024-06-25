using DevExpress.Map;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class ShapeImage : MapDemoModule {
        CoordPoint initialCenterPoint;

        public ShapeImage() {
            InitializeComponent();
            initialCenterPoint = map.CenterPoint;
        }
        void ImageLayer_ViewportChanged(object sender, ViewportChangedEventArgs e) {
            if (!e.IsAnimated && e.ZoomLevel <= 14) {
                CoordPoint center = initialCenterPoint ?? map.CenterPoint;
                double xOffset = (e.BottomRight.GetX() - e.TopLeft.GetX()) / 2d;
                double yOffset = (e.BottomRight.GetY() - e.TopLeft.GetY()) / 2d;
                map.ScrollArea = new MapBounds(center.Offset(-xOffset, -yOffset), center.Offset(xOffset, yOffset));
            }
        }
    }
}
