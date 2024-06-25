using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace NavigationDemo {
    [CodeFile("ViewModels/DataBindingViewModel.(cs)")]
    public partial class DataBindingDemoModule : AccordionDemoModule {
        public DataBindingDemoModule() {
            InitializeComponent();
        }
    }
    public class EmployeeDetailsTemplateSelector : DataTemplateSelector {
        public DataTemplate EmployeeDetailsTemplate { get; set; }
        public DataTemplate EmptyDetailsTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is Employee)
                return EmployeeDetailsTemplate;
            return EmptyDetailsTemplate;
        }
    }
}
