using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core.FilteringUI;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors.Filtering;

namespace DevExpress.DevAV.Common.View {
    public class CustomFilterBehavior : FilterBehavior {
        public static readonly DependencyProperty HiddenPropertiesProperty =
            DependencyProperty.Register("HiddenProperties", typeof(IEnumerable<string>), typeof(CustomFilterBehavior), 
                new PropertyMetadata(null, (d, e) => ((CustomFilterBehavior)d).UpdateFields()));
        public static readonly DependencyProperty AdditionalPropertiesProperty =
            DependencyProperty.Register("AdditionalProperties", typeof(PropertyInfoCollection), typeof(CustomFilterBehavior), 
                new PropertyMetadata(null, (d, e) => ((CustomFilterBehavior)d).UpdateFields()));
        public static readonly DependencyProperty ObjectTypeProperty =
            DependencyProperty.Register("ObjectType", typeof(Type), typeof(CustomFilterBehavior), 
                new PropertyMetadata(null, (d, e) => ((CustomFilterBehavior)d).UpdateFields()));

        public IEnumerable<string> HiddenProperties {
            get { return (IEnumerable<string>)GetValue(HiddenPropertiesProperty); }
            set { SetValue(HiddenPropertiesProperty, value); }
        }
        public PropertyInfoCollection AdditionalProperties {
            get { return (PropertyInfoCollection)GetValue(AdditionalPropertiesProperty); }
            set { SetValue(AdditionalPropertiesProperty, value); }
        }
        public Type ObjectType {
            get { return (Type)GetValue(ObjectTypeProperty); }
            set { SetValue(ObjectTypeProperty, value); }
        }
        protected override void OnAttached() {
            base.OnAttached();
            UpdateFields();
        }
        void UpdateFields() {
            if(AssociatedObject == null || ObjectType == null)
                return;
            UpdateContext();

            Fields.Clear();
            var fieldsInfo = FilterFieldsHelper.GetFields(AssociatedObject, ObjectType, HiddenProperties ?? Enumerable.Empty<string>(), AdditionalProperties ?? new PropertyInfoCollection())
                .Where(x => !x.FieldName.EndsWith("Id"));

            foreach (var field in fieldsInfo) {
                Fields.Add(new FilterField() {
                    Caption = field.Caption,
                    EditSettings = field.EditSettings,
                    FieldName = field.FieldName
                });
            }
        }
        void UpdateContext() {
            var type = typeof(List<>).MakeGenericType(ObjectType);
            ItemsSource = Activator.CreateInstance(type);
        }
    }
}
