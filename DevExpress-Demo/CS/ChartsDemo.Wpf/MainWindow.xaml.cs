using System;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.Native;

namespace ChartsDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = ViewModelSource.Create(() => new MainViewModel());
        }
    }
    public class MainViewModel : ViewModelBase {
        public MainViewModel() {
        }
    }

    public class ChartDemoControl : DemoBaseControl {
        protected override void AppearDemoModule(Option<Either<Exception, DemoModule>> result) {
            base.AppearDemoModule(result);
               if (CurrentDemoModule is ChartsDemoModule)
                ((ChartsDemoModule)CurrentDemoModule).OnStartModule();
        }

        public override void LoadModule(DemoModuleDescription moduleDescription) {
            if (CurrentDemoModule is ChartsDemoModule)
                ((ChartsDemoModule)CurrentDemoModule).OnStopModule();
            base.LoadModule(moduleDescription);
        }
    }
}
