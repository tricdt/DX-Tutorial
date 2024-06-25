using System;
using System.Linq;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Navigation;

namespace ControlsDemo {
    public interface ITileBarService {
        void CloseFlyout();
    }
    public class TileBarService : ServiceBase, ITileBarService {
        public void CloseFlyout() {
            TileBar tileBar = AssociatedObject as TileBar;
            if(tileBar != null)
                tileBar.CloseFlyout();
        }
    }
}
