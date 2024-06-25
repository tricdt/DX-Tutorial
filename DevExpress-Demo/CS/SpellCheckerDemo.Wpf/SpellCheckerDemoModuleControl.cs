using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraSpellChecker.Native;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.SpellChecker;

namespace SpellCheckerDemo {
    public class SpellCheckerDemoModule : DemoModule {
        
        protected virtual List<FrameworkElement> CheckingElements { get { return null; } }

        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            if(CheckingElements == null)
                return;
            foreach(FrameworkElement element in CheckingElements)
                element.Visibility = Visibility.Visible;
        }
        protected override void HidePopupContent() {
            if(CheckingElements == null)
                return;
            foreach(FrameworkElement element in CheckingElements)
                element.Visibility = Visibility.Hidden;
            base.HidePopupContent();
        }
    }
}
