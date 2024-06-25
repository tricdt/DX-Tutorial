Imports DevExpress.Xpf.Editors
Imports System.Collections.Generic

Namespace GridDemo.ModuleResources

    Public Class SearchPanelParseModeItem

        Public Property Mode As SearchPanelParseMode

        Public Property Description As String
    End Class

    Public Class SearchPanelParseModes

        Private Shared _Items As List(Of GridDemo.ModuleResources.SearchPanelParseModeItem)

        Shared Sub New()
            Items = New List(Of SearchPanelParseModeItem)()
            Call Items.Add(New SearchPanelParseModeItem() With {.Mode = SearchPanelParseMode.Mixed, .Description = "Search words are combined by the Or operator. The operator changes to And if you specify a column name before a search word."})
            Call Items.Add(New SearchPanelParseModeItem() With {.Mode = SearchPanelParseMode.Exact, .Description = "The search engine does not split the query into individual words and thereby looks for exact matches."})
            Call Items.Add(New SearchPanelParseModeItem() With {.Mode = SearchPanelParseMode.Or, .Description = "Search words are combined by the Or operator."})
            Call Items.Add(New SearchPanelParseModeItem() With {.Mode = SearchPanelParseMode.And, .Description = "Search words are combined by the And operator."})
        End Sub

        Public Shared Property Items As List(Of SearchPanelParseModeItem)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of SearchPanelParseModeItem))
                _Items = value
            End Set
        End Property
    End Class
End Namespace
