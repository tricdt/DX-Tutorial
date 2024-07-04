using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyDX_Demo.Controls
{
    public class Clock : Control
    {
        public bool ShowSeconds
        {
            get { return (bool)GetValue(ShowSecondsProperty); }
            set { SetValue(ShowSecondsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ShowSeconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowSecondsProperty =
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(Clock), new PropertyMetadata(true));

        public static readonly RoutedEvent TimeChangedEvent =
            EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(Clock));
        public event RoutedPropertyChangedEventHandler<DateTime> TimeChanged
        {
            add
            {
                AddHandler(TimeChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TimeChangedEvent, value);
            }
        }
        public override void OnApplyTemplate()
        {
            //Binding showSecondHandBinding = new Binding
            //{
            //    Path = new PropertyPath(nameof(ShowSeconds)),
            //    Source=this,
            //    Converter = new BooleanToVisibilityConverter()
            //};
            //secondHand.SetBinding(VisibilityProperty, showSecondHandBinding);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (s, e) => OnTimeChanged(DateTime.Now);
            timer.Start();
            base.OnApplyTemplate();
        }
        protected virtual void OnTimeChanged(DateTime time)
        {
            UpdateTimeState(time);
            RaiseEvent(new RoutedPropertyChangedEventArgs<DateTime>(DateTime.Now.AddSeconds(-1), DateTime.Now, TimeChangedEvent));
        }
        private void UpdateTimeState(DateTime time)
        {
            if (time.Hour > 6 && time.Hour < 18)
            {
                VisualStateManager.GoToState(this, "Day", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Night", false);
            }
        }
    }
}
