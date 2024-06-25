Imports System
Imports System.Windows.Threading
Imports DevExpress.Mvvm

Namespace GaugesDemo

    Public Class GaugeRandomDataGenerator
        Inherits BindableBase

        Public Property NeedleValue As Double
            Get
                Return GetProperty(Function() Me.NeedleValue)
            End Get

            Set(ByVal value As Double)
                SetProperty(Function() NeedleValue, value)
            End Set
        End Property

        Public Property RangeBarValue As Double
            Get
                Return GetProperty(Function() Me.RangeBarValue)
            End Get

            Set(ByVal value As Double)
                SetProperty(Function() RangeBarValue, value)
            End Set
        End Property

        Public Property MarkerValue As Double
            Get
                Return GetProperty(Function() Me.MarkerValue)
            End Get

            Set(ByVal value As Double)
                SetProperty(Function() MarkerValue, value)
            End Set
        End Property

        Public Property LevelBarValue As Double
            Get
                Return GetProperty(Function() Me.LevelBarValue)
            End Get

            Set(ByVal value As Double)
                SetProperty(Function() LevelBarValue, value)
            End Set
        End Property

        Public Property MinValue As Double

        Public Property MaxValue As Double

        Public Property UpdateInterval As Double
            Get
                Return updateTimer.Interval.TotalMilliseconds
            End Get

            Set(ByVal value As Double)
                updateTimer.Interval = TimeSpan.FromMilliseconds(value)
            End Set
        End Property

        Private ReadOnly random As Random = New Random()

        Private ReadOnly updateTimer As DispatcherTimer = New DispatcherTimer()

        Private Function NextValue() As Double
            Return MinValue + (MaxValue - MinValue) * random.NextDouble()
        End Function

        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
            NeedleValue = NextValue()
            RangeBarValue = NextValue()
            MarkerValue = NextValue()
            LevelBarValue = NextValue()
        End Sub

        Public Sub Start()
            AddHandler updateTimer.Tick, AddressOf OnTimerTick
            updateTimer.Start()
        End Sub

        Public Sub [Stop]()
            updateTimer.Stop()
            RemoveHandler updateTimer.Tick, AddressOf OnTimerTick
        End Sub
    End Class
End Namespace
