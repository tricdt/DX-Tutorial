Imports DevExpress.Xpf.Charts
Imports System.Windows.Input

Namespace ChartsDemo

    Public Partial Class Pie3DTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub chart_QueryChartCursor(ByVal sender As Object, ByVal e As QueryChartCursorEventArgs)
            Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Position)
            If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing AndAlso Mouse.PrimaryDevice.LeftButton = MouseButtonState.Released Then e.Cursor = Cursors.Hand
        End Sub

        Private isAnimationCompletedField As Boolean = False

        Public Overrides ReadOnly Property IsAnimationCompleted As Boolean
            Get
                Return isAnimationCompletedField
            End Get
        End Property

        Private Sub Storyboard_Completed(ByVal sender As Object, ByVal e As System.EventArgs)
            isAnimationCompletedField = True
        End Sub
    End Class
End Namespace
