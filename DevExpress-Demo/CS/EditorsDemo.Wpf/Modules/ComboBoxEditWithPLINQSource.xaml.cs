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
using DevExpress.Data.PLinq.Helpers;
using GridDemo;

namespace EditorsDemo {
    public partial class ComboBoxEditWithPLINQSource : EditorsDemoModule {
        public ComboBoxEditWithPLINQSource() {
            PLinqServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            PLinqInstantFeedbackDemoViewModel viewModel = new PLinqInstantFeedbackDemoViewModel(false);
            viewModel.CountItems = new CountItemCollection(){
                new CountItem(){Count = 100000, DisplayName = "Small"},
                new CountItem(){Count=1000000, DisplayName="Medium"},
                new CountItem(){Count=3000000, DisplayName="Large" },
            };
            viewModel.SelectedCountItem = viewModel.CountItems[1];
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
