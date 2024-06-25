Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class SalesSummaryViewModel
        Inherits DataViewModel

        Public Shared Function Create() As SalesSummaryViewModel
            Return ViewModelSource.Create(Function() New SalesSummaryViewModel())
        End Function

        Public Sub New()
            SalesBySectorViewModel = SalesBySectorViewModel.Create()
            ThisYearAnnualSalesPerformanceViewModel = AnnualSalesPerformanceViewModel.Create(Date.Now)
            LastYearAnnualSalesPerformanceViewModel = AnnualSalesPerformanceViewModel.Create(Date.Now.AddYears(-1))
            SalesForecastViewModel = SalesForecastViewModel.Create()
            SalesForecastHeader = String.Format("SALES FORECAST ({0})", DateTimeUtils.GetCurrentYear())
        End Sub

        Public Overridable Property SalesBySectorViewModel As SalesBySectorViewModel

        Public Overridable Property ThisYearAnnualSalesPerformanceViewModel As AnnualSalesPerformanceViewModel

        Public Overridable Property LastYearAnnualSalesPerformanceViewModel As AnnualSalesPerformanceViewModel

        Public Overridable Property SalesForecastViewModel As SalesForecastViewModel

        Public Overridable Property SalesForecastHeader As String
    End Class
End Namespace
