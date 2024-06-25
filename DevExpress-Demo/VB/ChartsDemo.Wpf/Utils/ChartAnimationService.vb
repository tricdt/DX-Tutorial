Imports System
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Interface IChartAnimationService

        Sub Animate()

    End Interface

    Public Class ChartAnimationService
        Inherits ServiceBase
        Implements IChartAnimationService

        Private ReadOnly Property Chart As ChartControl
            Get
                Return CType(AssociatedObject, ChartControl)
            End Get
        End Property

        Public Sub Animate() Implements IChartAnimationService.Animate
            Chart.Dispatcher.BeginInvoke(New Action(AddressOf Chart.Animate))
        End Sub
    End Class
End Namespace
