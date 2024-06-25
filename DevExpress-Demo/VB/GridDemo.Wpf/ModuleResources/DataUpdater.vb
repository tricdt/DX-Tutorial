Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Threading

Namespace GridDemo

#Region "DataUpdaterBase"
    Public MustInherit Class DataUpdaterBase

        Private _NeedStop As Boolean

        Private ReadOnly rndRow As Random = New Random()

        Private ReadOnly list As IList(Of MarketData)

        Private changeCount As Integer = 0

        Protected Property NeedStop As Boolean
            Get
                Return _NeedStop
            End Get

            Private Set(ByVal value As Boolean)
                _NeedStop = value
            End Set
        End Property

        Public Sub New(ByVal list As IList(Of MarketData))
            Me.list = list
        End Sub

        Public Function GetAndResetChangesCount() As Integer
            Dim result = changeCount
            changeCount = 0
            Return result
        End Function

        Public Sub [Stop]()
            NeedStop = True
        End Sub

        Public MustOverride Sub SetLoad(ByVal loadValue As Integer, ByVal maxLoadValue As Integer)

        Protected Sub RandomlyUpdateOneRow()
            Dim row As Integer = rndRow.Next(0, list.Count)
            list(row).Update()
            changeCount += 1
        End Sub
    End Class

#End Region
#Region "RealTimeDataUpdater"
    Public Class RealTimeDataUpdater
        Inherits DataUpdaterBase

        Public Sub New(ByVal list As IList(Of MarketData))
            MyBase.New(list)
            Threading.Tasks.Task.Factory.StartNew(New Action(AddressOf UpdateData), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)
        End Sub

        Private Sub UpdateData()
            Dim watch As Stopwatch = Stopwatch.StartNew()
            Do
                watch.Restart()
                RandomlyUpdateOneRow()
                Dim sleep As Integer = CInt(Math.Floor(1000.0 * Convert.ToDouble((interEventDelay - watch.ElapsedTicks) / Stopwatch.Frequency)))
                If sleep > 0 Then Thread.Sleep(sleep)
                While interEventDelay - watch.ElapsedTicks > 0
                    Thread.Sleep(0)
                End While

                watch.Stop()
            Loop While Not NeedStop
        End Sub

        Private interEventDelay As Integer

        Public Overrides Sub SetLoad(ByVal loadValue As Integer, ByVal maxLoadValue As Integer)
            Dim pow As Double =(maxLoadValue - loadValue + 3) * 0.5
            interEventDelay = CInt(Math.Pow(2.0, pow))
        End Sub
    End Class

#End Region
#Region "BackgroundDataUpdater"
    Public Class BackgroundDataUpdater
        Inherits DataUpdaterBase

        Private timer As DispatcherTimer

        Public Sub New(ByVal list As IList(Of MarketData))
            MyBase.New(list)
            timer = New DispatcherTimer(DispatcherPriority.Background)
            AddHandler timer.Tick, AddressOf OnTimerTick
            timer.Interval = TimeSpan.FromMilliseconds(0)
            timer.Start()
        End Sub

        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
            If NeedStop Then
                timer.Stop()
                Return
            End If

            For i As Integer = 0 To 2 - 1
                RandomlyUpdateOneRow()
            Next
        End Sub

        Public Overrides Sub SetLoad(ByVal loadValue As Integer, ByVal maxLoadValue As Integer)
            timer.Interval = TimeSpan.FromMilliseconds((maxLoadValue - loadValue) * 2)
        End Sub
    End Class
#End Region
End Namespace
