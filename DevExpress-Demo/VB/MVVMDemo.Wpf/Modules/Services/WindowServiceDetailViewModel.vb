Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Windows

Namespace MVVMDemo.Services

    Public Class WindowServiceDetailViewModel

        Public Property CustomerName As String

        Public Sub Register()
            Dim service As ICurrentWindowService = GetRequiredService(Of ICurrentWindowService)()
            service.Close()
            MessageBox.Show("Registered")
        End Sub

        Public Function CanRegister() As Boolean
            Return Not String.IsNullOrEmpty(CustomerName)
        End Function

        Public Shared Function Create() As WindowServiceDetailViewModel
            Return ViewModelSource.Create(Function() New WindowServiceDetailViewModel())
        End Function

        Protected Sub New()
        End Sub
    End Class
End Namespace
