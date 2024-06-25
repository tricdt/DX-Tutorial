Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Friend Module Random3DDataGenerator

        Public Function Generate() As List(Of ChartsDemo.Simple3dDataPoint)
            Dim rnd As ChartsDemo.NormalRandom = New ChartsDemo.NormalRandom(1)
            Dim list As System.Collections.Generic.List(Of ChartsDemo.Simple3dDataPoint) = New System.Collections.Generic.List(Of ChartsDemo.Simple3dDataPoint)(3000)
            For i As Integer = 0 To 3 - 1
                Dim centerX As Double = 35 * (2 - i)
                Dim centerY As Double = 40 * i
                Dim centerZ As Double = 20
                For j As Integer = 0 To 100 - 1
                    Dim x As Double = centerX + (rnd.NextDouble() - 0.5) * 10
                    Dim y As Double = centerY + (rnd.NextDouble() - 0.5) * 15
                    Dim z As Double = centerZ + (rnd.NextDouble() - 0.5) * 15
                    list.Add(New ChartsDemo.Simple3dDataPoint() With {.Series = "Series " & i, .X = x, .Y = y, .Value = z})
                Next
            Next

            Return list
        End Function
    End Module

    Friend Class NormalRandom

        Private ReadOnly rnd As System.Random

        Public Sub New(ByVal seek As Integer)
            Me.rnd = New System.Random(seek)
        End Sub

        Public Function NextDouble() As Double
            Dim u1 As Double = 1.0 - Me.rnd.NextDouble()
            Dim u2 As Double = 1.0 - Me.rnd.NextDouble()
            Return System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2)
        End Function
    End Class

    Friend Class Simple3dDataPoint

        Public Property Series As String

        Public Property X As Double

        Public Property Y As Double

        Public Property Value As Double
    End Class
End Namespace
