Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Documents
Imports DevExpress.Xpf.Charts
Imports DevExpress.XtraPrinting.Native

Namespace ChartsDemo

    Public Partial Class SimpleDiagramTitlesTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub chart_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each series As PieSeries2D In chart.Diagram.Series
                series.Titles(0).Content = series.DisplayName
            Next

            If chart.Diagram.Series.Count > 0 Then chart.Diagram.Series(0).ShowInLegend = True
        End Sub

        Private Sub Hyperlink_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim source As Hyperlink = TryCast(sender, Hyperlink)
            If source IsNot Nothing Then
                Call ProcessLaunchHelper.StartConfirmed(source.NavigateUri.ToString())
            End If
        End Sub
    End Class
End Namespace
