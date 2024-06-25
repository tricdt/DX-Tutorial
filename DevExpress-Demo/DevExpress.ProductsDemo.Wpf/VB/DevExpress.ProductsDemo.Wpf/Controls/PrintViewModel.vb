Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Printing

Namespace ProductsDemo

    Public Class PrintViewModel
        Inherits ViewModelBase

        Public Overridable Property PrintableControlLink As PrintableControlLink

        Public Sub UpdatePrintableControlLink()
            If PrintingService.PrintableControlLink IsNot Nothing Then
                PrintableControlLink = PrintingService.PrintableControlLink
                PrintableControlLink.CreateDocument(True)
            End If
        End Sub
    End Class

    Public Module PrintingService

        Public PrintableControlLink As PrintableControlLink

        Public ReadOnly Property HasPrinting As Boolean
            Get
                Return PrintableControlLink IsNot Nothing
            End Get
        End Property
    End Module
End Namespace
