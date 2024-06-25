using System;
using System.Linq;
using System.Windows;
using DevExpress.Diagram.Core;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class ProhibitingActions1Example {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            // Code Example Start
            // Each time the end-user tries to perform an action on a diagram item, the QueryItemsAction event is raised.
            // The following implementation restricts certain actions based on the item's content.

            diagram.QueryItemsAction += (sender, e) => {
                switch(e.Action) {
                case ItemsActionKind.MoveCopy:
                case ItemsActionKind.Move:
                    if(e.Items.OfType<DiagramShape>().Any(x => x.Content == "Non-movable"))
                        e.Allow = false;
                    break;
                case ItemsActionKind.Resize:
                    if(e.Items.OfType<DiagramShape>().Any(x => x.Content == "Non-resizable"))
                        e.Allow = false;
                    break;
                case ItemsActionKind.Rotate:
                    if(e.Items.OfType<DiagramShape>().Any(x => x.Content == "Non-rotatable"))
                        e.Allow = false;
                    break;
                }
            };

            diagram.Items.Add(new DiagramShape() { Shape = BasicShapes.Ellipse, Content = "Non-movable", Position = new Point(22.5, 40), Width = 120, Height = 50 });
            diagram.Items.Add(new DiagramShape() { Shape = BasicShapes.Rectangle, Content = "Non-resizable", Position = new Point(142.5, 120), Width = 120, Height = 50 });
            diagram.Items.Add(new DiagramShape() { Shape = BasicShapes.Parallelogram, Content = "Non-rotatable", Position = new Point(262.5, 200), Width = 120, Height = 50 });
            // Code Example End
            return diagram;
        }
    }
}
