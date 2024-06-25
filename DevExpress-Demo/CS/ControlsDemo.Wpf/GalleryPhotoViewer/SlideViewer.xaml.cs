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
using System.Windows.Media.Animation;
using DevExpress.Xpf.Utils;

namespace ControlsDemo.GalleryDemo {
    public partial class SlideViewer : UserControl {
        public static readonly DependencyProperty NextSlideImageSourceProperty =
            DependencyPropertyManager.Register("NextSlideImageSource", typeof(ImageSource), typeof(SlideViewer), new FrameworkPropertyMetadata(null));
        public ImageSource NextSlideImageSource {
            get { return (ImageSource)GetValue(NextSlideImageSourceProperty); }
            set { SetValue(NextSlideImageSourceProperty, value); }
        }

        public event EventHandler BeforeCurrentSlideLoading;

        public SlideViewer() {
            InitializeComponent();
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            this.currentSlideControl = slide1Control;
            this.nextSlideControl = slide2Control;
        }
        public void Play() {
            this.currentSlideControl = slide1Control;
            this.nextSlideControl = slide2Control;
            this.currentSlideControl.Source = null;
            this.currentSlideControl.Opacity = 1;
            this.nextSlideControl.Source = NextSlideImageSource;
            this.nextSlideControl.Opacity = 0;
            BeginSlideChanging();
        }
        public void Stop() {
            this.changeSlideStoryboard.Stop();
            this.changeSlideStoryboard.Completed -= ChangeSlideStoryboard_Completed;
            this.changeSlideStoryboard = null;
        }

        protected Storyboard CreateChangedSlideStoryboard() {
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(5));
            Random rnd = new Random();
            switch(rnd.Next(5)) {
                case 0:
                    CreateMoveUpAnimation(storyboard);
                    break;
                case 1:
                    CreateMoveLeftAnimation(storyboard);
                    break;
                case 2:
                    CreateMoveDownAnimation(storyboard);
                    break;
                case 3:
                    CreateMoveRightAnimation(storyboard);
                    break;
                case 4:
                    CreateOpacityAnimation(storyboard);
                    break;
            }
            return storyboard;
        }
        protected void CreateOpacityAnimation(Storyboard storyboard) {
            this.currentSlideControl.RenderTransform = null;
            this.nextSlideControl.RenderTransform = null;
            this.currentSlideControl.Opacity = 1;
            this.nextSlideControl.Opacity = 0;
            DoubleAnimation slide1Animation = new DoubleAnimation();
            slide1Animation.To = 1;
            slide1Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            Storyboard.SetTarget(slide1Animation, this.nextSlideControl);
            Storyboard.SetTargetProperty(slide1Animation, new PropertyPath("Opacity"));
            DoubleAnimation slide2Animation = new DoubleAnimation();
            slide2Animation.To = 0;
            slide2Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            Storyboard.SetTarget(slide2Animation, this.currentSlideControl);
            Storyboard.SetTargetProperty(slide2Animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(slide1Animation);
            storyboard.Children.Add(slide2Animation);
        }
        protected void CreateMoveRightAnimation(Storyboard storyboard) {
            this.currentSlideControl.Opacity = 1;
            this.nextSlideControl.Opacity = 1;
            this.currentSlideControl.RenderTransform = new TranslateTransform();
            DoubleAnimation slide1Animation = new DoubleAnimation();
            slide1Animation.To = this.ActualWidth;
            slide1Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide1Animation, this.currentSlideControl, "X");
            this.nextSlideControl.RenderTransform = new TranslateTransform() { X = -this.ActualWidth };
            DoubleAnimation slide2Animation = new DoubleAnimation();
            slide2Animation.To = 0;
            slide2Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide2Animation, this.nextSlideControl, "X");
            storyboard.Children.Add(slide1Animation);
            storyboard.Children.Add(slide2Animation);
        }
        protected void CreateMoveLeftAnimation(Storyboard storyboard) {
            this.currentSlideControl.Opacity = 1;
            this.nextSlideControl.Opacity = 1;
            this.currentSlideControl.RenderTransform = new TranslateTransform();
            DoubleAnimation slide1Animation = new DoubleAnimation();
            slide1Animation.To = -this.ActualWidth;
            slide1Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide1Animation, this.currentSlideControl, "X");
            this.nextSlideControl.RenderTransform = new TranslateTransform() { X = this.ActualWidth };
            DoubleAnimation slide2Animation = new DoubleAnimation();
            slide2Animation.To = 0;
            slide2Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide2Animation, this.nextSlideControl, "X");
            storyboard.Children.Add(slide1Animation);
            storyboard.Children.Add(slide2Animation);
        }
        protected void CreateMoveUpAnimation(Storyboard storyboard) {
            this.currentSlideControl.Opacity = 1;
            this.nextSlideControl.Opacity = 1;
            this.currentSlideControl.RenderTransform = new TranslateTransform();
            DoubleAnimation slide1Animation = new DoubleAnimation();
            slide1Animation.To = -this.ActualHeight;
            slide1Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide1Animation, this.currentSlideControl, "Y");
            this.nextSlideControl.RenderTransform = new TranslateTransform() { Y = this.ActualHeight };
            DoubleAnimation slide2Animation = new DoubleAnimation();
            slide2Animation.To = 0;
            slide2Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide2Animation, this.nextSlideControl, "Y");
            storyboard.Children.Add(slide1Animation);
            storyboard.Children.Add(slide2Animation);
        }
        protected void CreateMoveDownAnimation(Storyboard storyboard) {
            this.currentSlideControl.Opacity = 1;
            this.nextSlideControl.Opacity = 1;
            this.currentSlideControl.RenderTransform = new TranslateTransform();
            DoubleAnimation slide1Animation = new DoubleAnimation();
            slide1Animation.To = this.ActualHeight;
            slide1Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide1Animation, this.currentSlideControl, "Y");
            this.nextSlideControl.RenderTransform = new TranslateTransform() { Y = -this.ActualHeight };
            DoubleAnimation slide2Animation = new DoubleAnimation();
            slide2Animation.To = 0;
            slide1Animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SetAnimationTarget(slide2Animation, this.nextSlideControl, "Y");
            storyboard.Children.Add(slide1Animation);
            storyboard.Children.Add(slide2Animation);
        }

        void ChangeSlideStoryboard_Completed(object sender, EventArgs e) {
            this.changeSlideStoryboard.Completed -= ChangeSlideStoryboard_Completed;
            this.currentSlideControl.BeginAnimation(UIElement.OpacityProperty, null);
            this.nextSlideControl.BeginAnimation(UIElement.OpacityProperty, null);
            Image slideControl = this.currentSlideControl;
            this.currentSlideControl = this.nextSlideControl;
            this.nextSlideControl = slideControl;
            if(BeforeCurrentSlideLoading != null)
                BeforeCurrentSlideLoading(this, new EventArgs());
            this.nextSlideControl.Source = NextSlideImageSource;
            BeginSlideChanging();
        }
        void BeginSlideChanging() {
            if(this.changeSlideStoryboard != null)
                this.changeSlideStoryboard.Completed -= ChangeSlideStoryboard_Completed;
            this.changeSlideStoryboard = CreateChangedSlideStoryboard();
            this.changeSlideStoryboard.Completed += new EventHandler(ChangeSlideStoryboard_Completed);
            this.changeSlideStoryboard.Begin();
        }

        void SetAnimationTarget(DoubleAnimation animation, Image target, string path) {
            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.(TranslateTransform." + path + ")"));
        }

        Image currentSlideControl { get; set; }
        Image nextSlideControl { get; set; }
        Storyboard changeSlideStoryboard { get; set; }
    }
}
