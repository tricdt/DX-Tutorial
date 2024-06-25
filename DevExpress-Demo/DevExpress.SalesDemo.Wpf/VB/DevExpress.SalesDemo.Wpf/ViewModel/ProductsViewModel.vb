Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class ProductsViewModel
        Inherits PageViewModel

        Public Shared Function Create() As ProductsViewModel
            Return ViewModelSource.Create(Function() New ProductsViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property DailySalesByProductViewModel As PerformanceBarChartViewModel

        Public Overridable Property UnitSalesByProductViewModel As PerformanceBarChartViewModel

        Public Overridable Property PieGaugeRangeViewModel As PieBarRangeViewModel

        Protected Overrides Sub Initialize()
            DailySalesByProductViewModel = PerformanceBarChartViewModel.Create(Products, PerformanceViewMode.Daily)
            UnitSalesByProductViewModel = PerformanceBarChartViewModel.Create(Products, PerformanceViewMode.Monthly)
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Products)
        End Sub
    End Class
End Namespace
