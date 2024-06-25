Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Threading

Namespace ChartsDemo

    Friend Class SensorDataGenerator

        Const DataGenerationIntervalMilliseconds As Integer = 15

        Const InitialDataPointsCount As Integer = 10000

        Private ReadOnly buffer As System.Collections.Generic.List(Of ChartsDemo.SensorIndicationItem) = New System.Collections.Generic.List(Of ChartsDemo.SensorIndicationItem)()

        Private counter As Integer

        Private ReadOnly dataSourceField As ChartsDemo.RealTimeDataCollection = New ChartsDemo.RealTimeDataCollection()

        Private generatingEnabled As Boolean = False

        Private generatingThread As System.Threading.Thread

        Private ReadOnly random As System.Random = New System.Random(1)

        Private ReadOnly sync As Object = New Object()

        Private yAddition1 As Double = 0

        Private yAddition2 As Double = 0

        Private yAddition3 As Double = 0

        Private yAddition4 As Double = 0

        Private yAddition5 As Double = 0

        Private yAddition6 As Double = 0

        Private yAddition7 As Double = 0

        Private yAddition8 As Double = 0

        Private Sub AddPoint(ByVal timeStamp As System.DateTime)
            Dim point As ChartsDemo.SensorIndicationItem = Me.CreatePoint(timeStamp)
            SyncLock Me.sync
                Me.buffer.Add(point)
            End SyncLock
        End Sub

        Private Function CreatePoint(ByVal timeStamp As System.DateTime) As SensorIndicationItem
            Me.counter += 1
            Dim arg As Double = timeStamp.ToOADate()
            arg = arg * 250000R
            If Me.counter Mod Me.random.[Next](300, 500) = 0 Then Me.yAddition1 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition1 < -30 Then Me.yAddition1 += 10
            If Me.yAddition1 > 30 Then Me.yAddition1 -= 10
            Dim indication1 As Double = 5 * System.Math.Sin(5R / 2R * System.Math.Cos(arg)) + 100 + (Me.random.NextDouble() - 0.5) * 5 + Me.yAddition1
            If Me.counter Mod Me.random.[Next](100, 300) = 0 Then Me.yAddition2 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition2 < -30 Then Me.yAddition2 += 10
            If Me.yAddition2 > 30 Then Me.yAddition2 -= 10
            Dim indication2 As Double = 4 * System.Math.Sin(7 * System.Math.Cos(arg - 1.5)) + 90 + (Me.random.NextDouble() - 0.5) * 7 + Me.yAddition2
            If Me.counter Mod Me.random.[Next](100, 500) = 0 Then Me.yAddition3 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition3 < -30 Then Me.yAddition3 += 10
            If Me.yAddition3 > 30 Then Me.yAddition3 -= 10
            Dim indication3 As Double = 10 * (System.Math.Sin(arg) + System.Math.Sin(arg / 1.2R) + System.Math.Sin(arg / 1.5R)) + 100 + Me.random.NextDouble() * 12 + Me.yAddition3
            If Me.counter Mod Me.random.[Next](50, 100) = 0 Then Me.yAddition4 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition4 < -30 Then Me.yAddition4 += 10
            If Me.yAddition4 > 30 Then Me.yAddition4 -= 10
            Dim indication4 As Double = 10 * (System.Math.Cos(arg + 1.5) + System.Math.Sin(arg / 1.2R + 0.5) + System.Math.Cos(arg / 1.5R + 0.3)) + 120 + Me.random.NextDouble() * 15 + Me.yAddition4
            If Me.counter Mod Me.random.[Next](300, 400) = 0 Then Me.yAddition5 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition5 < -30 Then Me.yAddition5 += 10
            If Me.yAddition5 > 30 Then Me.yAddition5 -= 10
            Dim indication5 As Double = 15 * System.Math.Cos(System.Math.Tan(arg + Me.random.NextDouble() / 10)) + 500 + Me.random.NextDouble() * 15 + Me.yAddition5
            If Me.counter Mod Me.random.[Next](400, 1000) = 0 Then Me.yAddition6 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition6 < -30 Then Me.yAddition6 += 10
            If Me.yAddition6 > 30 Then Me.yAddition6 -= 10
            Dim indication6 As Double = 20 * System.Math.Sin(System.Math.Tan(arg + 1)) + 450 + Me.random.NextDouble() * 9 + Me.yAddition6
            If Me.counter Mod Me.random.[Next](200, 300) = 0 Then Me.yAddition7 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition7 < -30 Then Me.yAddition7 += 10
            If Me.yAddition7 > 30 Then Me.yAddition7 -= 10
            Dim indication7 As Double = 30 * System.Math.Abs(System.Math.Sin(System.Math.Tan(arg + 1))) + System.Math.Cos(arg) + System.Math.Sin(arg) + 750 + Me.random.NextDouble() * 15 + Me.yAddition7
            If Me.counter Mod Me.random.[Next](300, 350) = 0 Then Me.yAddition8 += Me.random.[Next](10, 20) * System.Math.Sign(Me.random.NextDouble() - 0.5)
            If Me.yAddition8 < -30 Then Me.yAddition8 += 10
            If Me.yAddition8 > 30 Then Me.yAddition8 -= 10
            Dim indication8 As Double = 30 * (1 - System.Math.Cos(arg)) / Me.random.[Next](1, 5) + 700 + Me.random.NextDouble() * 15 + Me.yAddition8
            Return New ChartsDemo.SensorIndicationItem(timeStamp, indication1, indication2, indication3, indication4, indication5, indication6, indication7, indication8)
        End Function

        Private Sub GeneratingLoop()
            Dim timeStamp As System.DateTime = System.DateTime.Now
            While Me.generatingEnabled
                Dim newTimeStamp As System.DateTime = timeStamp.AddMilliseconds(ChartsDemo.SensorDataGenerator.DataGenerationIntervalMilliseconds)
                Dim span As System.TimeSpan = newTimeStamp - System.DateTime.Now
                If span.Ticks > 0 Then Call System.Threading.Thread.Sleep(CInt(span.TotalMilliseconds))
                timeStamp = newTimeStamp
                Me.AddPoint(timeStamp)
            End While
        End Sub

        Friend Sub GenerateInitialData()
            Dim baseTimeStamp As System.DateTime = System.DateTime.Now.AddMilliseconds(-ChartsDemo.SensorDataGenerator.InitialDataPointsCount * ChartsDemo.SensorDataGenerator.DataGenerationIntervalMilliseconds)
            Dim argument As System.DateTime = baseTimeStamp
            For i As Integer = 0 To ChartsDemo.SensorDataGenerator.InitialDataPointsCount - 1 - 1
                argument = argument.AddMilliseconds(ChartsDemo.SensorDataGenerator.DataGenerationIntervalMilliseconds)
                Dim point As ChartsDemo.SensorIndicationItem = Me.CreatePoint(argument)
                Me.dataSourceField.Add(point)
            Next
        End Sub

        Friend Sub Start()
            If Me.generatingThread Is Nothing Then Me.generatingThread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf Me.GeneratingLoop))
            Me.generatingEnabled = True
            Me.generatingThread.Start()
        End Sub

        Friend Sub [Stop]()
            Me.generatingEnabled = False
            If Me.generatingThread IsNot Nothing Then Me.generatingThread.Join()
            Me.generatingThread = Nothing
        End Sub

        Friend Sub UpdateDataSource()
            SyncLock Me.sync
                Me.dataSourceField.AddRange(Me.buffer)
                If Me.dataSourceField.Count > ChartsDemo.SensorDataGenerator.InitialDataPointsCount Then Me.dataSourceField.RemoveRangeAt(0, Me.buffer.Count)
                Me.buffer.Clear()
            End SyncLock
        End Sub

        Public ReadOnly Property DataSource As RealTimeDataCollection
            Get
                Return Me.dataSourceField
            End Get
        End Property
    End Class

    Public Class SensorIndicationItem

        Private _SensorIndication1 As Double, _SensorIndication2 As Double, _SensorIndication3 As Double, _SensorIndication4 As Double, _SensorIndication5 As Double, _SensorIndication6 As Double, _SensorIndication7 As Double, _SensorIndication8 As Double, _TimeStamp As DateTime

        Friend Sub New(ByVal timeStamp As System.DateTime, ByVal sensorIndication1 As Double, ByVal sensorIndication2 As Double, ByVal sensorIndication3 As Double, ByVal sensorIndication4 As Double, ByVal sensorIndication5 As Double, ByVal sensorIndication6 As Double, ByVal sensorIndication7 As Double, ByVal sensorIndication8 As Double)
            Me.TimeStamp = timeStamp
            Me.SensorIndication1 = sensorIndication1
            Me.SensorIndication2 = sensorIndication2
            Me.SensorIndication3 = sensorIndication3
            Me.SensorIndication4 = sensorIndication4
            Me.SensorIndication5 = sensorIndication5
            Me.SensorIndication6 = sensorIndication6
            Me.SensorIndication7 = sensorIndication7
            Me.SensorIndication8 = sensorIndication8
        End Sub

        Public Property SensorIndication1 As Double
            Get
                Return _SensorIndication1
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication1 = value
            End Set
        End Property

        Public Property SensorIndication2 As Double
            Get
                Return _SensorIndication2
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication2 = value
            End Set
        End Property

        Public Property SensorIndication3 As Double
            Get
                Return _SensorIndication3
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication3 = value
            End Set
        End Property

        Public Property SensorIndication4 As Double
            Get
                Return _SensorIndication4
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication4 = value
            End Set
        End Property

        Public Property SensorIndication5 As Double
            Get
                Return _SensorIndication5
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication5 = value
            End Set
        End Property

        Public Property SensorIndication6 As Double
            Get
                Return _SensorIndication6
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication6 = value
            End Set
        End Property

        Public Property SensorIndication7 As Double
            Get
                Return _SensorIndication7
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication7 = value
            End Set
        End Property

        Public Property SensorIndication8 As Double
            Get
                Return _SensorIndication8
            End Get

            Private Set(ByVal value As Double)
                _SensorIndication8 = value
            End Set
        End Property

        Public Property TimeStamp As DateTime
            Get
                Return _TimeStamp
            End Get

            Private Set(ByVal value As DateTime)
                _TimeStamp = value
            End Set
        End Property
    End Class

    Public Class RealTimeDataCollection
        Inherits System.Collections.ObjectModel.ObservableCollection(Of ChartsDemo.SensorIndicationItem)

        Public Sub AddRange(ByVal items As System.Collections.Generic.IList(Of ChartsDemo.SensorIndicationItem))
            For Each item As ChartsDemo.SensorIndicationItem In items
                Me.Items.Add(item)
            Next

            Me.OnCollectionChanged(New System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Add, CType(items, System.Collections.IList), Me.Items.Count - items.Count))
        End Sub

        Public Sub RemoveRangeAt(ByVal startingIndex As Integer, ByVal count As Integer)
            Dim removedItems = New System.Collections.Generic.List(Of ChartsDemo.SensorIndicationItem)(count)
            For i As Integer = 0 To count - 1
                removedItems.Add(Me.Items(startingIndex))
                Me.Items.RemoveAt(startingIndex)
            Next

            If count > 0 Then Me.OnCollectionChanged(New System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Remove, removedItems, startingIndex))
        End Sub
    End Class
End Namespace
