Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Utils
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo

    Public Class OscilloscopeViewModel

        Private _AxisGridLines As List(Of GaugesDemo.GridLineItem)

        Const toolTipOffset As Integer = 15

        Private dataOffset As Double = -1

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Public Overridable Property HorizontalPosition As Double

        Public Overridable Property HorizontalSensitivity As Double

        Public Overridable Property VerticalPosition As Double

        Public Overridable Property VerticalSensitivity As Double

        Public Overridable Property ReferenceVoltage As Double

        Public Overridable Property HorizontalMin As Double

        Public Overridable Property HorizontalMax As Double

        Public Overridable Property VerticalMin As Double

        Public Overridable Property VerticalMax As Double

        Public Overridable Property EnableSignalUp As Boolean

        Public Overridable Property ActiveNeedle As ArcScaleNeedle

        Public Overridable Property ActiveGauge As CircularGaugeControl

        Public Overridable Property ActiveGaugePositionX As Double

        Public Overridable Property ActiveGaugePositionY As Double

        Public Overridable Property Oscillations As List(Of Point)

        Public Property AxisGridLines As List(Of GridLineItem)
            Get
                Return _AxisGridLines
            End Get

            Private Set(ByVal value As List(Of GridLineItem))
                _AxisGridLines = value
            End Set
        End Property

        Protected Sub New()
            timer.Interval = TimeSpan.FromMilliseconds(50)
            ReferenceVoltage = 2
            VerticalSensitivity = 5.5
            HorizontalSensitivity = 10.5
            AxisGridLines = CreateOscilloscopeGrid().ToList()
            UpdateData()
        End Sub

        Private Sub UpdateVerticalRange()
            VerticalMin = VerticalPosition - 0.5 * VerticalSensitivity
            VerticalMax = VerticalPosition + 0.5 * VerticalSensitivity
        End Sub

        Private Sub UpdateHorizontalRange()
            HorizontalMin = HorizontalPosition - 0.5 * HorizontalSensitivity
            HorizontalMax = HorizontalPosition + 0.5 * HorizontalSensitivity
        End Sub

        Protected Sub OnVerticalSensitivityChanged()
            UpdateVerticalRange()
        End Sub

        Protected Sub OnHorizontalSensitivityChanged()
            UpdateHorizontalRange()
        End Sub

        Protected Sub OnVerticalPositionChanged()
            UpdateVerticalRange()
        End Sub

        Protected Sub OnHorizontalPositionChanged()
            UpdateHorizontalRange()
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateData()
        End Sub

        Private Sub UpdateData()
            If Math.Abs(ReferenceVoltage) <= 1 Then
                dataOffset = If(EnableSignalUp, 0, 1)
            Else
                dataOffset += 0.5
                If dataOffset > 1 Then dataOffset = -1
                If dataOffset < -1 Then dataOffset = 1
            End If

            Dim oscillations As List(Of Point) = New List(Of Point)()
            For i As Integer = -25 To 25 - 1
                oscillations.Add(New Point(i + dataOffset, Math.Abs(i Mod 2) * 2 - 1))
            Next

            Me.Oscillations = oscillations
        End Sub

        Private Iterator Function CreateOscilloscopeGrid() As IEnumerable(Of GridLineItem)
            For i As Double = 0.25 To 4 - 1 Step 0.25R
                Yield New GridLineItem(i, i / 0.25 Mod 2 = 0)
            Next
        End Function

        Public Sub ShowToolTip(ByVal gauge As CircularGaugeControl, ByVal position As Point)
            ActiveNeedle = gauge.CalcHitInfo(position).Needle
            If ActiveNeedle IsNot Nothing Then
                ActiveGauge = gauge
                ActiveGaugePositionX = position.X + toolTipOffset
                ActiveGaugePositionY = position.Y - gauge.ActualHeight + toolTipOffset
            End If
        End Sub

        Public Sub HideToolTip()
            ActiveNeedle = Nothing
        End Sub

        Public Sub Start()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Start()
        End Sub

        Public Sub [Stop]()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf OnTimedEvent
        End Sub
    End Class

    Public Class GridLineItem
        Inherits ImmutableObject

        Private _Value As Double, _Major As Boolean

        Public Property Value As Double
            Get
                Return _Value
            End Get

            Private Set(ByVal value As Double)
                _Value = value
            End Set
        End Property

        Public Property Major As Boolean
            Get
                Return _Major
            End Get

            Private Set(ByVal value As Boolean)
                _Major = value
            End Set
        End Property

        Public Sub New(ByVal value As Double, ByVal major As Boolean)
            Me.Value = value
            Me.Major = major
        End Sub
    End Class
End Namespace
