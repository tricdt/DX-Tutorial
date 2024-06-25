using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    public class PrintCardViewViewModel : PrintViewModelBase {
        public  bool UseCustomPrintStyles {
            get { return GetProperty(() => UseCustomPrintStyles); }
            set { SetProperty(() => UseCustomPrintStyles, value); }
        }

        public override void ReOpenPreviewTabs() {
            base.ReOpenPreviewTabs();
            UseCustomPrintStyles = true;
            ShowPreviewInNewTab();
            UseCustomPrintStyles = false;
        }

        protected override string GetTitle() {
            return (UseCustomPrintStyles ? "Uniform Cards Size" : "Default") + " Style Print Preview";
        }
    }
}
