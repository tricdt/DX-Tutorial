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
using Microsoft.Win32;
using System.IO;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Docking;
using RibbonDemo;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase;

namespace RibbonDemo {
    [CodeFiles(@"Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml;
                 Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml.(cs);
                 Modules/RibbonMerging/Views/PaintUserControl.xaml;
                 Modules/RibbonMerging/Views/PaintUserControl.xaml.(cs);
                 Modules/RibbonMerging/Views/TextUserControl.xaml;
                 Modules/RibbonMerging/Views/TextUserControl.xaml.(cs);                 
                 Modules/RibbonMerging/ViewModels/PaintPanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/TextPanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/PanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/RibbonMergingViewModel.(cs);")]    

    public partial class RibbonMergingUserControl : RibbonDemoModule {
        public RibbonMergingUserControl() {
            InitializeComponent();
            Ribbon = mainRibbon;
        }

        protected override void Hide() {
            mainRibbon.CloseApplicationMenu();
            base.Hide();
        }
    }
}
