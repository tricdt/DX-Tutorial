Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo

    <CodeFile("Modules/Highlighted/Histogram.xaml"), CodeFile("Modules/Highlighted/Histogram.xaml.(cs)"), CodeFile("Modules/Highlighted/HistogramViewModel.(cs)"), NoAutogeneratedCodeFiles>
    Public Partial Class HistogramDemo
        Inherits ChartsDemoModule

        Private random As Random = New Random(1)

        Public Sub New()
            InitializeComponent()
            GeneratePoints()
        End Sub

        Private Sub GeneratePoints()
            Dim data As List(Of IEnumerable(Of NumericPoint)) = New List(Of IEnumerable(Of NumericPoint))()
            data.Add(PointGenerator.GenerateCluster(random, random.Next(20, 70), random.Next(120, 180), random.Next(0, 10), random.Next(70, 120), 2000))
            data.Add(PointGenerator.GenerateCluster(random, random.Next(0, 10), random.Next(70, 120), random.Next(40, 80), random.Next(160, 200), 2000))
            data.Add(PointGenerator.GenerateCluster(random, random.Next(60, 100), random.Next(160, 200), random.Next(40, 80), random.Next(160, 200), 2000))
            For i As Integer = 0 To chart.Diagram.Series.Count - 1
                Dim dataIndex As Integer = CInt(Math.Floor(i / 3R))
                chart.Diagram.Series(i).DataSource = data(dataIndex)
            Next
        End Sub

        Private Sub BtnGeneratePoints_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GeneratePoints()
        End Sub
    End Class
End Namespace