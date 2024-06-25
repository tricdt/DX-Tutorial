using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using System.Windows;
namespace BarsDemo {
    [CodeFiles("Modules/SimplePad.xaml",
               "Modules/SimplePad.xaml.(cs)",
               "ViewModels/SimplePadViewModel.(cs)")]
    public partial class SimplePad : BarsDemoModule {
        public SimplePad() {
            InitializeComponent();
            ModuleLoaded += OnModuleLoaded;
        }

        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            if(!this.IsInDesignTool()) {
                richControl.SetFocus();
            }
        }
    }
}
