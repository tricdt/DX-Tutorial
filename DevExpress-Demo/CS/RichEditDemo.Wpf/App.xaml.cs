using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoCenterBase;

namespace WpfDemo {
    public partial class App : Application {
        static App() {
            DemoBaseControl.SetApplicationTheme();
            DemoRunner.ShowApplicationSplashScreen(allowDrag: true);
        }
#if DEBUG
        public bool IsDebug { get { return true; } }
#endif
    }
}
