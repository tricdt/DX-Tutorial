using System;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;
using ResizeMode = DevExpress.Diagram.Core.ResizeMode;

namespace DiagramDemo {
    public static class MaxWidthExample {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300), EnableProportionalResizing = false };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            // Code Example Start
            // Each time the end-user tries to resize a diagram item, the ItemsResizing event is raised.
            // The following implementation sets the maximum width based on the item's content.

            diagram.ItemsResizing += (sender, e) => {
                foreach(ResizingItem c in e.Items) {
                    double maxWidth = 0d;
                    if(c.Item is DiagramShape)
                        double.TryParse(((DiagramShape)c.Item).Content, out maxWidth);
                    double widthOver = c.NewSize.Width - maxWidth;
                    if(widthOver <= 0d) continue;

                    c.NewSize = new Size(maxWidth, c.NewSize.Height);
                    if(e.Mode == ResizeMode.Left || e.Mode == ResizeMode.TopLeft || e.Mode == ResizeMode.BottomLeft)
                        c.NewDiagramPosition = new Point(c.NewDiagramPosition.X + widthOver, c.NewDiagramPosition.Y);
                }
            };

            diagram.Items.Add(new DiagramShape() { Content = "300", Position = new Point(60, 75), Width = 280, Height = 150, CanRotate = false, FontSize = 24 });
            // Code Example End
            return diagram;
        }
    }
}
