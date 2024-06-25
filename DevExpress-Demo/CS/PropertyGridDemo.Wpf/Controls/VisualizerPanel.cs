using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PropertyGridDemo {
    public class VisualizerPanel : StackPanel {
        public event EventHandler Changed;
        protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved) {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            Dispatcher.BeginInvoke(new Action(RaiseChanged));
        }
        private void RaiseChanged() {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }
}
