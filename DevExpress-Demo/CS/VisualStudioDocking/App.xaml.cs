using DevExpress.Xpf.DemoBase;
using System.Windows;

namespace VisualStudioDocking {
    public partial class App : Application {
        static App() {
            DemoBaseControl.SetApplicationTheme();
        }
#if DEBUG
        public bool IsDebug { get { return true; } }
#endif
    }
}
