using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Grid;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase;

namespace PropertyGridDemo {
    
    
    
    [CodeFile("Resources/CustomTemplatesModuleTemplates.xaml")]
    [CodeFile("ViewModels/LayoutCustomizationViewModel.(cs)")]
    public partial class LayoutCustomizationModule : PropertyGridDemoModule {
        public LayoutCustomizationModule() {
            InitializeComponent();
        }
    }
}
