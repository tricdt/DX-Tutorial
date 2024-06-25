using DevExpress.Mvvm.UI;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public interface IPivotGridDemoService {
        void BeginUpdate();
        void CollapseAll();
        void EndUpdate();
    }

    public class PivotGridDemoService : ViewServiceBase, IPivotGridDemoService {
        public PivotGridControl PivotGrid { get { return AssociatedObject as PivotGridControl; } }

        public void BeginUpdate() {
            PivotGrid.BeginUpdate();
        }
        public void CollapseAll() {
            PivotGrid.CollapseAll();
        }
        public void EndUpdate() {
            PivotGrid.EndUpdate();
        }
    }
}
