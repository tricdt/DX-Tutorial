using System.Windows;
using System.Windows.Markup;

namespace ChartsDemo {
    [ContentProperty("SeriesTemplate")]
    public class SeriesInfo : DependencyObject {
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register("DataSource", typeof(object), typeof(SeriesInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(SeriesInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty SeriesTemplateProperty = DependencyProperty.Register("SeriesTemplate", typeof(DataTemplate), typeof(SeriesInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty DiagramTypeProperty = DependencyProperty.Register("DiagramType", typeof(DiagramType), typeof(SeriesInfo), new PropertyMetadata(DiagramType.XY));

        public string Content {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public object DataSource {
            get { return GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public DataTemplate SeriesTemplate {
            get { return (DataTemplate)GetValue(SeriesTemplateProperty); }
            set { SetValue(SeriesTemplateProperty, value); }
        }
        public DiagramType DiagramType {
            get { return (DiagramType)GetValue(DiagramTypeProperty); }
            set { SetValue(DiagramTypeProperty, value); }
        }
    }

    public enum DiagramType {
        XY,
        Simple,
        Radar,
        Polar,
        XY3D
    }
}
