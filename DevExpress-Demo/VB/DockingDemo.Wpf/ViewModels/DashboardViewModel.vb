Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Map.Native
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports DockingDemo.Helpers

Namespace DockingDemo.ViewModels

    Public Class DashboardViewModel

        Private stateIndex As Integer = 0

        Public Sub New()
            Dim generator = New SalesPerformanceDataGenerator(DataLoader.LoadSales())
            generator.Generate()
            KeyMetrics = generator.KeyMetrics
            MonthlySales = generator.MonthlySales
            TotalSales = generator.TotalSales
            DeltaInfos = DeltaInfo.GetAllInfo()
            states = MonthlySales.[Select](Function(x) x.State).Distinct().ToList()
            State = states.FirstOrDefault()
        End Sub

        Public Property KeyMetrics As IEnumerable(Of KeyMetricsItem)

        Public Property MonthlySales As IEnumerable(Of MonthlySalesItem)

        Public Property TotalSales As IEnumerable(Of TotalSalesItem)

        Public Overridable Property CurrentMonthlySales As IEnumerable(Of MonthlySalesItem)

        Public Overridable Property CurrentTotalSales As IEnumerable(Of TotalSalesItem)

        Public Property DeltaInfos As List(Of DeltaInfo)

        Public Overridable Property TopSales As IEnumerable(Of ProductData)

        Public Overridable Property CategoriesData As IEnumerable(Of CategoryData)

        Public ReadOnly Property MapTitle As String
            Get
                Return "Sales by State - " & State
            End Get
        End Property

        Private states As List(Of String)

        Private dataState As String

        Private stateField As String

        Public Property State As String
            Get
                Return stateField
            End Get

            Set(ByVal value As String)
                stateField = value
                dataState = GetStateByIndex()
                OnStateChanged()
                RaisePropertyChanged(Function(x) MapTitle)
            End Set
        End Property

        Private selectedStateField As MapPath

        Public Property SelectedState As MapPath
            Get
                Return selectedStateField
            End Get

            Set(ByVal value As MapPath)
                selectedStateField = value
                State = TryCast(value, IMapItemCore).Text
            End Set
        End Property

        Protected Sub OnCurrentMonthlySalesChanged(ByVal oldValue As IEnumerable(Of MonthlySalesItem))
            TopSales = CurrentMonthlySales.GroupBy(Function(x) x.Product).[Select](Function(x) New ProductData With {.Product = x.Key, .Total = x.Sum(Function(y) y.Revenue), .Sales = x.[Select](Function(y) New SaleData With {.Price = y.Revenue, .[Date] = y.CurrentDate})}).OrderBy(Function(x) x.Total, ListSortDirection.Descending).Take(5).ToList()
            CategoriesData = CurrentMonthlySales.GroupBy(Function(x) x.Category).[Select](Function(x) New CategoryData With {.Category = x.Key, .Revenue = x.Sum(Function(y) y.UnitsSoldTarget)})
        End Sub

        Private Sub OnStateChanged()
            CurrentMonthlySales = MonthlySales.Where(Function(x) Equals(x.State, dataState))
            CurrentTotalSales = TotalSales.Where(Function(x) Equals(x.State, dataState))
        End Sub

        Private Function GetStateByIndex() As String
            If states.Contains(stateField) Then Return stateField
            If stateIndex >= states.Count Then stateIndex = 0
            Return states(Math.Min(Threading.Interlocked.Increment(stateIndex), stateIndex - 1))
        End Function
    End Class

    Public Class DeltaInfo

        Public Sub New(ByVal delta As String, ByVal summary As String, ByVal name As String)
            Me.Delta = "+$" & delta
            Me.Summary = "$" & summary
            Me.Name = name
        End Sub

        Public Property Delta As String

        Public Property Summary As String

        Public Property Name As String

        Public Shared Function GetAllInfo() As List(Of DeltaInfo)
            Dim result = New List(Of DeltaInfo)()
            result.Add(New DeltaInfo("6.24M", "159M", "Revenue YTD"))
            result.Add(New DeltaInfo("15.3K", "30.6M", "Expenses YTD"))
            result.Add(New DeltaInfo("6.23K", "129M", "Profit YTD"))
            result.Add(New DeltaInfo("114K", "955K", "Avg Order Size"))
            result.Add(New DeltaInfo("15.8K", "207K", "New Customers"))
            result.Add(New DeltaInfo("523K", "6.31M", "Market Share"))
            Return result
        End Function
    End Class

    Public Class ProductData

        Public Property Product As String

        Public Property Total As Decimal

        Public Property Sales As IEnumerable(Of SaleData)
    End Class

    Public Class SaleData

        Public Property Price As Decimal

        Public Property [Date] As Date
    End Class

    Public Class CategoryData

        Public Property Category As String

        Public Property Revenue As Decimal
    End Class
End Namespace
