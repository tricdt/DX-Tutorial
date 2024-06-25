Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Wpf.Helpers
Imports System.Windows.Media.Imaging

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class LoginViewModel

        Public Shared Function Create() As LoginViewModel
            Return ViewModelSource.Create(Function() New LoginViewModel())
        End Function

        Public Shared Function Clone(ByVal vm As LoginViewModel) As LoginViewModel
            Dim res = Create()
            res.UserName = vm.UserName
            Return res
        End Function

        Protected Sub New()
            Icon = ResourceImageHelper.GetResourceImage("User.png")
            UserName = "John Smith"
        End Sub

        Public Overridable Property Icon As BitmapImage

        Public Overridable Property UserName As String

        Public Overridable Property Password As String
    End Class
End Namespace
