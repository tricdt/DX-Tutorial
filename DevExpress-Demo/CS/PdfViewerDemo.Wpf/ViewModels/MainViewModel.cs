using DevExpress.Mvvm;
using System;
using System.IO;
using System.Windows;

namespace PdfViewerDemo {
    public class MainViewModel {
        public virtual Stream PdfStream { get; set; }
    }
}
