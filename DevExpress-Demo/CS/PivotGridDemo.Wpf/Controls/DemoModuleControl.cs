using System;
using System.Linq;
using System.Windows;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public class PivotGridDemoModule : DemoModule {
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected override void HidePopupContent() {
            var pivotGrid = Content as PivotGridControl ?? LayoutTreeHelper.GetVisualChildren((DependencyObject)Content).OfType<PivotGridControl>().FirstOrDefault();
            if(pivotGrid != null)
                pivotGrid.HideFieldList();
            base.HidePopupContent();
        }
    }
}
