using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Threading;
using System.Windows.Threading;
using DevExpress.Xpf.Utils;


namespace ControlsDemo.GalleryDemo {
    public class ZoomStackPanel : Panel {
        public double MinChildOffset {
            get { return _minChildOffset; }
            set { _minChildOffset = value; }
        }
        public double MaxChildOffset {
            get { return _maxChildOffset; }
            set { _maxChildOffset = value; }
        }
        public double ChildOffset {
            get { return _childOffset; }
            set {
                double actualValue = CoerceChildOffset(value);
                if(actualValue == _childOffset)
                    return;
                _childOffset = actualValue;
                OnChildOffsetChanged();
            }
        }
        double _childOffset = 0;
        double _minChildOffset = -3;
        double _maxChildOffset = 3;

        protected virtual double CoerceChildOffset(double value) {
            double res = value;
            double intervalLength = MaxChildOffset - MinChildOffset;
            while(res < MinChildOffset) res += intervalLength;
            while(res > MaxChildOffset) res -= intervalLength;
            return res;
        }
        protected virtual void OnChildOffsetChanged() {
            InvalidateArrange();
            UpdateLayout();
        }

        protected override Size MeasureOverride(Size availableSize) {
            double sector = Children.Count / 5;
            Size res = new Size();
            for(int i = 0; i < Children.Count; i++) {
                UIElement child = Children[i];
                child.Measure(availableSize);
                res.Width += child.DesiredSize.Width;
                res.Height = Math.Max(res.Height, child.DesiredSize.Height);
                if(i < sector) res.Width += 1;
                else if(i < sector * 2) res.Width += 2;
                else if(i < sector * 3) res.Width += 3;
                else if(i < sector * 4) res.Width += 2;
                else res.Width += 1;
            }
            return res;
        }
        protected override Size ArrangeOverride(Size finalSize) {
            double sector = Children.Count / 5;
            double x = ChildOffset;
            for(int i = 0; i < Children.Count; i++) {
                UIElement child = Children[i];
                Point pos = new Point(x, 0);
                Size size = new Size(child.DesiredSize.Width, finalSize.Height);
                child.Arrange(new Rect(pos, size));
                if(i < sector) x += size.Width + 1;
                else if(i < sector * 2) x += size.Width + 2;
                else if(i < sector * 3) x += size.Width + 3;
                else if(i < sector * 4) x += size.Width + 2;
                else if(i <= Children.Count) x += size.Width + 1;
            }
            return finalSize;
        }
    }
    public partial class ZoomScroll : UserControl {
        #region Dependency Properties
        public static readonly DependencyProperty IncreaseDeltaProperty =
            DependencyProperty.Register("IncreaseDelta", typeof(double), typeof(ZoomScroll), new PropertyMetadata(50d));
        public static readonly DependencyProperty MouseWheelDeltaProperty =
            DependencyProperty.Register("MouseWheelDelta", typeof(double), typeof(ZoomScroll), new PropertyMetadata(25d));
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(ZoomScroll), new PropertyMetadata(10d));
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(ZoomScroll), new PropertyMetadata(400d));
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ZoomScroll),
            new PropertyMetadata(100d, new PropertyChangedCallback(OnValuePropertyChanged), new CoerceValueCallback(CoerceValueProperty)));
        protected static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ZoomScroll obj = (ZoomScroll)d;
            obj.OnValueChanged();
        }
        protected static object CoerceValueProperty(DependencyObject d, object value) {
            ZoomScroll obj = (ZoomScroll)d;
            return obj.CoerceValue((double)value);
        }
        #endregion Dependency Properties
        public double IncreaseDelta {
            get { return (double)GetValue(IncreaseDeltaProperty); }
            set { SetValue(IncreaseDeltaProperty, value); }
        }
        public double MouseWheelDelta {
            get { return (double)GetValue(MouseWheelDeltaProperty); }
            set { SetValue(MouseWheelDeltaProperty, value); }
        }
        public double MinValue {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public double MaxValue {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public double Value {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public bool IsMouseLeftButtonPressed { get; protected set; }

        public ZoomScroll() {
            Cursor = Cursors.Hand;
            InitializeComponent();
            InitializeMouseMoveAnimation();
            InitializeMouseWheelAnimation();
            InitializeIncreaseAnimation();
        }

        protected virtual void OnZoomDecreaseClick(object sender, EventArgs e) {
            
            
            IncreaseAnimation.To = Value - IncreaseDelta;
            IncreaseAnimationStoryboard.Begin();
        }
        protected virtual void OnZoomIncreaseClick(object sender, EventArgs e) {
            
            
            IncreaseAnimation.To = Value + IncreaseDelta;
            IncreaseAnimationStoryboard.Begin();
        }
        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged() {
            ZoomPanel.ChildOffset = Value;
            if(ValueChanged != null)
                ValueChanged(this, new EventArgs());
            return;
        }
        protected virtual double CoerceValue(double value) {
            if(value < MinValue)
                return MinValue;
            if(value > MaxValue)
                return MaxValue;
            return value;
        }

        protected virtual void InitializeMouseMoveAnimation() {
            MouseMoveAnimationStoryboard = new Storyboard();
            MouseMoveAnimation = new DoubleAnimation();
            MouseMoveAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.0));
            Storyboard.SetTarget(MouseMoveAnimation, this);
            Storyboard.SetTargetProperty(MouseMoveAnimation, new PropertyPath(ValueProperty));
            MouseMoveAnimationStoryboard.Children.Add(MouseMoveAnimation);
        }
        protected virtual void InitializeMouseWheelAnimation() {
            MouseWheelAnimationStoryboard = new Storyboard();
            MouseWheelAnimation = new DoubleAnimation();
            MouseWheelAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(MouseWheelAnimation, this);
            Storyboard.SetTargetProperty(MouseWheelAnimation, new PropertyPath(ValueProperty));
            MouseWheelAnimationStoryboard.Children.Add(MouseWheelAnimation);
        }
        protected virtual void InitializeIncreaseAnimation() {
            IncreaseAnimationStoryboard = new Storyboard();
            IncreaseAnimation = new DoubleAnimation();
            IncreaseAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(IncreaseAnimation, this);
            Storyboard.SetTargetProperty(IncreaseAnimation, new PropertyPath(ValueProperty));
            IncreaseAnimationStoryboard.Children.Add(IncreaseAnimation);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            CaptureMouse();
            IsMouseLeftButtonPressed = true;
            this.PrevMousePos = e.GetPosition(this);
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            ReleaseMouseCapture();
            IsMouseLeftButtonPressed = false;
        }
        protected override void OnLostMouseCapture(MouseEventArgs e) {
            IsMouseLeftButtonPressed = false;
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            if(!IsMouseLeftButtonPressed) return;
            
            
            double changeX = e.GetPosition(this).X - this.PrevMousePos.X;
            this.PrevMousePos = e.GetPosition(this);
            MouseMoveAnimation.To = Value + changeX;
            MouseMoveAnimationStoryboard.Begin();
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            base.OnMouseWheel(e);
            if(e.Delta > 0) SetZoomValue(Value + MouseWheelDelta, 0.5);
            else SetZoomValue(Value - MouseWheelDelta, 0.5);
        }
        public virtual void SetZoomValue(double value, double duration) {
            
            
            MouseWheelAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));
            MouseWheelAnimation.To = value;
            MouseWheelAnimationStoryboard.Begin();
        }
        Point PrevMousePos;
        Storyboard MouseMoveAnimationStoryboard;
        DoubleAnimation MouseMoveAnimation;
        Storyboard MouseWheelAnimationStoryboard;
        DoubleAnimation MouseWheelAnimation;
        Storyboard IncreaseAnimationStoryboard;
        DoubleAnimation IncreaseAnimation;
    }
}
