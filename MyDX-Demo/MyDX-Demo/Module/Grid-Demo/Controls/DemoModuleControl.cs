using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.Grid_Demo.Controls
{
    public class GridDemoModule : DemoModule
    {
        static readonly DependencyPropertyKey GridControlPropertyKey;
        public static readonly DependencyProperty GridControlProperty;

        static GridDemoModule()
        {
            GridControlPropertyKey = DependencyProperty.RegisterReadOnly("GridControl", typeof(GridControl), typeof(GridDemoModule), new PropertyMetadata(null));
            GridControlProperty = GridControlPropertyKey.DependencyProperty;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GridControl = Content as GridControl ?? LayoutTreeHelper.GetVisualChildren((DependencyObject)Content).OfType<GridControl>().FirstOrDefault();
            if (OptionsDataContext == null)
                OptionsDataContext = GridControl;
        }
        public GridControl GridControl
        {
            get { return (GridControl)GetValue(GridControlProperty); }
            private set { SetValue(GridControlPropertyKey, value); }
        }
        protected override void HidePopupContent()
        {
            if (GridControl != null)
                GridControl.View.HideColumnChooser();
            base.HidePopupContent();
        }
        protected override void Hide()
        {
            if (GridControl != null)
            {

                GridControl.View.CommitEditing();
            }
            base.Hide();
        }

    }
}
