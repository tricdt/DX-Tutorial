using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DemoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ControlsDemo {
    [CodeFile("ViewModels/TaskbarServicesViewModel.(cs)")]
    [CodeFile("Helpers/ImageNameConverter.(cs)")]
    public partial class TaskbarServices : DemoModule {
        public TaskbarServices() {
            InitializeComponent();
        }
        protected override void Clear() {
            try {
                IDisposable disposableViewModel = ViewHelper.GetViewModelFromView(this) as IDisposable;
                if(disposableViewModel != null)
                    disposableViewModel.Dispose();
            } finally {
                base.Clear();
            }
        }
    }

    public class ReverseStackPanel : Panel {
        protected override Size MeasureOverride(Size availableSize) {
            Size childAvailableSize = new Size(availableSize.Width, double.PositiveInfinity);
            Size size = new Size();
            foreach(UIElement child in InternalChildren) {
                child.Measure(childAvailableSize);
                size.Width = Math.Max(size.Width, child.DesiredSize.Width);
                size.Height += child.DesiredSize.Height;
            }
            return size;
        }
        protected override Size ArrangeOverride(Size finalSize) {
            double y = 0.0;
            foreach(UIElement child in InternalChildren.Cast<UIElement>().Reverse()) {
                double childHeight = child.DesiredSize.Height;
                child.Arrange(new Rect(new Point(0.0, y), new Size(finalSize.Width, childHeight)));
                y += childHeight;
            }
            return finalSize;
        }
    }
}
