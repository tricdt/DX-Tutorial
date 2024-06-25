using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DevExpress.Diagram.Core;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class CategorizedConnectorsExample {
        public static FrameworkElement Run() {
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            diagram.Loaded += (s, e) => diagram.FitToPage();
            diagram.ConnectorTool = new FactoryConnectorTool("Example", () => "Connector", x => new DiagramConnector() { Type = ConnectorType.Curved, BeginPointRestrictions = ConnectorPointRestrictions.KeepConnected, EndPointRestrictions = ConnectorPointRestrictions.KeepConnected });
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
            // Each time the end-user moves the cursor with the active Connector Tool near shapes or their connection points, the QueryConnectionPoints event is raised.
            // The following implementation prevents the end-user from connecting items with different background colors.

            diagram.QueryConnectionPoints += (sender, e) => {
                if(e.OppositeItem != null && e.OppositeItem.Background.ToString() != e.HoveredItem.Background.ToString()) {
                    e.ItemConnectionBorderState = ConnectionElementState.Hidden;
                    foreach(var p in e.ItemConnectionPointStates)
                        p.State = ConnectionElementState.Hidden;
                }
            };

            diagram.Items.Add(new DiagramShape() { Position = new Point(50, 40), Width = 120, Height = 50, Background = new SolidColorBrush(Color.FromRgb(0x01, 0x73, 0xC7)) });
            diagram.Items.Add(new DiagramShape() { Position = new Point(230, 120), Width = 120, Height = 50, Background = new SolidColorBrush(Color.FromRgb(0x01, 0x73, 0xC7)) });
            diagram.Items.Add(new DiagramShape() { Position = new Point(50, 200), Width = 120, Height = 50, Background = new SolidColorBrush(Color.FromRgb(0x01, 0x73, 0xC7)) });
            diagram.Items.Add(new DiagramShape() { Position = new Point(230, 40), Width = 120, Height = 50, Background = new SolidColorBrush(Color.FromRgb(0xC7, 0x73, 0x01)) });
            diagram.Items.Add(new DiagramShape() { Position = new Point(50, 120), Width = 120, Height = 50, Background = new SolidColorBrush(Color.FromRgb(0xC7, 0x73, 0x01)) });
            diagram.Items.Add(new DiagramShape() { Position = new Point(230, 200), Width = 120, Height = 50, Background = new SolidColorBrush(Color.FromRgb(0xC7, 0x73, 0x01)) });

            diagram.ActiveTool = diagram.ConnectorTool;
            // Code Example End
            return diagram;
        }
    }
}
