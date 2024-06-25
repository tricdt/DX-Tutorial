Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Printing

Namespace DevExpress.DevAV.Common.View

    Public Interface IPrintableControlPreviewService

        Function GetLink() As PrintableControlLink

    End Interface

    Public Class PrintableControlPreviewService
        Inherits ServiceBase
        Implements IPrintableControlPreviewService

        Public Property IsLandscape As Boolean

        Public Function GetLink() As PrintableControlLink Implements IPrintableControlPreviewService.GetLink
            Dim link As PrintableControlLink = New PrintableControlLink(TryCast(AssociatedObject, IPrintableControl))
            link.Landscape = IsLandscape
            link.CreateDocument(True)
            Return link
        End Function
    End Class
End Namespace
