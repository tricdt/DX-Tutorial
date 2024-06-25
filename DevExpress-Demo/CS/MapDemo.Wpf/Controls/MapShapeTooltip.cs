using System;
using System.Windows;
using System.Windows.Controls;

namespace MapDemo {
    public class MapShapeTooltip : Control {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
           typeof(string), typeof(MapShapeTooltip), new PropertyMetadata(String.Empty));
        public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top",
           typeof(double), typeof(MapShapeTooltip), new PropertyMetadata(0.0));
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left",
           typeof(double), typeof(MapShapeTooltip), new PropertyMetadata(0.0));

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public double Left {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }
        public double Top {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        const double verticalOffset = -30;
        const double horizontalOffset = 0;

        public MapShapeTooltip() {
            DefaultStyleKey = typeof(MapShapeTooltip);
        }
        public void Show(FrameworkElement relativeTo, Point position, string text) {
            Left = position.X + horizontalOffset;
            Top = position.Y + verticalOffset;
            Text = text;
            Visibility = System.Windows.Visibility.Visible;
        }
        public void Hide() {
            Visibility = System.Windows.Visibility.Collapsed;
            Text = "";
        }
    }
}
