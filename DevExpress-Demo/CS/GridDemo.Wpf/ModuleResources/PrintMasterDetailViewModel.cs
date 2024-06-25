using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    [POCOViewModel]
    public class PrintMasterDetailViewModel : PrintViewModelBase {

        protected override string GetTitle() {
            return "Print Preview";
        }
    }
}
