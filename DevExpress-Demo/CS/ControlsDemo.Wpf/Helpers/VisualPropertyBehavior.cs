using DevExpress.Mvvm.UI.Interactivity;
using System.Windows;
using System.Windows.Media;

namespace ControlsDemo {
    public class VisualPropertyBehavior : Behavior<Visual> {
        public Visual Visual {
            get { return (Visual)GetValue(VisualProperty); }
            set { SetValue(VisualProperty, value); }
        }
        public static readonly DependencyProperty VisualProperty =
            DependencyProperty.Register("Visual", typeof(Visual), typeof(VisualPropertyBehavior), new PropertyMetadata(null));
        protected override void OnAttached() {
            base.OnAttached();
            Visual = AssociatedObject;
            ((FrameworkElement)AssociatedObject).Unloaded += VisualPropertyBehavior_Unloaded;
        }
        void VisualPropertyBehavior_Unloaded(object sender, RoutedEventArgs e) {
            Visual = null;
            ((FrameworkElement)AssociatedObject).Unloaded -= VisualPropertyBehavior_Unloaded;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            Visual = null;
        }
    }
}
