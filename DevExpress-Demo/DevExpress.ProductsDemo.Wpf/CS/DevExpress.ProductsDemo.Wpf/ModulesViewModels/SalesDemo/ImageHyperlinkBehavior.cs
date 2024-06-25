using DevExpress.Mvvm.UI.Interactivity;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductsDemo {
    public class ImageHyperlinkBehavior : Behavior<Image> {
        public static readonly DependencyProperty NavigatetUriProperty =
            DependencyProperty.Register("NavigatetUri", typeof(string), typeof(ImageHyperlinkBehavior), new PropertyMetadata(null));
        public string NavigatetUri {
            get { return (string)GetValue(NavigatetUriProperty); }
            set { SetValue(NavigatetUriProperty, value); }
        }
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonUp += OnImageMouseLeftButtonUp;
        }
        protected override void OnDetaching() {
            AssociatedObject.MouseLeftButtonUp -= OnImageMouseLeftButtonUp;
            base.OnDetaching();
        }
        void OnImageMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Process.Start(new ProcessStartInfo { FileName = NavigatetUri.ToString(), UseShellExecute = true });
        }
    }
}
