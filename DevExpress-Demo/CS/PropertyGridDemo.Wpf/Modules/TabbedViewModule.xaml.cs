using DevExpress.Xpf.DemoBase;
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

namespace PropertyGridDemo {    
    [CodeFile("ViewModels/TabbedViewModule/Model/Metadata/BorderOptions.(cs)")]
    [CodeFile("ViewModels/TabbedViewModule/Model/Metadata/Effects.(cs)")]
    [CodeFile("ViewModels/TabbedViewModule/Model/Metadata/FillOptions.(cs)")]
    [CodeFile("ViewModels/TabbedViewModule/Model/Metadata/SeriesOptions.(cs)")]

    [CodeFile("Resources/TabbedViewModuleResources/Resources.xaml")]
    [CodeFile("Resources/TabbedViewModuleResources/Templates/Effects.xaml")]
    [CodeFile("Resources/TabbedViewModuleResources/Templates/FillnLine.xaml")]
    [CodeFile("Resources/TabbedViewModuleResources/Templates/Misc.xaml")]
    [CodeFile("Resources/TabbedViewModuleResources/Templates/SeriesOptions.xaml")]
    public partial class TabbedViewModule {
        public TabbedViewModule() {
            InitializeComponent();
        }
        void OnCustomExpand(object sender, DevExpress.Xpf.PropertyGrid.CustomExpandEventArgs args) {
            args.IsExpanded = true;
        }
    }
}
