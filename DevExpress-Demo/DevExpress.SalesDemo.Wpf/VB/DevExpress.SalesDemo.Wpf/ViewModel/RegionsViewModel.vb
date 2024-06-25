Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class RegionsViewModel
        Inherits PageViewModel

        Public Shared Function Create() As RegionsViewModel
            Return ViewModelSource.Create(Function() New RegionsViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property DailySalesByRegionViewModel As PerformanceBarChartViewModel

        Public Overridable Property UnitSalesByRegionViewModel As PerformanceBarChartViewModel

        Public Overridable Property PieGaugeRangeViewModel As PieBarRangeViewModel

        Protected Overrides Sub Initialize()
            DailySalesByRegionViewModel = PerformanceBarChartViewModel.Create(Modules.Regions, PerformanceViewMode.Daily)
            UnitSalesByRegionViewModel = PerformanceBarChartViewModel.Create(Modules.Regions, PerformanceViewMode.Monthly)
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Modules.Regions)
        End Sub
    End Class
End Namespace
