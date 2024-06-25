Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media.Animation

Namespace ControlsDemo.GalleryDemo

    Public Class ZoomStackPanel
        Inherits Panel

        Public Property MinChildOffset As Double
            Get
                Return _minChildOffset
            End Get

            Set(ByVal value As Double)
                _minChildOffset = value
            End Set
        End Property

        Public Property MaxChildOffset As Double
            Get
                Return _maxChildOffset
            End Get

            Set(ByVal value As Double)
                _maxChildOffset = value
            End Set
        End Property

        Public Property ChildOffset As Double
            Get
                Return _childOffset
            End Get

            Set(ByVal value As Double)
                Dim actualValue As Double = CoerceChildOffset(value)
                If actualValue = _childOffset Then Return
                _childOffset = actualValue
                OnChildOffsetChanged()
            End Set
        End Property

        Private _childOffset As Double = 0

        Private _minChildOffset As Double = -3

        Private _maxChildOffset As Double = 3

        Protected Overridable Function CoerceChildOffset(ByVal value As Double) As Double
            Dim res As Double = value
            Dim intervalLength As Double = MaxChildOffset - MinChildOffset
            While res < MinChildOffset
                res += intervalLength
            End While

            While res > MaxChildOffset
                res -= intervalLength
            End While

            Return res
        End Function

        Protected Overridable Sub OnChildOffsetChanged()
            InvalidateArrange()
            UpdateLayout()
        End Sub

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            Dim sector As Double = Children.Count \ 5
            Dim res As Size = New Size()
            For i As Integer = 0 To Children.Count - 1
                Dim child As UIElement = Children(i)
                child.Measure(availableSize)
                res.Width += child.DesiredSize.Width
                res.Height = Math.Max(res.Height, child.DesiredSize.Height)
                If i < sector Then
                    res.Width += 1
                ElseIf i < sector * 2 Then
                    res.Width += 2
                ElseIf i < sector * 3 Then
                    res.Width += 3
                ElseIf i < sector * 4 Then
                    res.Width += 2
                Else
                    res.Width += 1
                End If
            Next

            Return res
        End Function

        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            Dim sector As Double = Children.Count \ 5
            Dim x As Double = ChildOffset
            For i As Integer = 0 To Children.Count - 1
                Dim child As UIElement = Children(i)
                Dim pos As Point = New Point(x, 0)
                Dim size As Size = New Size(child.DesiredSize.Width, finalSize.Height)
                child.Arrange(New Rect(pos, size))
                If i < sector Then
                    x += size.Width + 1
                ElseIf i < sector * 2 Then
                    x += size.Width + 2
                ElseIf i < sector * 3 Then
                    x += size.Width + 3
                ElseIf i < sector * 4 Then
                    x += size.Width + 2
                ElseIf i <= Children.Count Then
                    x += size.Width + 1
                End If
            Next

            Return finalSize
        End Function
    End Class

    Public Partial Class ZoomScroll
        Inherits UserControl

        Private _IsMouseLeftButtonPressed As Boolean

#Region "Dependency Properties"
        Public Shared ReadOnly IncreaseDeltaProperty As DependencyProperty = DependencyProperty.Register("IncreaseDelta", GetType(Double), GetType(ZoomScroll), New PropertyMetadata(50R))

        Public Shared ReadOnly MouseWheelDeltaProperty As DependencyProperty = DependencyProperty.Register("MouseWheelDelta", GetType(Double), GetType(ZoomScroll), New PropertyMetadata(25R))

        Public Shared ReadOnly MinValueProperty As DependencyProperty = DependencyProperty.Register("MinValue", GetType(Double), GetType(ZoomScroll), New PropertyMetadata(10R))

        Public Shared ReadOnly MaxValueProperty As DependencyProperty = DependencyProperty.Register("MaxValue", GetType(Double), GetType(ZoomScroll), New PropertyMetadata(400R))

        Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.Register("Value", GetType(Double), GetType(ZoomScroll), New PropertyMetadata(100R, New PropertyChangedCallback(AddressOf ControlsDemo.GalleryDemo.ZoomScroll.OnValuePropertyChanged), New CoerceValueCallback(AddressOf ControlsDemo.GalleryDemo.ZoomScroll.CoerceValueProperty)))

        Protected Shared Sub OnValuePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim obj As ZoomScroll = CType(d, ZoomScroll)
            obj.OnValueChanged()
        End Sub

        Protected Shared Function CoerceValueProperty(ByVal d As DependencyObject, ByVal value As Object) As Object
            Dim obj As ZoomScroll = CType(d, ZoomScroll)
            Return obj.CoerceValue(CDbl(value))
        End Function

#End Region  ' Dependency Properties
        Public Property IncreaseDelta As Double
            Get
                Return CDbl(GetValue(IncreaseDeltaProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(IncreaseDeltaProperty, value)
            End Set
        End Property

        Public Property MouseWheelDelta As Double
            Get
                Return CDbl(GetValue(MouseWheelDeltaProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MouseWheelDeltaProperty, value)
            End Set
        End Property

        Public Property MinValue As Double
            Get
                Return CDbl(GetValue(MinValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MinValueProperty, value)
            End Set
        End Property

        Public Property MaxValue As Double
            Get
                Return CDbl(GetValue(MaxValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MaxValueProperty, value)
            End Set
        End Property

        Public Property Value As Double
            Get
                Return CDbl(GetValue(ValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(ValueProperty, value)
            End Set
        End Property

        Public Property IsMouseLeftButtonPressed As Boolean
            Get
                Return _IsMouseLeftButtonPressed
            End Get

            Protected Set(ByVal value As Boolean)
                _IsMouseLeftButtonPressed = value
            End Set
        End Property

        Public Sub New()
            Cursor = Cursors.Hand
            InitializeComponent()
            InitializeMouseMoveAnimation()
            InitializeMouseWheelAnimation()
            InitializeIncreaseAnimation()
        End Sub

        Protected Overridable Sub OnZoomDecreaseClick(ByVal sender As Object, ByVal e As EventArgs)
            IncreaseAnimation.To = Value - IncreaseDelta
            IncreaseAnimationStoryboard.Begin()
        End Sub

        Protected Overridable Sub OnZoomIncreaseClick(ByVal sender As Object, ByVal e As EventArgs)
            IncreaseAnimation.To = Value + IncreaseDelta
            IncreaseAnimationStoryboard.Begin()
        End Sub

        Public Event ValueChanged As EventHandler

        Protected Overridable Sub OnValueChanged()
            ZoomPanel.ChildOffset = Value
            RaiseEvent ValueChanged(Me, New EventArgs())
            Return
        End Sub

        Protected Overridable Overloads Function CoerceValue(ByVal value As Double) As Double
            If value < MinValue Then Return MinValue
            If value > MaxValue Then Return MaxValue
            Return value
        End Function

        Protected Overridable Sub InitializeMouseMoveAnimation()
            MouseMoveAnimationStoryboard = New Storyboard()
            MouseMoveAnimation = New DoubleAnimation()
            MouseMoveAnimation.Duration = New Duration(TimeSpan.FromSeconds(0.0))
            Storyboard.SetTarget(MouseMoveAnimation, Me)
            Call Storyboard.SetTargetProperty(MouseMoveAnimation, New PropertyPath(ValueProperty))
            MouseMoveAnimationStoryboard.Children.Add(MouseMoveAnimation)
        End Sub

        Protected Overridable Sub InitializeMouseWheelAnimation()
            MouseWheelAnimationStoryboard = New Storyboard()
            MouseWheelAnimation = New DoubleAnimation()
            MouseWheelAnimation.Duration = New Duration(TimeSpan.FromSeconds(0.5))
            Storyboard.SetTarget(MouseWheelAnimation, Me)
            Call Storyboard.SetTargetProperty(MouseWheelAnimation, New PropertyPath(ValueProperty))
            MouseWheelAnimationStoryboard.Children.Add(MouseWheelAnimation)
        End Sub

        Protected Overridable Sub InitializeIncreaseAnimation()
            IncreaseAnimationStoryboard = New Storyboard()
            IncreaseAnimation = New DoubleAnimation()
            IncreaseAnimation.Duration = New Duration(TimeSpan.FromSeconds(0.5))
            Storyboard.SetTarget(IncreaseAnimation, Me)
            Call Storyboard.SetTargetProperty(IncreaseAnimation, New PropertyPath(ValueProperty))
            IncreaseAnimationStoryboard.Children.Add(IncreaseAnimation)
        End Sub

        Protected Overrides Sub OnMouseLeftButtonDown(ByVal e As MouseButtonEventArgs)
            CaptureMouse()
            IsMouseLeftButtonPressed = True
            PrevMousePos = e.GetPosition(Me)
        End Sub

        Protected Overrides Sub OnMouseLeftButtonUp(ByVal e As MouseButtonEventArgs)
            ReleaseMouseCapture()
            IsMouseLeftButtonPressed = False
        End Sub

        Protected Overrides Sub OnLostMouseCapture(ByVal e As MouseEventArgs)
            IsMouseLeftButtonPressed = False
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            If Not IsMouseLeftButtonPressed Then Return
            Dim changeX As Double = e.GetPosition(Me).X - PrevMousePos.X
            PrevMousePos = e.GetPosition(Me)
            MouseMoveAnimation.To = Value + changeX
            MouseMoveAnimationStoryboard.Begin()
        End Sub

        Protected Overrides Sub OnMouseWheel(ByVal e As MouseWheelEventArgs)
            MyBase.OnMouseWheel(e)
            If e.Delta > 0 Then
                SetZoomValue(Value + MouseWheelDelta, 0.5)
            Else
                SetZoomValue(Value - MouseWheelDelta, 0.5)
            End If
        End Sub

        Public Overridable Sub SetZoomValue(ByVal value As Double, ByVal duration As Double)
            MouseWheelAnimation.Duration = New Duration(TimeSpan.FromSeconds(duration))
            MouseWheelAnimation.To = value
            MouseWheelAnimationStoryboard.Begin()
        End Sub

        Private PrevMousePos As Point

        Private MouseMoveAnimationStoryboard As Storyboard

        Private MouseMoveAnimation As DoubleAnimation

        Private MouseWheelAnimationStoryboard As Storyboard

        Private MouseWheelAnimation As DoubleAnimation

        Private IncreaseAnimationStoryboard As Storyboard

        Private IncreaseAnimation As DoubleAnimation
    End Class
End Namespace
