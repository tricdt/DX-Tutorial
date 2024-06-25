using System.Linq;
using System.Windows;
using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Native;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class ExpandContainerOnDragOver {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };

            // Code Example Start
            // Each time the end-user tries to move a diagram item, the ItemsMoving event is raised.
            // The following implementation expands a collapsed container when the end-user is dragging an item over it.
            var shape = new DiagramShape() { Position = new Point(150, 130), Width = 100, Height = 80, Content = "Drag me over the container" };
            var container = new DiagramContainer {
                Position = new Point(100, 50),
                Width = 200,
                Height = 150,
                ShowHeader = true,
                Header = "Collapsed container",
                CanCollapse = true,
                IsCollapsed = true,
                CanCopy = false,
                CanMove = false,
            };

            var containerCollapseState = false;
            diagram.ItemsMoving += (s, e) => {
                if(e.ActionSource != ItemsActionSource.Mouse)
                    return;
                if(e.Stage == DiagramActionStage.Start) {
                    containerCollapseState = container.IsCollapsed;
                } else if(e.Stage == DiagramActionStage.Canceled)
                    container.IsCollapsed = containerCollapseState;
                else if(containerCollapseState) {
                    var containerBounds = container.DiagramBounds();
                    container.IsCollapsed = !e.Items.Any(x => containerBounds.IntersectsWith(new Rect(x.NewDiagramPosition, x.Item.RenderSize)));
                }
            };

            diagram.Items.Add(shape);
            diagram.Items.Add(container);
            diagram.SelectItem(shape);
            // Code Example End
            return diagram;
        }
    }
}
