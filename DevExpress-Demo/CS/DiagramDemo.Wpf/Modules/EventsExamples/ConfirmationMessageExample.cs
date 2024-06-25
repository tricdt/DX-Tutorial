using System;
using System.Linq;
using System.Windows;
using DevExpress.Diagram.Core;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class ConfirmationMessageExample {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            diagram.QueryItemsAction += (sender, e) => {
                if(e.Action == ItemsActionKind.Move)
                    e.Allow = false;
            };
            diagram.QueryConnectionPoints += (sender, e) => {
                if(Equals(e.OppositeItem, e.HoveredItem)) {
                    e.ItemConnectionBorderState = ConnectionElementState.Hidden;
                    foreach(ConnectionPoint p in e.ItemConnectionPointStates)
                        p.State = ConnectionElementState.Hidden;
                }
            };
            diagram.ItemsChanged += (sender, e) => {
                if(e.Action == ItemsChangedAction.Added && e.Item is DiagramConnector) {
                    ((DiagramConnector)e.Item).CanChangeRoute = false;
                    e.Item.StrokeThickness = 2;
                    e.Item.StrokeId = DiagramThemeColorId.Black_3;
                }
            };
            // Code Example Start
            // Each time the end-user tries to modify a connector, the ConnectionChanging event is raised.
            // The following implementation invokes the confirmation dialog window prompting the user to confirm the action.

            diagram.ConnectionChanging += (sender, e) => {
                var result = ThemedMessageBox.Show(Window.GetWindow(diagram), "Confirmation", "Confirm the connection changing action.", MessageBoxButton.OKCancel, null, MessageBoxImage.None);
                if(result != MessageBoxResult.OK)
                    e.Cancel = true;
            };

            DiagramShape item1 = new DiagramShape() { Shape = BasicShapes.Parallelogram, Position = new Point(150, 40), Width = 120, Height = 80 };
            diagram.Items.Add(item1);
            DiagramShape item2 = new DiagramShape() { Shape = BasicShapes.Ellipse, Position = new Point(250, 200), Width = 120, Height = 80 };
            diagram.Items.Add(item2);
            diagram.Items.Add(new DiagramShape() { Shape = BasicShapes.Hexagon, Position = new Point(30, 200), Width = 120, Height = 80 });
            DiagramConnector connector = new DiagramConnector() { BeginItem = item1, EndItem = item2 };
            diagram.Items.Add(connector);
            diagram.SelectItem(connector);
            // Code Example End
            return diagram;
        }
    }
}
