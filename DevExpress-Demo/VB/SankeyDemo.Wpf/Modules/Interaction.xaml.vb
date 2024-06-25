Imports System.Windows

Namespace SankeyDemo

    Public Partial Class Interaction
        Inherits SankeyDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub map_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            map.ZoomToFitLayerItems(0)
        End Sub

        Private Sub mapLayer_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            map.ZoomToFitLayerItems(0)
        End Sub

        Private Sub gridTable_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            gridTable.BestFitColumns()
        End Sub
    End Class
End Namespace
