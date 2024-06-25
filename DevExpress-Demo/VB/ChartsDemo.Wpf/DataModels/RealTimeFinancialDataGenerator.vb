Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Windows
Imports System.Windows.Resources

Namespace ChartsDemo

    Friend Class RealTimeFinancialDataGenerator

        Const MinPrice As Double = 50

        Const InitialDataPointsCount As Integer = 18000

        Const MaxPointsCount As Integer = InitialDataPointsCount

        Const PeriodMilliseconds As Integer = 41

        Private ReadOnly dataSourceField As ObservableCollection(Of FinancialDataPoint) = New ObservableCollection(Of FinancialDataPoint)()

        Private ReadOnly random As Random = New Random(3)

        Private ReadOnly buffer As List(Of FinancialDataPoint) = New List(Of FinancialDataPoint)()

        Private ReadOnly bufferSync As Object = New Object()

        Private prevPoint As FinancialDataPoint

        Private generatingEnabled As Boolean = False

        Private generatingThread As Thread

        Private prevMinute As Integer = -1

        Private volumeIndex As Integer = 0

        Private currentAggregatingPoint As FinancialDataPoint

        Public ReadOnly Property DataSource As ObservableCollection(Of FinancialDataPoint)
            Get
                Return dataSourceField
            End Get
        End Property

        Public ReadOnly Property LastArgument As Date
            Get
                Return prevPoint.DateTimeStamp
            End Get
        End Property

        Private Function GeneratePoint(ByVal argument As Date, ByVal locPrevPoint As FinancialDataPoint, ByVal volume As Double) As FinancialDataPoint
            Dim priceDelta As Double =(random.NextDouble() - 0.5) / 1000
            Dim close As Double = locPrevPoint.Close + priceDelta
            If close <= MinPrice Then close = 2 * MinPrice - close
            Dim open As Double = locPrevPoint.Close
            Dim high As Double = Math.Max(open, close) + (random.NextDouble()) / 4000R
            Dim low As Double = Math.Min(open, close) - (random.NextDouble()) / 4000R
            Return New FinancialDataPoint(argument, open, high, low, close, volume)
        End Function

        Private Sub GeneratingLoop()
            Dim timeStamp As Date = Date.Now
            While generatingEnabled
                Dim newTimeStamp As Date = timeStamp.AddMilliseconds(PeriodMilliseconds)
                Dim span As TimeSpan = newTimeStamp - Date.Now
                If span.Ticks > 0 Then Call Thread.Sleep(CInt(span.TotalMilliseconds))
                timeStamp = newTimeStamp
                AddPoint(timeStamp)
            End While
        End Sub

        Private Sub AddPoint(ByVal timeStamp As Date)
            If volumeIndex > dataSourceField.Count - 2 Then volumeIndex = -1
            If timeStamp.Minute <> prevMinute Then
                volumeIndex += 1
                prevMinute = timeStamp.Minute
            End If

            Dim point As FinancialDataPoint = GeneratePoint(timeStamp, prevPoint, dataSourceField(volumeIndex).Volume / 2000R)
            If currentAggregatingPoint.DateTimeStamp.Minute = timeStamp.Minute Then
                currentAggregatingPoint.Close = point.Close
                currentAggregatingPoint.High = Math.Max(currentAggregatingPoint.High, point.High)
                currentAggregatingPoint.Low = Math.Min(currentAggregatingPoint.Low, point.Low)
                currentAggregatingPoint.Volume += point.Volume
                SyncLock bufferSync
                    If buffer.Count > 0 Then
                        buffer(buffer.Count - 1) = currentAggregatingPoint
                    Else
                        buffer.Add(currentAggregatingPoint)
                    End If

                End SyncLock
            Else
                SyncLock bufferSync
                    currentAggregatingPoint = point
                    buffer.Add(point)
                    If buffer.Count > MaxPointsCount Then buffer.RemoveAt(0)
                End SyncLock
            End If

            prevPoint = point
        End Sub

        Private Function TheSameMinute(ByVal dt1 As Date, ByVal dt2 As Date) As Boolean
            Return(dt1 - Date.MinValue).TotalMinutes = (dt2 - Date.MinValue).TotalMinutes
        End Function

        Friend Sub ReadInitialData()
            Dim stamp As Date = Date.Now
            Dim uri As Uri = New Uri("/ChartsDemo;component/Data/InitialFinData.csv", UriKind.RelativeOrAbsolute)
            Dim info As StreamResourceInfo = Application.GetResourceStream(uri)
            Dim reader As StreamReader = New StreamReader(info.Stream)
            For i As Integer = 0 To InitialDataPointsCount - 1
                If Not reader.EndOfStream Then
                    Dim line As String = reader.ReadLine()
                    Dim values As String() = line.Split(","c)
                    Dim point As FinancialDataPoint = New FinancialDataPoint()
                    stamp = stamp.AddMinutes(-1)
                    If stamp.DayOfWeek = DayOfWeek.Sunday Then stamp = stamp.AddDays(-2)
                    point.DateTimeStamp = stamp
                    point.Open = Double.Parse(values(0), CultureInfo.InvariantCulture)
                    point.High = Double.Parse(values(1), CultureInfo.InvariantCulture)
                    point.Low = Double.Parse(values(2), CultureInfo.InvariantCulture)
                    point.Close = Double.Parse(values(3), CultureInfo.InvariantCulture)
                    point.Volume = Double.Parse(values(4), CultureInfo.InvariantCulture)
                    dataSourceField.Insert(0, point)
                End If
            Next

            prevPoint = dataSourceField.Last()
            currentAggregatingPoint = prevPoint
        End Sub

        Friend Sub UpdateDataSource()
            Dim tempBuffer As List(Of FinancialDataPoint)
            SyncLock bufferSync
                tempBuffer = New List(Of FinancialDataPoint)(buffer)
                buffer.Clear()
            End SyncLock

            If tempBuffer.Count = 0 Then Return
            If TheSameMinute(tempBuffer(0).DateTimeStamp, dataSourceField(dataSourceField.Count - 1).DateTimeStamp) Then
                dataSourceField(dataSourceField.Count - 1) = tempBuffer(0)
            Else
                dataSourceField.Add(tempBuffer(0))
            End If

            If tempBuffer.Count > 1 Then
                For i As Integer = 1 To tempBuffer.Count - 1
                    dataSourceField.Add(tempBuffer(i))
                Next
            End If

            Dim overflow As Integer = dataSourceField.Count - MaxPointsCount
            For i As Integer = 0 To overflow - 1
                dataSourceField.RemoveAt(0)
            Next
        End Sub

        Friend Sub Start()
            If generatingThread Is Nothing Then generatingThread = New Thread(New ThreadStart(AddressOf GeneratingLoop))
            generatingEnabled = True
            generatingThread.Start()
        End Sub

        Public Sub [Stop]()
            generatingEnabled = False
            If generatingThread IsNot Nothing Then generatingThread.Join()
            generatingThread = Nothing
        End Sub
    End Class
End Namespace
