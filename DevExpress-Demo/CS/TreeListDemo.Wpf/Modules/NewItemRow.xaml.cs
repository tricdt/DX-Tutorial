using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    
    
    
    public partial class NewItemRow : TreeListDemoModule {
        public NewItemRow() {
            InitializeComponent();
        }
        void OnInitNewNode(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs e) {
            view.SetNodeValue(e.Node, "ID", view.TotalNodesCount + 1);
            view.SetNodeValue(e.Node, "StartDate", DateTime.Now);
            view.SetNodeValue(e.Node, "DueDate", DateTime.Now);
        }
    }
}
