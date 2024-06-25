Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace WindowsUIDemo

    Public Partial Class ImageViewer
        Inherits UserControl

#Region "Dependency Properties"
        Public Shared ReadOnly FlipProperty As DependencyProperty = DependencyProperty.Register("Flip", GetType(Boolean), GetType(ImageViewer), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnFlipPropertyChanged)))

        Public Shared ReadOnly ScaleProperty As DependencyProperty = DependencyProperty.Register("Scale", GetType(Double), GetType(ImageViewer), New PropertyMetadata(1R, New PropertyChangedCallback(AddressOf OnScalePropertyChanged)))

        Public Shared ReadOnly RotationProperty As DependencyProperty = DependencyProperty.Register("Rotation", GetType(Rotation), GetType(ImageViewer), New PropertyMetadata(Rotation.Rotate0))

        Public Shared ReadOnly ImageSourceProperty As DependencyProperty = DependencyProperty.Register("ImageSource", GetType(ImageSource), GetType(ImageViewer), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly MinScaleValueProperty As DependencyProperty = DependencyProperty.Register("MinScaleValue", GetType(Double), GetType(ImageViewer), New PropertyMetadata(0.1R))

        Public Shared ReadOnly MaxScaleValueProperty As DependencyProperty = DependencyProperty.Register("MaxScaleValue", GetType(Double), GetType(ImageViewer), New PropertyMetadata(4R))

        Protected Shared Sub OnScalePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImageViewer).OnScaleChanged(CDbl(e.OldValue))
        End Sub

        Protected Shared Sub OnFlipPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImageViewer).OnFlipChanged(CBool(e.OldValue))
        End Sub

#End Region  ' Dependency Properties
        Public Property Scale As Double
            Get
                Return CDbl(GetValue(ScaleProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(ScaleProperty, value)
            End Set
        End Property

        Public Property Flip As Boolean
            Get
                Return CBool(GetValue(FlipProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(FlipProperty, value)
            End Set
        End Property

        Public Property Rotation As Rotation
            Get
                Return CType(GetValue(RotationProperty), Rotation)
            End Get

            Set(ByVal value As Rotation)
                SetValue(RotationProperty, value)
            End Set
        End Property

        Public Property ImageSource As ImageSource
            Get
                Return CType(GetValue(ImageSourceProperty), ImageSource)
            End Get

            Set(ByVal value As ImageSource)
                SetValue(ImageSourceProperty, value)
            End Set
        End Property

        Public Property MinScaleValue As Double
            Get
                Return CDbl(GetValue(MinScaleValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MinScaleValueProperty, value)
            End Set
        End Property

        Public Property MaxScaleValue As Double
            Get
                Return CDbl(GetValue(MaxScaleValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MaxScaleValueProperty, value)
            End Set
        End Property

        Public ReadOnly Property HorizontalFitScale As Double
            Get
                Dim horzScale As Double = OriginalViewportSize.Width / OriginalContentSize.Width * Scale
                If OriginalViewportSize.Height < OriginalContentSize.Height / Scale * horzScale Then
                    horzScale =(OriginalViewportSize.Width - scrollBarSize) / OriginalContentSize.Width * Scale
                End If

                Return horzScale
            End Get
        End Property

        Public ReadOnly Property VerticalFitScale As Double
            Get
                Dim vertScale As Double = OriginalViewportSize.Height / OriginalContentSize.Height * Scale
                If OriginalViewportSize.Width < OriginalContentSize.Width / Scale * vertScale Then
                    vertScale =(OriginalViewportSize.Height - scrollBarSize) / OriginalContentSize.Height * Scale
                End If

                Return vertScale
            End Get
        End Property

        Public Event MouseWheelZoom As EventHandler

        Public ReadOnly Property PartImage As Image
            Get
                Return image
            End Get
        End Property

        Public ReadOnly Property Viewport As Border
            Get
                Return border
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Unloaded, AddressOf OnUnloaded
            AddHandler scrollHost.PreviewMouseWheel, AddressOf OnScrollHostMouseWheel
        End Sub

        Public Function GetBestFitScale() As Double
            Dim bestScale As Double = HorizontalFitScale
            If scrollHost.ViewportHeight < CType(scrollHost.Content, UIElement).DesiredSize.Height / Scale * bestScale Then Return VerticalFitScale
            Return bestScale
        End Function

        Public Sub ScaleCenter(ByVal scaleValue As Double)
            Dim scalePoint As Point = New Point(scrollHost.HorizontalOffset + CDbl(OriginalViewportSize.Width) / 2, scrollHost.VerticalOffset + CDbl(OriginalViewportSize.Height) / 2)
            ScaleAndScroll(scalePoint, scaleValue)
        End Sub

        Protected Overrides Sub OnMouseWheel(ByVal e As MouseWheelEventArgs)
            e.Handled = True
            Dim point As Point = e.GetPosition(CType(scrollHost.Content, UIElement))
            If e.Delta > 0 Then
                ScaleAndScroll(point, Math.Min(Scale + 0.1, MaxScaleValue))
            Else
                ScaleAndScroll(point, Math.Max(Scale - 0.1, MinScaleValue))
            End If

            RaiseEvent MouseWheelZoom(Me, New EventArgs())
            Return
        End Sub

        Protected Overridable Sub OnScaleChanged(ByVal oldValue As Double)
            scaleIsChanged = True
        End Sub

        Protected Overridable Sub OnFlipChanged(ByVal oldValue As Boolean)
            scaleIsChanged = True
        End Sub

        Protected Overridable Sub OnLayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            UpdateScrollbarVisibility()
            UpdateMouseCursor()
            If scaleIsChanged Then
                scrollHost.ScrollToHorizontalOffset(scrollOffsetAfterScaleChanged.X)
                scrollHost.ScrollToVerticalOffset(scrollOffsetAfterScaleChanged.Y)
                scaleIsChanged = False
            End If
        End Sub

        Protected Overridable Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler LayoutUpdated, AddressOf Me.OnLayoutUpdated
        End Sub

        Protected Overridable Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler LayoutUpdated, AddressOf Me.OnLayoutUpdated
        End Sub

        Protected Overridable Sub OnImageMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Not isLeftButtonDown Then Return
            Dim point As Point = e.GetPosition(scrollHost)
            Dim horizontalDragDelta As Double = dragPoint.X - point.X
            Dim verticalDragDelta As Double = dragPoint.Y - point.Y
            scrollHost.ScrollToHorizontalOffset(dragOffset.Width + horizontalDragDelta)
            scrollHost.ScrollToVerticalOffset(dragOffset.Height + verticalDragDelta)
        End Sub

        Protected Overridable Sub OnImageMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            CType(sender, UIElement).CaptureMouse()
            isLeftButtonDown = True
            dragPoint = e.GetPosition(scrollHost)
            dragOffset = New Size(scrollHost.HorizontalOffset, scrollHost.VerticalOffset)
        End Sub

        Protected Overridable Sub OnImageMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            CType(sender, UIElement).ReleaseMouseCapture()
            isLeftButtonDown = False
        End Sub

        Protected Overridable Sub OnImageLostMouseCapture(ByVal sender As Object, ByVal e As EventArgs)
            isLeftButtonDown = False
        End Sub

        Protected Overridable Sub ScaleAndScroll(ByVal scalePoint As Point, ByVal scaleValue As Double)
            Dim originalImageSize As Size = New Size(OriginalContentSize.Width / Scale, OriginalContentSize.Height / Scale)
            Dim viewportOffset As Point = New Point(scalePoint.X - scrollHost.HorizontalOffset, scalePoint.Y - scrollHost.VerticalOffset)
            scrollOffsetAfterScaleChanged = New Point(scalePoint.X / Scale * scaleValue - viewportOffset.X, scalePoint.Y / Scale * scaleValue - viewportOffset.Y)
            Scale = scaleValue
        End Sub

        Protected Overridable Sub OnScrollHostMouseWheel(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
            OnMouseWheel(e)
        End Sub

        Private Sub UpdateScrollbarVisibility()
            If OriginalContentSize.Height - OriginalViewportSize.Height > 0.5R Then
                scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Visible
            Else
                scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            End If

            If OriginalContentSize.Width - OriginalViewportSize.Width > 0.5R Then
                scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible
            Else
                scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
            End If
        End Sub

        Private Sub UpdateMouseCursor()
            If scrollHost.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible OrElse scrollHost.VerticalScrollBarVisibility = ScrollBarVisibility.Visible Then
                CType(scrollHost.Content, ImagePresenter).Cursor = Cursors.Hand
            Else
                CType(scrollHost.Content, ImagePresenter).Cursor = Cursors.Arrow
            End If
        End Sub

        Private ReadOnly Property OriginalContentSize As Size
            Get
                Return CType(scrollHost.Content, UIElement).DesiredSize
            End Get
        End Property

        Private ReadOnly Property OriginalViewportSize As Size
            Get
                Dim originViewportWidth As Double = If(scrollHost.ComputedVerticalScrollBarVisibility = Visibility.Visible, scrollHost.ViewportWidth + scrollBarSize, scrollHost.ViewportWidth)
                Dim originViewportHeight As Double = If(scrollHost.ComputedHorizontalScrollBarVisibility = Visibility.Visible, scrollHost.ViewportHeight + scrollBarSize, scrollHost.ViewportHeight)
                Return New Size(originViewportWidth, originViewportHeight)
            End Get
        End Property

        Private dragPoint As Point = New Point()

        Private dragOffset As Size = New Size()

        Private scrollOffsetAfterScaleChanged As Point = New Point()

        Private scaleIsChanged As Boolean = False

        Private isLeftButtonDown As Boolean = False

        Const scrollBarSize As Double = 12
    End Class

    Public Class ImagePresenter
        Inherits Panel

#Region "Dependency Properties"
        Public Shared ReadOnly FlipProperty As DependencyProperty = DependencyProperty.Register("Flip", GetType(Boolean), GetType(ImagePresenter), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnFlipPropertyChanged)))

        Public Shared ReadOnly RotationProperty As DependencyProperty = DependencyProperty.Register("Rotation", GetType(Rotation), GetType(ImagePresenter), New PropertyMetadata(Rotation.Rotate90, New PropertyChangedCallback(AddressOf OnRotationPropertyChanged)))

        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(UIElement), GetType(ImagePresenter), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnContentPropertyChanged)))

        Public Shared ReadOnly ScaleProperty As DependencyProperty = DependencyProperty.Register("Scale", GetType(Double), GetType(ImagePresenter), New PropertyMetadata(1R, New PropertyChangedCallback(AddressOf OnScalePropertyChanged)))

        Protected Shared Sub OnRotationPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnRotationChanged(CType(e.OldValue, Rotation))
        End Sub

        Protected Shared Sub OnFlipPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnFlipChanged(CBool(e.OldValue))
        End Sub

        Protected Shared Sub OnContentPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnContentChanged(CType(e.OldValue, UIElement))
        End Sub

        Protected Shared Sub OnScalePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ImagePresenter).OnScaleChanged(CDbl(e.OldValue))
        End Sub

#End Region  ' Dependency Properties
        Public Property Rotation As Rotation
            Get
                Return CType(GetValue(RotationProperty), Rotation)
            End Get

            Set(ByVal value As Rotation)
                SetValue(RotationProperty, value)
            End Set
        End Property

        Public Property Content As UIElement
            Get
                Return CType(GetValue(ContentProperty), UIElement)
            End Get

            Set(ByVal value As UIElement)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Property Scale As Double
            Get
                Return CDbl(GetValue(ScaleProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(ScaleProperty, value)
            End Set
        End Property

        Public Property Flip As Boolean
            Get
                Return CBool(GetValue(FlipProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(FlipProperty, value)
            End Set
        End Property

        Protected Overridable Sub OnContentChanged(ByVal oldValue As UIElement)
            If oldValue IsNot Nothing Then
                oldValue.RenderTransform = Nothing
                Children.Remove(oldValue)
            End If

            If Content IsNot Nothing Then
                Dim transformGroup As TransformGroup = New TransformGroup()
                Dim scaleTransform As ScaleTransform = New ScaleTransform() With {.ScaleX = Scale, .ScaleY = Scale}
                Dim rotateTransform As RotateTransform = New RotateTransform() With {.Angle = GetAngleByRotation(Rotation)}
                transformGroup.Children.Add(scaleTransform)
                transformGroup.Children.Add(rotateTransform)
                transformGroup.Children.Add(New TranslateTransform())
                Content.RenderTransform = transformGroup
                Content.RenderTransformOrigin = New Point(0.5, 0.5)
                Children.Add(Content)
            End If
        End Sub

        Protected Overridable Sub OnScaleChanged(ByVal oldValue As Double)
            If Content IsNot Nothing Then
                TryCast(TryCast(Content.RenderTransform, TransformGroup).Children(0), ScaleTransform).ScaleX = If(Flip, -Scale, Scale)
                TryCast(TryCast(Content.RenderTransform, TransformGroup).Children(0), ScaleTransform).ScaleY = Scale
                Content.InvalidateArrange()
                InvalidateMeasure()
            End If

            InvalidateArrange()
            UpdateLayout()
        End Sub

        Protected Overridable Sub OnRotationChanged(ByVal oldValue As Rotation)
            If Content IsNot Nothing Then
                TryCast(TryCast(Content.RenderTransform, TransformGroup).Children(1), RotateTransform).Angle = GetAngleByRotation(Rotation)
                Content.InvalidateMeasure()
                InvalidateMeasure()
            End If
        End Sub

        Protected Overridable Sub OnFlipChanged(ByVal oldValue As Boolean)
            If Content IsNot Nothing Then
                TryCast(TryCast(Content.RenderTransform, TransformGroup).Children(0), ScaleTransform).ScaleX = If(Flip, -Scale, Scale)
                TryCast(TryCast(Content.RenderTransform, TransformGroup).Children(0), ScaleTransform).ScaleY = Scale
                Content.InvalidateArrange()
                InvalidateMeasure()
            End If

            InvalidateArrange()
            UpdateLayout()
        End Sub

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            If Content Is Nothing Then Return New Size()
            Dim baseSize As Size = New Size(Double.PositiveInfinity, Double.PositiveInfinity)
            Content.Measure(baseSize)
            Select Case Rotation
                Case Rotation.Rotate0, Rotation.Rotate180
                    Return New Size(Content.DesiredSize.Width * Scale, Content.DesiredSize.Height * Scale)
                Case Else
                    Return New Size(Content.DesiredSize.Height * Scale, Content.DesiredSize.Width * Scale)
            End Select
        End Function

        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            If Content Is Nothing Then Return New Size()
            Content.Arrange(New Rect(0, 0, Content.DesiredSize.Width, Content.DesiredSize.Height))
            Return finalSize
        End Function

        Private Function GetAngleByRotation(ByVal rotation As Rotation) As Double
            If rotation = Rotation.Rotate0 Then
                Return 0
            ElseIf rotation = Rotation.Rotate90 Then
                Return 90
            ElseIf rotation = Rotation.Rotate180 Then
                Return 180
            ElseIf rotation = Rotation.Rotate270 Then
                Return 270
            End If

            Return 0
        End Function

        Private Function GetTranslatePoint() As Point
            Select Case Rotation
                Case Rotation.Rotate0
                    Return New Point(0, 0)
                Case Rotation.Rotate90
                    Return New Point(Content.DesiredSize.Height * Scale, 0)
                Case Rotation.Rotate180
                    Return New Point(Content.DesiredSize.Width * Scale, Content.DesiredSize.Height * Scale)
                Case Rotation.Rotate270
                    Return New Point(0, Content.DesiredSize.Width * Scale)
            End Select

            Return New Point()
        End Function
    End Class
End Namespace
