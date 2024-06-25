using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public class MapEditorViewModel : BindableBase {
        readonly Dictionary<Type, long> itemIndexes = new Dictionary<Type, long>();

        ObservableCollection<MapItem> activeItems;
        Color fillColor;
        Color strokeColor;

        public ObservableCollection<MapItem> ActiveItems {
            get { return activeItems; }
            set {
                activeItems = value;
                OnActiveItemsChanged();
            }
        }
        public Color FillColor {
            get { return fillColor; }
            set {
                if(fillColor == value)
                    return;
                fillColor = value;
                OnColorValueChanged();
            }
        }
        public Color StrokeColor {
            get { return strokeColor; }
            set {
                if(strokeColor == value)
                    return;
                strokeColor = value;
                OnColorValueChanged();
            }
        }

        public MapEditorViewModel() {
            SetDefaultColor();
        }

        string GenerateName(MapItem item) {
            Type itemType = item.GetType();
            if(!itemIndexes.ContainsKey(itemType))
                itemIndexes[itemType] = 0;
            itemIndexes[itemType]++;
            return string.Format("{0} {1}", itemType.Name, itemIndexes[itemType]);
        }
        void SetDefaultColor() {
            fillColor = Color.FromArgb(142, 0, 176, 80);
            strokeColor = Colors.White;
            RaisePropertyChanged("FillColor");
            RaisePropertyChanged("StrokeColor");
        }
        void OnColorValueChanged() {
            if(ActiveItems != null)
                foreach(MapItem item in ActiveItems)
                    UpdateColor(item);
        }
        void OnActiveItemsChanged() {
            MapShapeBase shape = (ActiveItems != null && ActiveItems.Count == 1) ? ActiveItems[0] as MapShapeBase : null;
            if(shape != null) {
                this.fillColor = ((SolidColorBrush)shape.Fill).Color;
                this.strokeColor = ((SolidColorBrush)shape.Stroke).Color;
                RaisePropertyChanged("FillColor");
                RaisePropertyChanged("StrokeColor");
            }
            else
                SetDefaultColor();
        }
        void UpdateColor(MapItem item) {
            MapShapeBase shape = item as MapShapeBase;
            if(shape != null) {
                shape.Fill = new SolidColorBrush(FillColor);
                shape.Stroke = new SolidColorBrush(StrokeColor);
            }
        }

        public void OnShapesLoaded(List<MapItem> items) {
            items[0].EnableHighlighting = false;
            items[0].IsHitTestVisible = false;
            ActiveItems = new ObservableCollection<MapItem>() { items[50] };
        }
        public void OnMapItemCreating(MapItem item) {
            item.Attributes.Add(new MapItemAttribute() { Name = "name", Value = GenerateName(item) });
            UpdateColor(item);
        }
        public void Export(VectorLayerBase layer) {
            using(var dialog = new DXSaveFileDialog()) {
                dialog.Filter = "KML files|*.kml";
                dialog.CreatePrompt = true;
                dialog.OverwritePrompt = true;
                if(dialog.ShowDialog().Value) {
                    layer.ExportToKml(dialog.FileName);
                    ThemedMessageBox.Show("Info", string.Format("Items successfully exported to {0} file", dialog.FileName),
                      MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }

    public class SelectedItemToPerimeterConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            MapShape shape = value as MapShape;
            return shape != null ? Math.Round(GeoUtils.CalculateStrokeLength(shape), 3) + " m" : string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class SelectedItemToAreaConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            MapShape shape = value as MapShape;
            return shape != null ? Math.Round(GeoUtils.CalculateArea(shape), 3) + " m\xB2" : string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class SelectedItemToDiameterConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            MapDot dot = value as MapDot;
            return dot != null ? (int)dot.Size + " dip" : string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class FontHeightToRectangleSizeConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)((double)value / 2);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class EditorModeToToolTipEnabledConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class ItemToolTipTemplateConverter : MarkupExtension, IValueConverter {
        public DataTemplate ShapeTemplate { get; set; }
        public DataTemplate DotTemplate { get; set; }
        public DataTemplate PinTemplate { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is MapDot)
                return DotTemplate;
            return value is MapShape ? ShapeTemplate : PinTemplate;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
