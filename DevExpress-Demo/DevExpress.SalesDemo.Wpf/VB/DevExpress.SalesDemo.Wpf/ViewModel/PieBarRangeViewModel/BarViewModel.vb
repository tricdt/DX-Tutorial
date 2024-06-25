Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class BarViewModel
        Inherits DataViewModel

        Public Shared Function Create() As BarViewModel
            Return ViewModelSource.Create(Function() New BarViewModel())
        End Function

        Protected Sub New()
            PieArgumentDataMember = "GroupName"
            PieValueDataMember = "TotalCost"
            If IsInDesignMode() Then OnInitializeInDesignMode()
        End Sub

        Public Overridable Property PieDataSource As Object

        Public Overridable Property PieArgumentDataMember As String

        Public Overridable Property PieValueDataMember As String

        Public Sub LoadData(ByVal data As Object)
            PieDataSource = data
        End Sub

        Private Sub OnInitializeInDesignMode()
            Dim today As Date = Date.Today
            Dim monthAgo As Date = today.AddMonths(-1)
            Dim ThirdOfMonth As Integer = 10
            Dim rangeStart = monthAgo
            Dim rangeEnd = today
            Dim selectedRangeStart = monthAgo.AddDays(ThirdOfMonth)
            Dim selectedRangeEnd = monthAgo.AddDays(2 * ThirdOfMonth)
            RequestData("PieDataSource", Function(x) x.GetSalesBySector(selectedRangeStart, selectedRangeEnd, GroupingPeriod.All), Sub(x) PieDataSource = x)
        End Sub
    End Class
End Namespace
