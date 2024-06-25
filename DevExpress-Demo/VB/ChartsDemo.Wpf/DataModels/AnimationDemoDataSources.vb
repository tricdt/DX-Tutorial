Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Windows

Namespace ChartsDemo

    Public Class AnimationDataPointSources

        Private Class GoogleStockData

            Public ReadOnly Property ShortData As DataTable
                Get
                    Return GetData().AsEnumerable().Reverse().Take(30).Reverse().CopyToDataTable()
                End Get
            End Property

            Private Function LoadDataTableFromXml(ByVal fileName As String, ByVal tableName As String) As DataTable
                Dim uri As Uri = New Uri("pack://application:,,,/ChartsDemo;component/Data/" & fileName)
                Dim xmlStream As Stream = Application.GetResourceStream(uri).Stream
                Dim xmlDataSet As DataSet = New DataSet()
                xmlDataSet.ReadXml(xmlStream)
                xmlStream.Close()
                Return xmlDataSet.Tables(tableName)
            End Function

            Private Function GetData() As DataTable
                Return LoadDataTableFromXml("GoogleStockData.xml", "StockPrice")
            End Function
        End Class

        Private dataSourceField As List(Of List(Of DataPoint))

        Private dataSource2Field As List(Of List(Of DataPoint))

        Private pieDataSourceField As List(Of List(Of DataPoint))

        Private nestedDonutDataSourceField As List(Of List(Of DataPoint))

        Private barDataSourceField As List(Of List(Of DataPoint))

        Private barDataSource2Field As List(Of List(Of DataPoint))

        Private scatterDataSourceField As List(Of List(Of DataPoint))

        Private funnelDataSourceField As List(Of List(Of DataPoint))

        Private polarDataSourceField As List(Of List(Of PolarDataPoint))

        Private polarRangeDataSourceField As List(Of List(Of PolarDataPoint))

        Private bubbleDataSourceField As List(Of List(Of DataPoint))

        Private rangeDataSourceField As List(Of List(Of DataPoint))

        Private rangeDataSource2Field As List(Of List(Of DataPoint))

        Private financialDataSourceField As List(Of DataTable)

        Private boxPlotDataSourceField As List(Of List(Of BoxPlotDataPoint))

        Public ReadOnly Property DataSource As List(Of List(Of DataPoint))
            Get
                Return If(dataSourceField, Function()
                    dataSourceField = CreateDataSource()
                    Return dataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property DataSource2 As List(Of List(Of DataPoint))
            Get
                Return If(dataSource2Field, Function()
                    dataSource2Field = DataSource.Take(1).ToList()
                    Return dataSource2Field
                End Function())
            End Get
        End Property

        Public ReadOnly Property PieDataSource As List(Of List(Of DataPoint))
            Get
                Return If(pieDataSourceField, Function()
                    pieDataSourceField = CreatePieDataSource().ToList()
                    Return pieDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property NestedDonutDataSource As List(Of List(Of DataPoint))
            Get
                Return If(nestedDonutDataSourceField, Function()
                    nestedDonutDataSourceField = CreatePieDataSource().Concat(CreatePieDataSource()).ToList()
                    Return nestedDonutDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property BarDataSource As List(Of List(Of DataPoint))
            Get
                Return If(barDataSourceField, Function()
                    barDataSourceField = CreateBarDataSource()
                    Return barDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property BarDataSource2 As List(Of List(Of DataPoint))
            Get
                Return If(barDataSource2Field, Function()
                    barDataSource2Field = BarDataSource.Take(3).ToList()
                    Return barDataSource2Field
                End Function())
            End Get
        End Property

        Public ReadOnly Property ScatterDataSource As List(Of List(Of DataPoint))
            Get
                Return If(scatterDataSourceField, Function()
                    scatterDataSourceField = CreateScatterDataSource().ToList()
                    Return scatterDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property FunnelDataSource As List(Of List(Of DataPoint))
            Get
                Return If(funnelDataSourceField, Function()
                    funnelDataSourceField = CreateFunnelDataSource().ToList()
                    Return funnelDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property PolarDataSource As List(Of List(Of PolarDataPoint))
            Get
                Return If(polarDataSourceField, Function()
                    polarDataSourceField = CreatePolarDataSource().ToList()
                    Return polarDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property PolarRangeDataSource As List(Of List(Of PolarDataPoint))
            Get
                Return If(polarRangeDataSourceField, Function()
                    polarRangeDataSourceField = CreatePolarRangeDataSource().ToList()
                    Return polarRangeDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property BubbleDataSource As List(Of List(Of DataPoint))
            Get
                Return If(bubbleDataSourceField, Function()
                    bubbleDataSourceField = CreateBubbleDataSource().ToList()
                    Return bubbleDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property RangeDataSource As List(Of List(Of DataPoint))
            Get
                Return If(rangeDataSourceField, Function()
                    rangeDataSourceField = CreateRangeDataSource().ToList()
                    Return rangeDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property RangeDataSource2 As List(Of List(Of DataPoint))
            Get
                Return If(rangeDataSource2Field, Function()
                    rangeDataSource2Field = RangeDataSource.Take(1).ToList()
                    Return rangeDataSource2Field
                End Function())
            End Get
        End Property

        Public ReadOnly Property FinancialDataSource As List(Of DataTable)
            Get
                Return If(financialDataSourceField, Function()
                    financialDataSourceField = CreateFinancialDataSource().ToList()
                    Return financialDataSourceField
                End Function())
            End Get
        End Property

        Public ReadOnly Property BoxPlotDataSource As List(Of List(Of BoxPlotDataPoint))
            Get
                Return If(boxPlotDataSourceField, Function()
                    boxPlotDataSourceField = CreateBoxPlotDataSource()
                    Return boxPlotDataSourceField
                End Function())
            End Get
        End Property

        Private Function CreateGroups(ByVal arguments As String(), ByVal groups As List(Of Double())) As List(Of List(Of DataPoint))
            Return groups.[Select](Function(x) x.Zip(arguments, Function(value, argument) New DataPoint(argument:=argument, value:=value)).ToList()).ToList()
        End Function

        Private Function CreateDataSource() As List(Of List(Of DataPoint))
            Dim args = {"A", "B", "C", "D", "E", "F"}
            Dim group0 = New Double() {15, 13, 7, 5, 23, 21}
            Dim group1 = New Double() {8, 12, 4, 9, 15, 19}
            Dim group2 = New Double() {3, 10, 6, 6, 8, 10}
            Return CreateGroups(args, New List(Of Double()) From {group0, group1, group2})
        End Function

        Private Iterator Function CreatePieDataSource() As IEnumerable(Of List(Of DataPoint))
            Dim dataSource = New List(Of DataPoint)()
            Dim random = New Random(0)
            For i As Integer = 0 To 16 - 1
                dataSource.Add(New DataPoint("1", random.NextDouble() * 3 + 1))
            Next

            Yield dataSource
        End Function

        Private Function CreateBarDataSource() As List(Of List(Of DataPoint))
            Dim dataSource = New List(Of DataPoint)()
            Dim args = {"A", "B", "C", "D", "E", "F"}
            Dim group0 = New Double() {1, 2, 5, -2, -2.1, -2.4}
            Dim group1 = New Double() {3, 10, 6, -3, -3.2, -3.8}
            Dim group2 = New Double() {8, 12, 7, -1.5, -1, -0.7}
            Dim group3 = New Double() {15, 13, 4, -1.3, -0.6, -4}
            Return CreateGroups(args, New List(Of Double()) From {group0, group1, group2, group3})
        End Function

        Private Iterator Function CreateScatterDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {New DataPoint("A", 15), New DataPoint("B", 11), New DataPoint("C", 7), New DataPoint("D", 9), New DataPoint("C", 23), New DataPoint("B", 21)}
        End Function

        Private Iterator Function CreateFunnelDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {New DataPoint("Visited a Website", 9152), New DataPoint("Downloaded a Trial", 6870), New DataPoint("Contacted to Support", 5121), New DataPoint("Subscribed", 2224), New DataPoint("Renewed", 1670)}
        End Function

        Private Iterator Function CreatePolarDataSource() As IEnumerable(Of List(Of PolarDataPoint))
            Dim random = New Random(0)
            Yield Enumerable.Range(0, 7).[Select](Function(x) New PolarDataPoint(Math.Floor(random.NextDouble() * 360), Math.Floor(random.NextDouble() * 25))).ToList()
        End Function

        Private Iterator Function CreatePolarRangeDataSource() As IEnumerable(Of List(Of PolarDataPoint))
            Dim random = New Random(0)
            Dim pointsCount = 6
            Yield Enumerable.Range(0, 7).[Select](Function(x) New PolarDataPoint(Math.Floor(x / CDbl(pointsCount) * 360), Math.Floor(random.NextDouble() * 10 + 30), Math.Floor(random.NextDouble() * 10 + 10))).ToList()
        End Function

        Private Iterator Function CreateBubbleDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {New DataPoint("A", value:=10, weight:=5.9), New DataPoint("B", value:=5, weight:=4.9), New DataPoint("C", value:=2, weight:=4.6), New DataPoint("D", value:=5, weight:=3), New DataPoint("E", value:=2, weight:=2.9), New DataPoint("F", value:=4, weight:=2.8), New DataPoint("G", value:=2, weight:=2.6), New DataPoint("H", value:=3, weight:=2.5), New DataPoint("I", value:=4, weight:=2.4)}
        End Function

        Private Function CreateBoxPlotDataSource() As List(Of List(Of BoxPlotDataPoint))
            Dim result As List(Of BoxPlotDataPoint) = New List(Of BoxPlotDataPoint)()
            Dim value As Double = 20
            Dim rnd As Random = New Random(1)
            Dim argChar As Char = "A"c
            For i As Integer = 0 To 10 - 1
                argChar = Microsoft.VisualBasic.ChrW(Microsoft.VisualBasic.AscW(argChar) + 1)
                Dim arg As String = argChar.ToString()
                result.Add(GenerateBoxPlotDataPoint(arg, value, rnd))
            Next

            Return New List(Of List(Of BoxPlotDataPoint))() From {result}
        End Function

        Private Function GenerateBoxPlotDataPoint(ByVal arg As String, ByRef value As Double, ByVal rnd As Random) As BoxPlotDataPoint
            Dim dataPoint As BoxPlotDataPoint = New BoxPlotDataPoint()
            dataPoint.Argument = arg
            value += rnd.Next(-5, 6)
            dataPoint.Min = value
            dataPoint.Quartile1 = value + rnd.Next(1, 5)
            dataPoint.Median = dataPoint.Quartile1 + rnd.Next(4, 8)
            dataPoint.Mean = dataPoint.Median + rnd.Next(-3, 3)
            dataPoint.Quartile3 = dataPoint.Mean + rnd.Next(4, 9)
            dataPoint.Max = dataPoint.Quartile3 + rnd.Next(1, 5)
            dataPoint.Outliers = New Double(2) {}
            dataPoint.Outliers(0) = dataPoint.Min - rnd.Next(1, 3)
            dataPoint.Outliers(1) = dataPoint.Max + rnd.Next(1, 3)
            dataPoint.Outliers(2) = dataPoint.Max + rnd.Next(4, 10)
            Return dataPoint
        End Function

        Private Iterator Function CreateRangeDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {New DataPoint("A", 3, 13), New DataPoint("B", 5, 8), New DataPoint("C", 2, 9), New DataPoint("D", -2, -8), New DataPoint("E", -1, -6), New DataPoint("F", -3, -7)}
            Yield New List(Of DataPoint) From {New DataPoint("A", 5, 15), New DataPoint("B", 3, 11), New DataPoint("C", 6, 11), New DataPoint("D", -1, -9), New DataPoint("E", -3, -9), New DataPoint("F", -2, -6)}
        End Function

        Private Iterator Function CreateFinancialDataSource() As IEnumerable(Of DataTable)
            Yield New GoogleStockData().ShortData
        End Function
    End Class

    Public Class DataPoint

        Public Property Argument As String

        Public Property Value As Double

        Public Property Value2 As Double

        Public Property Weight As Double

        Public Sub New(ByVal argument As String, ByVal value As Double, ByVal Optional value2 As Double = 0, ByVal Optional weight As Double = 0)
            Me.Argument = argument
            Me.Value = value
            Me.Value2 = value2
            Me.Weight = weight
        End Sub
    End Class

    Public Class PolarDataPoint

        Public Property Argument As Double

        Public Property Value As Double

        Public Property Value2 As Double

        Public Sub New(ByVal argument As Double, ByVal value As Double, ByVal Optional value2 As Double = 0)
            Me.Argument = argument
            Me.Value = value
            Me.Value2 = value2
        End Sub
    End Class

    Public Class BoxPlotDataPoint

        Public Property Argument As String

        Public Property Min As Double

        Public Property Quartile1 As Double

        Public Property Median As Double

        Public Property Quartile3 As Double

        Public Property Max As Double

        Public Property Mean As Double

        Public Property Outliers As Double()
    End Class
End Namespace
