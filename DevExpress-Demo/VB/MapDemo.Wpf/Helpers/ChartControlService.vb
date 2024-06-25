Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Charts

Namespace MapDemo

    Public Interface IChartControlService

        Sub Animate()

        Sub UpdateData()

    End Interface

    Public Class ChartControlService
        Inherits ServiceBase
        Implements IChartControlService

        Public ReadOnly Property ChartControl As ChartControl
            Get
                Return CType(AssociatedObject, ChartControl)
            End Get
        End Property

        Public Sub UpdateData() Implements IChartControlService.UpdateData
            ChartControl.UpdateData()
        End Sub

        Public Sub Animate() Implements IChartControlService.Animate
            ChartControl.Animate()
        End Sub
    End Class
End Namespace
