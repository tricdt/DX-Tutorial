using System;
using System.Windows.Controls;

namespace GridDemo.Filtering {
    public partial class IncrementalSearch : UserControl {
        public IncrementalSearch() {
            InitializeComponent();
            view.IncrementalSearchStart("M");
        }
    }
}
