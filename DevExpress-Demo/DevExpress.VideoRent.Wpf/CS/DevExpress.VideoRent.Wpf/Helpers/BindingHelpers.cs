using System;
using System.Windows;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Charts;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public static class GridControlHelper {
        #region Dependency Properties
        public static readonly DependencyProperty GridProperty;
        public static readonly DependencyProperty GridCardViewProperty;
        public static readonly DependencyProperty GridTableViewProperty;
        static GridControlHelper() {
            Type ownerType = typeof(GridControlHelper);
            GridProperty = DependencyProperty.RegisterAttached("Grid", typeof(GridControl), ownerType, new PropertyMetadata(null));
            GridCardViewProperty = DependencyProperty.RegisterAttached("GridCardView", typeof(CardView), ownerType, new PropertyMetadata(null));
            GridTableViewProperty = DependencyProperty.RegisterAttached("GridTableView", typeof(TableView), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public static GridControl GetGrid(DependencyObject d) { return (GridControl)d.GetValue(GridProperty); }
        public static void SetGrid(DependencyObject d, GridControl value) { d.SetValue(GridProperty, value); }
        public static CardView GetGridCardView(DependencyObject d) { return (CardView)d.GetValue(GridCardViewProperty); }
        public static void SetGridCardView(DependencyObject d, CardView value) { d.SetValue(GridCardViewProperty, value); }
        public static TableView GetGridTableView(DependencyObject d) { return (TableView)d.GetValue(GridTableViewProperty); }
        public static void SetGridTableView(DependencyObject d, TableView value) { d.SetValue(GridTableViewProperty, value); }
    }
    public static class ImageEditHelper {
        #region Dependency Properties
        public static readonly DependencyProperty ImageEditProperty;
        static ImageEditHelper() {
            Type ownerType = typeof(ImageEditHelper);
            ImageEditProperty = DependencyProperty.RegisterAttached("ImageEdit", typeof(ImageEdit), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public static ImageEdit GetImageEdit(DependencyObject d) { return (ImageEdit)d.GetValue(ImageEditProperty); }
        public static void SetImageEdit(DependencyObject d, ImageEdit value) { d.SetValue(ImageEditProperty, value); }
    }
    public static class EditHelper {
        #region Dependency Properties
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty EditValueProperty;
        public static readonly DependencyProperty ButtonContentProperty;
        static EditHelper() {
            Type ownerType = typeof(EditHelper);
            ItemsSourceProperty = DependencyProperty.RegisterAttached("ItemsSource", typeof(object), ownerType, new PropertyMetadata(null));
            EditValueProperty = DependencyProperty.RegisterAttached("EditValue", typeof(object), ownerType, new PropertyMetadata(null));
            ButtonContentProperty = DependencyProperty.RegisterAttached("ButtonContent", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public static object GetItemsSource(DependencyObject d) { return d.GetValue(ItemsSourceProperty); }
        public static void SetItemsSource(DependencyObject d, object value) { d.SetValue(ItemsSourceProperty, value); }
        public static object GetEditValue(DependencyObject d) { return d.GetValue(EditValueProperty); }
        public static void SetEditValue(DependencyObject d, object value) { d.SetValue(EditValueProperty, value); }
        public static object GetButtonContent(DependencyObject d) { return d.GetValue(ButtonContentProperty); }
        public static void SetButtonContent(DependencyObject d, object value) { d.SetValue(ButtonContentProperty, value); }
    }
    public static class CommandHelper {
        #region Dependency Properties
        public static readonly DependencyProperty Command0Property;
        public static readonly DependencyProperty Command1Property;
        static CommandHelper() {
            Type ownerType = typeof(CommandHelper);
            Command0Property = DependencyProperty.RegisterAttached("Command0", typeof(object), ownerType, new PropertyMetadata(null));
            Command1Property = DependencyProperty.RegisterAttached("Command1", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public static object GetCommand0(DependencyObject d) { return d.GetValue(Command0Property); }
        public static void SetCommand0(DependencyObject d, object value) { d.SetValue(Command0Property, value); }
        public static object GetCommand1(DependencyObject d) { return d.GetValue(Command1Property); }
        public static void SetCommand1(DependencyObject d, object value) { d.SetValue(Command1Property, value); }
    }
    public static class ChartHelper {
        #region Dependency Properties
        public static readonly DependencyProperty MarkerSizeProperty;
        public static readonly DependencyProperty Marker2DModelProperty;
        public static readonly DependencyProperty MarkerAngleProperty;
        static ChartHelper() {
            Type ownerType = typeof(ChartHelper);
            MarkerSizeProperty = DependencyProperty.RegisterAttached("MarkerSize", typeof(double), ownerType, new PropertyMetadata(0.0));
            Marker2DModelProperty = DependencyProperty.RegisterAttached("Marker2DModel", typeof(Marker2DModel), ownerType, new PropertyMetadata(null));
            MarkerAngleProperty = DependencyProperty.RegisterAttached("MarkerAngle", typeof(double), ownerType, new PropertyMetadata(0.0));
        }
        #endregion
        public static double GetMarkerAngle(DependencyObject d) { return (double)d.GetValue(MarkerAngleProperty); }
        public static void SetMarkerAngle(DependencyObject d, double value) { d.SetValue(MarkerAngleProperty, value); }
        public static double GetMarkerSize(DependencyObject d) { return (double)d.GetValue(MarkerSizeProperty); }
        public static void SetMarkerSize(DependencyObject d, double value) { d.SetValue(MarkerSizeProperty, value); }
        public static Marker2DModel GetMarker2DModel(DependencyObject d) { return (Marker2DModel)d.GetValue(Marker2DModelProperty); }
        public static void SetMarker2DModel(DependencyObject d, Marker2DModel value) { d.SetValue(Marker2DModelProperty, value); }
    }
    public static class ForwardingHelper {
        #region Dependency Properties
        public static readonly DependencyProperty DataObjectProperty;
        static ForwardingHelper() {
            Type ownerType = typeof(ForwardingHelper);
            DataObjectProperty = DependencyProperty.RegisterAttached("DataObject", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public static object GetDataObject(DependencyObject d) { return d.GetValue(DataObjectProperty); }
        public static void SetDataObject(DependencyObject d, object value) { d.SetValue(DataObjectProperty, value); }
    }
}
