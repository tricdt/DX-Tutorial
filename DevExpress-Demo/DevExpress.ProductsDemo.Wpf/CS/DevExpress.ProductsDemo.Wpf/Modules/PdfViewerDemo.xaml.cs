using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Xpf.PdfViewer.Helpers;

namespace ProductsDemo.Modules {
    
    
    
    public partial class PdfViewerDemo : UserControl {
        public PdfViewerDemo() {
            InitializeComponent();
            object source = Utils.GetRelativePath("Demo.pdf");
            DataContext = ViewModelSource.Create(() => new MainViewModel { PdfSource = source});
        }
    }
    public class MainViewModel {
        public virtual object PdfSource { get; set; }
        public void ShowNewDocument() {
        }
    }
    public class PdfDocumentAttachedBehavior : Behavior<PdfViewerControl> {
        MainViewModel Model { get { return AssociatedObject.DataContext as MainViewModel; } }
    }
}
