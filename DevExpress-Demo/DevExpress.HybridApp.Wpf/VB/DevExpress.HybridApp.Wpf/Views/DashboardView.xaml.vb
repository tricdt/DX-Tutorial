Imports System.Windows.Controls
Imports System.Windows
Imports DevExpress.Xpf.Charts

Namespace DevExpress.DevAV.Views

    Public Partial Class DashboardView
        Inherits UserControl

        Const highTopSpacing As Integer = 10

        Const lowTopSpacing As Integer = -15

        Const highBottomSpacing As Integer = 5

        Const lowBottomSpacing As Integer = 0

        Const heightThreshold As Integer = 150

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub goodsSold_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            Dim legend As Legend = CType(sender, ChartControl).Legend
            If e.NewSize.Height < heightThreshold AndAlso (CInt(legend.Margin.Top) <> lowTopSpacing OrElse CInt(legend.Margin.Bottom) <> lowBottomSpacing) Then legend.Margin = New Thickness With {.Top = lowTopSpacing, .Bottom = lowBottomSpacing}
            If e.NewSize.Height >= heightThreshold AndAlso (CInt(legend.Margin.Top) <> highTopSpacing OrElse CInt(legend.Margin.Bottom) <> highBottomSpacing) Then legend.Margin = New Thickness With {.Top = highTopSpacing, .Bottom = highBottomSpacing}
        End Sub
    End Class
End Namespace
