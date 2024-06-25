using System.Windows.Controls;
using System.Windows;
using DevExpress.DevAV;
using System.Windows.Media;
using System.Reflection;

namespace DevExpress.DevAV.Views {
    public partial class CustomerCollectionView : UserControl {
        public CustomerCollectionView() {
            InitializeComponent();
        }
    }
    public class SlideViewTemplateSelector : DataTemplateSelector {
        public DataTemplate ContactsTemplate { get; set; }
        public DataTemplate StoresTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is CustomerEmployee)
                return ContactsTemplate;
            if(item is CustomerStore)
                return StoresTemplate;
            return base.SelectTemplate(item, container);
        }
    }
}
