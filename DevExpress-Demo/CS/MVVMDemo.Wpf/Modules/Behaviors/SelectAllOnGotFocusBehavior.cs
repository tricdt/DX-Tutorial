using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Controls;

namespace MVVMDemo.Behaviors {
    public sealed class SelectAllOnGotFocusBehavior : Behavior<TextBox> {
        TextBox TextBox { get { return AssociatedObject; } }

        protected override void OnAttached() {
            base.OnAttached();
            TextBox.GotFocus += OnGotFocus;
        }

        protected override void OnDetaching() {
            TextBox.GotFocus -= OnGotFocus;
            base.OnDetaching();
        }

        void OnGotFocus(object sender, System.Windows.RoutedEventArgs e) {
            TextBox.SelectAll();
        }
    }
}
