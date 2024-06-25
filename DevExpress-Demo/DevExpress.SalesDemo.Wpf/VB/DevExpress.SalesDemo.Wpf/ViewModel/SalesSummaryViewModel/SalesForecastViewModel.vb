Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class SalesForecastViewModel
        Inherits DataViewModel

        Public Shared Function Create() As SalesForecastViewModel
            Return ViewModelSource.Create(Function() New SalesForecastViewModel())
        End Function

        Protected Sub New()
            RequestData("YTDSalesVolume", Function(x) x.GetTotalSalesByRange(DateTimeUtils.GetYtdRange()).TotalCost, Sub(x)
                Dim YTDSalesVolume = x
                YTDSalesForecast = SalesForecastMaker.GetYtdForecast(YTDSalesVolume)
            End Sub)
            Dim badSalesRange As DecimalRange = SalesRangeProvider.GetBadSalesRange()
            Dim normalSalesRange As DecimalRange = SalesRangeProvider.GetNormalSalesRange()
            Dim goodSalesRange As DecimalRange = SalesRangeProvider.GetGoodSalesRange()
            AnnualSalesFirstRangeEnd = badSalesRange.End
            AnnualSalesSecondRangeEnd = normalSalesRange.End
            AnnualSalesThirdRangeEnd = goodSalesRange.End
        End Sub

        Public Overridable Property AnnualSalesFirstRangeEnd As Decimal

        Public Overridable Property AnnualSalesSecondRangeEnd As Decimal

        Public Overridable Property AnnualSalesThirdRangeEnd As Decimal

        Public Overridable Property YTDSalesForecast As Decimal
    End Class
End Namespace
