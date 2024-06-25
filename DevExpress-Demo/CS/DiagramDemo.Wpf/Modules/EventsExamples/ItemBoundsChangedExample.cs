using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Diagram.Core;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class ItemBoundsChangedExample {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300), EnableProportionalResizing = false };
            diagram.Loaded += (s, e) => {
                diagram.Items.OfType<DiagramContentItem>().First(x => Equals(x.Tag, Orientation.Vertical)).Content = new VRuler();
                diagram.Items.OfType<DiagramContentItem>().First(x => Equals(x.Tag, Orientation.Horizontal)).Content = new HRuler();
                diagram.FitToPage();
            };
            diagram.QueryItemsAction += (sender, e) => {
                if(e.Action != ItemsActionKind.Resize && e.Items.Any(x => x.Tag is Orientation)) {
                    e.Allow = false;
                    return;
                }
                if(e.Items.Any(x => !(x.Tag is Orientation)))
                    e.Allow = false;
            };
            diagram.QueryItemSnapping += (sender, e) => {
                if(diagram.Items.Any(x => x.Tag is Orientation))
                    e.Allow = false;
            };
            diagram.ItemsResizing += (sender, e) => {
                foreach(ResizingItem c in e.Items) {
                    if(c.Item.Tag is Orientation && c.NewDiagramPosition != c.OldDiagramPosition) {
                        c.NewSize = c.OldSize;
                        c.NewDiagramPosition = c.OldDiagramPosition;
                    } else if(Equals(c.Item.Tag, Orientation.Vertical)) {
                        c.NewSize = new Size(c.OldSize.Width, c.NewSize.Height);
                    } else if(Equals(c.Item.Tag, Orientation.Horizontal)) {
                        c.NewSize = new Size(c.NewSize.Width, c.OldSize.Height);
                    }
                }
            };
            // Code Example Start
            // Each time the end-user tries to modify the bounds a diagram item, the ItemsBoundsChanged event is raised.
            // The following implementation allows the end-user to change the size of the ellipse by resizing the ruler items that are tagged by their orientation.

            DiagramShape item = new DiagramShape() { Position = new Point(65, 65), Width = 200, Height = 160, CanRotate = false, CanMove = false };

            diagram.ItemBoundsChanged += (sender, e) => {
                if(Equals(e.Item.Tag, Orientation.Vertical)) {
                    item.Height = e.NewSize.Height;
                    item.Position = new Point(item.Position.X, e.NewPosition.Y);
                } else if(Equals(e.Item.Tag, Orientation.Horizontal)) {
                    item.Width = e.NewSize.Width;
                    item.Position = new Point(e.NewPosition.X, item.Position.Y);
                }
            };

            diagram.Items.Add(item);
            DiagramContentItem vRuler = new DiagramContentItem() { Position = new Point(25, 65), Width = 20, Height = 160, Background = Brushes.Transparent, Tag = Orientation.Vertical };
            DiagramContentItem hRuler = new DiagramContentItem() { Position = new Point(65, 25), Width = 200, Height = 20, Background = Brushes.Transparent, Tag = Orientation.Horizontal };
            diagram.Items.Add(vRuler);
            diagram.Items.Add(hRuler);
            diagram.SelectItem(hRuler);
            // Code Example End
            return diagram;
        }
    }
    public class VRuler : Decorator {
        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
            var pen = new Pen(Brushes.Black, 1);
            drawingContext.DrawRectangle(Brushes.Black, pen, new Rect(0, 0, 2, RenderSize.Height));
            for(double y = 0d; y < RenderSize.Height; y += 10d)
                drawingContext.DrawRectangle(Brushes.Black, pen, new Rect(0, y, 8, 2));
            for(double y1 = 0d; y1 < RenderSize.Height; y1 += 20d)
                drawingContext.DrawRectangle(Brushes.Black, pen, new Rect(0, y1, 12, 2));
        }
    }
    public class HRuler : Decorator {
        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
            var pen = new Pen(Brushes.Black, 1);
            drawingContext.DrawRectangle(Brushes.Black, pen, new Rect(0, 0, RenderSize.Width, 2));
            for(double x = 0d; x < RenderSize.Width; x += 10d)
                drawingContext.DrawRectangle(Brushes.Black, pen, new Rect(x, 0, 2, 8));
            for(double x1 = 0d; x1 < RenderSize.Width; x1 += 20d)
                drawingContext.DrawRectangle(Brushes.Black, pen, new Rect(x1, 0, 2, 12));
        }
    }
}
