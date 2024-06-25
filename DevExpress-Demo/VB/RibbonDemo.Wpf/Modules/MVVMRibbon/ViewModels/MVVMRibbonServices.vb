Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Ribbon

Namespace RibbonDemo

    Public Interface IRibbonMergeingService

        Sub Remerge()

    End Interface

    Public Class RibbonMergeingService
        Inherits ServiceBaseGeneric(Of RibbonControl)
        Implements IRibbonMergeingService

        Public Sub Remerge() Implements IRibbonMergeingService.Remerge
            Call CType(If(AssociatedObject.ActualMergedParent, AssociatedObject), IRibbonControl).ReMerge()
        End Sub
    End Class
End Namespace
