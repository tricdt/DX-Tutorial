using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class RestrictedPartOfTheCanvasExample {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            DiagramShape restrictedPartMarker = new DiagramShape() { Position = new Point(180, 50), Width = 180, Height = 200, StrokeDashArray = new DoubleCollection(new[] { 8d, 8d }), Stroke = new SolidColorBrush(Color.FromRgb(0xC8, 0x14, 0x14)), Background = Brushes.Transparent, StrokeThickness = 3 };
            restrictedPartMarker.CanMove = restrictedPartMarker.CanSelect = restrictedPartMarker.CanResize = restrictedPartMarker.CanRotate = false;
            // Code Example Start
            // Each time the end-user tries to move a diagram item, the ItemsMoving event is raised.
            // The following implementation prevents items from being moved in a certain area of the canvas.

            diagram.ItemsMoving += (sender, e) => {
                foreach(MovingItem c in e.Items) {
                    double x1 = restrictedPartMarker.Position.X - c.Item.Width;
                    double x2 = restrictedPartMarker.Position.X + restrictedPartMarker.Width;
                    double y1 = restrictedPartMarker.Position.Y - c.Item.Height;
                    double y2 = restrictedPartMarker.Position.Y + restrictedPartMarker.Height;
                    if(c.NewDiagramPosition.X > x1 && c.NewDiagramPosition.X < x2 && c.NewDiagramPosition.Y > y1 && c.NewDiagramPosition.Y < y2) {
                        double coercedX = c.NewDiagramPosition.X - x1 < (x2 - x1) / 2d ? x1 : x2;
                        double coercedY = c.NewDiagramPosition.Y - y1 < (y2 - y1) / 2d ? y1 : y2;
                        if(Math.Abs(coercedX - c.NewDiagramPosition.X) < Math.Abs(coercedY - c.NewDiagramPosition.Y))
                            c.NewDiagramPosition = new Point(coercedX, c.NewDiagramPosition.Y);
                        else
                            c.NewDiagramPosition = new Point(c.NewDiagramPosition.X, coercedY);
                    }
                }
            };

            var item = new DiagramShape() { Position = new Point(50, 100), Width = 100, Height = 100, CanRotate = false, CanResize = false };
            diagram.Items.Add(item);
            diagram.Items.Add(restrictedPartMarker);
            diagram.SelectItem(item);
            // Code Example End
            return diagram;
        }
    }
}
