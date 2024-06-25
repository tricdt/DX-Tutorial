using System.Reflection;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;

namespace PdfViewerDemo {
    [CodeFile("ViewModels/MainViewModel.(cs)")]
    public partial class MainDemoModule : PdfViewerDemoModule {
        public MainDemoModule() {
            InitializeComponent();
            var currentAssembly = Assembly.GetExecutingAssembly();
            DataContext = ViewModelSource.Create(() =>
                new MainViewModel { PdfStream = AssemblyHelper.GetResourceStream(currentAssembly, "Data/Demo.pdf", true) });
        }
    }
}
