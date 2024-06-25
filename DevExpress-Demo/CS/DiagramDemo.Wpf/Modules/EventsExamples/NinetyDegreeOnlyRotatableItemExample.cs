using System;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class NinetyDegreeOnlyRotatableItemExample {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            // Code Example Start
            // Each time the end-user tries to rotate a diagram item, the ItemsRotating event is raised.
            // The following implementation sets the fixed 90-degree angle on which the end-user can rotate items.

            diagram.ItemsRotating += (sender, e) => {
                foreach(RotatingItem c in e.Items)
                    c.NewAngle = Math.Round(c.NewAngle / 90d) * 90d;
            };

            DiagramShape item = new DiagramShape() { Position = new Point(100, 70), Width = 200, Height = 160, CanResize = false };
            diagram.Items.Add(item);
            diagram.SelectItem(item);
            // Code Example End
            return diagram;
        }
    }
}
