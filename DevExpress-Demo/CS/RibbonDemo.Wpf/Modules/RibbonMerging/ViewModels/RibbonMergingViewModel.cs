using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
namespace RibbonDemo {
    public class RibbonMergingViewModel {
        public RibbonMergingViewModel() {
            Panels = new ObservableCollection<PanelViewModel>();
            Panels.Add(TextPanelViewModel.Create("Simple Pad", new Point(0.0, 0.0), this));
            Panels.Add(PaintPanelViewModel.Create("Simple Paint", new Point(300.0, 50.0), this));
        }
        public virtual IList<PanelViewModel> Panels { get; set; }
        public void CreateNewTextPanel() {
            Panels.Add(TextPanelViewModel.Create("Simple Pad", new Point(50, 50), this));
            Panels[Panels.Count - 1].IsActive = true;
        }
        public void CreateNewPaintPanel() {
            Panels.Add(PaintPanelViewModel.Create("Simple Paint", new Point(70, 60), this));
            Panels[Panels.Count - 1].IsActive = true;
        }
    }
}
