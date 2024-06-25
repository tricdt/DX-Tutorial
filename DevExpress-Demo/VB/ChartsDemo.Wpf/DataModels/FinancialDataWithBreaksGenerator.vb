Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices

Namespace ChartsDemo

    Friend Class FinancialDataWithBreaksGenerator

        Const StartPrice As Double = 26

        Const MaxPrice As Double = 100

        Const MinPrice As Double = 5

        Const StartWorkingHour As Integer = 8

        Const EndWorkingHour As Integer = 18

        Const Holiday1Day As Integer = 1

        Const Holiday1Month As Integer = 1

        Const Holiday2Day As Integer = 1

        Const Holiday2Month As Integer = 5

        Const Weekend1 As DayOfWeek = DayOfWeek.Saturday

        Const Weekend2 As DayOfWeek = DayOfWeek.Sunday

        Private Function GeneratePoint(ByVal dateTime As Date, ByVal previousClose As Double, ByVal random As Random, <Out> ByRef newPreviousClose As Double, <Out> ByRef point As FinancialDataPoint) As Boolean
            If dateTime.Hour < StartWorkingHour OrElse dateTime.Hour >= EndWorkingHour OrElse dateTime.DayOfWeek = Weekend1 OrElse dateTime.DayOfWeek = Weekend2 OrElse dateTime.Day = Holiday1Day AndAlso dateTime.Month = Holiday1Month OrElse dateTime.Day = Holiday2Day AndAlso dateTime.Month = Holiday2Month Then
                newPreviousClose = Double.NaN
                point = New FinancialDataPoint()
                Return False
            End If

            Dim open As Double
            If dateTime.Hour = StartWorkingHour Then
                open = Math.Round(previousClose + (random.NextDouble() - 0.5) / 2R, 2)
            Else
                open = Math.Round(previousClose, 2)
            End If

            Dim close As Double = Math.Round(open + (random.NextDouble() - 0.5) / 5R, 2)
            If close > MaxPrice Then close = Math.Round(0.8 * close, 2)
            If close <= MinPrice Then close = Math.Round(2 * MinPrice - close, 2)
            Dim high As Double = Math.Round(Math.Max(open, close) + random.NextDouble() / 5R, 2)
            Dim low As Double = Math.Round(Math.Min(open, close) - random.NextDouble() / 5R, 2)
            Dim volume As Double = Math.Round((random.NextDouble() + 0.1) * 1000R, 2)
            newPreviousClose = close
            point = New FinancialDataPoint(dateTime, open, high, low, close, volume)
            Return True
        End Function

        Friend Function Generate() As List(Of FinancialDataPoint)
            Dim random As Random = New Random(28)
            Dim points As List(Of FinancialDataPoint) = New List(Of FinancialDataPoint)()
            Dim startYear As Integer = Date.Now.Year - 3
            Dim currentDateTime As Date = New DateTime(startYear, 1, 2, 8, 0, 0)
            Dim endDateTime As Date = New DateTime(startYear + 3, 1, 1, 0, 0, 0)
            Dim previousClose As Double = StartPrice
            While currentDateTime < endDateTime
                Dim point As FinancialDataPoint = New FinancialDataPoint()
                Dim newPreviousClose As Double
                Dim generated As Boolean = GeneratePoint(currentDateTime, previousClose, random, newPreviousClose, point)
                If generated Then
                    previousClose = newPreviousClose
                    points.Add(point)
                End If

                currentDateTime = currentDateTime.AddHours(1)
            End While

            Return points
        End Function
    End Class
End Namespace
