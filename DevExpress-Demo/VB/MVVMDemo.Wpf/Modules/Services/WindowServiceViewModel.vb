Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services

    Public Class WindowServiceViewModel

        Public Sub ShowRegistrationForm()
            Dim detailViewModel As WindowServiceDetailViewModel = WindowServiceDetailViewModel.Create()
            Dim service As IWindowService = GetRequiredService(Of IWindowService)()
            service.Title = "Registration Form"
            service.Show(viewModel:=detailViewModel)
        End Sub
    End Class
End Namespace
