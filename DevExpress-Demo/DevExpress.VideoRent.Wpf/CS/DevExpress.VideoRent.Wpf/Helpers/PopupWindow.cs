using System;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Core;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class PopupWindow : ThemedWindow {
        #region Dependency Properties
        public static readonly DependencyProperty FixedSizeProperty;
        static PopupWindow() {
            Type ownerType = typeof(PopupWindow);
            FixedSizeProperty = DependencyProperty.RegisterAttached("FixedSize", typeof(bool), ownerType, new PropertyMetadata(false));
        }
        #endregion
        public static bool GetFixedSize(FrameworkElement d) { return (bool)d.GetValue(FixedSizeProperty); }
        public static void SetFixedSize(FrameworkElement d, bool fixedSize) { d.SetValue(FixedSizeProperty, fixedSize); }

        public PopupWindow() {
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Icon = Application.Current.MainWindow.Icon;
            Owner = RetrieveParentWindow();
        }
        public new bool? ShowDialog() {
            MouseHelper.WaitIdle();
            return base.ShowDialog();
        }
        public new void Show() {
            MouseHelper.WaitIdle();
            base.Show();
        }
        protected override void OnContentRendered(EventArgs e) {
            UpdateSizes();
            base.OnContentRendered(e);
        }
        void UpdateSizes() {
            FrameworkElement contentElement = Content as FrameworkElement;
            double horizontalPadding = ActualWidth - contentElement.ActualWidth;
            double verticalPadding = ActualHeight - contentElement.ActualHeight;
            IValueConverter sumConverter = new SummConteverter();
            SetBinding(MinWidthProperty, new Binding("Content.MinWidth") { Source = this, Converter = sumConverter, ConverterParameter = horizontalPadding });
            SetBinding(MaxWidthProperty, new Binding("Content.MaxWidth") { Source = this, Converter = sumConverter, ConverterParameter = horizontalPadding });
            SetBinding(MinHeightProperty, new Binding("Content.MinHeight") { Source = this, Converter = sumConverter, ConverterParameter = verticalPadding });
            SetBinding(MaxHeightProperty, new Binding("Content.MaxHeight") { Source = this, Converter = sumConverter, ConverterParameter = verticalPadding });
            bool widthFixed = contentElement.Width != double.NaN;
            bool heightFixed = contentElement.Height != double.NaN;
            if(widthFixed) {
                SizeToContent = heightFixed ? SizeToContent.Manual : SizeToContent.Height;
            } else {
                SizeToContent = heightFixed ? SizeToContent.Width : SizeToContent.WidthAndHeight;
            }
            contentElement.Width = double.NaN;
            contentElement.Height = double.NaN;
            if(GetFixedSize(contentElement))
                ResizeMode = ResizeMode.CanMinimize;
        }
        static Window RetrieveParentWindow() {
            Window mostPossibleParent = null;
            foreach(Window item in Application.Current.Windows) {
                if(item.IsLoaded) mostPossibleParent = item;
            }
            return mostPossibleParent == null ? Application.Current.MainWindow : mostPossibleParent;
        }
    }
    public static class ElementHelper {
        #region Dependency Properties
        public static readonly DependencyProperty FixMinHeightProperty;
        public static readonly DependencyProperty FixMinWidthProperty;
        static ElementHelper() {
            Type ownerType = typeof(ElementHelper);
            FixMinHeightProperty = DependencyProperty.RegisterAttached("FixMinHeight", typeof(bool), ownerType, new PropertyMetadata(false, OnFixMinHeightChanged));
            FixMinWidthProperty = DependencyProperty.RegisterAttached("FixMinWidth", typeof(bool), ownerType, new PropertyMetadata(false, OnFixMinWidthChanged));
        }
        #endregion
        public static bool GetFixMinHeight(FrameworkElement element) { return (bool)element.GetValue(FixMinHeightProperty); }
        public static void SetFixMinHeight(FrameworkElement element, bool value) { element.SetValue(FixMinHeightProperty, value); }
        public static bool GetFixMinWidth(FrameworkElement element) { return (bool)element.GetValue(FixMinWidthProperty); }
        public static void SetFixMinWidth(FrameworkElement element, bool value) { element.SetValue(FixMinWidthProperty, value); }
        static void SubscribeToLoaded(FrameworkElement element) {
            element.Loaded -= OnElementLoaded;
            element.Loaded += OnElementLoaded;
        }
        static void OnElementLoaded(object sender, RoutedEventArgs e) {
            FrameworkElement element = (FrameworkElement)sender;
            if(GetFixMinHeight(element)) {
                element.MinHeight = element.ActualHeight;
            }
            if(GetFixMinWidth(element)) {
                element.MinWidth = element.ActualWidth;
            }
        }
        static void OnFixMinHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            SubscribeToLoaded((FrameworkElement)d);
        }
        static void OnFixMinWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            SubscribeToLoaded((FrameworkElement)d);
        }
    }
}
