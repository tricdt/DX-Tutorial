using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Utils;
namespace WindowsUIDemo {
    public partial class ImageViewer : UserControl {
        #region Dependency Properties
        public static readonly DependencyProperty FlipProperty =
            DependencyProperty.Register("Flip", typeof(bool), typeof(ImageViewer),
            new PropertyMetadata(false, new PropertyChangedCallback(OnFlipPropertyChanged)));
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(ImageViewer),
            new PropertyMetadata(1d, new PropertyChangedCallback(OnScalePropertyChanged)));
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(Rotation), typeof(ImageViewer), new PropertyMetadata(Rotation.Rotate0));
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty MinScaleValueProperty =
            DependencyProperty.Register("MinScaleValue", typeof(double), typeof(ImageViewer), new PropertyMetadata(0.1d));
        public static readonly DependencyProperty MaxScaleValueProperty =
            DependencyProperty.Register("MaxScaleValue", typeof(double), typeof(ImageViewer), new PropertyMetadata(4d));
        protected static void OnScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ImageViewer)d).OnScaleChanged((double)e.OldValue);
        }
        protected static void OnFlipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ImageViewer)d).OnFlipChanged((bool)e.OldValue);
        }
        #endregion Dependency Properties
        public double Scale {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        public bool Flip {
            get { return (bool)GetValue(FlipProperty); }
            set { SetValue(FlipProperty, value); }
        }
        public Rotation Rotation {
            get { return (Rotation)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }
        public ImageSource ImageSource {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public double MinScaleValue {
            get { return (double)GetValue(MinScaleValueProperty); }
            set { SetValue(MinScaleValueProperty, value); }
        }
        public double MaxScaleValue {
            get { return (double)GetValue(MaxScaleValueProperty); }
            set { SetValue(MaxScaleValueProperty, value); }
        }
        public double HorizontalFitScale {
            get {
                double horzScale = OriginalViewportSize.Width / OriginalContentSize.Width * Scale;
                if(OriginalViewportSize.Height < OriginalContentSize.Height / Scale * horzScale) {
                    horzScale = (OriginalViewportSize.Width - scrollBarSize) / OriginalContentSize.Width * Scale;
                }
                return horzScale;
            }
        }
        public double VerticalFitScale {
            get {
                double vertScale = OriginalViewportSize.Height / OriginalContentSize.Height * Scale;
                if(OriginalViewportSize.Width < OriginalContentSize.Width / Scale * vertScale) {
                    vertScale = (OriginalViewportSize.Height - scrollBarSize) / OriginalContentSize.Height * Scale;
                }
                return vertScale;
            }
        }
        public event EventHandler MouseWheelZoom;
        public Image PartImage { get { return image; } }
        public Border Viewport { get { return border; } }
        public ImageViewer() {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            scrollHost.PreviewMouseWheel += OnScrollHostMouseWheel;
        }
        public double GetBestFitScale() {
            double bestScale = HorizontalFitScale;
            if(scrollHost.ViewportHeight < ((UIElement)scrollHost.Content).DesiredSize.Height / Scale * bestScale) return VerticalFitScale;
            return bestScale;
        }
        public void ScaleCenter(double scaleValue) {
            Point scalePoint = new Point(scrollHost.HorizontalOffset + (double)OriginalViewportSize.Width / 2, scrollHost.VerticalOffset + (double)OriginalViewportSize.Height / 2);
            ScaleAndScroll(scalePoint, scaleValue);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            e.Handled = true;
            Point point = e.GetPosition((UIElement)scrollHost.Content);
            if(e.Delta > 0)
                ScaleAndScroll(point, Math.Min(Scale + 0.1, MaxScaleValue));
            else
                ScaleAndScroll(point, Math.Max(Scale - 0.1, MinScaleValue));
            if(MouseWheelZoom != null)
                MouseWheelZoom(this, new EventArgs());
            return;
        }
        protected virtual void OnScaleChanged(double oldValue) {
            scaleIsChanged = true;
        }
        protected virtual void OnFlipChanged(bool oldValue) {
            scaleIsChanged = true;
        }
        protected virtual void OnLayoutUpdated(object sender, EventArgs e) {
            UpdateScrollbarVisibility();
            UpdateMouseCursor();
            if(this.scaleIsChanged) {
                scrollHost.ScrollToHorizontalOffset(this.scrollOffsetAfterScaleChanged.X);
                scrollHost.ScrollToVerticalOffset(this.scrollOffsetAfterScaleChanged.Y);
                this.scaleIsChanged = false;
            }
        }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) {
            LayoutUpdated += OnLayoutUpdated;
        }
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e) {
            LayoutUpdated -= OnLayoutUpdated;
        }
        protected virtual void OnImageMouseMove(object sender, MouseEventArgs e) {
            if(!this.isLeftButtonDown)
                return;
            Point point = e.GetPosition(scrollHost);
            double horizontalDragDelta = this.dragPoint.X - point.X;
            double verticalDragDelta = this.dragPoint.Y - point.Y;
            scrollHost.ScrollToHorizontalOffset(this.dragOffset.Width + horizontalDragDelta);
            scrollHost.ScrollToVerticalOffset(this.dragOffset.Height + verticalDragDelta);
        }
        protected virtual void OnImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            ((UIElement)sender).CaptureMouse();
            this.isLeftButtonDown = true;
            this.dragPoint = e.GetPosition(scrollHost);
            this.dragOffset = new Size(scrollHost.HorizontalOffset, scrollHost.VerticalOffset);
            
        }
        protected virtual void OnImageMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ((UIElement)sender).ReleaseMouseCapture();
            this.isLeftButtonDown = false;
            
        }
        protected virtual void OnImageLostMouseCapture(object sender, EventArgs e) {
            this.isLeftButtonDown = false;
        }
        protected virtual void ScaleAndScroll(Point scalePoint, double scaleValue) {
            Size originalImageSize = new Size(OriginalContentSize.Width / Scale, OriginalContentSize.Height / Scale);
            Point viewportOffset = new Point(scalePoint.X - scrollHost.HorizontalOffset, scalePoint.Y - scrollHost.VerticalOffset);
            
            scrollOffsetAfterScaleChanged = new Point(scalePoint.X / Scale * scaleValue - viewportOffset.X, scalePoint.Y / Scale * scaleValue - viewportOffset.Y);
            Scale = scaleValue;
        }
        protected virtual void OnScrollHostMouseWheel(object sender, MouseWheelEventArgs e) {
            OnMouseWheel(e);
        }

        void UpdateScrollbarVisibility() {
            if(OriginalContentSize.Height - OriginalViewportSize.Height > 0.5d) {
                scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            } else
                scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            if(OriginalContentSize.Width - OriginalViewportSize.Width > 0.5d) {
                scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            } else
                scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
        void UpdateMouseCursor() {
            if(scrollHost.HorizontalScrollBarVisibility == ScrollBarVisibility.Visible || scrollHost.VerticalScrollBarVisibility == ScrollBarVisibility.Visible) {
                ((ImagePresenter)scrollHost.Content).Cursor = Cursors.Hand;
            } else {
                ((ImagePresenter)scrollHost.Content).Cursor = Cursors.Arrow;
            }
        }

        Size OriginalContentSize { get { return ((UIElement)scrollHost.Content).DesiredSize; } }
        Size OriginalViewportSize {
            get {
                double originViewportWidth = scrollHost.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Visible ? scrollHost.ViewportWidth + scrollBarSize : scrollHost.ViewportWidth;
                double originViewportHeight = scrollHost.ComputedHorizontalScrollBarVisibility == System.Windows.Visibility.Visible ? scrollHost.ViewportHeight + scrollBarSize : scrollHost.ViewportHeight;
                return new Size(originViewportWidth, originViewportHeight);
            }
        }

        Point dragPoint = new Point();
        Size dragOffset = new Size();
        Point scrollOffsetAfterScaleChanged = new Point();
        bool scaleIsChanged = false;
        bool isLeftButtonDown = false;

        const double scrollBarSize = 12;
    }

    public class ImagePresenter : Panel {
        #region Dependency Properties
        public static readonly DependencyProperty FlipProperty =
            DependencyProperty.Register("Flip", typeof(bool), typeof(ImagePresenter),
            new PropertyMetadata(false, new PropertyChangedCallback(OnFlipPropertyChanged)));

        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(Rotation), typeof(ImagePresenter),
            new PropertyMetadata(Rotation.Rotate90, new PropertyChangedCallback(OnRotationPropertyChanged)));
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(UIElement), typeof(ImagePresenter),
            new PropertyMetadata(null, new PropertyChangedCallback(OnContentPropertyChanged)));
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(ImagePresenter),
            new PropertyMetadata(1d, new PropertyChangedCallback(OnScalePropertyChanged)));
        protected static void OnRotationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ImagePresenter)d).OnRotationChanged((Rotation)e.OldValue);
        }
        protected static void OnFlipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ImagePresenter)d).OnFlipChanged((bool)e.OldValue);
        }
        protected static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ImagePresenter)d).OnContentChanged((UIElement)e.OldValue);
        }
        protected static void OnScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ImagePresenter)d).OnScaleChanged((double)e.OldValue);
        }
        #endregion Dependency Properties
        public Rotation Rotation {
            get { return (Rotation)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }
        public UIElement Content {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public double Scale {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        public bool Flip {
            get { return (bool)GetValue(FlipProperty); }
            set { SetValue(FlipProperty, value); }
        }

        protected virtual void OnContentChanged(UIElement oldValue) {
            if(oldValue != null) {
                oldValue.RenderTransform = null;
                Children.Remove(oldValue);
            }
            if(Content != null) {
                TransformGroup transformGroup = new TransformGroup();
                ScaleTransform scaleTransform = new ScaleTransform() { ScaleX = Scale, ScaleY = Scale };
                RotateTransform rotateTransform = new RotateTransform() { Angle = GetAngleByRotation(Rotation) };
                transformGroup.Children.Add(scaleTransform);
                transformGroup.Children.Add(rotateTransform);
                transformGroup.Children.Add(new TranslateTransform());
                Content.RenderTransform = transformGroup;
                Content.RenderTransformOrigin = new Point(0.5, 0.5);
                Children.Add(Content);
            }
        }
        protected virtual void OnScaleChanged(double oldValue) {
            if(Content != null) {
                ((Content.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ScaleX = Flip ? -Scale : Scale;
                ((Content.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ScaleY = Scale;
                Content.InvalidateArrange();
                InvalidateMeasure();
            }
            InvalidateArrange();
            UpdateLayout();
        }
        protected virtual void OnRotationChanged(Rotation oldValue) {
            if(Content != null) {
                ((Content.RenderTransform as TransformGroup).Children[1] as RotateTransform).Angle = GetAngleByRotation(Rotation);
                Content.InvalidateMeasure();
                InvalidateMeasure();
            }
        }
        protected virtual void OnFlipChanged(bool oldValue) {
            if(Content != null) {
                ((Content.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ScaleX = Flip ? -Scale : Scale;
                ((Content.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ScaleY = Scale;
                Content.InvalidateArrange();
                InvalidateMeasure();
            }
            InvalidateArrange();
            UpdateLayout();
        }

        protected override Size MeasureOverride(Size availableSize) {
            if(Content == null) return new Size();
            Size baseSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Content.Measure(baseSize);
            
            
            switch(Rotation) {
                case Rotation.Rotate0:
                case Rotation.Rotate180:
                    return new Size(Content.DesiredSize.Width * Scale, Content.DesiredSize.Height * Scale);
                default:
                    return new Size(Content.DesiredSize.Height * Scale, Content.DesiredSize.Width * Scale);
            }
        }
        protected override Size ArrangeOverride(Size finalSize) {
            if(Content == null) return new Size();
            Content.Arrange(new Rect(0, 0, Content.DesiredSize.Width, Content.DesiredSize.Height));
            return finalSize;
        }

        double GetAngleByRotation(Rotation rotation) {
            if(rotation == System.Windows.Media.Imaging.Rotation.Rotate0)
                return 0;
            else if(rotation == System.Windows.Media.Imaging.Rotation.Rotate90)
                return 90;
            else if(rotation == System.Windows.Media.Imaging.Rotation.Rotate180)
                return 180;
            else if(rotation == System.Windows.Media.Imaging.Rotation.Rotate270)
                return 270;
            return 0;
        }
        Point GetTranslatePoint() {
            switch(Rotation) {
                case Rotation.Rotate0:
                    return new Point(0, 0);
                case Rotation.Rotate90:
                    return new Point(Content.DesiredSize.Height * Scale, 0);
                case Rotation.Rotate180:
                    return new Point(Content.DesiredSize.Width * Scale, Content.DesiredSize.Height * Scale);
                case Rotation.Rotate270:
                    return new Point(0, Content.DesiredSize.Width * Scale);

            }
            return new Point();
        }
    }
}
