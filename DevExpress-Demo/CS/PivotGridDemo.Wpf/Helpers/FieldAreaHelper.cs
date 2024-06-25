using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    
    public class FieldAreaHelper : DependencyObject {
        public static readonly DependencyProperty FixAreasProperty;

        static FieldAreaHelper() {
            FixAreasProperty = DependencyProperty.RegisterAttached("FixAreas", typeof(bool), typeof(FieldAreaHelper), new PropertyMetadata(OnFixAreasPropertyChanged));
        }

        public static bool GetFixAreas(DependencyObject element) {
            if(!(element is PivotGridControl))
                throw new ArgumentNullException("element");
            return (bool)((PivotGridControl)element).GetValue(FixAreasProperty);
        }

        public static void SetFixAreas(DependencyObject element, bool value) {
            if(!(element is PivotGridControl))
                throw new ArgumentNullException("element");
            element.SetValue(FixAreasProperty, value);
        }

        static void OnFixAreasPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PivotGridControl pivot = d as PivotGridControl;
            if(pivot == null) return;
            pivot.FieldAreaChanging += OnPivotFieldAreaChanging;
        }

        static void OnPivotFieldAreaChanging(object sender, PivotFieldAreaChangingEventArgs e) {
            PivotGridField field = e.Field;
            if(field == null ||
                !(field.Parent is PivotGridControl) ||
                ((PivotGridControl)field.Parent).OlapConnectionString != null ||
                !(field.DataBinding is DataSourceColumnBinding))
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
