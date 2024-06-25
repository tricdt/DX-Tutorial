using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Windows.Media.Animation;
using System.Windows.Input;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Map.Native;


namespace MapDemo {
    public class PhotoGalleryControl : VisibleControl, IViewportAnimatableElement {
        public static readonly DependencyProperty ActualItemsProperty = DependencyProperty.Register("ActualItems",
            typeof(ObservableCollection<object>), typeof(PhotoGalleryControl), new PropertyMetadata(null));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable), typeof(PhotoGalleryControl), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourcePropertyChanged)));
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate",
            typeof(DataTemplate), typeof(PhotoGalleryControl), new PropertyMetadata(null));
        public static readonly DependencyProperty AnchorPointProperty = DependencyProperty.Register("AnchorPoint",
            typeof(Point), typeof(PhotoGalleryControl), new PropertyMetadata(new Point(0, 0)));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",
            typeof(string), typeof(PhotoGalleryControl), new PropertyMetadata(string.Empty));

        static void ItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PhotoGalleryControl gallery = d as PhotoGalleryControl;
            if(gallery != null)
                gallery.Update();
        }

        [Browsable(false)]
        public ObservableCollection<object> ActualItems {
            get { return (ObservableCollection<object>)GetValue(ActualItemsProperty); }
        }
        public IEnumerable ItemsSource {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public DataTemplate ItemTemplate {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        public Point AnchorPoint {
            get { return (Point)GetValue(AnchorPointProperty); }
            set { SetValue(AnchorPointProperty, value); }
        }
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        readonly AnimationProgress progress;
        Storyboard storyboard = null;
        bool animationInProgress = false;
        ItemsControl elements = null;

        Panel LayoutPanel { get { return CommonUtils.GetChildPanel(elements); } }
        Storyboard Storyboard {
            get {
                if(storyboard == null) {
                    storyboard = new Storyboard();
                    storyboard.Completed += new EventHandler(StoryboardCompleted);
                    AnimationHelper.AddStoryboard(this, storyboard, storyboard.GetHashCode());
                }
                return storyboard;
            }
        }
        public double AnimationProgress { get { return progress.ActualProgress; } }

        public PhotoGalleryControl() {
            DefaultStyleKey = typeof(PhotoGalleryControl);
            progress = new AnimationProgress(this);
        }
        bool IViewportAnimatableElement.InProgress { get { return animationInProgress; } }
        void IViewportAnimatableElement.ProgressChanged() {
            Invalidate();
        }
        void StoryboardCompleted(object sender, EventArgs e) {
            StopAnimation();
        }
        void StopAnimation() {
            animationInProgress = false;
            progress.FinishAnimation();
        }
        void Animate() {
            if(progress.ActualProgress > 0) {
                StopAnimation();
                PrepareStoryboard();
                animationInProgress = true;
                progress.StartAnimation();
                this.Storyboard.Begin();
            }
        }
        void PrepareStoryboard() {
            this.Storyboard.Stop();
            this.Storyboard.Children.Clear();
            AnimationHelper.PrepareStoryboard(Storyboard, progress, "Progress");
        }
        void Update() {
            ObservableCollection<object> items = new ObservableCollection<object>();
            if(ItemsSource != null)
                foreach(object item in ItemsSource)
                    items.Add(item);
            SetValue(ActualItemsProperty, items);
        }
        void Invalidate() {
            if(LayoutPanel != null)
                LayoutPanel.InvalidateMeasure();
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            elements = GetTemplateChild("PART_Elements") as ItemsControl;
        }
        protected override void VisibleChanged() {
            base.VisibleChanged();
            if(Visible)
                Animate();
        }
    }

    public interface IViewportAnimatableElement {
        bool InProgress { get; }
        void ProgressChanged();
    }

    public class AnimationProgress : DependencyObject {
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress",
            typeof(double), typeof(AnimationProgress), new PropertyMetadata(1.0, new PropertyChangedCallback(ProgressPropertyChanged)));

        public double Progress {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        static void ProgressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            AnimationProgress animationProgress = d as AnimationProgress;
            if(animationProgress != null)
                animationProgress.RaiseProgressChanged();
        }

        readonly IViewportAnimatableElement animationTarget;

        bool InProgress {
            get { return animationTarget != null ? animationTarget.InProgress : false; }
        }
        public double ActualProgress {
            get {
                if(InProgress)
                    return Progress;
                return 1.0;
            }
        }
        public AnimationProgress(IViewportAnimatableElement animationTarget) {
            this.animationTarget = animationTarget;
        }
        protected void RaiseProgressChanged() {
            if(animationTarget != null)
                animationTarget.ProgressChanged();
        }
        public void StartAnimation() {
            Progress = 0.0;
            RaiseProgressChanged();
        }
        public void FinishAnimation() {
            Progress = 1.0;
        }
    }

    public class AnimationHelper {
        static Timeline CreateTimeline() {
            DoubleAnimation timeline = new DoubleAnimation();
            timeline.From = 0;
            timeline.To = 1.0;
            timeline.Duration = new Duration(TimeSpan.FromSeconds(2.0));
            timeline.BeginTime = TimeSpan.Zero;
            PowerEase easingFunction = new PowerEase();
            easingFunction.EasingMode = EasingMode.EaseInOut;
            easingFunction.Power = 3.0;
            timeline.EasingFunction = easingFunction;
            return timeline;
        }
        public static void PrepareStoryboard(Storyboard storyboard, AnimationProgress progress, string propertyName) {
            Timeline timeline = CreateTimeline();
            Storyboard.SetTarget(timeline, progress);
            Storyboard.SetTargetProperty(timeline, new PropertyPath(propertyName));
            storyboard.Children.Add(timeline);
            storyboard.BeginTime = TimeSpan.Zero;
        }
        public static void AddStoryboard(Control owner, Storyboard storyboard, int resourceKey) {
            if(storyboard != null && !owner.Resources.Contains(resourceKey.ToString()))
                owner.Resources.Add(resourceKey.ToString(), storyboard);
        }
    }

    public class PhotoGalleryPanel : Panel {
        const double defaultWidth = 300.0;
        const double defaultHeight = 300.0;

        int rowCount = 0;
        int columnCount = 0;

        Point TransformedPoint { get { return ((FrameworkElement)LayoutHelper.GetTopLevelVisual(this)).TransformToVisual(this).Transform(AnchorPoint); } }

        PhotoGalleryControl Gallery { get { return DataContext as PhotoGalleryControl; } }
        Point AnchorPoint { get { return Gallery != null ? Gallery.AnchorPoint : new Point(0, 0); } }

        double GetProgress(int elementNumber, int elementCount) {
            if(Gallery == null)
                return 1d;
            double step = 1d / (elementCount + 1);
            double result = 0.5d * (Gallery.AnimationProgress - elementNumber * step) / step;
            return Math.Max(0d, Math.Min(1d, result));
        }
        void CalculateLayout(Size constraint, int elementCount) {
            double sizeRatio = constraint.Width / constraint.Height * 0.5;
            rowCount = (int)Math.Max(1, Math.Round(Math.Sqrt(elementCount / sizeRatio)));
            columnCount = (int)Math.Ceiling((double)elementCount / (double)rowCount);
        }
        Size AnimateSize(Size anchorSize, int elementNumber, int elementCount) {
            double progress = GetProgress(elementNumber, elementCount);
            return new Size(anchorSize.Width * progress, anchorSize.Height * progress);
        }
        Rect AnimateRect(Rect targetRect, int elementNumber, int elementCount) {
            double progress = GetProgress(elementNumber, elementCount);
            return new Rect(TransformedPoint.X + progress * (targetRect.X - TransformedPoint.X), TransformedPoint.Y + progress * (targetRect.Y - TransformedPoint.Y),
                targetRect.Width * progress, targetRect.Height * progress);
        }
        Rect CalculateElementLayout(Size elementSize, int elementNumber) {
            double x = (elementNumber % columnCount) * elementSize.Width;
            double y = Math.Ceiling((double)(elementNumber / columnCount)) * elementSize.Height;
            return new Rect(x, y, elementSize.Width, elementSize.Height);
        }

        protected override Size MeasureOverride(Size availableSize) {
            double constraintWidth = double.IsInfinity(availableSize.Width) ? defaultWidth : availableSize.Width;
            double constraintHeight = double.IsInfinity(availableSize.Height) ? defaultHeight : availableSize.Height;
            Size constraint = new Size(constraintWidth, constraintHeight);
            CalculateLayout(constraint, Children.Count);
            Size elementSize = new Size(constraint.Width / (double)columnCount, constraint.Height / (double)rowCount);
            double width = 0, height = 0;
            for(int i = 0; i < Children.Count; i++) {
                Children[i].Measure(elementSize);
                width = Math.Max(width, Children[i].DesiredSize.Width);
                height = Math.Max(height, Children[i].DesiredSize.Height);
            }
            return new Size(columnCount * width, rowCount * height);
        }
        protected override Size ArrangeOverride(Size arrangeSize) {
            for(int i = 0; i < Children.Count; i++) {
                Children[i].Opacity = GetProgress(i, Children.Count);
                Children[i].Arrange(AnimateRect(CalculateElementLayout(Children[i].DesiredSize, i), i, Children.Count));
            }
            return arrangeSize;
        }
    }

}
