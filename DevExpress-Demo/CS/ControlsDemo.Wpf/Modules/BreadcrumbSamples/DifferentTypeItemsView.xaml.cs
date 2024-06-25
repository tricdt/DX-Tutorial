using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Controls;

namespace ControlsDemo.BreadcrumbSamples {
    public partial class DifferentTypeItemsView : UserControl {
        public DifferentTypeItemsView() {
            InitializeComponent();
            breadcrumb.ItemsSource = NWindContext.Create().Categories.OrderBy(x => x.CategoryName);
            breadcrumb.ChildSelector = new NWindChildSelector();
            breadcrumb.CustomDisplayText += GetCustomText;
            breadcrumb.CustomImage += GetCustomImage;
            breadcrumb.EmptyItemText = "Select Category";
        }
        void GetCustomImage(object sender, BreadcrumbCustomImageEventArgs e) {
            e.Image = NWindObjectHelper.GetCustomImage(e.Item);
        }
        void GetCustomText(object sender, BreadcrumbCustomDisplayTextEventArgs e) {
            e.DisplayText = NWindObjectHelper.GetCustomText(e.Item);
        }
    }
}
