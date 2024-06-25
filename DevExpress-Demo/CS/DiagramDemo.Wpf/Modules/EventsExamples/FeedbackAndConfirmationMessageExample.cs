using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Diagram.Core;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public static class FeedbackAndConfirmationMessageExample {
        public static FrameworkElement Run() {
            var panel = new Grid();
            var diagram = new DiagramControl() { PageSize = new Size(400, 300) };
            panel.Children.Add(diagram);
            diagram.Loaded += (s, e) => diagram.FitToPage();
            var label = new TextBlock() { Margin = new Thickness(30), IsHitTestVisible = false, FontSize = 24 };
            panel.Children.Add(label);
            // Code Example Start
            // Each time the end-user tries to move a diagram item, the ItemsMoving event is raised.
            // The following implementation displays a label as the end-user is moving an item and invokes the confirmation dialog window prompting the user to confirm the action.

            diagram.ItemsMoving += (sender, e) => {
                switch(e.Stage) {
                case DiagramActionStage.Start:
                    label.Text = "Moving...";
                    break;
                case DiagramActionStage.Canceled:
                    label.Text = "";
                    break;
                case DiagramActionStage.Finished:
                    var result = ThemedMessageBox.Show(Window.GetWindow(diagram), "Confirmation", "Confirm the moving action.", MessageBoxButton.OKCancel, null, MessageBoxImage.None);
                    if(result != MessageBoxResult.OK)
                        e.Cancel = true;
                    goto case DiagramActionStage.Canceled;
                }
            };

            DiagramShape item1 = new DiagramShape() { Shape = BasicShapes.Ellipse, Position = new Point(30, 50), Width = 120, Height = 80 };
            DiagramShape item2 = new DiagramShape() { Shape = BasicShapes.Hexagon, Position = new Point(30, 150), Width = 120, Height = 80 };
            diagram.Items.Add(item1);
            diagram.Items.Add(item2);
            diagram.SelectItems(item1, item2);
            // Code Example End
            return panel;
        }
    }
}
