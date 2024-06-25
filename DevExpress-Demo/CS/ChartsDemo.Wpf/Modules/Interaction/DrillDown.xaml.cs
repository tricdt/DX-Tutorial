using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interaction/DrillDown.xaml"),
     CodeFile("Modules/Interaction/DrillDown.xaml.(cs)"),
     CodeFile("Modules/Interaction/DrillDownViewModel.(cs)"),
     CodeFile("DataModels/DevAVSalesData.(cs)"),
     CodeFile("DataModels/DevAVHierarchicalSalesData.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class DrillDownDemo : ChartsDemoModule {
        public DrillDownDemo() {
            InitializeComponent();
        }
    }

    class ChildrenSelector : MarkupExtension, IChildrenSelector {
        public DataTemplate BranchTemplate { get; set; }
        public DataTemplate CategoryTemplate { get; set; }
        public DataTemplate ProductTemplate { get; set; }

        IEnumerable IChildrenSelector.SelectChildren(object item) {
            if (item is DevAVBranch)
                return new List<DevAVBranch> { (DevAVBranch)item };
            if (item is DevAVProductCategory)
                return ((DevAVProductCategory)item).Products;
            if (item is DevAVProduct)
                return new List<DevAVProduct> { (DevAVProduct)item };
            else
                return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }


    class DevAVSeriesTemplateSelector : DataTemplateSelector {
        public DataTemplate BranchStackedBarTemplate { get; set; }
        public DataTemplate BranchColorEachBarTemplate { get; set; }
        public DataTemplate SingleProductTemplate { get; set; }
        public DataTemplate ProductTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            Diagram diagram = (Diagram)container;
            if (item is DevAVBranch && diagram.BreadcrumbItems.Count == 1)
                return BranchStackedBarTemplate;
            if (item is DevAVBranch)
                return BranchColorEachBarTemplate;
            if (item is DevAVProduct && diagram.BreadcrumbItems[diagram.BreadcrumbItems.Count - 1].SourceObject is DevAVProduct)
                return SingleProductTemplate;
            if (item is DevAVProduct)
                return ProductTemplate;
            else
                return null;
        }
    }

    class DrillDownStateChangedEventArgsConverter : EventArgsConverterBase<DrillDownStateChangedEventArgs> {
        protected override object Convert(object sender, DrillDownStateChangedEventArgs args) {
            List<object> breadcrumbPath = new List<object>(args.BreadcrumbItems.Count);
            foreach (BreadcrumbItem breadcrumbItem in args.BreadcrumbItems)
                breadcrumbPath.Add(breadcrumbItem.SourceObject);
            return breadcrumbPath;
        }
    }
}