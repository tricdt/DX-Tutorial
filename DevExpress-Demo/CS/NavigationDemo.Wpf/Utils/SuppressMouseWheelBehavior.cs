using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.UI.Interactivity;

namespace NavigationDemo.Utils {
    public class SuppressMouseWheelBehavior : Behavior<UIElement> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.PreviewMouseWheel -= OnPreviewMouseWheel;
        }
        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            e.Handled = true;
            var args = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            args.RoutedEvent = UIElement.MouseWheelEvent;
            ((UIElement)sender).RaiseEvent(args);
        }
    }
}
