using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;

namespace DockingDemo {
    public partial class DockHintsVisualizer : DockingDemoModule {
        public DockHintsVisualizer() {
            InitializeComponent();
            DemoDockContainer.ShowingDockHints += OnShowingDockHints;
        }
        public bool AllowVisualizerEventsChecked {
            get { return allowVisualizerEvents.IsChecked.Value; }
        }
        void OnShowingDockHints(object sender, ShowingDockHintsEventArgs e) {
            if(!AllowVisualizerEventsChecked) return;
            if(Equals(e.DraggingTarget, Panel1) || (e.DraggingTarget is LayoutGroup)) {
                e.HideAll();
                return;
            }
            if(Equals(e.DraggingTarget, Panel2)) {
                return;
            }
            if(Equals(e.DraggingTarget, Panel3)) {
                e.DisableAll();
                return;
            }
            if(Equals(e.DraggingTarget, Panel4)) {
                e.Disable(DockHint.SideLeft);
                e.Disable(DockHint.AutoHideLeft);
                e.Disable(DockHint.CenterLeft);
                e.Disable(DockHint.CenterRight);
                e.Hide(DockGuide.Top);
                e.Hide(DockGuide.Bottom);
                return;
            }
        }
    }
}
