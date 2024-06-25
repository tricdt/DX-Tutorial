using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.WindowsUI;
using DevExpress.Xpf.WindowsUI.Navigation;

namespace WindowsUIDemo.Modules.Views {
    
    
    
    public partial class SlideViewEmployeesDetailsView : NavigationPage {
        public static readonly DependencyProperty ParameterProperty = 
            DependencyProperty.Register("Parameter", typeof(object), typeof(SlideViewEmployeesDetailsView), null);
        public SlideViewEmployeesDetailsView() {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            Parameter = e.Parameter;
        }
        private void DataLayoutControl_AutoGeneratingItem(object sender, DevExpress.Xpf.LayoutControl.DataLayoutControlAutoGeneratingItemEventArgs e) {
            var except = new[] { "Photo", "SubEmployee", "EmployeeID" };
            if(except.Contains(e.PropertyName)) e.Cancel = true;
        }
        public object Parameter {
            get { return (object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }
    }
}
