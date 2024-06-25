Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System
Imports System.Collections.Generic

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class SalesBySectorViewModel
        Inherits DataViewModel

        Public Shared Function Create() As SalesBySectorViewModel
            Return ViewModelSource.Create(Function() New SalesBySectorViewModel())
        End Function

        Protected Sub New()
            RequestData("SalesBySector", Function(x) x.GetSalesBySector(New DateTime(), Date.Now, GroupingPeriod.All), Sub(x) SalesBySector = x)
        End Sub

        Public Overridable Property SalesBySector As IEnumerable(Of SalesGroup)
    End Class
End Namespace
