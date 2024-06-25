using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Internal;
using Microsoft.Win32;

namespace EditorsDemo {
    public partial class PaintControl : UserControl, IColorEdit {

        #region static
        public static readonly DependencyProperty ToolProperty;
        public static readonly DependencyProperty ShowAutomaticButtonProperty;
        public static readonly DependencyProperty ShowNoColorButtonProperty;
        public static readonly DependencyProperty ShowMoreColorsButtonProperty;
        public static readonly DependencyProperty ChipSizeProperty;
        static PaintControl() {
            ToolProperty = DependencyProperty.Register("Tool", typeof(DrawingTool), typeof(PaintControl), new PropertyMetadata(null));
            ShowAutomaticButtonProperty = DependencyProperty.Register("ShowAutomaticButton", typeof(bool), typeof(PaintControl), new PropertyMetadata(true));
            ShowNoColorButtonProperty = DependencyProperty.Register("ShowNoColorButton", typeof(bool), typeof(PaintControl), new PropertyMetadata(false));
            ShowMoreColorsButtonProperty = DependencyProperty.Register("ShowMoreColorsButton", typeof(bool), typeof(PaintControl), new PropertyMetadata(false));
            ChipSizeProperty = DependencyProperty.Register("ChipSize", typeof(ChipSize), typeof(PaintControl), new PropertyMetadata(ChipSize.Default));
        }
        #endregion
        public DrawingTool Tool {
            get { return (DrawingTool)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
        }
        public bool ShowAutomaticButton {
            get { return (bool)GetValue(ShowAutomaticButtonProperty); }
            set { SetValue(ShowAutomaticButtonProperty, value); }
        }
        public bool ShowNoColorButton {
            get { return (bool)GetValue(ShowNoColorButtonProperty); }
            set { SetValue(ShowNoColorButtonProperty, value); }
        }
        public bool ShowMoreColorsButton {
            get { return (bool)GetValue(ShowMoreColorsButtonProperty); }
            set { SetValue(ShowMoreColorsButtonProperty, value); }
        }
        public ChipSize ChipSize {
            get { return (ChipSize)GetValue(ChipSizeProperty); }
            set { SetValue(ChipSizeProperty, value); }
        }
        System.Windows.Controls.Image currentCursor;
        public PaintControl() {
            InitializeComponent();
            Loaded += OnLoaded;
        }
        protected System.Windows.Controls.Image CurrentCursor {
            get { return currentCursor; }
            set {
                if(CurrentCursor == value) return;
                if(CurrentCursor != null)
                    canvas.Children.Remove(CurrentCursor);
                currentCursor = value;
                if(CurrentCursor != null) {
                    CurrentCursor.Width = 32d;
                    CurrentCursor.Height = 32d;
                    CurrentCursor.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor);
                    TransformGroup tgroup = new TransformGroup();
                    tgroup.Children.Add(new TranslateTransform() { X = 0, Y = 10 });
                    if(this.FlowDirection == System.Windows.FlowDirection.RightToLeft) {
                        tgroup.Children.Add(new ScaleTransform() { ScaleX = -1 });
                        tgroup.Children.Add(new TranslateTransform() { X = 32d });
                    }
                    CurrentCursor.RenderTransform = tgroup;
                    canvas.Children.Add(CurrentCursor);
                    Canvas.SetZIndex(CurrentCursor, canvas.Children.Count);
                }

            }
        }
        bool isLoadedFlag = false;
        void OnLoaded(object sender, RoutedEventArgs e) {
            InitSources();
            Canvas.SetLeft(logoImage, (canvas.ActualWidth - logoImage.ActualWidth) / 2);
            Canvas.SetTop(logoImage, (canvas.ActualHeight - logoImage.ActualHeight) / 2);
            isLoadedFlag = true;
            UpdateCurrentTool();
        }
        private void InitSources() {
            fontFamilyEdit.ItemsSource = FontFamilies.FontNames;
            fontSizeEdit.ItemsSource = FontSizes.Sizes;
            palettesCombo.ItemsSource = PredefinedPaletteCollections.Collections;
        }
        private void OnToolChanged(object sender, RoutedEventArgs e) {
            if(!isLoadedFlag)
                return;
            if(sender == penToolButton)
                textToolButton.IsChecked = false;
            else
                penToolButton.IsChecked = false;
            UpdateCurrentTool();
        }
        private void UpdateCurrentTool() {
            if(Tool != null)
                Tool.Release();
            if(penToolButton.IsChecked.Value)
                Tool = CreateBrushTool();
            else if(textToolButton.IsChecked.Value)
                Tool = CreateTextTool();

        }
        private void fontColorEdit_ColorChanged(object sender, EventArgs e) {
            if(Tool is TextTool)
                Tool.Color = fontColorEdit.Color;
        }

        private void brushColorEdit_ColorChanged(object sender, EventArgs e) {
            if(Tool is PenTool)
                Tool.Color = brushColorEdit.Color;
        }
        protected virtual PenTool CreateBrushTool() {
            PenTool tool = new PenTool(canvas);
            BindingOperations.SetBinding(tool, PenTool.ColorProperty, new Binding("Color") { Source = swatchesEdit, Mode = BindingMode.TwoWay });
            BindingOperations.SetBinding(tool, PenTool.SizeProperty, new Binding("Value") { Source = brushSizeEdit });
            tool.Color = brushColorEdit.Color;
            return tool;
        }
        protected virtual TextTool CreateTextTool() {
            TextTool tool = new TextTool(canvas);
            BindingOperations.SetBinding(tool, TextTool.ColorProperty, new Binding("Color") { Source = swatchesEdit, Mode = BindingMode.TwoWay });
            BindingOperations.SetBinding(tool, TextTool.SizeProperty, new Binding("EditValue") { Source = fontSizeEdit });
            BindingOperations.SetBinding(tool, TextTool.FontFamilyProperty, new Binding("EditValue") { Source = fontFamilyEdit });
            tool.Color = fontColorEdit.Color;
            return tool;
        }
        void canvas_MouseDown(object sender, MouseButtonEventArgs e) {
            Tool.OnMouseDown(e);
        }
        void canvas_MouseMove(object sender, MouseEventArgs e) {
            UpdateCursorPosition(e);
            Tool.OnMouseMove(e);
        }
        void UpdateCursorPosition(MouseEventArgs e) {
            if(CurrentCursor != null) {
                Canvas.SetLeft(CurrentCursor, e.GetPosition(canvas).X - Tool.CursorHorizontalOffset);
                Canvas.SetTop(CurrentCursor, e.GetPosition(canvas).Y - Tool.CursorVerticalOffset);
            }
        }
        void canvas_MouseUp(object sender, MouseButtonEventArgs e) {
            Tool.OnMouseUp(e);
        }
        void canvas_KeyDown(object sender, KeyEventArgs e) {
            Tool.OnKeyDown(e);
        }
        void canvas_MouseEnter(object sender, MouseEventArgs e) {
            if(Tool != null) {
                CurrentCursor = new System.Windows.Controls.Image() { Source = Tool.GetCursor(), UseLayoutRounding = true };
                UpdateCursorPosition(e);
            }
        }
        void canvas_MouseLeave(object sender, MouseEventArgs e) {
            CurrentCursor = null;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e) {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image Files (*.JPG)|*.JPG";
            if(dlg.ShowDialog() == true) {
                using(Stream stream = dlg.OpenFile()) {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth,
                        (int)canvas.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                    bmp.Render(canvas);
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(stream);
                }
            }

        }
        private void resetButton_Click(object sender, RoutedEventArgs e) {
            canvas.Children.Clear();
        }
        #region Inner classes
        public abstract class DrawingTool : DependencyObject {
            #region static
            public static readonly DependencyProperty ColorProperty;
            public static readonly DependencyProperty SizeProperty;
            static DrawingTool() {
                ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(DrawingTool), new PropertyMetadata(Colors.Black, OnPropertyChanged));
                SizeProperty = DependencyProperty.Register("Size", typeof(double), typeof(DrawingTool), new PropertyMetadata(16d, OnPropertyChanged));
            }
            protected static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((DrawingTool)d).OnPropertyChanged();
            }
            #endregion
            public DrawingTool(Canvas paramCanvas) {
                Canvas = paramCanvas;
            }
            public Color Color {
                get { return (Color)GetValue(ColorProperty); }
                set { SetValue(ColorProperty, value); }
            }
            public double Size {
                get { return (double)GetValue(SizeProperty); }
                set { SetValue(SizeProperty, value); }
            }
            protected Canvas Canvas { get; private set; }
            public abstract void OnMouseMove(MouseEventArgs e);
            public abstract void OnMouseDown(MouseButtonEventArgs e);
            public abstract void OnMouseUp(MouseButtonEventArgs e);
            public abstract void OnKeyDown(KeyEventArgs e);
            public virtual void Release() { }
            protected virtual void OnPropertyChanged() { }
            public abstract BitmapSource GetCursor();
            public virtual double CursorHorizontalOffset { get { return 0; } }
            public virtual double CursorVerticalOffset { get { return 0; } }
        }
        public class PenTool : DrawingTool {
            static readonly BitmapSource PenCursor = ImageHelper.CreateImageFromEmbeddedResource(typeof(PenTool).Assembly, DemoHelper.GetPath("EditorsDemo.Images.Cursors.", typeof(PenTool).Assembly) + "cursor_pen.png");
            public PenTool(Canvas canvas)
                : base(canvas) {
            }
            protected bool IsMouseDown { get; set; }
            protected Point LastPoint { get; set; }
            protected Point CurrentPoint { get; set; }
            public override void OnMouseDown(MouseButtonEventArgs e) {
                IsMouseDown = true;
                CurrentPoint = e.GetPosition(Canvas);
                DrawLine(CurrentPoint, CurrentPoint);
                LastPoint = CurrentPoint;
                Canvas.CaptureMouse();
            }
            public override void OnMouseMove(MouseEventArgs e) {
                if(IsMouseDown) {
                    CurrentPoint = e.GetPosition(Canvas);
                    DrawLine(CurrentPoint, LastPoint);
                    LastPoint = CurrentPoint;
                }
            }
            protected void DrawLine(Point fromPoint, Point toPoint) {
                Line line = new Line()
                {
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round,
                    StrokeThickness = Size,
                    Stroke = new SolidColorBrush(Color)
                };
                line.X1 = toPoint.X;
                line.Y1 = toPoint.Y;
                line.X2 = fromPoint.X;
                line.Y2 = fromPoint.Y;
                Canvas.Children.Add(line);
            }
            public override void OnMouseUp(MouseButtonEventArgs e) {
                Canvas.ReleaseMouseCapture();
                IsMouseDown = false;
            }
            public override void OnKeyDown(KeyEventArgs e) { }
            public override BitmapSource GetCursor() {
                return PenCursor;
            }
            public override double CursorHorizontalOffset { get { return 4; } }
            public override double CursorVerticalOffset { get { return 30; } }
        }
        public class TextTool : DrawingTool {
            static readonly BitmapSource TextCursor = ImageHelper.CreateImageFromEmbeddedResource(typeof(TextTool).Assembly, DemoHelper.GetPath("EditorsDemo.Images.Cursors.", typeof(TextTool).Assembly) + "cursor_text.png");
            #region static
            public static readonly DependencyProperty FontFamilyProperty;
            static TextTool() {
                FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(string), typeof(TextTool), new PropertyMetadata("", OnPropertyChanged));
            }
            #endregion
            public TextTool(Canvas canvas)
                : base(canvas) {
            }
            public string FontFamily {
                get { return (string)GetValue(FontFamilyProperty); }
                set { SetValue(FontFamilyProperty, value); }
            }
            protected TextEdit ActiveTextEdit { get; private set; }
            public override void OnMouseDown(MouseButtonEventArgs e) {
                if(ActiveTextEdit != null) {
                    Release();
                    return;
                }
                if(ActiveTextEdit == null) {
                    Point currentPoint = e.GetPosition(Canvas);
                    ActiveTextEdit = CreateTextEdit();
                    UpdateTextEditProperties();
                    Canvas.SetLeft(ActiveTextEdit, currentPoint.X);
                    Canvas.SetTop(ActiveTextEdit, currentPoint.Y);
                    Canvas.Children.Add(ActiveTextEdit);
                }
            }
            public override void OnKeyDown(KeyEventArgs e) {
                if(e.Key == Key.Escape && ActiveTextEdit != null) {
                    Canvas.Children.Remove(ActiveTextEdit);
                    ActiveTextEdit = null;
                }
            }
            public override void Release() {
                if(ActiveTextEdit == null) return;
                ActiveTextEdit.EditMode = EditMode.InplaceInactive;
                ActiveTextEdit = null;
            }
            public override void OnMouseMove(MouseEventArgs e) { }
            public override void OnMouseUp(MouseButtonEventArgs e) { }
            protected virtual TextEdit CreateTextEdit() {
                return new TextEdit() { AcceptsReturn = true, Background = new SolidColorBrush(Colors.Transparent) };
            }
            protected override void OnPropertyChanged() {
                base.OnPropertyChanged();
                UpdateTextEditProperties();
            }
            protected virtual void UpdateTextEditProperties() {
                if(ActiveTextEdit == null) return;
                ActiveTextEdit.FontSize = Size;
                ActiveTextEdit.FontFamily = new FontFamily(FontFamily);
                ActiveTextEdit.Foreground = new SolidColorBrush(Color);
                Dispatcher.BeginInvoke(new Action(() => { ActiveTextEdit.Focus(); }));
            }
            public override BitmapSource GetCursor() {
                return TextCursor;
            }
        }
        #endregion
        private void currentColor_MouseDown(object sender, MouseButtonEventArgs e) {
            ColorEditHelper.ShowColorChooserDialog(this);
        }
        #region IColorEdit Members
        void IColorEdit.AddCustomColor(Color color) {
            Tool.Color = color;
        }
        ColorPickerColorMode IColorEdit.ColorMode { get; set; }
        Color IColorEdit.Color {
            get { return Tool.Color; }
            set { }
        }
        event RoutedEventHandler IColorEdit.ColorChanged {
            add { }
            remove { }
        }
        Color IColorEdit.DefaultColor {
            get { return Tool.Color; }
            set { }
        }
        object IColorEdit.EditValue {
            get { return Tool.Color; }
            set { }
        }
        PaletteCollection IColorEdit.Palettes {
            get { return null; }
            set { }
        }
        CircularList<Color> IColorEdit.RecentColors {
            get { return null; }
        }
        bool IColorEdit.ShowAlphaChannel {
            get { return true; }
            set { }
        }
        #endregion
        double blockWidth = 0d;
        double comboWidth = 0d;
        void panel_SizeChanged(object sender, SizeChangedEventArgs e) {
            if(grid.ColumnDefinitions[1].ActualWidth < rightPanel.ActualWidth) {
                if(palettesTxt.Visibility != Visibility.Collapsed) {
                    blockWidth = palettesTxt.ActualWidth;
                    palettesTxt.Visibility = Visibility.Collapsed;
                }
                else {
                    comboWidth = palettesCombo.ActualWidth;
                    palettesCombo.ItemTemplate = Resources["smallItemTemplate"] as DataTemplate;
                }
            }
            else {
                if(object.Equals(palettesCombo.ItemTemplate, Resources["smallItemTemplate"])) {
                    if(grid.ColumnDefinitions[1].ActualWidth > comboWidth)
                        palettesCombo.ItemTemplate = Resources["itemTemplate"] as DataTemplate;
                }
                else if(grid.ColumnDefinitions[1].ActualWidth > rightPanel.ActualWidth + blockWidth)
                    palettesTxt.Visibility = Visibility.Visible;
            }
        }
    }
    #region Helpers
    public class FontSizes {
        public static double[] Sizes {
            get {
                return new double[] {
            8.0, 9.0,
            10.0, 11.0, 12.0, 13.0, 14.0, 15.0,
            16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
            32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0 };
            }
        }
    }
    public class FontFamilies {
        static List<string> fontNames;
        public static List<string> FontNames {
            get {
                if(fontNames == null)
                    fontNames = GetSystemFontNames();
                return fontNames;
            }
        }
        static List<string> GetSystemFontNames() {
            List<string> systemFontNames = new List<string>();
            foreach(FontFamily fam in Fonts.SystemFontFamilies) {
                systemFontNames.Add(fam.Source);
            }
            return systemFontNames;
        }
    }
    #endregion
    #region Converters
    public class PaletteCollectionConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            PaletteCollection collection = value as PaletteCollection;
            if(collection != null)
                return collection[0].Colors;
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
