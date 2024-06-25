Imports System.Windows

Namespace GridDemo

    Public Partial Class Grouping
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            GroupByCountryThenCity()
        End Sub

        Private Sub OnGroupByCountryThenCityClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GroupByCountryThenCity()
        End Sub

        Private Sub OnGroupByCountryCityOrderDateClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GroupByCountryThenCity()
            grid.GroupBy("OrderDate")
        End Sub

        Private Sub OnGroupByCityThenOrderDateClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ClearGrouping()
            grid.GroupBy("City")
            grid.GroupBy("OrderDate")
        End Sub

        Private Sub OnClearGroupingClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ClearGrouping()
        End Sub

        Private Sub GroupByCountryThenCity()
            grid.ClearGrouping()
            grid.GroupBy("Country")
            grid.GroupBy("City")
        End Sub
    End Class
End Namespace
