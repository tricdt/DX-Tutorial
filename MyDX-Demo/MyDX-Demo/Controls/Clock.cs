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
    [TemplateVisualState(Name = "Day", GroupName = "TimeStates")]
    [TemplateVisualState(Name = "Night", GroupName = "TimeStates")]
    public class Clock : Control
    {
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(Clock), new PropertyMetadata(DateTime.Now));


        public static readonly DependencyProperty ShowSecondsProperty =
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(Clock), new PropertyMetadata(true));

        public static readonly RoutedEvent TimeChangedEvent =
            EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(Clock));
        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        public bool ShowSeconds
        {
            get { return (bool)GetValue(ShowSecondsProperty); }
            set { SetValue(ShowSecondsProperty, value); }
        }
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
        protected virtual void OnTimeChanged(DateTime newTime)
        {
            UpdateTimeState(newTime);
            RaiseEvent(new RoutedPropertyChangedEventArgs<DateTime>(Time, newTime, TimeChangedEvent));
            Time= newTime;
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
