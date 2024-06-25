Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class DateTimeArgumentDataGenerator

        Const PointCount As Integer = 100000

        Public Function Generate() As List(Of ChartsDemo.DateTimePoint)
            Dim value As Double = 0
            Dim argument As System.DateTime = System.DateTime.Now.AddHours(-ChartsDemo.DateTimeArgumentDataGenerator.PointCount)
            Dim random As System.Random = New System.Random(1)
            Dim points = New System.Collections.Generic.List(Of ChartsDemo.DateTimePoint)()
            For i As Double = 0 To ChartsDemo.DateTimeArgumentDataGenerator.PointCount - 1
                points.Add(New ChartsDemo.DateTimePoint(argument.AddHours(i), System.Math.Abs(value)))
                value +=(random.NextDouble() * 10.0 - 5.0)
            Next

            Return points
        End Function
    End Class

    Public Class DateTimePoint

        Private _Argument As DateTime, _Value As Double

        Public Property Argument As DateTime
            Get
                Return _Argument
            End Get

            Private Set(ByVal value As DateTime)
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

        Public Sub New(ByVal argument As System.DateTime, ByVal value As Double)
            Me.Argument = argument
            Me.Value = value
        End Sub
    End Class
End Namespace
