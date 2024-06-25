using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.PivotGrid;
using System.Collections;

namespace ProductsDemo.Modules {
    public class FieldAreaHelper : DependencyObject {
        public static readonly DependencyProperty FixAreasProperty;

        static FieldAreaHelper() {
            FixAreasProperty = DependencyProperty.RegisterAttached("FixAreas", typeof(bool), typeof(FieldAreaHelper), new PropertyMetadata(OnFixAreasPropertyChanged));
        }

        public static bool GetFixAreas(DependencyObject element) {
            if(element == null || element as PivotGridControl == null)
                throw new ArgumentNullException("element");
            return (bool)((PivotGridControl)element).GetValue(FixAreasProperty);
        }

        public static void SetFixAreas(DependencyObject element, bool value) {
            if(element == null || element as PivotGridControl == null)
                throw new ArgumentNullException("element");
            element.SetValue(FixAreasProperty, value);
        }

        static void OnFixAreasPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PivotGridControl pivot = d as PivotGridControl;
            if(pivot == null) return;
            pivot.FieldAreaChanging += new PivotFieldAreaChangingEventHandler(OnPivotFieldAreaChanging);
        }

        static void OnPivotFieldAreaChanging(object sender, PivotFieldAreaChangingEventArgs e) {
            PivotGridField field = e.Field;
            if(field == null || field.Parent as PivotGridControl == null || ((PivotGridControl)field.Parent).OlapConnectionString != null 
                || field.UnboundType != FieldUnboundColumnType.Bound)
                return;
            if(field.Area == FieldArea.DataArea) {
                if(e.NewArea != FieldArea.DataArea)
                    e.Allow = false;
            } else {
                if(e.NewArea == FieldArea.DataArea)
                    e.Allow = false;
            }
        }
    }
}
