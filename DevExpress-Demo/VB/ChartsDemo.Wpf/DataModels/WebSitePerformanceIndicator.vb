Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class WebSitePerformanceIndicatorItem

        Public Property ReportDate As DateTime

        Public Property TrafficTime As Integer

        Public Property ResponseTime As Integer

        Public Property AveragePageLoadTime As Double

        Public Property MemoryUsage As Integer

        Public Property CPUUsage As Integer

        Public Property ClientErrors As Integer

        Public Property ServerErrors As Integer

        Public Property NewVisitors As Integer

        Public Property ReturnVisitors As Integer
    End Class

    Public Class WebSitePerformanceIndicator

        Private dataField As System.Collections.Generic.List(Of ChartsDemo.WebSitePerformanceIndicatorItem)

        Public ReadOnly Property Data As List(Of ChartsDemo.WebSitePerformanceIndicatorItem)
            Get
                If Me.dataField Is Nothing Then Me.dataField = Me.GetData()
                Return Me.dataField
            End Get
        End Property

        Private Function GetData() As List(Of ChartsDemo.WebSitePerformanceIndicatorItem)
            Dim data As System.Collections.Generic.List(Of ChartsDemo.WebSitePerformanceIndicatorItem) = New System.Collections.Generic.List(Of ChartsDemo.WebSitePerformanceIndicatorItem)()
            Dim lastDate As System.DateTime = System.DateTime.Now.AddDays(-1)
            Dim random As System.Random = New System.Random(1)
            For i As Integer = 0 To 30 - 1
                Dim newVisitors As Integer = random.[Next](18, 77)
                data.Add(New ChartsDemo.WebSitePerformanceIndicatorItem() With {.ReportDate = lastDate.AddDays(-i), .TrafficTime = random.[Next](3, 12), .ResponseTime = random.[Next](40, 110), .AveragePageLoadTime = random.NextDouble() * 3 + 0.5, .MemoryUsage = random.[Next](500, 2000), .CPUUsage = random.[Next](10, 77), .ClientErrors = random.[Next](2, 45), .ServerErrors = random.[Next](2, 7), .NewVisitors = newVisitors, .ReturnVisitors = random.[Next](10, newVisitors)})
            Next

            Return data
        End Function
    End Class
End Namespace
