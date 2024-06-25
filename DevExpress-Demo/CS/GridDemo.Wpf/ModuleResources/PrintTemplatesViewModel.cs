using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    public class PrintTemplatesViewModel : PrintViewModelBase {
        public PrintTemplatesMode Mode {
            get { return GetProperty(() => Mode); }
            set { SetProperty(() => Mode, value); }
        }

        public override void ReOpenPreviewTabs() {
            Mode = PrintTemplatesMode.Default;
            base.ReOpenPreviewTabs();
            Mode = PrintTemplatesMode.MailMerge;
            base.ShowPreviewInNewTab();
            Mode = PrintTemplatesMode.Detail;
            base.ShowPreviewInNewTab();
        }

        protected override string GetTitle() {
            return Mode.ToString() + " Print Preview";
        }
    }
    public enum PrintTemplatesMode {
        Detail,
        MailMerge,
        Default
    }
}
