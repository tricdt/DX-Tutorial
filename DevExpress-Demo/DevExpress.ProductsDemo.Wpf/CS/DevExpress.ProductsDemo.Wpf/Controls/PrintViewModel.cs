using DevExpress.Mvvm;
using DevExpress.Xpf.Printing;
using System;
namespace ProductsDemo {
    public class PrintViewModel : ViewModelBase {        
        public virtual PrintableControlLink PrintableControlLink { get; set; }

        public void UpdatePrintableControlLink() {
            if(PrintingService.PrintableControlLink != null) {
                PrintableControlLink = PrintingService.PrintableControlLink;
                PrintableControlLink.CreateDocument(true);
            }
        }
    }
    public static class PrintingService {
        public static PrintableControlLink PrintableControlLink;

        public static bool HasPrinting {
            get {
                return PrintableControlLink != null;
            }
        }
    }
}
