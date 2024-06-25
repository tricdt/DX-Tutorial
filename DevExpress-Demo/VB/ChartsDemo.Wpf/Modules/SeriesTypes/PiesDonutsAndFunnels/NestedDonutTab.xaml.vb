Imports System.Windows
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class NestedDonutTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ChartControlBoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim seriesCollection = chart.Diagram.Series
            If seriesCollection.Count > 0 Then
                For Each series As NestedDonutSeries2D In seriesCollection
                    series.ShowInLegend = False
                    Dim population As AgePopulation = TryCast(series.Points(0).Tag, AgePopulation)
                    If population IsNot Nothing Then series.Group = population.Name
                Next

                seriesCollection(0).ShowInLegend = True
            End If
        End Sub
    End Class
End Namespace
