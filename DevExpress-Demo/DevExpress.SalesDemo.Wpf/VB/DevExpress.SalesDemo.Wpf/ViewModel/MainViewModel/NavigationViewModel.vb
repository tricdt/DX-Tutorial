Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class NavigationViewModel

        Public Shared Function Create() As NavigationViewModel
            Return ViewModelSource.Create(Function() New NavigationViewModel())
        End Function

        Protected Sub New()
        End Sub
    End Class
End Namespace
