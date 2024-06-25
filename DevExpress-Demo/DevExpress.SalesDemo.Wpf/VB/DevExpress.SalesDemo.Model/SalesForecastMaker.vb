Namespace DevExpress.SalesDemo.Model

    Public Class SalesForecastMaker

        Public Shared Function GetForecast(ByVal currentSales As Decimal, ByVal elapsedPeriodPart As Decimal) As Decimal
            Return If(elapsedPeriodPart = 0, currentSales, currentSales / elapsedPeriodPart)
        End Function

        Public Shared Function GetYtdForecast(ByVal currentSales As Decimal) As Decimal
            Dim ytdRange As DateTimeRange = GetYtdRange()
            Dim wholeIntervalLength As Decimal = ytdRange.End.Ticks - ytdRange.Start.Ticks
            Dim elapsedTimeIntervalLength As Decimal = Date.Today.Ticks - ytdRange.Start.Ticks
            Dim elapsedPeriodPart As Decimal = If(wholeIntervalLength = 0, 0, elapsedTimeIntervalLength / wholeIntervalLength)
            Return GetForecast(currentSales, elapsedPeriodPart)
        End Function
    End Class
End Namespace
