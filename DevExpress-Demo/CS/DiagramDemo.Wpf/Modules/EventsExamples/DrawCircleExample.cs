using System;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class DrawCircleExample {
        public static FrameworkElement Run() {
            DiagramControl diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            // Code Example Start
            // Each time the end-user tries to draw a diagram item, the ItemDrawing event is raised.
            // The following implementation constrains the drawing tool to draw regular polygons (a circle in case of the Ellipse tool selected by default).

            diagram.ItemDrawing += (sender, e) => {
                double width = e.EndPosition.X - e.StartPosition.X;
                double height = e.EndPosition.Y - e.StartPosition.Y;
                if(Math.Abs(height) > Math.Abs(width))
                    e.EndPosition = new Point(e.StartPosition.X + Math.Sign(width) * Math.Abs(height), e.EndPosition.Y);
                else
                    e.EndPosition = new Point(e.EndPosition.X, e.StartPosition.Y + Math.Sign(height) * Math.Abs(width));
            };

            diagram.ActiveTool = diagram.EllipseTool;
            // Code Example End
            return diagram;
        }
    }
}
