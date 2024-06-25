Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Internal
Imports Microsoft.Win32

Namespace EditorsDemo

    Public Partial Class PaintControl
        Inherits UserControl
        Implements IColorEdit

#Region "static"
        Public Shared ReadOnly ToolProperty As DependencyProperty

        Public Shared ReadOnly ShowAutomaticButtonProperty As DependencyProperty

        Public Shared ReadOnly ShowNoColorButtonProperty As DependencyProperty

        Public Shared ReadOnly ShowMoreColorsButtonProperty As DependencyProperty

        Public Shared ReadOnly ChipSizeProperty As DependencyProperty

        Shared Sub New()
            ToolProperty = DependencyProperty.Register("Tool", GetType(DrawingTool), GetType(PaintControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))
            ShowAutomaticButtonProperty = DependencyProperty.Register("ShowAutomaticButton", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(True))
            ShowNoColorButtonProperty = DependencyProperty.Register("ShowNoColorButton", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(False))
            ShowMoreColorsButtonProperty = DependencyProperty.Register("ShowMoreColorsButton", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(False))
            ChipSizeProperty = DependencyProperty.Register("ChipSize", GetType(ChipSize), GetType(PaintControl), New PropertyMetadata(ChipSize.Default))
        End Sub

#End Region
        Public Property Tool As DrawingTool
            Get
                Return CType(GetValue(ToolProperty), DrawingTool)
            End Get

            Set(ByVal value As DrawingTool)
                SetValue(ToolProperty, value)
            End Set
        End Property

        Public Property ShowAutomaticButton As Boolean
            Get
                Return CBool(GetValue(ShowAutomaticButtonProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(ShowAutomaticButtonProperty, value)
            End Set
        End Property

        Public Property ShowNoColorButton As Boolean
            Get
                Return CBool(GetValue(ShowNoColorButtonProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(ShowNoColorButtonProperty, value)
            End Set
        End Property

        Public Property ShowMoreColorsButton As Boolean
            Get
                Return CBool(GetValue(ShowMoreColorsButtonProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(ShowMoreColorsButtonProperty, value)
            End Set
        End Property

        Public Property ChipSize As ChipSize
            Get
                Return CType(GetValue(ChipSizeProperty), ChipSize)
            End Get

            Set(ByVal value As ChipSize)
                SetValue(ChipSizeProperty, value)
            End Set
        End Property

        Private currentCursorField As Windows.Controls.Image

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Protected Property CurrentCursor As Windows.Controls.Image
            Get
                Return currentCursorField
            End Get

            Set(ByVal value As Windows.Controls.Image)
                If CurrentCursor Is value Then Return
                If CurrentCursor IsNot Nothing Then canvas.Children.Remove(CurrentCursor)
                currentCursorField = value
                If CurrentCursor IsNot Nothing Then
                    CurrentCursor.Width = 32R
                    CurrentCursor.Height = 32R
                    CurrentCursor.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor)
                    Dim tgroup As TransformGroup = New TransformGroup()
                    tgroup.Children.Add(New TranslateTransform() With {.X = 0, .Y = 10})
                    If FlowDirection = FlowDirection.RightToLeft Then
                        tgroup.Children.Add(New ScaleTransform() With {.ScaleX = -1})
                        tgroup.Children.Add(New TranslateTransform() With {.X = 32R})
                    End If

                    CurrentCursor.RenderTransform = tgroup
                    canvas.Children.Add(CurrentCursor)
                    Call Canvas.SetZIndex(CurrentCursor, canvas.Children.Count)
                End If
            End Set
        End Property

        Private isLoadedFlag As Boolean = False

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            InitSources()
            Call Canvas.SetLeft(logoImage, (canvas.ActualWidth - logoImage.ActualWidth) / 2)
            Call Canvas.SetTop(logoImage, (canvas.ActualHeight - logoImage.ActualHeight) / 2)
            isLoadedFlag = True
            UpdateCurrentTool()
        End Sub

        Private Sub InitSources()
            fontFamilyEdit.ItemsSource = FontFamilies.FontNames
            fontSizeEdit.ItemsSource = FontSizes.Sizes
            palettesCombo.ItemsSource = PredefinedPaletteCollections.Collections
        End Sub

        Private Sub OnToolChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not isLoadedFlag Then Return
            If sender Is penToolButton Then
                textToolButton.IsChecked = False
            Else
                penToolButton.IsChecked = False
            End If

            UpdateCurrentTool()
        End Sub

        Private Sub UpdateCurrentTool()
            If Tool IsNot Nothing Then Tool.Release()
            If penToolButton.IsChecked.Value Then
                Tool = CreateBrushTool()
            ElseIf textToolButton.IsChecked.Value Then
                Tool = CreateTextTool()
            End If
        End Sub

        Private Sub fontColorEdit_ColorChanged(ByVal sender As Object, ByVal e As EventArgs)
            If TypeOf Tool Is TextTool Then Tool.Color = fontColorEdit.Color
        End Sub

        Private Sub brushColorEdit_ColorChanged(ByVal sender As Object, ByVal e As EventArgs)
            If TypeOf Tool Is PenTool Then Tool.Color = brushColorEdit.Color
        End Sub

        Protected Overridable Function CreateBrushTool() As PenTool
            Dim tool As PenTool = New PenTool(canvas)
            Call BindingOperations.SetBinding(tool, DrawingTool.ColorProperty, New Binding("Color") With {.Source = swatchesEdit, .Mode = BindingMode.TwoWay})
            Call BindingOperations.SetBinding(tool, DrawingTool.SizeProperty, New Binding("Value") With {.Source = brushSizeEdit})
            tool.Color = brushColorEdit.Color
            Return tool
        End Function

        Protected Overridable Function CreateTextTool() As TextTool
            Dim tool As TextTool = New TextTool(canvas)
            Call BindingOperations.SetBinding(tool, DrawingTool.ColorProperty, New Binding("Color") With {.Source = swatchesEdit, .Mode = BindingMode.TwoWay})
            Call BindingOperations.SetBinding(tool, DrawingTool.SizeProperty, New Binding("EditValue") With {.Source = fontSizeEdit})
            Call BindingOperations.SetBinding(tool, TextTool.FontFamilyProperty, New Binding("EditValue") With {.Source = fontFamilyEdit})
            tool.Color = fontColorEdit.Color
            Return tool
        End Function

        Private Sub canvas_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Tool.OnMouseDown(e)
        End Sub

        Private Sub canvas_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            UpdateCursorPosition(e)
            Tool.OnMouseMove(e)
        End Sub

        Private Sub UpdateCursorPosition(ByVal e As MouseEventArgs)
            If CurrentCursor IsNot Nothing Then
                Call Canvas.SetLeft(CurrentCursor, e.GetPosition(canvas).X - Tool.CursorHorizontalOffset)
                Call Canvas.SetTop(CurrentCursor, e.GetPosition(canvas).Y - Tool.CursorVerticalOffset)
            End If
        End Sub

        Private Sub canvas_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Tool.OnMouseUp(e)
        End Sub

        Private Sub canvas_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            Tool.OnKeyDown(e)
        End Sub

        Private Sub canvas_MouseEnter(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Tool IsNot Nothing Then
                CurrentCursor = New Windows.Controls.Image() With {.Source = Tool.GetCursor(), .UseLayoutRounding = True}
                UpdateCursorPosition(e)
            End If
        End Sub

        Private Sub canvas_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
            CurrentCursor = Nothing
        End Sub

        Private Sub saveButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim dlg As SaveFileDialog = New SaveFileDialog()
            dlg.Filter = "Image Files (*.JPG)|*.JPG"
            If dlg.ShowDialog() = True Then
                Using stream As Stream = dlg.OpenFile()
                    Dim encoder As JpegBitmapEncoder = New JpegBitmapEncoder()
                    Dim bmp As RenderTargetBitmap = New RenderTargetBitmap(CInt(canvas.ActualWidth), CInt(canvas.ActualHeight), 1 \ 96, 1 \ 96, PixelFormats.Pbgra32)
                    bmp.Render(canvas)
                    encoder.Frames.Add(BitmapFrame.Create(bmp))
                    encoder.Save(stream)
                End Using
            End If
        End Sub

        Private Sub resetButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            canvas.Children.Clear()
        End Sub

#Region "Inner classes"
        Public MustInherit Class DrawingTool
            Inherits DependencyObject

            Private _Canvas As Canvas

#Region "static"
            Public Shared ReadOnly ColorProperty As DependencyProperty

            Public Shared ReadOnly SizeProperty As DependencyProperty

            Shared Sub New()
                ColorProperty = DependencyProperty.Register("Color", GetType(Color), GetType(DrawingTool), New PropertyMetadata(Colors.Black, AddressOf EditorsDemo.PaintControl.DrawingTool.OnPropertyChanged))
                SizeProperty = DependencyProperty.Register("Size", GetType(Double), GetType(DrawingTool), New PropertyMetadata(16R, AddressOf EditorsDemo.PaintControl.DrawingTool.OnPropertyChanged))
            End Sub

            Protected Shared Overloads Sub OnPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
                CType(d, DrawingTool).OnPropertyChanged()
            End Sub

#End Region
            Public Sub New(ByVal paramCanvas As Canvas)
                Canvas = paramCanvas
            End Sub

            Public Property Color As Color
                Get
                    Return CType(GetValue(ColorProperty), Color)
                End Get

                Set(ByVal value As Color)
                    SetValue(ColorProperty, value)
                End Set
            End Property

            Public Property Size As Double
                Get
                    Return CDbl(GetValue(SizeProperty))
                End Get

                Set(ByVal value As Double)
                    SetValue(SizeProperty, value)
                End Set
            End Property

            Protected Property Canvas As Canvas
                Get
                    Return _Canvas
                End Get

                Private Set(ByVal value As Canvas)
                    _Canvas = value
                End Set
            End Property

            Public MustOverride Sub OnMouseMove(ByVal e As MouseEventArgs)

            Public MustOverride Sub OnMouseDown(ByVal e As MouseButtonEventArgs)

            Public MustOverride Sub OnMouseUp(ByVal e As MouseButtonEventArgs)

            Public MustOverride Sub OnKeyDown(ByVal e As KeyEventArgs)

            Public Overridable Sub Release()
            End Sub

            Protected Overridable Overloads Sub OnPropertyChanged()
            End Sub

            Public MustOverride Function GetCursor() As BitmapSource

            Public Overridable ReadOnly Property CursorHorizontalOffset As Double
                Get
                    Return 0
                End Get
            End Property

            Public Overridable ReadOnly Property CursorVerticalOffset As Double
                Get
                    Return 0
                End Get
            End Property
        End Class

        Public Class PenTool
            Inherits DrawingTool

            Private Shared ReadOnly PenCursor As BitmapSource = ImageHelper.CreateImageFromEmbeddedResource(GetType(PenTool).Assembly, DemoHelper.GetPath("EditorsDemo.Images.Cursors.", GetType(PenTool).Assembly) & "cursor_pen.png")

            Public Sub New(ByVal canvas As Canvas)
                MyBase.New(canvas)
            End Sub

            Protected Property IsMouseDown As Boolean

            Protected Property LastPoint As Point

            Protected Property CurrentPoint As Point

            Public Overrides Sub OnMouseDown(ByVal e As MouseButtonEventArgs)
                IsMouseDown = True
                CurrentPoint = e.GetPosition(Canvas)
                DrawLine(CurrentPoint, CurrentPoint)
                LastPoint = CurrentPoint
                Canvas.CaptureMouse()
            End Sub

            Public Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
                If IsMouseDown Then
                    CurrentPoint = e.GetPosition(Canvas)
                    DrawLine(CurrentPoint, LastPoint)
                    LastPoint = CurrentPoint
                End If
            End Sub

            Protected Sub DrawLine(ByVal fromPoint As Point, ByVal toPoint As Point)
                Dim line As Line = New Line() With {.StrokeStartLineCap = PenLineCap.Round, .StrokeEndLineCap = PenLineCap.Round, .StrokeThickness = Size, .Stroke = New SolidColorBrush(Color)}
                line.X1 = toPoint.X
                line.Y1 = toPoint.Y
                line.X2 = fromPoint.X
                line.Y2 = fromPoint.Y
                Canvas.Children.Add(line)
            End Sub

            Public Overrides Sub OnMouseUp(ByVal e As MouseButtonEventArgs)
                Canvas.ReleaseMouseCapture()
                IsMouseDown = False
            End Sub

            Public Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
            End Sub

            Public Overrides Function GetCursor() As BitmapSource
                Return PenCursor
            End Function

            Public Overrides ReadOnly Property CursorHorizontalOffset As Double
                Get
                    Return 4
                End Get
            End Property

            Public Overrides ReadOnly Property CursorVerticalOffset As Double
                Get
                    Return 30
                End Get
            End Property
        End Class

        Public Class TextTool
            Inherits DrawingTool

            Private _ActiveTextEdit As TextEdit

            Private Shared ReadOnly TextCursor As BitmapSource = ImageHelper.CreateImageFromEmbeddedResource(GetType(TextTool).Assembly, DemoHelper.GetPath("EditorsDemo.Images.Cursors.", GetType(TextTool).Assembly) & "cursor_text.png")

#Region "static"
            Public Shared ReadOnly FontFamilyProperty As DependencyProperty

            Shared Sub New()
                FontFamilyProperty = DependencyProperty.Register("FontFamily", GetType(String), GetType(TextTool), New PropertyMetadata("", AddressOf EditorsDemo.PaintControl.DrawingTool.OnPropertyChanged))
            End Sub

#End Region
            Public Sub New(ByVal canvas As Canvas)
                MyBase.New(canvas)
            End Sub

            Public Property FontFamily As String
                Get
                    Return CStr(GetValue(FontFamilyProperty))
                End Get

                Set(ByVal value As String)
                    SetValue(FontFamilyProperty, value)
                End Set
            End Property

            Protected Property ActiveTextEdit As TextEdit
                Get
                    Return _ActiveTextEdit
                End Get

                Private Set(ByVal value As TextEdit)
                    _ActiveTextEdit = value
                End Set
            End Property

            Public Overrides Sub OnMouseDown(ByVal e As MouseButtonEventArgs)
                If ActiveTextEdit IsNot Nothing Then
                    Release()
                    Return
                End If

                If ActiveTextEdit Is Nothing Then
                    Dim currentPoint As Point = e.GetPosition(Canvas)
                    ActiveTextEdit = CreateTextEdit()
                    UpdateTextEditProperties()
                    Call Canvas.SetLeft(ActiveTextEdit, currentPoint.X)
                    Call Canvas.SetTop(ActiveTextEdit, currentPoint.Y)
                    Canvas.Children.Add(ActiveTextEdit)
                End If
            End Sub

            Public Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
                If e.Key = Key.Escape AndAlso ActiveTextEdit IsNot Nothing Then
                    Canvas.Children.Remove(ActiveTextEdit)
                    ActiveTextEdit = Nothing
                End If
            End Sub

            Public Overrides Sub Release()
                If ActiveTextEdit Is Nothing Then Return
                ActiveTextEdit.EditMode = EditMode.InplaceInactive
                ActiveTextEdit = Nothing
            End Sub

            Public Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            End Sub

            Public Overrides Sub OnMouseUp(ByVal e As MouseButtonEventArgs)
            End Sub

            Protected Overridable Function CreateTextEdit() As TextEdit
                Return New TextEdit() With {.AcceptsReturn = True, .Background = New SolidColorBrush(Colors.Transparent)}
            End Function

            Protected Overrides Sub OnPropertyChanged()
                MyBase.OnPropertyChanged()
                UpdateTextEditProperties()
            End Sub

            Protected Overridable Sub UpdateTextEditProperties()
                If ActiveTextEdit Is Nothing Then Return
                ActiveTextEdit.FontSize = Size
                ActiveTextEdit.FontFamily = New FontFamily(FontFamily)
                ActiveTextEdit.Foreground = New SolidColorBrush(Color)
                Dispatcher.BeginInvoke(New Action(Sub() ActiveTextEdit.Focus()))
            End Sub

            Public Overrides Function GetCursor() As BitmapSource
                Return TextCursor
            End Function
        End Class

#End Region
        Private Sub currentColor_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            ColorEditHelper.ShowColorChooserDialog(Me)
        End Sub

#Region "IColorEdit Members"
        Private Sub AddCustomColor(ByVal color As Color) Implements IColorEdit.AddCustomColor
            Tool.Color = color
        End Sub

        Private Property ColorMode As ColorPickerColorMode Implements IColorEdit.ColorMode

        Private Property Color As Color Implements IColorEdit.Color
            Get
                Return Tool.Color
            End Get

            Set(ByVal value As Color)
            End Set
        End Property

        Private Custom Event ColorChanged As RoutedEventHandler Implements IColorEdit.ColorChanged
            AddHandler(ByVal value As RoutedEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As RoutedEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As RoutedEventArgs)
            End RaiseEvent
        End Event

        Private Property DefaultColor As Color Implements IColorEdit.DefaultColor
            Get
                Return Tool.Color
            End Get

            Set(ByVal value As Color)
            End Set
        End Property

        Private Property EditValue As Object Implements IColorEdit.EditValue
            Get
                Return Tool.Color
            End Get

            Set(ByVal value As Object)
            End Set
        End Property

        Private Property Palettes As PaletteCollection Implements IColorEdit.Palettes
            Get
                Return Nothing
            End Get

            Set(ByVal value As PaletteCollection)
            End Set
        End Property

        Private ReadOnly Property RecentColors As CircularList(Of Color) Implements IColorEdit.RecentColors
            Get
                Return Nothing
            End Get
        End Property

        Private Property ShowAlphaChannel As Boolean Implements IColorEdit.ShowAlphaChannel
            Get
                Return True
            End Get

            Set(ByVal value As Boolean)
            End Set
        End Property

#End Region
        Private blockWidth As Double = 0R

        Private comboWidth As Double = 0R

        Private Sub panel_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            If grid.ColumnDefinitions(1).ActualWidth < rightPanel.ActualWidth Then
                If palettesTxt.Visibility <> Visibility.Collapsed Then
                    blockWidth = palettesTxt.ActualWidth
                    palettesTxt.Visibility = Visibility.Collapsed
                Else
                    comboWidth = palettesCombo.ActualWidth
                    palettesCombo.ItemTemplate = TryCast(Resources("smallItemTemplate"), DataTemplate)
                End If
            Else
                If Equals(palettesCombo.ItemTemplate, Resources("smallItemTemplate")) Then
                    If grid.ColumnDefinitions(1).ActualWidth > comboWidth Then palettesCombo.ItemTemplate = TryCast(Resources("itemTemplate"), DataTemplate)
                ElseIf grid.ColumnDefinitions(1).ActualWidth > rightPanel.ActualWidth + blockWidth Then
                    palettesTxt.Visibility = Visibility.Visible
                End If
            End If
        End Sub
    End Class

#Region "Helpers"
    Public Class FontSizes

        Public Shared ReadOnly Property Sizes As Double()
            Get
                Return New Double() {8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0, 32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0}
            End Get
        End Property
    End Class

    Public Class FontFamilies

        Private Shared fontNamesField As List(Of String)

        Public Shared ReadOnly Property FontNames As List(Of String)
            Get
                If fontNamesField Is Nothing Then fontNamesField = GetSystemFontNames()
                Return fontNamesField
            End Get
        End Property

        Private Shared Function GetSystemFontNames() As List(Of String)
            Dim systemFontNames As List(Of String) = New List(Of String)()
            For Each fam As FontFamily In Fonts.SystemFontFamilies
                systemFontNames.Add(fam.Source)
            Next

            Return systemFontNames
        End Function
    End Class

#End Region
#Region "Converters"
    Public Class PaletteCollectionConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim collection As PaletteCollection = TryCast(value, PaletteCollection)
            If collection IsNot Nothing Then Return collection(0).Colors
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class
#End Region
End Namespace
