Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class PieViewModel
        Inherits BarViewModel

        Public Overloads Shared Function Create() As PieViewModel
            Return ViewModelSource.Create(Function() New PieViewModel())
        End Function

        Protected Sub New()
        End Sub
    End Class
End Namespace
