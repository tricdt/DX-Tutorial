Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class PieTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub chart_QueryChartCursor(ByVal sender As Object, ByVal e As QueryChartCursorEventArgs)
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Position)
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing Then e.Cursor = Cursors.Hand
        End Sub

        Private isAnimationCompletedField As Boolean = False

        Public Overrides ReadOnly Property IsAnimationCompleted As Boolean
            Get
                Return isAnimationCompletedField
            End Get
        End Property

        Private Sub Storyboard_Completed(ByVal sender As Object, ByVal e As EventArgs)
            isAnimationCompletedField = True
        End Sub
    End Class

    Friend Class ShowTotalInToTitleVisibleConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing OrElse Not(TypeOf value Is String) Then Return value
            Dim str As String = CStr(value)
            Return Equals(str, "Title")
        End Function
    End Class

    Friend Class ShowTotalInToPieTotalLabelVisibilityConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing OrElse Not(TypeOf value Is String) Then Return value
            Dim str As String = CStr(value)
            If Equals(str, "Label") Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Function
    End Class

    Public Class PieChartRotationBehavior
        Inherits Behavior(Of ChartControl)

        Private ReadOnly Property Chart As ChartControl
            Get
                Return AssociatedObject
            End Get
        End Property

        Private ReadOnly Property Series As PieSeries2D
            Get
                Return CType(Chart.Diagram.Series(0), PieSeries2D)
            End Get
        End Property

        Private rotate As Boolean

        Private startPosition As Point

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler Chart.MouseDown, AddressOf Chart_MouseDown
            AddHandler Chart.MouseMove, AddressOf Chart_MouseMove
            AddHandler Chart.MouseUp, AddressOf Chart_MouseUp
        End Sub

        Private Sub Chart_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim position As Point = e.GetPosition(Chart)
            Dim hitInfo As ChartHitInfo = Chart.CalcHitInfo(position)
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing Then
                rotate = True
                startPosition = position
            End If
        End Sub

        Private Sub Chart_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim position = e.GetPosition(Chart)
            Dim hitInfo = Chart.CalcHitInfo(position)
            If hitInfo IsNot Nothing AndAlso rotate Then
                Dim angleDelta = CalcAngle(startPosition, position)
                angleDelta *= If(Series.SweepDirection = PieSweepDirection.Clockwise, -1.0, 1.0)
                Dim newAngle = Series.Rotation + angleDelta
                If Math.Abs(newAngle) > 360 Then newAngle += -720 * Math.Sign(newAngle)
                Series.Rotation = newAngle
                startPosition = position
            End If
        End Sub

        Private Sub Chart_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            rotate = False
        End Sub

        Private Function CalcAngle(ByVal p1 As Point, ByVal p2 As Point) As Double
            Dim center = New Point(Chart.Diagram.ActualWidth / 2, Chart.ActualHeight / 2)
            Dim startVector As Vector = p1 - center
            Dim endVector As Vector = p2 - center
            Return Vector.AngleBetween(endVector, startVector)
        End Function
    End Class
End Namespace
