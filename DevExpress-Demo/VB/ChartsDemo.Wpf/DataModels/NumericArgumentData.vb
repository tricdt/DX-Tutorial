Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Class NumericArgumentDataGenerator

        Const PointCount As Integer = 500000

        Public Function Generate() As List(Of NumericPoint)
            Dim value As Double = 0
            Dim argument As Double = 1
            Dim random As Random = New Random(0)
            Dim points = New List(Of NumericPoint)()
            For i As Integer = 0 To PointCount - 1
                points.Add(New NumericPoint(argument, value))
                value +=(random.NextDouble() * 10.0 - 5.0)
                argument += 1
            Next

            Return points
        End Function
    End Class

    Public Class NumericPoint

        Private _Argument As Double, _Value As Double

        Public Property Argument As Double
            Get
                Return _Argument
            End Get

            Private Set(ByVal value As Double)
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

        Public Sub New(ByVal argument As Double, ByVal value As Double)
            Me.Argument = argument
            Me.Value = value
        End Sub
    End Class
End Namespace
