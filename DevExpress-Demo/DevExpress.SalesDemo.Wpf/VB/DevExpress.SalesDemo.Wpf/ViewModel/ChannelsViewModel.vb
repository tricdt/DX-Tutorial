Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class ChannelsViewModel
        Inherits PageViewModel

        Public Shared Function Create() As ChannelsViewModel
            Return ViewModelSource.Create(Function() New ChannelsViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property PerformanceLineChartViewModel As PerformanceLineChartViewModel

        Public Overridable Property PieGaugeRangeViewModel As PieBarRangeViewModel

        Protected Overrides Sub Initialize()
            PerformanceLineChartViewModel = PerformanceLineChartViewModel.Create(Channels)
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Channels)
        End Sub
    End Class
End Namespace
