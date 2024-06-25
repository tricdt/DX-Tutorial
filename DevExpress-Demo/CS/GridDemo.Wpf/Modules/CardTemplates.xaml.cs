using System.Windows;

namespace GridDemo {
    public partial class CardTemplates : GridDemoModule {
        #region SelectedTabIndex attached property
        public static readonly DependencyProperty SelectedTabIndexProperty
            = DependencyProperty.RegisterAttached("SelectedTabIndex", typeof(int), typeof(CardTemplates),
                new PropertyMetadata(0, null, (d, baseValue) => ((int)baseValue == -1) ? GetSelectedTabIndex(d) : baseValue));
        public static void SetSelectedTabIndex(DependencyObject element, int value) {
            element.SetValue(SelectedTabIndexProperty, value);
        }
        public static int GetSelectedTabIndex(DependencyObject element) {
            return (int)element.GetValue(SelectedTabIndexProperty);
        }
        #endregion

        public CardTemplates() {
            InitializeComponent();
        }
    }
}
