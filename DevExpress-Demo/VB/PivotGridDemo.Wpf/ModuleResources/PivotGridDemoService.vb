Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Interface IPivotGridDemoService

        Sub BeginUpdate()

        Sub CollapseAll()

        Sub EndUpdate()

    End Interface

    Public Class PivotGridDemoService
        Inherits ViewServiceBase
        Implements IPivotGridDemoService

        Public ReadOnly Property PivotGrid As PivotGridControl
            Get
                Return TryCast(AssociatedObject, PivotGridControl)
            End Get
        End Property

        Public Sub BeginUpdate() Implements IPivotGridDemoService.BeginUpdate
            PivotGrid.BeginUpdate()
        End Sub

        Public Sub CollapseAll() Implements IPivotGridDemoService.CollapseAll
            PivotGrid.CollapseAll()
        End Sub

        Public Sub EndUpdate() Implements IPivotGridDemoService.EndUpdate
            PivotGrid.EndUpdate()
        End Sub
    End Class
End Namespace
