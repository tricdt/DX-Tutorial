using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.UI.Interactivity;

namespace PropertyGridDemo
{
    public class GridColumnHelper : Behavior<Grid> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
        }

        void AssociatedObject_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e) { AssociatedObject.ColumnDefinitions[1].Width = new GridLength(e.NewSize.Width / 3, GridUnitType.Pixel); }
    }
}
