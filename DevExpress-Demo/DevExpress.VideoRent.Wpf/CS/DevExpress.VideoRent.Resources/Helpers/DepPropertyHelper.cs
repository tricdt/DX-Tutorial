using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace DevExpress.VideoRent.Resources.Helpers {
    public static class DepPropertyHelper {
        class HardBinding {
            EventHandler onChanged;

            public HardBinding(DependencyObject target, DependencyProperty targetProperty, DependencyObject source, DependencyProperty sourceProperty) {
                Target = target;
                TargetProperty = targetProperty;
                Source = source;
                SourceProperty = sourceProperty;
                onChanged = (s, e) => { Target.SetValue(TargetProperty, Source.GetValue(SourceProperty)); };
            }
            public void Bind() {
                SubscribeToChanged(Target, TargetProperty, onChanged);
                SubscribeToChanged(Source, SourceProperty, onChanged);
                onChanged(Source, EventArgs.Empty);
            }
            public void Unbind() {
                UnsubscribeFromChanged(Source, SourceProperty, onChanged);
                UnsubscribeFromChanged(Target, TargetProperty, onChanged);
            }
            public DependencyObject Target { get; private set; }
            public DependencyProperty TargetProperty { get; private set; }
            public DependencyObject Source { get; private set; }
            public DependencyProperty SourceProperty { get; private set; }
        }
        #region Dependency Properties
        static readonly DependencyProperty HardBindingsProperty;
        static DepPropertyHelper() {
            Type ownerType = typeof(DepPropertyHelper);
            HardBindingsProperty = DependencyProperty.RegisterAttached("HardBindings", typeof(Dictionary<DependencyProperty, HardBinding>), ownerType, new PropertyMetadata(null));
        }
        #endregion
        static Dictionary<DependencyProperty, HardBinding> GetHardBindings(DependencyObject d) {
            Dictionary<DependencyProperty, HardBinding> hardBindings = (Dictionary<DependencyProperty, HardBinding>)d.GetValue(HardBindingsProperty);
            if(hardBindings == null) {
                hardBindings = new Dictionary<DependencyProperty, HardBinding>();
                d.SetValue(HardBindingsProperty, hardBindings);
            }
            return hardBindings;
        }
        static void SetHardBindings(DependencyObject d, Dictionary<DependencyProperty, HardBinding> hardBindings) { d.SetValue(HardBindingsProperty, hardBindings); }
        public static void SubscribeToChanged(DependencyObject d, DependencyProperty property, EventHandler handler) {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(property, d.GetType());
            descriptor.AddValueChanged(d, handler);
        }
        public static void UnsubscribeFromChanged(DependencyObject d, DependencyProperty property, EventHandler handler) {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(property, d.GetType());
            descriptor.RemoveValueChanged(d, handler);
        }
        public static void BindHard(DependencyObject target, DependencyProperty targetProperty, DependencyObject source, DependencyProperty sourceProperty) {
            UnbindHard(target, targetProperty);
            Dictionary<DependencyProperty, HardBinding> hardBindings = GetHardBindings(target);
            HardBinding binding = new HardBinding(target, targetProperty, source, sourceProperty);
            hardBindings.Add(targetProperty, binding);
            binding.Bind();
        }
        public static void UnbindHard(DependencyObject target, DependencyProperty targetProperty) {
            Dictionary<DependencyProperty, HardBinding> hardBindings = GetHardBindings(target);
            HardBinding binding;
            if(!hardBindings.TryGetValue(targetProperty, out binding)) return;
            binding.Unbind();
            hardBindings.Remove(targetProperty);
        }
    }
}
