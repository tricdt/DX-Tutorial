Imports DevExpress.Mvvm.DataAnnotations

Namespace GridDemo

    <POCOViewModel>
    Public Class PrintMasterDetailViewModel
        Inherits PrintViewModelBase

        Protected Overrides Function GetTitle() As String
            Return "Print Preview"
        End Function
    End Class
End Namespace
