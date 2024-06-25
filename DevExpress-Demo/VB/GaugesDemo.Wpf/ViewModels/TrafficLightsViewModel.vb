Imports System
Imports System.Windows.Threading

Namespace GaugesDemo

    Public Enum SegmentState
        Enabled
        Disabled
        Blinking
    End Enum

    Public Class TrafficLightsState

        Private _GreenSegment As SegmentState, _YellowSegment As SegmentState, _RedSegment As SegmentState, _ArrowSegment As SegmentState, _GangerRedSegment As SegmentState, _ResetTimerTicks As Integer?

        Public Shared Function Red(ByVal Optional resetTimerTicks As Integer? = Nothing) As TrafficLightsState
            Return New TrafficLightsState(AddressOf ArrowBlinking, redSegment:=SegmentState.Enabled, arrowSegment:=SegmentState.Enabled, gangerRedSegment:=SegmentState.Enabled, resetTimerTicks:=resetTimerTicks)
        End Function

        Public Shared Function ArrowBlinking() As TrafficLightsState
            Return New TrafficLightsState(AddressOf YellowRed, redSegment:=SegmentState.Enabled, arrowSegment:=SegmentState.Blinking, gangerRedSegment:=SegmentState.Enabled)
        End Function

        Public Shared Function YellowRed() As TrafficLightsState
            Return New TrafficLightsState(AddressOf Green, redSegment:=SegmentState.Enabled, yellowSegment:=SegmentState.Enabled, gangerRedSegment:=SegmentState.Enabled)
        End Function

        Public Shared Function Green() As TrafficLightsState
            Return New TrafficLightsState(AddressOf GreenBlinking, greenSegment:=SegmentState.Enabled, resetTimerTicks:=2)
        End Function

        Public Shared Function GreenBlinking() As TrafficLightsState
            Return New TrafficLightsState(AddressOf Yellow, greenSegment:=SegmentState.Blinking)
        End Function

        Public Shared Function Yellow() As TrafficLightsState
            Return New TrafficLightsState(Function() Red(), yellowSegment:=SegmentState.Enabled, gangerRedSegment:=SegmentState.Enabled, resetTimerTicks:=4)
        End Function

        Private ReadOnly [next] As Func(Of TrafficLightsState)

        Public Property GreenSegment As SegmentState
            Get
                Return _GreenSegment
            End Get

            Private Set(ByVal value As SegmentState)
                _GreenSegment = value
            End Set
        End Property

        Public Property YellowSegment As SegmentState
            Get
                Return _YellowSegment
            End Get

            Private Set(ByVal value As SegmentState)
                _YellowSegment = value
            End Set
        End Property

        Public Property RedSegment As SegmentState
            Get
                Return _RedSegment
            End Get

            Private Set(ByVal value As SegmentState)
                _RedSegment = value
            End Set
        End Property

        Public Property ArrowSegment As SegmentState
            Get
                Return _ArrowSegment
            End Get

            Private Set(ByVal value As SegmentState)
                _ArrowSegment = value
            End Set
        End Property

        Public Property GangerRedSegment As SegmentState
            Get
                Return _GangerRedSegment
            End Get

            Private Set(ByVal value As SegmentState)
                _GangerRedSegment = value
            End Set
        End Property

        Public Property ResetTimerTicks As Integer?
            Get
                Return _ResetTimerTicks
            End Get

            Private Set(ByVal value As Integer?)
                _ResetTimerTicks = value
            End Set
        End Property

        Private Sub New(ByVal [next] As Func(Of TrafficLightsState), ByVal Optional greenSegment As SegmentState = SegmentState.Disabled, ByVal Optional yellowSegment As SegmentState = SegmentState.Disabled, ByVal Optional redSegment As SegmentState = SegmentState.Disabled, ByVal Optional arrowSegment As SegmentState = SegmentState.Disabled, ByVal Optional gangerRedSegment As SegmentState = SegmentState.Disabled, ByVal Optional resetTimerTicks As Integer? = Nothing)
            Me.next = [next]
            Me.GreenSegment = greenSegment
            Me.YellowSegment = yellowSegment
            Me.RedSegment = redSegment
            Me.ArrowSegment = arrowSegment
            Me.GangerRedSegment = gangerRedSegment
            Me.ResetTimerTicks = resetTimerTicks
        End Sub

        Public Function NextState() As TrafficLightsState
            Return [next]()
        End Function
    End Class

    Public Class TrafficLightsViewModel

        Public Overridable Property WaitTime As Integer

        Public Overridable Property IsTimerGreen As Boolean

        Public Overridable Property IsRedSegmentEnabled As Boolean

        Public Overridable Property IsYellowSegmentEnabled As Boolean

        Public Overridable Property IsGreenLeftSegmentEnabled As Boolean

        Public Overridable Property IsGreenRightSegmentEnabled As Boolean

        Public Overridable Property IsGangerRedSegmentEnabled As Boolean

        Public Overridable Property IsGangerGreenSegmentEnabled As Boolean

        Const initialTicksCount As Integer = 3

        Private ReadOnly blinkingTimer As DispatcherTimer = New DispatcherTimer()

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Private currentState As TrafficLightsState

        Private changeStateTicksCount As Integer

        Protected Sub New()
            TryChangeState(Function() TrafficLightsState.Red(3))
            timer.Interval = TimeSpan.FromSeconds(1)
            blinkingTimer.Interval = TimeSpan.FromMilliseconds(300)
        End Sub

        Public Sub Start()
            AddHandler blinkingTimer.Tick, AddressOf OnBlinkingTimedEvent
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Start()
        End Sub

        Public Sub [Stop]()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTimedEvent
            blinkingTimer.Stop()
            RemoveHandler blinkingTimer.Tick, AddressOf OnBlinkingTimedEvent
        End Sub

        Private Sub UpdateSegemntsState()
            If currentState.ResetTimerTicks.HasValue Then WaitTime = currentState.ResetTimerTicks.Value * initialTicksCount
            IsGreenLeftSegmentEnabled = currentState.GreenSegment <> SegmentState.Disabled
            IsGreenRightSegmentEnabled = currentState.ArrowSegment <> SegmentState.Disabled
            IsYellowSegmentEnabled = currentState.YellowSegment <> SegmentState.Disabled
            IsRedSegmentEnabled = currentState.RedSegment <> SegmentState.Disabled
            IsGangerGreenSegmentEnabled = currentState.GreenSegment <> SegmentState.Disabled
            IsGangerRedSegmentEnabled = currentState.GangerRedSegment <> SegmentState.Disabled
            IsTimerGreen = Not IsGangerRedSegmentEnabled
            blinkingTimer.IsEnabled = currentState.GreenSegment = SegmentState.Blinking OrElse currentState.ArrowSegment = SegmentState.Blinking
            changeStateTicksCount = initialTicksCount
        End Sub

        Private Sub TryChangeState(ByVal nextState As Func(Of TrafficLightsState))
            If changeStateTicksCount = 0 Then
                currentState = nextState()
                UpdateSegemntsState()
            End If

            changeStateTicksCount -= 1
            WaitTime -= 1
        End Sub

        Private Sub ChangeBlinkingState()
            If currentState.GreenSegment = SegmentState.Blinking Then
                IsGangerGreenSegmentEnabled = Not IsGangerGreenSegmentEnabled
                IsGreenLeftSegmentEnabled = Not IsGreenLeftSegmentEnabled
            End If

            If currentState.ArrowSegment = SegmentState.Blinking Then IsGreenRightSegmentEnabled = Not IsGreenRightSegmentEnabled
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            TryChangeState(Function() currentState.NextState())
        End Sub

        Private Sub OnBlinkingTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            ChangeBlinkingState()
        End Sub
    End Class
End Namespace
