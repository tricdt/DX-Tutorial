Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class TimeSpanArgumentDataGenerator

        Const PointCount As Integer = 40000

        Public Function Generate(ByVal initialValue As Double, ByVal min As Double, ByVal max As Double) As List(Of TimeSpanPoint)
            Dim value As Double = initialValue
            Dim random As Random = New Random(1)
            Dim threshold As Double =(min - initialValue) / 0.2
            Dim points = New List(Of TimeSpanPoint)()
            For i As Double = 0 To threshold - 1
                points.Add(New TimeSpanPoint(TimeSpan.FromSeconds(i), value))
                value += random.NextDouble() - 0.3
            Next

            For i As Double = threshold To PointCount - 1
                points.Add(New TimeSpanPoint(TimeSpan.FromSeconds(i), value))
                value = Math.Max(Math.Min(value + random.NextDouble() - 0.5, max), min)
            Next

            Return points
        End Function
    End Class

    Public Class TimeSpanPoint

        Private _Argument As TimeSpan, _Value As Double

        Public Property Argument As TimeSpan
            Get
                Return _Argument
            End Get

            Private Set(ByVal value As TimeSpan)
                _Argument = value
            End Set
        End Property

        Public Property Value As Double
            Get
                Return _Value
            End Get

            Private Set(ByVal value As Double)
                _Value = value
            End Set
        End Property

        Public Sub New(ByVal argument As TimeSpan, ByVal value As Double)
            Me.Argument = argument
            Me.Value = value
        End Sub
    End Class
End Namespace
