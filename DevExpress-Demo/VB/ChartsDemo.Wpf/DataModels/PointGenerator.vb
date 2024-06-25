Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Friend Class PointGenerator

        Const startValue As Double = 0

        Public Shared Function Generate() As List(Of SimpleDataPoint)
            Dim random1 As Random = New Random(2)
            Dim random2 As Random = New Random(3)
            Dim previousValue As Double = startValue
            Dim list As List(Of SimpleDataPoint) = New List(Of SimpleDataPoint)() From {New SimpleDataPoint(0, previousValue)}
            Dim x As Double = -2000
            While x < 2000
                Dim value As Double = previousValue + random1.Next(-98, 100)
                Dim pointValue As Double = value - 3000
                list.Add(New SimpleDataPoint(x, pointValue))
                previousValue = value
                x += random2.NextDouble() * 3
            End While

            Return list
        End Function

        Public Shared Function GenerateCluster(ByVal random As Random, ByVal xPlus As Integer, ByVal xMinus As Integer, ByVal yPlus As Integer, ByVal yMinus As Integer, ByVal count As Integer) As IEnumerable(Of NumericPoint)
            Dim points As List(Of NumericPoint) = New List(Of NumericPoint)()
            Dim deltaX As Integer = xMinus - xPlus
            Dim deltaY As Integer = yMinus - yPlus
            Dim centerX As Integer = xMinus \ 2 + xPlus \ 2
            Dim centerY As Integer = yMinus \ 2 + yPlus \ 2
            For i As Integer = 0 To count - 1
                Dim half As Integer = i \ 2 + 1
                Dim ratio As Double = Math.Max(2.1, CDbl(count) / half)
                Dim xOffset As Integer = CInt(deltaX / ratio)
                Dim yOffset As Integer = CInt(deltaY / ratio)
                Dim delta As Integer = xMinus - xOffset - centerX
                Dim rx, ry As Integer
                Do
                    rx = random.Next(xPlus + xOffset, xMinus - xOffset)
                    ry = random.Next(yPlus + yOffset, yMinus - yOffset)
                Loop While delta * delta < Math.Pow(centerX - rx, 2) + Math.Pow(centerY - ry, 2)

                points.Add(New NumericPoint(rx, ry))
            Next

            Return points
        End Function
    End Class
End Namespace
