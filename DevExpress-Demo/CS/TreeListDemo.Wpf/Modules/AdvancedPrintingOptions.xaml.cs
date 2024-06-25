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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;

namespace TreeListDemo {
    public partial class AdvancedPrintingOptions : PrintTreeListDemoModule {
        public AdvancedPrintingOptions() {
            InitializeComponent();
            DXTabControl = tabControl;
            Loaded += AdvancedPrintingOptions_Loaded;
        }

        private void AdvancedPrintingOptions_Loaded(object sender, RoutedEventArgs e) {
            Loaded -= AdvancedPrintingOptions_Loaded;
            ExpandNodes();
            OnShowPrintPreviewInNewTab("TreeList Preview");
        }

        void ExpandNodes() {
            View.Nodes[0].IsExpanded = true;
            View.Nodes[0].Nodes[0].IsExpanded = true;
            View.Nodes[0].Nodes[1].IsExpanded = true;
        }
        void printStyleChooser_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(printStyleChooser.SelectedIndex > 0) {
                treeView.PrintColumnHeaderStyle = (Style)Resources["customPrintColumnHeaderStyle"];
                treeView.PrintCellStyle = (Style)Resources["customPrintCellStyle"];
                treeView.PrintRowIndentStyle = (Style)Resources["customIndentStyle"];
                treeView.PrintTotalSummaryStyle = (Style)Resources["customPrintTotalSummaryStyle"];
            }
            else {
                treeView.ClearValue(TreeListView.PrintColumnHeaderStyleProperty);
                treeView.ClearValue(TreeListView.PrintCellStyleProperty);
                treeView.ClearValue(TreeListView.PrintRowIndentStyleProperty);
                treeView.ClearValue(TreeListView.PrintTotalSummaryStyleProperty);
            }
        }
    }
}
