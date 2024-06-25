using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using DevExpress.VideoRent.ViewModel;
using DevExpress.Xpf.Charts;

namespace DevExpress.VideoRent.Wpf {
    public interface IChartSeriesGenerator {
        ChartControl ChartControl { get; set; }
    }
    public class GeneratorToChartAttacher {
        #region Dependency Properties
        public static readonly DependencyProperty ChartSeriesGeneratorProperty;
        static GeneratorToChartAttacher() {
            Type ownerType = typeof(GeneratorToChartAttacher);
            ChartSeriesGeneratorProperty = DependencyProperty.RegisterAttached("ChartSeriesGenerator", typeof(IChartSeriesGenerator), ownerType, new PropertyMetadata(null, OnChartGeneratorChanged));
        }
        #endregion
        public static IChartSeriesGenerator GetChartSeriesGenerator(DependencyObject d) { return (IChartSeriesGenerator)d.GetValue(ChartSeriesGeneratorProperty); }
        public static void SetChartSeriesGenerator(DependencyObject d, IChartSeriesGenerator value) { d.SetValue(ChartSeriesGeneratorProperty, value); }
        static void OnChartGeneratorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(d is ChartControl && e.NewValue is IChartSeriesGenerator) {
                ((IChartSeriesGenerator)e.NewValue).ChartControl = d as ChartControl;
            }
        }
    }
    public class CategoryPricesChartSeriesGenerator : DependencyObject, IChartSeriesGenerator {
        const string reserveFieldName = "LateRentPrice";
        const string fieldNamePattern = "Days{0}RentPrice";
        const string pointsKeyPattern = "{0}_{1}";

        static Dictionary<string, PropertyInfo> propertyInfos = new Dictionary<string, PropertyInfo>();

        #region Dependency Properties
        public static readonly DependencyProperty ChartControlProperty;
        public static readonly DependencyProperty MovieCategoryProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty IsChartVisibleProperty;
        static CategoryPricesChartSeriesGenerator() {
            Type ownerType = typeof(CategoryPricesChartSeriesGenerator);
            ChartControlProperty = DependencyProperty.Register("ChartControl", typeof(ChartControl), ownerType, new PropertyMetadata(null, (d, e) => ((CategoryPricesChartSeriesGenerator)d).OnChartControlChanged(e)));
            MovieCategoryProperty = DependencyProperty.Register("MovieCategory", typeof(MovieCategory), ownerType, new PropertyMetadata(null, (d, e) => ((CategoryPricesChartSeriesGenerator)d).OnMovieCategoryChanged(e)));
            ItemStyleProperty = DependencyProperty.Register("ItemStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((CategoryPricesChartSeriesGenerator)d).OnItemStyleChanged(e)));
            IsChartVisibleProperty = DependencyProperty.Register("IsChartVisible", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((CategoryPricesChartSeriesGenerator)d).OnIsChartVisibleChanged(e)));
        }
        #endregion
        Dictionary<string, SeriesPoint> pointsDictionary = new Dictionary<string, SeriesPoint>();

        public ChartControl ChartControl {
            get { return (ChartControl)GetValue(ChartControlProperty); }
            set { SetValue(ChartControlProperty, value); }
        }
        public MovieCategory MovieCategory {
            get { return (MovieCategory)GetValue(MovieCategoryProperty); }
            set { SetValue(MovieCategoryProperty, value); }
        }
        public Style ItemStyle {
            get { return (Style)GetValue(ItemStyleProperty); }
            set { SetValue(ItemStyleProperty, value); }
        }
        public bool IsChartVisible {
            get { return (bool)GetValue(IsChartVisibleProperty); }
            set { SetValue(IsChartVisibleProperty, value); }
        }
        void OnChartControlChanged(DependencyPropertyChangedEventArgs e) {
            UpdateBindingToIsVisible();
            GenerateSeries();
        }
        void OnMovieCategoryChanged(DependencyPropertyChangedEventArgs e) {
            pointsDictionary.Clear();
            UpdateBindingToIsVisible();
            GenerateSeries();
        }
        void OnItemStyleChanged(DependencyPropertyChangedEventArgs e) {
            if(ChartControl == null) return;
            if(!ChartControl.IsVisible) return;
            foreach(LineStackedSeries2D serie in ChartControl.Diagram.Series) {
                serie.Style = (Style)e.NewValue;
            }
        }
        void OnIsChartVisibleChanged(DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue)
                GenerateSeries();
        }
        void OnRentPricePropertyChanged(object sender, PropertyChangedEventArgs e) {
            UpdatePoint(sender as MovieCategoryPrice, e.PropertyName);
        }
        void UpdatePoint(MovieCategoryPrice price, string fieldName) {
            SeriesPoint point = null;
            if(fieldName == reserveFieldName) {
                UpdateAllPoints(price);
            } else {
                if(!string.IsNullOrEmpty(fieldName) && pointsDictionary.TryGetValue(string.Format(pointsKeyPattern, ((MovieCategoryPrice)price).Format, fieldName), out point)) {
                    point.Value = GetPropertyValueByNameWithEnsure(price, fieldName);
                }
            }
        }
        void UpdateAllPoints(MovieCategoryPrice price) {
            for(int i = 1; i < 8; ++i) {
                string fieldName = string.Format(fieldNamePattern, i);
                string key = string.Format(pointsKeyPattern, price.Format, fieldName);
                SeriesPoint point = null;
                if(pointsDictionary.TryGetValue(key, out point)) {
                    point.Value = GetPropertyValueByNameWithEnsure(price, fieldName);
                }
            }
        }
        void UpdateBindingToIsVisible() {
            BindingOperations.ClearBinding(this, IsChartVisibleProperty);
            BindingOperations.SetBinding(this, IsChartVisibleProperty, new Binding("IsVisible") { Source = ChartControl });
        }
        void GenerateSeries() {
            if(MovieCategory == null) return;
            if(ChartControl == null) return;
            if(!ChartControl.IsVisible) return;
            ChartControl.Diagram.Series.Clear();
            foreach(MovieCategoryPrice price in MovieCategory.Prices) {
                LineSeries2D serie = GenerateSerie(price);
                if(ItemStyle != null) serie.Style = ItemStyle;
                ChartControl.Diagram.Series.Add(serie);
                SeriesLabel label = new SeriesLabel();
                label.SetBinding(MarkerSeries2D.AngleProperty, new Binding() { Source = serie, Path = new PropertyPath(MarkerSeries2D.AngleProperty) });
                serie.Label = label;
            }
        }
        LineSeries2D GenerateSerie(MovieCategoryPrice price) {
            LineSeries2D serie = new LineSeries2D() { DisplayName = price.Format.ToString(), LabelsVisibility = true };
            for(int i = 1; i <= MovieCategoryPrice.TermsRentCount; ++i) {
                string fieldName = string.Format(fieldNamePattern, i);
                string key = string.Format(pointsKeyPattern, price.Format, fieldName);
                SeriesPoint point = null;
                if(!pointsDictionary.TryGetValue(key, out point)) {
                    point = new SeriesPoint(i, GetPropertyValueByNameWithEnsure(price, fieldName));
                    pointsDictionary.Add(key, point);
                }
                serie.Points.Add(point);
            }
            UpdateSubscriptionToPropertyChanged(price);
            return serie;
        }
        void UpdateSubscriptionToPropertyChanged(INotifyPropertyChanged implementator) {
            implementator.PropertyChanged -= OnRentPricePropertyChanged;
            implementator.PropertyChanged += OnRentPricePropertyChanged;
        }
        static object GetPropertyValueByName(MovieCategoryPrice price, string propertyName) {
            PropertyInfo propertyInfo;
            if(!propertyInfos.TryGetValue(propertyName, out propertyInfo)) {
                propertyInfo = typeof(MovieCategoryPrice).GetProperty(propertyName);
                propertyInfos.Add(propertyName, propertyInfo);
            }
            return propertyInfo.GetValue(price, null);
        }
        static double GetPropertyValueByNameWithEnsure(MovieCategoryPrice price, string propertyName) {
            object value = GetPropertyValueByName(price, propertyName);
            return value == null || (double)(decimal)value == 0.0 ? (double)(decimal)(GetPropertyValueByName(price, reserveFieldName)) : (double)(decimal)value;
        }
    }
    public partial class MovieFormatChartSourceGenerator : DependencyObject, IChartSeriesGenerator {
        #region Dependency Properties
        public static readonly DependencyProperty ChartControlProperty;
        public static readonly DependencyProperty FormatsItemSourceProperty;
        public static readonly DependencyProperty RentalsItemSourceProperty;
        public static readonly DependencyProperty SelectedFormatIndexProperty;
        static MovieFormatChartSourceGenerator() {
            Type ownerType = typeof(MovieFormatChartSourceGenerator);
            ChartControlProperty = DependencyProperty.Register("ChartControl", typeof(ChartControl), ownerType, new PropertyMetadata(null, (d, e) => ((MovieFormatChartSourceGenerator)d).OnChartControlChanged(e)));
            FormatsItemSourceProperty = DependencyProperty.Register("FormatsItemSource", typeof(List<FormatsChartSourceItem>), ownerType, new PropertyMetadata(null, new PropertyChangedCallback((d, e) => ((MovieFormatChartSourceGenerator)d).OnFormatsItemSourceChanged(e))));
            RentalsItemSourceProperty = DependencyProperty.Register("RentalsItemSource", typeof(List<RentalsChartSourceItem>), ownerType, new PropertyMetadata(null, new PropertyChangedCallback((d, e) => ((MovieFormatChartSourceGenerator)d).OnRentalsChanged(e))));
            SelectedFormatIndexProperty = DependencyProperty.Register("SelectedFormatIndex", typeof(int), ownerType, new PropertyMetadata(0, new PropertyChangedCallback((d, e) => ((MovieFormatChartSourceGenerator)d).OnSelectedIndexChanged(e))));
        }
        #endregion
        Series FormatSeries { get { return ChartControl == null ? null : ChartControl.Diagram.Series[0]; } }
        Series RentalSalesInfoSeries { get { return ChartControl == null ? null : ChartControl.Diagram.Series[1]; } }
        List<SerieSource> seriesPoints;
        bool skipEmptyPoints;

        public MovieFormatChartSourceGenerator() {
            skipEmptyPoints = true;
        }
        public int SelectedFormatIndex {
            get { return (int)GetValue(SelectedFormatIndexProperty); }
            set { SetValue(SelectedFormatIndexProperty, value); }
        }
        public List<FormatsChartSourceItem> FormatsItemSource {
            get { return (List<FormatsChartSourceItem>)GetValue(FormatsItemSourceProperty); }
            set { SetValue(FormatsItemSourceProperty, value); }
        }
        public List<RentalsChartSourceItem> RentalsItemSource {
            get { return (List<RentalsChartSourceItem>)GetValue(RentalsItemSourceProperty); }
            set { SetValue(RentalsItemSourceProperty, value); }
        }
        public ChartControl ChartControl {
            get { return (ChartControl)GetValue(ChartControlProperty); }
            set { SetValue(ChartControlProperty, value); }
        }
        void OnRentalsChanged(DependencyPropertyChangedEventArgs e) {
            UpdateRentalSeriesSource();
        }
        void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e) {
            UpdateRentalSeriesView();
        }
        void OnFormatsItemSourceChanged(DependencyPropertyChangedEventArgs e) {
            UpdateFormatSeries();
        }
        void OnChartControlChanged(DependencyPropertyChangedEventArgs e) {
            if(ChartControl != null) {
                SubscribeToCustomDraw();
                if(ChartControl.Diagram != null && ChartControl.Diagram.Series.Count >= 2) {
                    UpdateFormatSeries();
                    UpdateRentalSeriesSource();
                }
            }
        }
        void ChartCustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            if(skipEmptyPoints && e.SeriesPoint.Value == 0) e.LabelText = string.Empty;
        }
        void ProcessFormatRentals(RentalsChartSourceItem format) {
            List<SeriesPoint> points = new List<SeriesPoint>();
            foreach(KeyValuePair<string, int> pair in format.Points) {
                points.Add(new SeriesPoint() { Argument = pair.Key, Value = pair.Value });
            }
            seriesPoints.Add(new SerieSource(new Title() { Content = format.FormatName, HorizontalAlignment = HorizontalAlignment.Center }, points));
        }
        bool RetrieveSkipEmptyPoints() {
            if(SelectedFormatIndex < 0) return true;
            foreach(SeriesPoint point in seriesPoints[SelectedFormatIndex].Points) {
                if(point.Value != 0.0) return true;
            }
            return false;
        }
        void UpdateFormatSeries() {
            if(FormatSeries == null || FormatsItemSource == null) return;
            FormatSeries.Points.Clear();
            foreach(FormatsChartSourceItem pair in FormatsItemSource) {
                FormatSeries.Points.Add(new SeriesPoint() { Argument = pair.PointName, Value = pair.PointValue });
            }
            FormatSeries.Animate();
        }
        void UpdateRentalSeriesSource() {
            if(RentalSalesInfoSeries == null || RentalsItemSource == null) return;
            if(seriesPoints == null) seriesPoints = new List<SerieSource>();
            seriesPoints.Clear();
            foreach(RentalsChartSourceItem pair in RentalsItemSource) {
                ProcessFormatRentals(pair);
            }
            UpdateRentalSeriesView();
        }
        void UpdateRentalSeriesView() {
            if(seriesPoints == null || SelectedFormatIndex < 0) return;
            skipEmptyPoints = RetrieveSkipEmptyPoints();
            RentalSalesInfoSeries.Points.Clear();
            RentalSalesInfoSeries.Points.AddRange(seriesPoints[SelectedFormatIndex].Points);
            ((PieSeries2D)RentalSalesInfoSeries).Titles.Clear();
            ((PieSeries2D)RentalSalesInfoSeries).Titles.Add(seriesPoints[SelectedFormatIndex].Title);
            RentalSalesInfoSeries.Animate();
        }
        void SubscribeToCustomDraw() {
            ChartControl.CustomDrawSeriesPoint -= ChartCustomDrawSeriesPoint;
            ChartControl.CustomDrawSeriesPoint += ChartCustomDrawSeriesPoint;
        }
    }
    public class PaletteNameToPaletteConverter : IValueConverter {
        static Dictionary<string, Palette> palettesDictionary = new Dictionary<string, Palette>();

        static Dictionary<string, Palette> GeneratePalettesDictionary() {
            Dictionary<string, Palette> toReturn = new Dictionary<string, Palette>();
            foreach(PaletteKind item in Palette.GetPredefinedKinds()) {
                Palette palette = Activator.CreateInstance(item.Type) as Palette;
                if(palette == null) continue;
                toReturn.Add(palette.PaletteName, palette);
            }
            return toReturn;
        }
        static void UpdateDictionaryIfNeeded() {
            if(palettesDictionary == null || palettesDictionary.Count == 0)
                palettesDictionary = GeneratePalettesDictionary();
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is string)) return null;
            UpdateDictionaryIfNeeded();
            if(string.IsNullOrEmpty((string)value)) return null;
            Palette toReturn;
            if(!palettesDictionary.TryGetValue(value as string, out toReturn)) {
                toReturn = null;
            }
            return toReturn;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is Palette)) return null;
            return ((Palette)value).PaletteName;
        }
    }
    public class SerieSource {
        public Title Title { get; set; }
        public List<SeriesPoint> Points { get; set; }
        public SerieSource(Title title, List<SeriesPoint> points) {
            Title = title;
            Points = points;
        }
    }
    public class Marker2DKindNameToMarker2DModelConverter : IValueConverter {
        static Dictionary<string, Marker2DModel> models;
        static Marker2DKindNameToMarker2DModelConverter() {
            models = new Dictionary<string, Marker2DModel>();
            foreach(Marker2DKind kind in Marker2DModel.GetPredefinedKinds()) {
                models.Add(kind.Name, (Marker2DModel)Activator.CreateInstance(kind.Type));
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string name = value as string;
            if(name == null || name == ConstStrings.Get("None")) return null;
            Marker2DModel model;
            if(!models.TryGetValue(name, out model)) return null;
            return model;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
