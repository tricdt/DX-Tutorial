using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using DevExpress.DemoData.Models;
using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    [CodeFile("ViewModels/CarsViewModel.(cs)")]
    public partial class pageCars : LayoutControlDemoModule {
        public pageCars() {
            InitializeComponent();
        }
    }

    public class CarDataTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var control = (FlowLayoutControl)container;
            return (DataTemplate)control.Resources[item.GetType().Name + "DataTemplate"];
        }
    }
}
