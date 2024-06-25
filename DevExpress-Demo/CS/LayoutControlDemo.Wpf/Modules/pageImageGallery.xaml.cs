using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    public partial class pageImageGallery : LayoutControlDemoModule {
        public pageImageGallery() {
            InitializeComponent();
        }

        public IEnumerable Images {
            get {
                var result = new List<BitmapImage>();
                for (int i = 1; i <= 10; i++)
                    result.Add(new BitmapImage(new Uri(string.Format(UriPrefix + "/Images/Photos/{0:D2}.jpg", i), UriKind.Relative)));
                return result;
            }
        }

        void layoutImagesItemsSizeChanged(object sender, ValueChangedEventArgs<Size> e) {
            Size size = layoutImages.MaximizedElementOriginalSize;
            if (!double.IsInfinity(e.NewValue.Width))
                size.Height = double.NaN;
            else
                size.Width = double.NaN;
            layoutImages.MaximizedElementOriginalSize = size;
        }
    }

    public class ImageContainer : ContentControlBase {
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);
            if (Controller.IsMouseLeftButtonDown) {
                var layoutControl = Parent as FlowLayoutControl;
                if (layoutControl != null) {
                    Controller.IsMouseEntered = false;
                    layoutControl.MaximizedElement = layoutControl.MaximizedElement == this ? null : this;
                }
            }
        }
        protected override void OnSizeChanged(SizeChangedEventArgs e) {
            base.OnSizeChanged(e);
            if (!double.IsNaN(Width) && !double.IsNaN(Height))
                if (e.NewSize.Width != e.PreviousSize.Width)
                    Height = double.NaN;
                else
                    Width = double.NaN;
        }
    }
}
