using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.Native;
using System.Linq;
using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace DockingDemo {
    public partial class PaintControl : UserControl {
        const string filterString = "JPEG Files (*.JPG)|*.jpg;*.jpeg";

        #region static        
        public static readonly DependencyProperty ShowAutomaticButtonProperty;
        public static readonly DependencyProperty ShowNoColorButtonProperty;
        public static readonly DependencyProperty ShowMoreColorsButtonProperty;
        public static readonly DependencyProperty ChipSizeProperty;
        public static readonly DependencyProperty ShowEditorsProperty;
        public static readonly DependencyProperty BrushSizeProperty;
        public static readonly DependencyProperty BrushOpacityProperty;
        public static readonly DependencyProperty TextFontFamilyProperty;
        public static readonly DependencyProperty TextFontSizeProperty;
        public static readonly DependencyProperty TextFontColorProperty;
        public static readonly DependencyProperty BackgroundTextColorProperty;

        public static readonly DependencyProperty BackgroundImageSourceProperty;
        public static readonly DependencyProperty BrushColorProperty;
        public static readonly DependencyProperty MaxBrushSizeProperty;
        public static readonly DependencyProperty MinBrushSizeProperty;
        public static readonly DependencyProperty MousePositionProperty;
        public static readonly DependencyProperty IsBrushToolSelectedProperty;
        public static readonly DependencyProperty IsTextToolSelectedProperty;
        public static readonly DependencyProperty IsTextEditingProperty;

        static PaintControl() {
            ShowAutomaticButtonProperty = DependencyProperty.Register("ShowAutomaticButton", typeof(bool), typeof(PaintControl), new PropertyMetadata(true));
            ShowNoColorButtonProperty = DependencyProperty.Register("ShowNoColorButton", typeof(bool), typeof(PaintControl), new PropertyMetadata(false));
            ShowMoreColorsButtonProperty = DependencyProperty.Register("ShowMoreColorsButton", typeof(bool), typeof(PaintControl), new PropertyMetadata(false));
            ChipSizeProperty = DependencyProperty.Register("ChipSize", typeof(ChipSize), typeof(PaintControl), new PropertyMetadata(ChipSize.Default));
            BrushSizeProperty = DependencyProperty.Register("BrushSize", typeof(double), typeof(PaintControl), new PropertyMetadata(8d));
            BrushOpacityProperty = DependencyProperty.Register("BrushOpacity", typeof(double), typeof(PaintControl), new PropertyMetadata(1d));
            TextFontFamilyProperty = DependencyProperty.Register("TextFontFamily", typeof(FontFamily), typeof(PaintControl), new PropertyMetadata(null));
            TextFontSizeProperty = DependencyProperty.Register("TextFontSize", typeof(double), typeof(PaintControl), new PropertyMetadata(12d));
            TextFontColorProperty = DependencyProperty.Register("TextFontColor", typeof(Color), typeof(PaintControl), new PropertyMetadata(Colors.Black));
            BackgroundTextColorProperty = DependencyProperty.Register("BackgroundTextColor", typeof(Color), typeof(PaintControl), new PropertyMetadata(Colors.Transparent));

            BackgroundImageSourceProperty = DependencyProperty.Register("BackgroundImageSource", typeof(ImageSource), typeof(PaintControl), new PropertyMetadata((BitmapImage)null, new PropertyChangedCallback(OnBackgroundImagePropertyChanged)));
            BrushColorProperty = DependencyProperty.Register("BrushColor", typeof(Color), typeof(PaintControl), new PropertyMetadata(Colors.Red));
            MaxBrushSizeProperty = DependencyProperty.Register("MaxBrushSize", typeof(double), typeof(PaintControl), new PropertyMetadata(24d));
            MinBrushSizeProperty = DependencyProperty.Register("MinBrushSize", typeof(double), typeof(PaintControl), new PropertyMetadata(1d));
            MousePositionProperty = DependencyProperty.Register("MousePosition", typeof(Point), typeof(PaintControl), new PropertyMetadata(new Point(-1, -1)));
            IsBrushToolSelectedProperty = DependencyProperty.Register("IsBrushToolSelected", typeof(bool), typeof(PaintControl), new PropertyMetadata(true, new PropertyChangedCallback(OnIsBrushToolSelectedPropertyChanged)));
            IsTextToolSelectedProperty = DependencyProperty.Register("IsTextToolSelected", typeof(bool), typeof(PaintControl), new PropertyMetadata(false, new PropertyChangedCallback(OnIsTextToolSelectedPropertyChanged)));
            IsTextEditingProperty = DependencyProperty.Register("IsTextEditing", typeof(bool), typeof(PaintControl), new PropertyMetadata(false));
        }
        public static void OnBackgroundImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PaintControl)d).OnBackgroundImageChanged(e);
        }
        private void OnBackgroundImageChanged(DependencyPropertyChangedEventArgs e) {
            backgroundImage.LayoutUpdated += new EventHandler(OnBackgroundImageLayoutUpdated);
        }

        void OnBackgroundImageLayoutUpdated(object sender, EventArgs e) {
            backgroundImage.LayoutUpdated -= OnBackgroundImageLayoutUpdated;
            UpdateCanvas();
        }
        private static void OnIsBrushToolSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PaintControl)d).OnIsBrushToolSelectedChanged();
        }
        private static void OnIsTextToolSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PaintControl)d).OnIsTextToolSelectedChanged();
        }
        #endregion
        #region props
        public Point MousePosition {
            get { return (Point)GetValue(MousePositionProperty); }
            set { SetValue(MousePositionProperty, value); }
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
        public double BrushSize {
            get { return (double)GetValue(BrushSizeProperty); }
            set { SetValue(BrushSizeProperty, value); }
        }
        public ChipSize ChipSize {
            get { return (ChipSize)GetValue(ChipSizeProperty); }
            set { SetValue(ChipSizeProperty, value); }
        }
        public double MinBrushSize {
            get { return (double)GetValue(MinBrushSizeProperty); }
            set { SetValue(MinBrushSizeProperty, value); }
        }
        public double MaxBrushSize {
            get { return (double)GetValue(MaxBrushSizeProperty); }
            set { SetValue(MaxBrushSizeProperty, value); }
        }
        public Color BrushColor {
            get { return (Color)GetValue(BrushColorProperty); }
            set { SetValue(BrushColorProperty, value); }
        }

        public Color TextFontColor {
            get { return (Color)GetValue(TextFontColorProperty); }
            set { SetValue(TextFontColorProperty, value); }
        }
        public Color BackgroundTextColor {
            get { return (Color)GetValue(BackgroundTextColorProperty); }
            set { SetValue(BackgroundTextColorProperty, value); }
        }
        public ImageSource BackgroundImageSource {
            get { return (ImageSource)GetValue(BackgroundImageSourceProperty); }
            set { SetValue(BackgroundImageSourceProperty, value); }
        }
        public double BrushOpacity {
            get { return (double)GetValue(BrushOpacityProperty); }
            set { SetValue(BrushOpacityProperty, value); }
        }
        public double TextFontSize {
            get { return (double)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }
        public FontFamily TextFontFamily {
            get { return (FontFamily)GetValue(TextFontFamilyProperty); }
            set { SetValue(TextFontFamilyProperty, value); }
        }
        public bool IsBrushToolSelected {
            get { return (bool)GetValue(IsBrushToolSelectedProperty); }
            set { SetValue(IsBrushToolSelectedProperty, value); }
        }
        public bool IsTextToolSelected {
            get { return (bool)GetValue(IsTextToolSelectedProperty); }
            set { SetValue(IsTextToolSelectedProperty, value); }
        }
        public bool IsTextEditing {
            get { return (bool)GetValue(IsTextEditingProperty); }
            set { SetValue(IsTextEditingProperty, value); }
        }

        #endregion
        #region commands
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        #endregion        

        public PaintControl() {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += new RoutedEventHandler(OnUnloaded);
            URStack = new Stack<UIElement>();
            UndoCommand = new DelegateCommand(UndoCommandExecute, CanUndoCommandExecute);
            RedoCommand = new DelegateCommand(RedoCommandExecute, CanRedoCommandExecute);
            ClearCommand = new DelegateCommand(ClearCommandExecute, CanClearCommandExecute);
            SaveCommand = new DelegateCommand(SaveCommandExecute);
            OpenCommand = new DelegateCommand(OpenCommandExecute);
            TextFontFamily = textEdit.FontFamily;
            UpdateCursor();
        }

        #region commands implementation
        protected void SaveCommandExecute() {
            CompleteTextEditing(true);
            SaveFileDialog dlg = new SaveFileDialog() { Filter = filterString, Title = "Save file..." };
            if (dlg.ShowDialog() == true) {
                using (Stream stream = dlg.OpenFile()) {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth,
                        (int)canvas.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                    bmp.Render(canvas);
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(stream);
                }
            }
        }
        protected void OpenCommandExecute() {
            CompleteTextEditing(true);
            OpenFileDialog dialog = new OpenFileDialog() { Filter = filterString, Title = "Open file..." };
            if (dialog.ShowDialog().Value == true) {
                ClearCommandExecute();
                BackgroundImageSource = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromStream(new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read));
            }
        }
        protected void ClearCommandExecute() {
            CompleteTextEditing(true);
            while (canvas.Children.Count > 2) {
                canvas.Children.RemoveAt(2);
            }
            URStack.Clear();
            RaiseCommandsCanExecuteChanged();
            SetCurrentValue(BackgroundImageSourceProperty, null);
        }
        protected bool CanClearCommandExecute() {
            return URStack.Count != 0 || canvas.Children.Count > 2;
        }
        protected void UndoCommandExecute() {
            URStack.Push(canvas.Children[canvas.Children.Count - 1] as UIElement);
            canvas.Children.RemoveAt(canvas.Children.Count - 1);
            RaiseCommandsCanExecuteChanged();
        }
        protected bool CanUndoCommandExecute() {
            return canvas.Children.Count > 2;
        }
        protected void RedoCommandExecute() {
            canvas.Children.Add(URStack.Pop());
            RaiseCommandsCanExecuteChanged();
        }
        protected bool CanRedoCommandExecute() {
            return URStack.Count != 0;
        }
        void RaiseCommandsCanExecuteChanged() {
            ((DelegateCommand)RedoCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)UndoCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ClearCommand).RaiseCanExecuteChanged();
        }
        #endregion



        void OnUnloaded(object sender, RoutedEventArgs e) {
            SizeChanged -= OnSizeChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
            UpdateCanvas();
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateCanvas();
        }

        private Stack<UIElement> URStack;
        int demoCenterBottomPanelHeightCoerceValue = 80;
        private void UpdateCanvas() {
            double left = (canvas.ActualWidth - backgroundImage.ActualWidth) / 2;
            Canvas.SetLeft(backgroundImage, left < 0 ? 0 : left);
            double top = (canvas.ActualHeight - demoCenterBottomPanelHeightCoerceValue - backgroundImage.ActualHeight) / 2;
            Canvas.SetTop(backgroundImage, top < 0 ? 0 : top);
        }
        private void OnIsBrushToolSelectedChanged() {
            if (IsBrushToolSelected) {
                IsTextToolSelected = false;
                UpdateCursor();
            }
        }
        private void OnIsTextToolSelectedChanged() {
            if (IsTextToolSelected) {
                IsBrushToolSelected = false;
                UpdateCursor();
            }
        }

        #region layout
        bool isLeftButtonPressed;
        Canvas paintLayer;
        Point currentPoint;
        Point lastPoint;
        void DrawLine(Point fromPoint, Point toPoint) {
            Line line = new Line() {
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                StrokeThickness = BrushSize,
                Stroke = new SolidColorBrush(BrushColor)
            };
            line.X1 = toPoint.X;
            line.Y1 = toPoint.Y;
            line.X2 = fromPoint.X;
            line.Y2 = fromPoint.Y;
            paintLayer.Children.Add(line);
        }
        void FocusTextEdit() {
            textEdit.Focus();
        }
        void CompleteTextEditing(bool cancel) {
            IsTextEditing = false;
            UpdateCursor();
            if (cancel || string.IsNullOrEmpty(textEdit.Text)) return;

            TextBlock textBlock = new TextBlock() { Foreground = new SolidColorBrush(TextFontColor), FontSize = TextFontSize, Text = textEdit.Text };
            Canvas.SetLeft(textBlock, Canvas.GetLeft(textEdit) + 2);
            Canvas.SetTop(textBlock, Canvas.GetTop(textEdit));
            if (TextFontFamily != null)
                textBlock.FontFamily = TextFontFamily;
            canvas.Children.Add(textBlock);
            URStack.Clear();
        }
        void OnCanvasMouseDown(object sender, MouseButtonEventArgs e) {
            isLeftButtonPressed = true;
            if (IsBrushToolSelected) {
                paintLayer = new Canvas() { Height = canvas.ActualHeight, Width = canvas.ActualWidth, FlowDirection = FlowDirection.LeftToRight, Visibility = Visibility.Visible };
                IsTextEditing = false;
                canvas.Children.Add(paintLayer);
                paintLayer.Opacity = BrushOpacity;
                currentPoint = e.GetPosition(canvas);
                DrawLine(currentPoint, currentPoint);
                lastPoint = currentPoint;
                canvas.CaptureMouse();
            } else {
                if (IsTextEditing) {
                    CompleteTextEditing(false);
                } else {
                    textEdit.Text = "";
                    IsTextEditing = true;
                    UpdateCursor();
                    Point currentPoint = e.GetPosition(canvas);
                    Canvas.SetLeft(textEdit, currentPoint.X - 4);
                    Canvas.SetTop(textEdit, currentPoint.Y - 8);
                    Dispatcher.BeginInvoke(new Action(FocusTextEdit));
                }
            }
        }
        void UpdateCursor() {
            if (IsBrushToolSelected) {
                Cursor = Cursors.Pen;
            } else {
                if (IsTextEditing)
                    Cursor = Cursors.Arrow;
                else
                    Cursor = Cursors.IBeam;
            }
        }
        void OnCanvasMouseMove(object sender, MouseEventArgs e) {
            if (IsBrushToolSelected && isLeftButtonPressed) {
                currentPoint = e.GetPosition(paintLayer);
                DrawLine(currentPoint, lastPoint);
                lastPoint = currentPoint;
            }
            MousePosition = e.GetPosition(canvas);
        }
        void OnCanvasMouseUp(object sender, MouseButtonEventArgs e) {
            isLeftButtonPressed = false;
            if (IsBrushToolSelected) {
                canvas.ReleaseMouseCapture();
                System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
                path.StrokeThickness = BrushSize;
                path.Stroke = new SolidColorBrush(BrushColor);
                path.StrokeStartLineCap = PenLineCap.Round;
                path.StrokeEndLineCap = PenLineCap.Round;
                path.Opacity = BrushOpacity;
                PathGeometry g = new PathGeometry();
                if (paintLayer != null)
                    foreach (Line l in paintLayer.Children) {
                        if (l == paintLayer.Children[0]) {
                            g.Figures.Add(new PathFigure() { StartPoint = new Point(l.X1, l.Y1) });
                            g.Figures[0].Segments.Add(new LineSegment() { Point = new Point(l.X2, l.Y2) });
                        } else {
                            g.Figures[0].Segments.Add(new LineSegment() { Point = new Point(l.X1, l.Y1) });
                            g.Figures[0].Segments.Add(new LineSegment() { Point = new Point(l.X2, l.Y2) });
                        }
                    }
                path.StrokeLineJoin = PenLineJoin.Round;
                path.Data = g;
                canvas.Children.Remove(paintLayer);
                canvas.Children.Add(path);
            } else {

            }
            URStack.Clear();
            RaiseCommandsCanExecuteChanged();
        }
        void OnCanvasKeyDown(object sender, KeyEventArgs e) {
            if (IsTextToolSelected) {
                if (e.Key == Key.Escape) {
                    CompleteTextEditing(true);
                }
            }
        }
        #endregion
    }
}
