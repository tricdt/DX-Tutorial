Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class SalesDashboardViewModel
        Inherits PageViewModel

        Public Shared Function Create() As SalesDashboardViewModel
            Return ViewModelSource.Create(Function() New SalesDashboardViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property SalesSummaryViewModel As SalesSummaryViewModel

        Public Overridable Property DailySalesPerformanceViewModel As PerformanceAreaChartViewModel

        Public Overridable Property MonthlySalesPerformanceViewModel As PerformanceAreaChartViewModel

        Protected Overrides Sub Initialize()
            SalesSummaryViewModel = SalesSummaryViewModel.Create()
            DailySalesPerformanceViewModel = PerformanceAreaChartViewModel.Create(Dashboard, PerformanceViewMode.Daily)
            MonthlySalesPerformanceViewModel = PerformanceAreaChartViewModel.Create(Dashboard, PerformanceViewMode.Monthly)
        End Sub
    End Class
End Namespace
