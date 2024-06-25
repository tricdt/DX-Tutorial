Imports System.Windows
Imports System.Windows.Controls
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Map.Native

Namespace MapDemo

    Public Class PhotoGalleryControl
        Inherits VisibleControl
        Implements IViewportAnimatableElement

        Public Shared ReadOnly ActualItemsProperty As DependencyProperty = DependencyProperty.Register("ActualItems", GetType(ObservableCollection(Of Object)), GetType(PhotoGalleryControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly ItemsSourceProperty As DependencyProperty = DependencyProperty.Register("ItemsSource", GetType(IEnumerable), GetType(PhotoGalleryControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf ItemsSourcePropertyChanged)))

        Public Shared ReadOnly ItemTemplateProperty As DependencyProperty = DependencyProperty.Register("ItemTemplate", GetType(DataTemplate), GetType(PhotoGalleryControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly AnchorPointProperty As DependencyProperty = DependencyProperty.Register("AnchorPoint", GetType(Point), GetType(PhotoGalleryControl), New PropertyMetadata(New Point(0, 0)))

        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(PhotoGalleryControl), New PropertyMetadata(String.Empty))

        Private Shared Sub ItemsSourcePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim gallery As PhotoGalleryControl = TryCast(d, PhotoGalleryControl)
            If gallery IsNot Nothing Then gallery.Update()
        End Sub

        <Browsable(False)>
        Public ReadOnly Property ActualItems As ObservableCollection(Of Object)
            Get
                Return CType(GetValue(ActualItemsProperty), ObservableCollection(Of Object))
            End Get
        End Property

        Public Property ItemsSource As IEnumerable
            Get
                Return CType(GetValue(ItemsSourceProperty), IEnumerable)
            End Get

            Set(ByVal value As IEnumerable)
                SetValue(ItemsSourceProperty, value)
            End Set
        End Property

        Public Property ItemTemplate As DataTemplate
            Get
                Return CType(GetValue(ItemTemplateProperty), DataTemplate)
            End Get

            Set(ByVal value As DataTemplate)
                SetValue(ItemTemplateProperty, value)
            End Set
        End Property

        Public Property AnchorPoint As Point
            Get
                Return CType(GetValue(AnchorPointProperty), Point)
            End Get

            Set(ByVal value As Point)
                SetValue(AnchorPointProperty, value)
            End Set
        End Property

        Public Property Title As String
            Get
                Return CStr(GetValue(TitleProperty))
            End Get

            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property

        Private ReadOnly progress As AnimationProgress

        Private storyboardField As Storyboard = Nothing

        Private animationInProgress As Boolean = False

        Private elements As ItemsControl = Nothing

        Private ReadOnly Property LayoutPanel As Panel
            Get
                Return CommonUtils.GetChildPanel(elements)
            End Get
        End Property

        Private ReadOnly Property Storyboard As Storyboard
            Get
                If storyboardField Is Nothing Then
                    storyboardField = New Storyboard()
                    AddHandler storyboardField.Completed, New EventHandler(AddressOf StoryboardCompleted)
                    AnimationHelper.AddStoryboard(Me, storyboardField, storyboardField.GetHashCode())
                End If

                Return storyboardField
            End Get
        End Property

        Public ReadOnly Property AnimationProgress As Double
            Get
                Return progress.ActualProgress
            End Get
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(PhotoGalleryControl)
            progress = New AnimationProgress(Me)
        End Sub

        Private ReadOnly Property InProgress As Boolean Implements IViewportAnimatableElement.InProgress
            Get
                Return animationInProgress
            End Get
        End Property

        Private Sub ProgressChanged() Implements IViewportAnimatableElement.ProgressChanged
            Invalidate()
        End Sub

        Private Sub StoryboardCompleted(ByVal sender As Object, ByVal e As EventArgs)
            StopAnimation()
        End Sub

        Private Sub StopAnimation()
            animationInProgress = False
            progress.FinishAnimation()
        End Sub

        Private Sub Animate()
            If progress.ActualProgress > 0 Then
                StopAnimation()
                PrepareStoryboard()
                animationInProgress = True
                progress.StartAnimation()
                Storyboard.Begin()
            End If
        End Sub

        Private Sub PrepareStoryboard()
            Storyboard.Stop()
            Storyboard.Children.Clear()
            AnimationHelper.PrepareStoryboard(Storyboard, progress, "Progress")
        End Sub

        Private Sub Update()
            Dim items As ObservableCollection(Of Object) = New ObservableCollection(Of Object)()
            If ItemsSource IsNot Nothing Then
                For Each item As Object In ItemsSource
                    items.Add(item)
                Next
            End If

            SetValue(ActualItemsProperty, items)
        End Sub

        Private Sub Invalidate()
            If LayoutPanel IsNot Nothing Then LayoutPanel.InvalidateMeasure()
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            elements = TryCast(GetTemplateChild("PART_Elements"), ItemsControl)
        End Sub

        Protected Overrides Sub VisibleChanged()
            MyBase.VisibleChanged()
            If Visible Then Animate()
        End Sub
    End Class

    Public Interface IViewportAnimatableElement

        ReadOnly Property InProgress As Boolean

        Sub ProgressChanged()

    End Interface

    Public Class AnimationProgress
        Inherits DependencyObject

        Public Shared ReadOnly ProgressProperty As DependencyProperty = DependencyProperty.Register("Progress", GetType(Double), GetType(AnimationProgress), New PropertyMetadata(1.0, New PropertyChangedCallback(AddressOf ProgressPropertyChanged)))

        Public Property Progress As Double
            Get
                Return CDbl(GetValue(ProgressProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(ProgressProperty, value)
            End Set
        End Property

        Private Shared Sub ProgressPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim animationProgress As AnimationProgress = TryCast(d, AnimationProgress)
            If animationProgress IsNot Nothing Then animationProgress.RaiseProgressChanged()
        End Sub

        Private ReadOnly animationTarget As IViewportAnimatableElement

        Private ReadOnly Property InProgress As Boolean
            Get
                Return If(animationTarget IsNot Nothing, animationTarget.InProgress, False)
            End Get
        End Property

        Public ReadOnly Property ActualProgress As Double
            Get
                If InProgress Then Return Progress
                Return 1.0
            End Get
        End Property

        Public Sub New(ByVal animationTarget As IViewportAnimatableElement)
            Me.animationTarget = animationTarget
        End Sub

        Protected Sub RaiseProgressChanged()
            If animationTarget IsNot Nothing Then animationTarget.ProgressChanged()
        End Sub

        Public Sub StartAnimation()
            Progress = 0.0
            RaiseProgressChanged()
        End Sub

        Public Sub FinishAnimation()
            Progress = 1.0
        End Sub
    End Class

    Public Class AnimationHelper

        Private Shared Function CreateTimeline() As Timeline
            Dim timeline As DoubleAnimation = New DoubleAnimation()
            timeline.From = 0
            timeline.To = 1.0
            timeline.Duration = New Duration(TimeSpan.FromSeconds(2.0))
            timeline.BeginTime = TimeSpan.Zero
            Dim easingFunction As PowerEase = New PowerEase()
            easingFunction.EasingMode = EasingMode.EaseInOut
            easingFunction.Power = 3.0
            timeline.EasingFunction = easingFunction
            Return timeline
        End Function

        Public Shared Sub PrepareStoryboard(ByVal storyboard As Storyboard, ByVal progress As AnimationProgress, ByVal propertyName As String)
            Dim timeline As Timeline = CreateTimeline()
            Storyboard.SetTarget(timeline, progress)
            Call Storyboard.SetTargetProperty(timeline, New PropertyPath(propertyName))
            storyboard.Children.Add(timeline)
            storyboard.BeginTime = TimeSpan.Zero
        End Sub

        Public Shared Sub AddStoryboard(ByVal owner As Control, ByVal storyboard As Storyboard, ByVal resourceKey As Integer)
            If storyboard IsNot Nothing AndAlso Not owner.Resources.Contains(resourceKey.ToString()) Then owner.Resources.Add(resourceKey.ToString(), storyboard)
        End Sub
    End Class

    Public Class PhotoGalleryPanel
        Inherits Panel

        Const defaultWidth As Double = 300.0

        Const defaultHeight As Double = 300.0

        Private rowCount As Integer = 0

        Private columnCount As Integer = 0

        Private ReadOnly Property TransformedPoint As Point
            Get
                Return CType(LayoutHelper.GetTopLevelVisual(Me), FrameworkElement).TransformToVisual(Me).Transform(AnchorPoint)
            End Get
        End Property

        Private ReadOnly Property Gallery As PhotoGalleryControl
            Get
                Return TryCast(DataContext, PhotoGalleryControl)
            End Get
        End Property

        Private ReadOnly Property AnchorPoint As Point
            Get
                Return If(Gallery IsNot Nothing, Gallery.AnchorPoint, New Point(0, 0))
            End Get
        End Property

        Private Function GetProgress(ByVal elementNumber As Integer, ByVal elementCount As Integer) As Double
            If Gallery Is Nothing Then Return 1R
            Dim [step] As Double = 1R / (elementCount + 1)
            Dim result As Double = 0.5R * (Gallery.AnimationProgress - elementNumber * [step]) / [step]
            Return Math.Max(0R, Math.Min(1R, result))
        End Function

        Private Sub CalculateLayout(ByVal constraint As Size, ByVal elementCount As Integer)
            Dim sizeRatio As Double = constraint.Width / constraint.Height * 0.5
            rowCount = CInt(Math.Max(1, Math.Round(Math.Sqrt(elementCount / sizeRatio))))
            columnCount = CInt(Math.Ceiling(CDbl(elementCount) / CDbl(rowCount)))
        End Sub

        Private Function AnimateSize(ByVal anchorSize As Size, ByVal elementNumber As Integer, ByVal elementCount As Integer) As Size
            Dim progress As Double = GetProgress(elementNumber, elementCount)
            Return New Size(anchorSize.Width * progress, anchorSize.Height * progress)
        End Function

        Private Function AnimateRect(ByVal targetRect As Rect, ByVal elementNumber As Integer, ByVal elementCount As Integer) As Rect
            Dim progress As Double = GetProgress(elementNumber, elementCount)
            Return New Rect(TransformedPoint.X + progress * (targetRect.X - TransformedPoint.X), TransformedPoint.Y + progress * (targetRect.Y - TransformedPoint.Y), targetRect.Width * progress, targetRect.Height * progress)
        End Function

        Private Function CalculateElementLayout(ByVal elementSize As Size, ByVal elementNumber As Integer) As Rect
            Dim x As Double =(elementNumber Mod columnCount) * elementSize.Width
            Dim y As Double = Math.Ceiling(CDbl(elementNumber \ columnCount)) * elementSize.Height
            Return New Rect(x, y, elementSize.Width, elementSize.Height)
        End Function

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            Dim constraintWidth As Double = If(Double.IsInfinity(availableSize.Width), defaultWidth, availableSize.Width)
            Dim constraintHeight As Double = If(Double.IsInfinity(availableSize.Height), defaultHeight, availableSize.Height)
            Dim constraint As Size = New Size(constraintWidth, constraintHeight)
            CalculateLayout(constraint, Children.Count)
            Dim elementSize As Size = New Size(constraint.Width / CDbl(columnCount), constraint.Height / CDbl(rowCount))
            Dim width As Double = 0, height As Double = 0
            For i As Integer = 0 To Children.Count - 1
                Children(i).Measure(elementSize)
                width = Math.Max(width, Children(i).DesiredSize.Width)
                height = Math.Max(height, Children(i).DesiredSize.Height)
            Next

            Return New Size(columnCount * width, rowCount * height)
        End Function

        Protected Overrides Function ArrangeOverride(ByVal arrangeSize As Size) As Size
            For i As Integer = 0 To Children.Count - 1
                Children(i).Opacity = GetProgress(i, Children.Count)
                Children(i).Arrange(AnimateRect(CalculateElementLayout(Children(i).DesiredSize, i), i, Children.Count))
            Next

            Return arrangeSize
        End Function
    End Class
End Namespace
