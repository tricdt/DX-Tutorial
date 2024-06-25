Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services

    Public Class DialogServiceDetailViewModel

        Public Property CustomerName As String

        Public Shared Function Create() As DialogServiceDetailViewModel
            Return ViewModelSource.Create(Function() New DialogServiceDetailViewModel())
        End Function

        Protected Sub New()
        End Sub
    End Class
End Namespace
