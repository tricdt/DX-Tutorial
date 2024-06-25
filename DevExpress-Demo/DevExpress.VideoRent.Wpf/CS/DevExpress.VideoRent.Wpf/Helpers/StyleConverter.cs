using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class StyleConverter {
        #region Dependency Properties
        public static readonly DependencyProperty StyleProperty;
        static StyleConverter() {
            Type ownerType = typeof(StyleConverter);
            StyleProperty = DependencyProperty.RegisterAttached("Style", typeof(Style), ownerType, new PropertyMetadata(null, RaiseStyleChanged));
        }
        #endregion
        static StyleConverter _default;
        public static StyleConverter Default {
            get {
                if(_default == null)
                    _default = new StyleConverter();
                return _default;
            }
        }
        public static Style GetStyle(DependencyObject d) { return (Style)d.GetValue(StyleProperty); }
        public static void SetStyle(DependencyObject d, Style style) { d.SetValue(StyleProperty, style); }
        static void RaiseStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            SetConvertedStyle(d, Default.Convert((Style)e.NewValue, d.GetType()));
        }
        static void SetConvertedStyle(DependencyObject d, Style style) {
            FrameworkElement fe = d as FrameworkElement;
            if(fe != null) {
                fe.Style = style;
                return;
            }
            FrameworkContentElement fce = d as FrameworkContentElement;
            if(fce != null) {
                fce.Style = style;
                return;
            }
        }

        public Style Convert(Style source, Type targetType) {
            if(source == null) return null;
            Style target = new Style(targetType, Convert(source.BasedOn, targetType));
            foreach(SetterBase sourceSetterBase in source.Setters) {
                Setter sourceSetter = sourceSetterBase as Setter;
                if(sourceSetter == null || object.Equals(sourceSetter.Value, DependencyProperty.UnsetValue)) continue;
                DependencyProperty sourceProperty = sourceSetter.Property;
                FieldInfo fieldInfo = FindStaticField(targetType, sourceProperty.Name + "Property");
                if(fieldInfo == null) continue;
                DependencyProperty targetProperty = fieldInfo.GetValue(null) as DependencyProperty;
                if(targetProperty == null) continue;
                Setter targetSetter = new Setter(targetProperty, sourceSetter.Value, sourceSetter.TargetName);
                target.Setters.Add(targetSetter);
            }
            return target;
        }
        FieldInfo FindStaticField(Type type, string name) {
            for(Type baseType = type; baseType != null; baseType = baseType.BaseType) {
                FieldInfo fieldInfo = FindStaticFieldCore(baseType, name);
                if(fieldInfo != null) return fieldInfo;
            }
            return null;
        }
        FieldInfo FindStaticFieldCore(Type type, string name) {
            return type.GetField(name, BindingFlags.Static | BindingFlags.Public);
        }
    }
}
