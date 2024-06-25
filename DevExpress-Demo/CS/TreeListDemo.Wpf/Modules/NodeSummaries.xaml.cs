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

namespace TreeListDemo {
    
    
    
    public partial class NodeSummaries : TreeListDemoModule {
        public NodeSummaries() {
            InitializeComponent();
        }
        private void OnShowingNodeFooter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListShowingNodeFooterEventArgs e) {
            if(!chkHideSingleNodeFooters.IsChecked.Value) return;
            if(e.IsRootNode || e.Node.Nodes.Where(node => node.IsVisible).Count() == 1)
                e.Allow = false;
        }
    }
}
