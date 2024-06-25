Imports System.Windows

Namespace GridDemo

    Public Partial Class GroupMerging
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnGroupByOrderDateAndCountryThenCityMergedClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ClearGrouping()
            grid.GroupBy("OrderDate")
            GroupByCountryThenCity(True)
        End Sub

        Private Sub OnGroupByCountryThenCityMergedClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ClearGrouping()
            GroupByCountryThenCity(True)
        End Sub

        Private Sub OnGroupByCountryThenCityUnmergedClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ClearGrouping()
            GroupByCountryThenCity(False)
        End Sub

        Private Sub OnClearGroupingClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ClearGrouping()
        End Sub

        Private Sub GroupByCountryThenCity(ByVal groupMerge As Boolean)
            grid.GroupBy("Country")
            grid.GroupBy("City", groupMerge)
        End Sub
    End Class
End Namespace
