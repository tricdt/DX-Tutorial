using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;

namespace PropertyGridDemo {
    public class SelectedElementVisualizer : Grid {
        public static readonly DependencyProperty SelectedElementProperty;

        static SelectedElementVisualizer() {
            Type ownerType = typeof(SelectedElementVisualizer);
            SelectedElementProperty = DependencyProperty.Register("SelectedElement", typeof(FrameworkElement), ownerType,
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, PropertyChangedCallback));
        }
        static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((SelectedElementVisualizer)d).SelectedElementChanged((FrameworkElement)e.OldValue, (FrameworkElement)e.NewValue);
        }
        readonly Locker childrenLocker = new Locker();
        public SelectedElementVisualizer() {
            Selector = new Selector();
        }
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved) {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            childrenLocker.DoLockedActionIfNotLocked(ChildrenUpdate);
        }
        void ChildrenUpdate() {
            Children.Remove(Selector);
            Children.Add(Selector);
        }
        Selector Selector { get; set; }
        public FrameworkElement SelectedElement {
            get { return (FrameworkElement)GetValue(SelectedElementProperty); }
            set { SetValue(SelectedElementProperty, value); }
        }

        protected virtual void SelectedElementChanged(FrameworkElement oldElement, FrameworkElement newElement) {
            if(oldElement!=null)
                oldElement.LayoutUpdated += OnSelectedElementLayoutUpdated;
            if(newElement!=null)
                newElement.SizeChanged += OnSelectedElementLayoutUpdated;
            BuildLayout();
        }

        void OnSelectedElementLayoutUpdated(object sender, EventArgs e) {
            BuildLayout();
        }

        void BuildLayout() {
            if (!LayoutHelper.IsChildElement(this, SelectedElement)) {
                Selector.Visibility = Visibility.Collapsed;
                return;
            }
            Selector.Visibility = Visibility.Visible;
            Rect rect = LayoutHelper.GetRelativeElementRect(SelectedElement, this);
            Selector.Margin = new Thickness(rect.Left, rect.Top, ActualWidth - rect.Left - rect.Width, ActualHeight - rect.Top - rect.Height);
        }
    }
    public class Selector : Grid {
        public Selector() {
            Border border = new Border() { Margin = new Thickness(-2), BorderBrush = Brushes.Red, BorderThickness = new Thickness(2), Opacity = 0.35, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
            Children.Add(border);
        }
    }
}
