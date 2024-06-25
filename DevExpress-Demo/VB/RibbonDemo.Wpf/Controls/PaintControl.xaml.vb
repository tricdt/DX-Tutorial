Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core.Native
Imports System.Linq
Imports System.Reflection
Imports System.IO
Imports Microsoft.Win32

Namespace RibbonDemo

    Public Partial Class PaintControl
        Inherits System.Windows.Controls.UserControl

        Const filterString As String = "JPEG Files (*.JPG)|*.jpg;*.jpeg"

#Region "static        "
        Public Shared ReadOnly ShowAutomaticButtonProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly ShowNoColorButtonProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly ShowMoreColorsButtonProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly ChipSizeProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly ShowEditorsProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly BrushSizeProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly BrushOpacityProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly TextFontFamilyProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly TextFontSizeProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly TextFontColorProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly BackgroundTextColorProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly BackgroundImageSourceProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly BrushColorProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly MaxBrushSizeProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly MinBrushSizeProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly MousePositionProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly IsBrushToolSelectedProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly IsTextToolSelectedProperty As System.Windows.DependencyProperty

        Public Shared ReadOnly IsTextEditingProperty As System.Windows.DependencyProperty

        Shared Sub New()
            RibbonDemo.PaintControl.ShowAutomaticButtonProperty = System.Windows.DependencyProperty.Register("ShowAutomaticButton", GetType(Boolean), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(True))
            RibbonDemo.PaintControl.ShowNoColorButtonProperty = System.Windows.DependencyProperty.Register("ShowNoColorButton", GetType(Boolean), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(False))
            RibbonDemo.PaintControl.ShowMoreColorsButtonProperty = System.Windows.DependencyProperty.Register("ShowMoreColorsButton", GetType(Boolean), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(False))
            RibbonDemo.PaintControl.ChipSizeProperty = System.Windows.DependencyProperty.Register("ChipSize", GetType(DevExpress.Xpf.Editors.ChipSize), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(DevExpress.Xpf.Editors.ChipSize.[Default]))
            RibbonDemo.PaintControl.BrushSizeProperty = System.Windows.DependencyProperty.Register("BrushSize", GetType(Double), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(8R))
            RibbonDemo.PaintControl.BrushOpacityProperty = System.Windows.DependencyProperty.Register("BrushOpacity", GetType(Double), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(1R))
            RibbonDemo.PaintControl.TextFontFamilyProperty = System.Windows.DependencyProperty.Register("TextFontFamily", GetType(System.Windows.Media.FontFamily), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))
            RibbonDemo.PaintControl.TextFontSizeProperty = System.Windows.DependencyProperty.Register("TextFontSize", GetType(Double), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(12R))
            RibbonDemo.PaintControl.TextFontColorProperty = System.Windows.DependencyProperty.Register("TextFontColor", GetType(System.Windows.Media.Color), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(System.Windows.Media.Colors.Black))
            RibbonDemo.PaintControl.BackgroundTextColorProperty = System.Windows.DependencyProperty.Register("BackgroundTextColor", GetType(System.Windows.Media.Color), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(System.Windows.Media.Colors.Transparent))
            RibbonDemo.PaintControl.BackgroundImageSourceProperty = System.Windows.DependencyProperty.Register("BackgroundImageSource", GetType(System.Windows.Media.ImageSource), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.Media.Imaging.BitmapImage), New System.Windows.PropertyChangedCallback(AddressOf RibbonDemo.PaintControl.OnBackgroundImagePropertyChanged)))
            RibbonDemo.PaintControl.BrushColorProperty = System.Windows.DependencyProperty.Register("BrushColor", GetType(System.Windows.Media.Color), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(System.Windows.Media.Colors.Red))
            RibbonDemo.PaintControl.MaxBrushSizeProperty = System.Windows.DependencyProperty.Register("MaxBrushSize", GetType(Double), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(24R))
            RibbonDemo.PaintControl.MinBrushSizeProperty = System.Windows.DependencyProperty.Register("MinBrushSize", GetType(Double), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(1R))
            RibbonDemo.PaintControl.MousePositionProperty = System.Windows.DependencyProperty.Register("MousePosition", GetType(System.Windows.Point), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(New System.Windows.Point(-1, -1)))
            RibbonDemo.PaintControl.IsBrushToolSelectedProperty = System.Windows.DependencyProperty.Register("IsBrushToolSelected", GetType(Boolean), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(True, New System.Windows.PropertyChangedCallback(AddressOf RibbonDemo.PaintControl.OnIsBrushToolSelectedPropertyChanged)))
            RibbonDemo.PaintControl.IsTextToolSelectedProperty = System.Windows.DependencyProperty.Register("IsTextToolSelected", GetType(Boolean), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(False, New System.Windows.PropertyChangedCallback(AddressOf RibbonDemo.PaintControl.OnIsTextToolSelectedPropertyChanged)))
            RibbonDemo.PaintControl.IsTextEditingProperty = System.Windows.DependencyProperty.Register("IsTextEditing", GetType(Boolean), GetType(RibbonDemo.PaintControl), New System.Windows.PropertyMetadata(False))
        End Sub

        Public Shared Sub OnBackgroundImagePropertyChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
            CType(d, RibbonDemo.PaintControl).OnBackgroundImageChanged(e)
        End Sub

        Private Sub OnBackgroundImageChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
            AddHandler Me.backgroundImage.LayoutUpdated, New System.EventHandler(AddressOf Me.OnBackgroundImageLayoutUpdated)
        End Sub

        Private Sub OnBackgroundImageLayoutUpdated(ByVal sender As Object, ByVal e As System.EventArgs)
            RemoveHandler Me.backgroundImage.LayoutUpdated, AddressOf Me.OnBackgroundImageLayoutUpdated
            Me.UpdateCanvas()
        End Sub

        Private Shared Sub OnIsBrushToolSelectedPropertyChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
            CType(d, RibbonDemo.PaintControl).OnIsBrushToolSelectedChanged()
        End Sub

        Private Shared Sub OnIsTextToolSelectedPropertyChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
            CType(d, RibbonDemo.PaintControl).OnIsTextToolSelectedChanged()
        End Sub

#End Region
#Region "props"
        Public Property MousePosition As Point
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.MousePositionProperty), System.Windows.Point)
            End Get

            Set(ByVal value As Point)
                Me.SetValue(RibbonDemo.PaintControl.MousePositionProperty, value)
            End Set
        End Property

        Public Property ShowAutomaticButton As Boolean
            Get
                Return CBool(Me.GetValue(RibbonDemo.PaintControl.ShowAutomaticButtonProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(RibbonDemo.PaintControl.ShowAutomaticButtonProperty, value)
            End Set
        End Property

        Public Property ShowNoColorButton As Boolean
            Get
                Return CBool(Me.GetValue(RibbonDemo.PaintControl.ShowNoColorButtonProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(RibbonDemo.PaintControl.ShowNoColorButtonProperty, value)
            End Set
        End Property

        Public Property ShowMoreColorsButton As Boolean
            Get
                Return CBool(Me.GetValue(RibbonDemo.PaintControl.ShowMoreColorsButtonProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(RibbonDemo.PaintControl.ShowMoreColorsButtonProperty, value)
            End Set
        End Property

        Public Property BrushSize As Double
            Get
                Return CDbl(Me.GetValue(RibbonDemo.PaintControl.BrushSizeProperty))
            End Get

            Set(ByVal value As Double)
                Me.SetValue(RibbonDemo.PaintControl.BrushSizeProperty, value)
            End Set
        End Property

        Public Property ChipSize As ChipSize
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.ChipSizeProperty), DevExpress.Xpf.Editors.ChipSize)
            End Get

            Set(ByVal value As ChipSize)
                Me.SetValue(RibbonDemo.PaintControl.ChipSizeProperty, value)
            End Set
        End Property

        Public Property MinBrushSize As Double
            Get
                Return CDbl(Me.GetValue(RibbonDemo.PaintControl.MinBrushSizeProperty))
            End Get

            Set(ByVal value As Double)
                Me.SetValue(RibbonDemo.PaintControl.MinBrushSizeProperty, value)
            End Set
        End Property

        Public Property MaxBrushSize As Double
            Get
                Return CDbl(Me.GetValue(RibbonDemo.PaintControl.MaxBrushSizeProperty))
            End Get

            Set(ByVal value As Double)
                Me.SetValue(RibbonDemo.PaintControl.MaxBrushSizeProperty, value)
            End Set
        End Property

        Public Property BrushColor As Color
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.BrushColorProperty), System.Windows.Media.Color)
            End Get

            Set(ByVal value As Color)
                Me.SetValue(RibbonDemo.PaintControl.BrushColorProperty, value)
            End Set
        End Property

        Public Property TextFontColor As Color
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.TextFontColorProperty), System.Windows.Media.Color)
            End Get

            Set(ByVal value As Color)
                Me.SetValue(RibbonDemo.PaintControl.TextFontColorProperty, value)
            End Set
        End Property

        Public Property BackgroundTextColor As Color
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.BackgroundTextColorProperty), System.Windows.Media.Color)
            End Get

            Set(ByVal value As Color)
                Me.SetValue(RibbonDemo.PaintControl.BackgroundTextColorProperty, value)
            End Set
        End Property

        Public Property BackgroundImageSource As ImageSource
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.BackgroundImageSourceProperty), System.Windows.Media.ImageSource)
            End Get

            Set(ByVal value As ImageSource)
                Me.SetValue(RibbonDemo.PaintControl.BackgroundImageSourceProperty, value)
            End Set
        End Property

        Public Property BrushOpacity As Double
            Get
                Return CDbl(Me.GetValue(RibbonDemo.PaintControl.BrushOpacityProperty))
            End Get

            Set(ByVal value As Double)
                Me.SetValue(RibbonDemo.PaintControl.BrushOpacityProperty, value)
            End Set
        End Property

        Public Property TextFontSize As Double
            Get
                Return CDbl(Me.GetValue(RibbonDemo.PaintControl.TextFontSizeProperty))
            End Get

            Set(ByVal value As Double)
                Me.SetValue(RibbonDemo.PaintControl.TextFontSizeProperty, value)
            End Set
        End Property

        Public Property TextFontFamily As FontFamily
            Get
                Return CType(Me.GetValue(RibbonDemo.PaintControl.TextFontFamilyProperty), System.Windows.Media.FontFamily)
            End Get

            Set(ByVal value As FontFamily)
                Me.SetValue(RibbonDemo.PaintControl.TextFontFamilyProperty, value)
            End Set
        End Property

        Public Property IsBrushToolSelected As Boolean
            Get
                Return CBool(Me.GetValue(RibbonDemo.PaintControl.IsBrushToolSelectedProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(RibbonDemo.PaintControl.IsBrushToolSelectedProperty, value)
            End Set
        End Property

        Public Property IsTextToolSelected As Boolean
            Get
                Return CBool(Me.GetValue(RibbonDemo.PaintControl.IsTextToolSelectedProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(RibbonDemo.PaintControl.IsTextToolSelectedProperty, value)
            End Set
        End Property

        Public Property IsTextEditing As Boolean
            Get
                Return CBool(Me.GetValue(RibbonDemo.PaintControl.IsTextEditingProperty))
            End Get

            Set(ByVal value As Boolean)
                Me.SetValue(RibbonDemo.PaintControl.IsTextEditingProperty, value)
            End Set
        End Property

#End Region
#Region "commands"
        Public Property UndoCommand As ICommand

        Public Property RedoCommand As ICommand

        Public Property ClearCommand As ICommand

        Public Property SaveCommand As ICommand

        Public Property OpenCommand As ICommand

#End Region
        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf Me.OnLoaded
            AddHandler Unloaded, New System.Windows.RoutedEventHandler(AddressOf Me.OnUnloaded)
            Me.URStack = New System.Collections.Generic.Stack(Of System.Windows.UIElement)()
            Me.UndoCommand = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.UndoCommandExecute, AddressOf Me.CanUndoCommandExecute)
            Me.RedoCommand = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.RedoCommandExecute, AddressOf Me.CanRedoCommandExecute)
            Me.ClearCommand = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.ClearCommandExecute, AddressOf Me.CanClearCommandExecute)
            Me.SaveCommand = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.SaveCommandExecute)
            Me.OpenCommand = New DevExpress.Mvvm.DelegateCommand(AddressOf Me.OpenCommandExecute)
            Me.TextFontFamily = Me.textEdit.FontFamily
            Me.UpdateCursor()
        End Sub

#Region "commands implementation"
        Protected Sub SaveCommandExecute()
            Me.CompleteTextEditing(True)
            Dim dlg As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog() With {.Filter = RibbonDemo.PaintControl.filterString, .Title = "Save file..."}
            If dlg.ShowDialog() = True Then
                Using stream As System.IO.Stream = dlg.OpenFile()
                    Dim encoder As System.Windows.Media.Imaging.JpegBitmapEncoder = New System.Windows.Media.Imaging.JpegBitmapEncoder()
                    Dim bmp As System.Windows.Media.Imaging.RenderTargetBitmap = New System.Windows.Media.Imaging.RenderTargetBitmap(CInt(Me.canvas.ActualWidth), CInt(Me.canvas.ActualHeight), 1 \ 96, 1 \ 96, System.Windows.Media.PixelFormats.Pbgra32)
                    bmp.Render(Me.canvas)
                    encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp))
                    encoder.Save(stream)
                End Using
            End If
        End Sub

        Protected Sub OpenCommandExecute()
            Me.CompleteTextEditing(True)
            Dim dialog As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog() With {.Filter = RibbonDemo.PaintControl.filterString, .Title = "Open file..."}
            If dialog.ShowDialog().Value = True Then
                Me.ClearCommandExecute()
                Me.BackgroundImageSource = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromStream(New System.IO.FileStream(dialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            End If
        End Sub

        Protected Sub ClearCommandExecute()
            Me.CompleteTextEditing(True)
            While Me.canvas.Children.Count > 2
                Me.canvas.Children.RemoveAt(2)
            End While

            Me.URStack.Clear()
            Me.RaiseCommandsCanExecuteChanged()
            Me.SetCurrentValue(RibbonDemo.PaintControl.BackgroundImageSourceProperty, Nothing)
        End Sub

        Protected Function CanClearCommandExecute() As Boolean
            Return Me.URStack.Count <> 0 OrElse Me.canvas.Children.Count > 2
        End Function

        Protected Sub UndoCommandExecute()
            Me.URStack.Push(TryCast(Me.canvas.Children(Me.canvas.Children.Count - 1), System.Windows.UIElement))
            Me.canvas.Children.RemoveAt(Me.canvas.Children.Count - 1)
            Me.RaiseCommandsCanExecuteChanged()
        End Sub

        Protected Function CanUndoCommandExecute() As Boolean
            Return Me.canvas.Children.Count > 2
        End Function

        Protected Sub RedoCommandExecute()
            Me.canvas.Children.Add(Me.URStack.Pop())
            Me.RaiseCommandsCanExecuteChanged()
        End Sub

        Protected Function CanRedoCommandExecute() As Boolean
            Return Me.URStack.Count <> 0
        End Function

        Private Sub RaiseCommandsCanExecuteChanged()
            CType(Me.RedoCommand, DevExpress.Mvvm.DelegateCommand).RaiseCanExecuteChanged()
            CType(Me.UndoCommand, DevExpress.Mvvm.DelegateCommand).RaiseCanExecuteChanged()
            CType(Me.ClearCommand, DevExpress.Mvvm.DelegateCommand).RaiseCanExecuteChanged()
        End Sub

#End Region
        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            RemoveHandler SizeChanged, AddressOf Me.OnSizeChanged
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            AddHandler SizeChanged, New System.Windows.SizeChangedEventHandler(AddressOf Me.OnSizeChanged)
            Me.UpdateCanvas()
        End Sub

        Private Sub OnSizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs)
            Me.UpdateCanvas()
        End Sub

        Private URStack As System.Collections.Generic.Stack(Of System.Windows.UIElement)

        Private demoCenterBottomPanelHeightCoerceValue As Integer = 80

        Private Sub UpdateCanvas()
            Dim left As Double =(Me.canvas.ActualWidth - Me.backgroundImage.ActualWidth) / 2
            Call System.Windows.Controls.Canvas.SetLeft(Me.backgroundImage, If(left < 0, 0, left))
            Dim top As Double =(Me.canvas.ActualHeight - Me.demoCenterBottomPanelHeightCoerceValue - Me.backgroundImage.ActualHeight) / 2
            Call System.Windows.Controls.Canvas.SetTop(Me.backgroundImage, If(top < 0, 0, top))
        End Sub

        Private Sub OnIsBrushToolSelectedChanged()
            If Me.IsBrushToolSelected Then
                Me.IsTextToolSelected = False
                Me.UpdateCursor()
            End If
        End Sub

        Private Sub OnIsTextToolSelectedChanged()
            If Me.IsTextToolSelected Then
                Me.IsBrushToolSelected = False
                Me.UpdateCursor()
            End If
        End Sub

#Region "layout"
        Private isLeftButtonPressed As Boolean

        Private paintLayer As System.Windows.Controls.Canvas

        Private currentPoint As System.Windows.Point

        Private lastPoint As System.Windows.Point

        Private Sub DrawLine(ByVal fromPoint As System.Windows.Point, ByVal toPoint As System.Windows.Point)
            Dim line As System.Windows.Shapes.Line = New System.Windows.Shapes.Line() With {.StrokeStartLineCap = System.Windows.Media.PenLineCap.Round, .StrokeEndLineCap = System.Windows.Media.PenLineCap.Round, .StrokeThickness = Me.BrushSize, .Stroke = New System.Windows.Media.SolidColorBrush(Me.BrushColor)}
            line.X1 = toPoint.X
            line.Y1 = toPoint.Y
            line.X2 = fromPoint.X
            line.Y2 = fromPoint.Y
            Me.paintLayer.Children.Add(line)
        End Sub

        Private Sub FocusTextEdit()
            Me.textEdit.Focus()
        End Sub

        Private Sub CompleteTextEditing(ByVal cancel As Boolean)
            Me.IsTextEditing = False
            Me.UpdateCursor()
            If cancel OrElse String.IsNullOrEmpty(Me.textEdit.Text) Then Return
            Dim textBlock As System.Windows.Controls.TextBlock = New System.Windows.Controls.TextBlock() With {.Foreground = New System.Windows.Media.SolidColorBrush(Me.TextFontColor), .FontSize = Me.TextFontSize, .Text = Me.textEdit.Text}
            Call System.Windows.Controls.Canvas.SetLeft(textBlock, System.Windows.Controls.Canvas.GetLeft(Me.textEdit) + 2)
            Call System.Windows.Controls.Canvas.SetTop(textBlock, System.Windows.Controls.Canvas.GetTop(Me.textEdit))
            If Me.TextFontFamily IsNot Nothing Then textBlock.FontFamily = Me.TextFontFamily
            Me.canvas.Children.Add(textBlock)
            Me.URStack.Clear()
        End Sub

        Private Sub OnCanvasMouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.isLeftButtonPressed = True
            If Me.IsBrushToolSelected Then
                Me.paintLayer = New System.Windows.Controls.Canvas() With {.Height = Me.canvas.ActualHeight, .Width = Me.canvas.ActualWidth, .FlowDirection = System.Windows.FlowDirection.LeftToRight, .Visibility = System.Windows.Visibility.Visible}
                Me.IsTextEditing = False
                Me.canvas.Children.Add(Me.paintLayer)
                Me.paintLayer.Opacity = Me.BrushOpacity
                Me.currentPoint = e.GetPosition(Me.canvas)
                Me.DrawLine(Me.currentPoint, Me.currentPoint)
                Me.lastPoint = Me.currentPoint
                Me.canvas.CaptureMouse()
            Else
                If Me.IsTextEditing Then
                    Me.CompleteTextEditing(False)
                Else
                    Me.textEdit.Text = ""
                    Me.IsTextEditing = True
                    Me.UpdateCursor()
                    Dim currentPoint As System.Windows.Point = e.GetPosition(Me.canvas)
                    Call System.Windows.Controls.Canvas.SetLeft(Me.textEdit, currentPoint.X - 4)
                    Call System.Windows.Controls.Canvas.SetTop(Me.textEdit, currentPoint.Y - 8)
                    Me.Dispatcher.BeginInvoke(New System.Action(AddressOf Me.FocusTextEdit))
                End If
            End If
        End Sub

        Private Sub UpdateCursor()
            If Me.IsBrushToolSelected Then
                Me.Cursor = System.Windows.Input.Cursors.Pen
            Else
                If Me.IsTextEditing Then
                    Me.Cursor = System.Windows.Input.Cursors.Arrow
                Else
                    Me.Cursor = System.Windows.Input.Cursors.IBeam
                End If
            End If
        End Sub

        Private Sub OnCanvasMouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs)
            If Me.IsBrushToolSelected AndAlso Me.isLeftButtonPressed Then
                Me.currentPoint = e.GetPosition(Me.paintLayer)
                Me.DrawLine(Me.currentPoint, Me.lastPoint)
                Me.lastPoint = Me.currentPoint
            End If

            Me.MousePosition = e.GetPosition(Me.canvas)
        End Sub

        Private Sub OnCanvasMouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.isLeftButtonPressed = False
            If Me.IsBrushToolSelected Then
                Me.canvas.ReleaseMouseCapture()
                Dim path As System.Windows.Shapes.Path = New System.Windows.Shapes.Path()
                path.StrokeThickness = Me.BrushSize
                path.Stroke = New System.Windows.Media.SolidColorBrush(Me.BrushColor)
                path.StrokeStartLineCap = System.Windows.Media.PenLineCap.Round
                path.StrokeEndLineCap = System.Windows.Media.PenLineCap.Round
                path.Opacity = Me.BrushOpacity
                Dim g As System.Windows.Media.PathGeometry = New System.Windows.Media.PathGeometry()
                If Me.paintLayer IsNot Nothing Then
                    For Each l As System.Windows.Shapes.Line In Me.paintLayer.Children
                        If l Is Me.paintLayer.Children(0) Then
                            g.Figures.Add(New System.Windows.Media.PathFigure() With {.StartPoint = New System.Windows.Point(l.X1, l.Y1)})
                            g.Figures(CInt((0))).Segments.Add(New System.Windows.Media.LineSegment() With {.Point = New System.Windows.Point(l.X2, l.Y2)})
                        Else
                            g.Figures(CInt((0))).Segments.Add(New System.Windows.Media.LineSegment() With {.Point = New System.Windows.Point(l.X1, l.Y1)})
                            g.Figures(CInt((0))).Segments.Add(New System.Windows.Media.LineSegment() With {.Point = New System.Windows.Point(l.X2, l.Y2)})
                        End If
                    Next
                End If

                path.StrokeLineJoin = System.Windows.Media.PenLineJoin.Round
                path.Data = g
                Me.canvas.Children.Remove(Me.paintLayer)
                Me.canvas.Children.Add(path)
            Else
            End If

            Me.URStack.Clear()
            Me.RaiseCommandsCanExecuteChanged()
        End Sub

        Private Sub OnCanvasKeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs)
            If Me.IsTextToolSelected Then
                If e.Key = System.Windows.Input.Key.Escape Then
                    Me.CompleteTextEditing(True)
                End If
            End If
        End Sub
#End Region
    End Class
End Namespace
