Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class AnnualSalesPerformanceViewModel
        Inherits DataViewModel

        Public Shared Function Create(ByVal [date] As Date) As AnnualSalesPerformanceViewModel
            Return ViewModelSource.Create(Function() New AnnualSalesPerformanceViewModel([date]))
        End Function

        Protected Sub New()
            Me.New(Date.Now)
        End Sub

        Protected Sub New(ByVal [date] As Date)
            Dim badSalesRange As DecimalRange = SalesRangeProvider.GetBadSalesRange()
            Dim normalSalesRange As DecimalRange = SalesRangeProvider.GetNormalSalesRange()
            Dim goodSalesRange As DecimalRange = SalesRangeProvider.GetGoodSalesRange()
            AnnualSalesFirstRangeEnd = badSalesRange.End
            AnnualSalesSecondRangeEnd = normalSalesRange.End
            AnnualSalesThirdRangeEnd = goodSalesRange.End
            If DateTimeUtils.IsCurrentYear([date]) Then
                VolumeHeader = "YEAR TO DATE"
                RequestData("Volume", Function(x) x.GetTotalSalesByRange(DateTimeUtils.GetYtdRange()).TotalCost, Sub(x) Volume = x)
            Else
                VolumeHeader = "YEAR " & [date].Year
                RequestData("Volume", Function(x) x.GetTotalSalesByRange(DateTimeUtils.GetYearRange([date])).TotalCost, Sub(x) Volume = x)
            End If
        End Sub

        Public Overridable Property AnnualSalesFirstRangeEnd As Decimal

        Public Overridable Property AnnualSalesSecondRangeEnd As Decimal

        Public Overridable Property AnnualSalesThirdRangeEnd As Decimal

        Public Overridable Property VolumeHeader As String

        Public Overridable Property Volume As Decimal
    End Class
End Namespace
