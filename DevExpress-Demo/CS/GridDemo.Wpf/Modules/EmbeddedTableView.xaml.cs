using System.Windows;

namespace GridDemo {
    public partial class EmbeddedTableView : GridDemoModule {
        #region SelectedTabIndex attached property
        public static readonly DependencyProperty SelectedTabIndexProperty = DependencyProperty.RegisterAttached("SelectedTabIndex", typeof(int), typeof(EmbeddedTableView), new PropertyMetadata(0));
        public static void SetSelectedTabIndex(DependencyObject element, int value) {
            element.SetValue(SelectedTabIndexProperty, value);
        }
        public static int GetSelectedTabIndex(DependencyObject element) {
            return (int)element.GetValue(SelectedTabIndexProperty);
        }
        #endregion
        public EmbeddedTableView() {
            InitializeComponent();
        }
    }
}
