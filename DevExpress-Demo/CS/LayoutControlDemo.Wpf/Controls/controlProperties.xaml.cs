using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    public partial class controlProperties : UserControl {
        #region Dependency Properties

        public static readonly DependencyProperty LayoutControlProperty =
            DependencyProperty.Register("LayoutControl", typeof(LayoutControlBase), typeof(controlProperties),
                new PropertyMetadata(new PropertyChangedCallback(OnLayoutControlChanged)));
        public static readonly DependencyProperty LayoutControlPropertiesProperty =
            DependencyProperty.Register("LayoutControlProperties", typeof(FrameworkElement), typeof(controlProperties), null);
        public static readonly DependencyProperty ItemPropertiesProperty =
            DependencyProperty.Register("ItemProperties", typeof(FrameworkElement), typeof(controlProperties), null);
        public static readonly DependencyProperty ItemPropertiesHeaderTemplateProperty =
            DependencyProperty.Register("ItemPropertiesHeaderTemplate", typeof(DataTemplate), typeof(controlProperties), null);
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(controlProperties),
                new PropertyMetadata(new PropertyChangedCallback(OnSelectedItemChanged)));

        static void OnLayoutControlChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((controlProperties)o).OnLayoutControlChanged();
        }
        static void OnSelectedItemChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((controlProperties)o).OnSelectedItemChanged(e.OldValue, e.NewValue);
        }

        #endregion Dependency Properties

        public controlProperties() {
            InitializeComponent();
        }

        public LayoutControlBase LayoutControl {
            get { return (LayoutControlBase)GetValue(LayoutControlProperty); }
            set { SetValue(LayoutControlProperty, value); }
        }
        public FrameworkElement LayoutControlProperties {
            get { return (FrameworkElement)GetValue(LayoutControlPropertiesProperty); }
            set { SetValue(LayoutControlPropertiesProperty, value); }
        }
        public FrameworkElement ItemProperties {
            get { return (FrameworkElement)GetValue(ItemPropertiesProperty); }
            set { SetValue(ItemPropertiesProperty, value); }
        }
        public DataTemplate ItemPropertiesHeaderTemplate {
            get { return (DataTemplate)GetValue(ItemPropertiesHeaderTemplateProperty); }
            set { SetValue(ItemPropertiesHeaderTemplateProperty, value); }
        }
        public object SelectedItem {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        Brush GetSelectedTileBackgroundMask() {
            var gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop { Offset = 0, Color = Color.FromArgb(70, 0, 0, 0) });
            gradientStops.Add(new GradientStop { Offset = 0.4, Color = Color.FromArgb(0, 0, 0, 0) });
            gradientStops.Add(new GradientStop { Offset = 0.6, Color = Color.FromArgb(0, 0, 0, 0) });
            gradientStops.Add(new GradientStop { Offset = 1, Color = Color.FromArgb(70, 0, 0, 0) });
            return new LinearGradientBrush(gradientStops, 0);
        }
        void OnInitLayoutControl(LayoutControlBase layoutControl) {
            foreach (FrameworkElement child in layoutControl.GetLogicalChildren(false))
                if (child is SampleLayoutItem)
                    ((SampleLayoutItem)child).IsSelectedChanged += OnItemIsSelectedChanged;
                else
                    if (child is Tile)
                        child.MouseLeftButtonDown += OnTileMouseLeftButtonDown;
                    else
                        if (child is LayoutControlBase)
                            OnInitLayoutControl((LayoutControlBase)child);
        }
        void OnItemIsSelectedChanged(object sender, EventArgs e) {
            var layoutItem = (SampleLayoutItem)sender;
            if (layoutItem.IsSelected)
                SelectedItem = layoutItem;
        }
        void OnLayoutControlChanged() {
            OnInitLayoutControl(LayoutControl);
        }
        void OnSelectedItemChanged(object oldValue, object newVaue) {
            if (oldValue is SampleLayoutItem)
                ((SampleLayoutItem)oldValue).IsSelected = false;
            if (oldValue is Tile) {
                var tile = (Tile)oldValue;
                tile.SetValue(Tile.CalculatedBackgroundProperty, SelectedTileCalculatedBackground);
                SelectedTileCalculatedBackground = null;
                tile.Loaded -= OnSelectedTileLoaded;
            }
            if (SelectedItem is Tile) {
                var tile = (Tile)SelectedItem;
                SelectedTileCalculatedBackground = tile.CalculatedBackground;
                UpdateSelectedTileBackgroundMask();
                tile.Loaded += OnSelectedTileLoaded;
            }
        }
        void OnSelectedTileLoaded(object sender, RoutedEventArgs e) {
            if (!LayoutControl.Controller.IsDragAndDrop)
                UpdateSelectedTileBackgroundMask();
        }
        void OnTileMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            SelectedItem = sender;
        }
        void UpdateSelectedTileBackgroundMask() {
            ((Tile)SelectedItem).SetValue(Tile.CalculatedBackgroundProperty, GetSelectedTileBackgroundMask());
        }

        Brush SelectedTileCalculatedBackground { get; set; }
    }

    public class ObjectToStringConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return string.IsNullOrEmpty((string)value) ? null : value;
        }
    }

    public class ObjectToTypeNameConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null)
                return value.GetType().Name;
            else
                return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ItemToHeaderConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null)
                if (value is SampleLayoutItem)
                    return ((SampleLayoutItem)value).Caption;
                else
                    if (value is Tile)
                        return ((Tile)value).Header as string;
                    else
                        return value.GetType().Name;
            else
                return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
