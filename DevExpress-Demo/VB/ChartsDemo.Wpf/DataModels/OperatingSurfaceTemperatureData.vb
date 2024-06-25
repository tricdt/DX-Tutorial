Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class OperatingSurfaceTemperatureData

        Const PointsCount As Integer = 250

        Private ReadOnly maxTemperaturePointField As TemperaturePoint = New TemperaturePoint(TimeSpan.Zero, Double.MinValue)

        Private ReadOnly minTemperaturePointField As TemperaturePoint = New TemperaturePoint(TimeSpan.MaxValue, Double.MaxValue)

        Private ReadOnly temperaturePointsField As List(Of TemperaturePoint) = New List(Of TemperaturePoint)(PointsCount)

        Public ReadOnly Property MinOptimalTemperature As Double
            Get
                Return 43
            End Get
        End Property

        Public ReadOnly Property MaxOptimalTemperature As Double
            Get
                Return 63
            End Get
        End Property

        Public ReadOnly Property OptimalTemperature As Double
            Get
                Return 53
            End Get
        End Property

        Public ReadOnly Property MaxTemperaturePoint As TemperaturePoint
            Get
                Return maxTemperaturePointField
            End Get
        End Property

        Public ReadOnly Property MinTemperaturePoint As TemperaturePoint
            Get
                Return minTemperaturePointField
            End Get
        End Property

        Public ReadOnly Property TemperaturePoints As List(Of TemperaturePoint)
            Get
                Return temperaturePointsField
            End Get
        End Property

        Public Sub New()
            Dim random As Random = New Random(9)
            Dim preTemperature As Double = 50
            For i As Integer = 0 To PointsCount - 1
                Dim time As TimeSpan = TimeSpan.FromSeconds(i)
                Dim temperature As Double = preTemperature + (random.NextDouble() - 0.5) * 10
                If temperature > 90 Then temperature -= 20
                If temperature < 20 Then temperature += 10
                Dim temperaturePoint As TemperaturePoint = New TemperaturePoint(time, temperature)
                If temperature < minTemperaturePointField.Temperature Then minTemperaturePointField = temperaturePoint
                If temperature > maxTemperaturePointField.Temperature Then maxTemperaturePointField = temperaturePoint
                temperaturePointsField.Add(temperaturePoint)
                preTemperature = temperature
            Next
        End Sub
    End Class

    Public Class TemperaturePoint

        Private _TimeStamp As TimeSpan, _Temperature As Double

        Public Property TimeStamp As TimeSpan
            Get
                Return _TimeStamp
            End Get

            Private Set(ByVal value As TimeSpan)
                _TimeStamp = value
            End Set
        End Property

        Public Property Temperature As Double
            Get
                Return _Temperature
            End Get

            Private Set(ByVal value As Double)
                _Temperature = value
            End Set
        End Property

        Friend Sub New(ByVal time As TimeSpan, ByVal temperature As Double)
            TimeStamp = time
            Me.Temperature = temperature
        End Sub
    End Class
End Namespace
