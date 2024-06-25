using System;
using System.Linq;

namespace ControlsDemo {
    public class TileBarViewModel : TileNavBaseViewModel {
        ITileBarService TileBarService { get { return ServiceContainer.GetService<ITileBarService>(); } }

        protected override void OnNavigationMessage(NavigationMessage message) {
            base.OnNavigationMessage(message);
            TileBarService.CloseFlyout();
        }
    }
}
