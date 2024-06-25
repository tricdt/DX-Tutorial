Imports System
Imports System.Windows.Threading

Namespace GaugesDemo

    Public Class CarDashboardViewModel

        Const NormalEngineTemperature As Double = 85

        Const GearCount As Integer = 6

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Private ReadOnly timerInitialAnimation As DispatcherTimer = New DispatcherTimer()

        Private ReadOnly timerUpdateDateTime As DispatcherTimer = New DispatcherTimer()

        Private ReadOnly Property GearInteval As Double
            Get
                Return 0.8 * (MaxSpeed / GearCount)
            End Get
        End Property

        Public Overridable Property IsAcceleratePressed As Boolean

        Public Overridable Property IsBrakePressed As Boolean

        Public Overridable Property Speed As Double

        Public Overridable Property CurrentEngineTemperature As Double

        Public Overridable Property TachometerValue As Double

        Public Overridable Property FuelLevel As Double

        Public Overridable Property CurrentDateTime As Date

        Public ReadOnly Property TachometerMaxValue As Double
            Get
                Return 8000
            End Get
        End Property

        Public ReadOnly Property MaxEngineTemperature As Double
            Get
                Return 130
            End Get
        End Property

        Public ReadOnly Property MaxSpeed As Double
            Get
                Return 120.0
            End Get
        End Property

        Public Sub New()
            timer.Interval = TimeSpan.FromMilliseconds(500)
            timerInitialAnimation.Interval = TimeSpan.FromMilliseconds(800)
            timerUpdateDateTime.Interval = TimeSpan.FromSeconds(1)
            CurrentEngineTemperature = 20
            TachometerValue = 900
            FuelLevel = 0.75
            UpdateDateTime()
        End Sub

        Private Sub OnTimedEventInitialAnimation(ByVal source As Object, ByVal e As EventArgs)
            timerInitialAnimation.Stop()
            Speed = 0
            TachometerValue = 0
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Start()
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            Update(timer.Interval.TotalSeconds)
        End Sub

        Private Sub Update(ByVal deltaTime As Double)
            UpdateSpeed(10 * deltaTime)
            Dim gear As Integer = CInt(Math.Min(GearCount, Math.Ceiling(Speed / GearInteval)))
            TachometerValue = If(gear > 0, TachometerMaxValue * (Speed - GearInteval * (gear - 1)) / GearInteval, 0)
            TachometerValue = Math.Max(0, Math.Min(TachometerMaxValue, TachometerValue))
            FuelLevel -= TachometerValue / TachometerMaxValue / 1000
            If TachometerMaxValue / 2 < TachometerValue OrElse CurrentEngineTemperature < NormalEngineTemperature Then
                CurrentEngineTemperature += TachometerValue / TachometerMaxValue / 2.5
            Else
                CurrentEngineTemperature -=(TachometerMaxValue - TachometerValue) / TachometerMaxValue / 2.5
            End If

            CurrentEngineTemperature = Math.Min(MaxEngineTemperature, CurrentEngineTemperature)
        End Sub

        Private Sub UpdateSpeed(ByVal delta As Double)
            If IsAcceleratePressed Then
                Speed += delta
            ElseIf IsBrakePressed Then
                Speed -= delta
            End If

            Speed = Math.Max(0, Math.Min(MaxSpeed, Speed))
        End Sub

        Private Sub UpdateDateTime()
            CurrentDateTime = Date.Now
        End Sub

        Private Sub OnTimedEventDateTime(ByVal source As Object, ByVal e As EventArgs)
            UpdateDateTime()
        End Sub

        Public Sub Start()
            AddHandler timerUpdateDateTime.Tick, AddressOf OnTimedEventDateTime
            timerUpdateDateTime.Start()
            Speed = MaxSpeed
            TachometerValue = TachometerMaxValue
            AddHandler timerInitialAnimation.Tick, AddressOf OnTimedEventInitialAnimation
            timerInitialAnimation.Start()
        End Sub

        Public Sub [Stop]()
            timerUpdateDateTime.Stop()
            RemoveHandler timerUpdateDateTime.Tick, AddressOf OnTimedEventDateTime
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTimedEvent
            timerInitialAnimation.Stop()
            RemoveHandler timerInitialAnimation.Tick, AddressOf OnTimedEventInitialAnimation
        End Sub
    End Class
End Namespace
