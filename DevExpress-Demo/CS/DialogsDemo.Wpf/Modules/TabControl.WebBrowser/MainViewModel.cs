using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace CommonDemo.TabControl.WebBrowser {
    public class MainViewModel {
        public void LaunchDemo() {
            this.GetService<IWindowService>().Show("WebBrowserView", null, this);
        }
    }
}
