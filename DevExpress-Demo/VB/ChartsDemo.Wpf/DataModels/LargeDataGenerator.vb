Imports System
Imports System.Collections.Generic

Namespace ChartsDemo

    Public Module LargeDataGenerator

        Private counter As Integer = 0

        Public Function GenerateSeriesDataSource(ByVal pointsCount As Integer) As List(Of SimpleDataPoint)
            Dim points As List(Of SimpleDataPoint) = New List(Of SimpleDataPoint)()
            Dim value As Double = 0
            Dim argument As Double = 0
            counter += 1
            For i As Integer = 0 To pointsCount - 1
                argument = i
                value = CSng(Math.Sin(argument) + Math.Sin(argument / 100.0) + 30 * Math.Sin(argument / 1000.0) + 1000 * (1 + 0.1 * counter Mod 10) * Math.Sin(argument / 100000.0 + Math.PI / 6 * counter))
                points.Add(New SimpleDataPoint(argument, value))
            Next

            Return points
        End Function
    End Module
End Namespace
