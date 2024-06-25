Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports DevExpress.Xpf.Utils

Namespace ControlsDemo.GalleryDemo

    Public Partial Class SlideViewer
        Inherits UserControl

        Public Shared ReadOnly NextSlideImageSourceProperty As DependencyProperty = DependencyPropertyManager.Register("NextSlideImageSource", GetType(ImageSource), GetType(SlideViewer), New FrameworkPropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property NextSlideImageSource As ImageSource
            Get
                Return CType(GetValue(NextSlideImageSourceProperty), ImageSource)
            End Get

            Set(ByVal value As ImageSource)
                SetValue(NextSlideImageSourceProperty, value)
            End Set
        End Property

        Public Event BeforeCurrentSlideLoading As EventHandler

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            currentSlideControl = slide1Control
            nextSlideControl = slide2Control
        End Sub

        Public Sub Play()
            currentSlideControl = slide1Control
            nextSlideControl = slide2Control
            currentSlideControl.Source = Nothing
            currentSlideControl.Opacity = 1
            nextSlideControl.Source = NextSlideImageSource
            nextSlideControl.Opacity = 0
            BeginSlideChanging()
        End Sub

        Public Sub [Stop]()
            changeSlideStoryboard.Stop()
            RemoveHandler changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            changeSlideStoryboard = Nothing
        End Sub

        Protected Function CreateChangedSlideStoryboard() As Storyboard
            Dim storyboard As Storyboard = New Storyboard()
            storyboard.Duration = New Duration(TimeSpan.FromSeconds(5))
            Dim rnd As Random = New Random()
            Select Case rnd.Next(5)
                Case 0
                    CreateMoveUpAnimation(storyboard)
                Case 1
                    CreateMoveLeftAnimation(storyboard)
                Case 2
                    CreateMoveDownAnimation(storyboard)
                Case 3
                    CreateMoveRightAnimation(storyboard)
                Case 4
                    CreateOpacityAnimation(storyboard)
            End Select

            Return storyboard
        End Function

        Protected Sub CreateOpacityAnimation(ByVal storyboard As Storyboard)
            currentSlideControl.RenderTransform = Nothing
            nextSlideControl.RenderTransform = Nothing
            currentSlideControl.Opacity = 1
            nextSlideControl.Opacity = 0
            Dim slide1Animation As DoubleAnimation = New DoubleAnimation()
            slide1Animation.To = 1
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            Storyboard.SetTarget(slide1Animation, nextSlideControl)
            Call Storyboard.SetTargetProperty(slide1Animation, New PropertyPath("Opacity"))
            Dim slide2Animation As DoubleAnimation = New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            Storyboard.SetTarget(slide2Animation, currentSlideControl)
            Call Storyboard.SetTargetProperty(slide2Animation, New PropertyPath("Opacity"))
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub

        Protected Sub CreateMoveRightAnimation(ByVal storyboard As Storyboard)
            currentSlideControl.Opacity = 1
            nextSlideControl.Opacity = 1
            currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As DoubleAnimation = New DoubleAnimation()
            slide1Animation.To = ActualWidth
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, currentSlideControl, "X")
            nextSlideControl.RenderTransform = New TranslateTransform() With {.X = -ActualWidth}
            Dim slide2Animation As DoubleAnimation = New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, nextSlideControl, "X")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub

        Protected Sub CreateMoveLeftAnimation(ByVal storyboard As Storyboard)
            currentSlideControl.Opacity = 1
            nextSlideControl.Opacity = 1
            currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As DoubleAnimation = New DoubleAnimation()
            slide1Animation.To = -ActualWidth
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, currentSlideControl, "X")
            nextSlideControl.RenderTransform = New TranslateTransform() With {.X = ActualWidth}
            Dim slide2Animation As DoubleAnimation = New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, nextSlideControl, "X")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub

        Protected Sub CreateMoveUpAnimation(ByVal storyboard As Storyboard)
            currentSlideControl.Opacity = 1
            nextSlideControl.Opacity = 1
            currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As DoubleAnimation = New DoubleAnimation()
            slide1Animation.To = -ActualHeight
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, currentSlideControl, "Y")
            nextSlideControl.RenderTransform = New TranslateTransform() With {.Y = ActualHeight}
            Dim slide2Animation As DoubleAnimation = New DoubleAnimation()
            slide2Animation.To = 0
            slide2Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, nextSlideControl, "Y")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub

        Protected Sub CreateMoveDownAnimation(ByVal storyboard As Storyboard)
            currentSlideControl.Opacity = 1
            nextSlideControl.Opacity = 1
            currentSlideControl.RenderTransform = New TranslateTransform()
            Dim slide1Animation As DoubleAnimation = New DoubleAnimation()
            slide1Animation.To = ActualHeight
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide1Animation, currentSlideControl, "Y")
            nextSlideControl.RenderTransform = New TranslateTransform() With {.Y = -ActualHeight}
            Dim slide2Animation As DoubleAnimation = New DoubleAnimation()
            slide2Animation.To = 0
            slide1Animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            SetAnimationTarget(slide2Animation, nextSlideControl, "Y")
            storyboard.Children.Add(slide1Animation)
            storyboard.Children.Add(slide2Animation)
        End Sub

        Private Sub ChangeSlideStoryboard_Completed(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            currentSlideControl.BeginAnimation(OpacityProperty, Nothing)
            nextSlideControl.BeginAnimation(OpacityProperty, Nothing)
            Dim slideControl As Image = currentSlideControl
            currentSlideControl = nextSlideControl
            nextSlideControl = slideControl
            RaiseEvent BeforeCurrentSlideLoading(Me, New EventArgs())
            nextSlideControl.Source = NextSlideImageSource
            BeginSlideChanging()
        End Sub

        Private Sub BeginSlideChanging()
            If changeSlideStoryboard IsNot Nothing Then RemoveHandler changeSlideStoryboard.Completed, AddressOf ChangeSlideStoryboard_Completed
            changeSlideStoryboard = CreateChangedSlideStoryboard()
            AddHandler changeSlideStoryboard.Completed, New EventHandler(AddressOf ChangeSlideStoryboard_Completed)
            changeSlideStoryboard.Begin()
        End Sub

        Private Sub SetAnimationTarget(ByVal animation As DoubleAnimation, ByVal target As Image, ByVal path As String)
            Storyboard.SetTarget(animation, target)
            Call Storyboard.SetTargetProperty(animation, New PropertyPath("RenderTransform.(TranslateTransform." & path & ")"))
        End Sub

        Private Property currentSlideControl As Image

        Private Property nextSlideControl As Image

        Private Property changeSlideStoryboard As Storyboard
    End Class
End Namespace
