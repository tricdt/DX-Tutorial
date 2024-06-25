Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts.Sankey
Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Media

Namespace SankeyDemo

    Public Class InteractionViewModel
        Inherits DevExpress.Mvvm.BindableBase

        Public Shared Function Create() As InteractionViewModel
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New SankeyDemo.InteractionViewModel())
        End Function

        Private ReadOnly data As System.Collections.Generic.List(Of SankeyDemo.Export)

        Private continentColorPairsField As System.Collections.Generic.Dictionary(Of String, System.Windows.Media.Color) = New System.Collections.Generic.Dictionary(Of String, System.Windows.Media.Color)()

        Private continentCountriesPairs As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of String))

        Private countryNameExportPairs As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of SankeyDemo.Export)) = New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of SankeyDemo.Export))()

        Private countryNameMapItemPairs As System.Collections.Generic.Dictionary(Of String, DevExpress.Xpf.Map.MapItem) = New System.Collections.Generic.Dictionary(Of String, DevExpress.Xpf.Map.MapItem)()

        Private exportMapArrowPairs As System.Collections.Generic.Dictionary(Of SankeyDemo.Export, DevExpress.Xpf.Map.MapPolyline) = New System.Collections.Generic.Dictionary(Of SankeyDemo.Export, DevExpress.Xpf.Map.MapPolyline)()

        Private highlightedSankeyItemsField As System.Collections.ObjectModel.ObservableCollection(Of Object) = New System.Collections.ObjectModel.ObservableCollection(Of Object)()

        Private selectedExportItemsField As System.Collections.ObjectModel.ObservableCollection(Of Object) = New System.Collections.ObjectModel.ObservableCollection(Of Object)()

        Private selectedMapItemsField As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem) = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem)()

        Private updateSelectedArrows As Boolean = True

        Public Overridable Property MapArrowStorage As MapItemStorage

        Public Overridable Property DataAdapter As ShapefileDataAdapter

        Public Overridable Property ContinentColorPairs As Dictionary(Of String, System.Windows.Media.Color)
            Get
                Return Me.continentColorPairsField
            End Get

            Set(ByVal value As Dictionary(Of String, System.Windows.Media.Color))
                Me.continentColorPairsField = value
            End Set
        End Property

        Public Overridable ReadOnly Property SankeyData As List(Of SankeyDemo.Export)
            Get
                Return Me.data
            End Get
        End Property

        Public Overridable Property Colorizer As SankeyColorizerBase

        Public Overridable ReadOnly Property SelectedMapItems As IList
            Get
                Return Me.selectedMapItemsField
            End Get
        End Property

        Public Overridable ReadOnly Property HighlightedSankeyItems As IList
            Get
                Return Me.highlightedSankeyItemsField
            End Get
        End Property

        Public Overridable ReadOnly Property SelectedExportItems As IList
            Get
                Return Me.selectedExportItemsField
            End Get
        End Property

        Public Overridable Property MapFileUri As Uri

        Protected Sub New()
            AddHandler Me.highlightedSankeyItemsField.CollectionChanged, Sub(sender, eventArgs) Me.ShowArrowsByHighlightedItems(Me.highlightedSankeyItemsField)
            AddHandler Me.selectedExportItemsField.CollectionChanged, Sub(sender, eventArgs) Me.ShowArrowsBySelectedItems(Me.selectedExportItemsField)
            AddHandler Me.selectedMapItemsField.CollectionChanged, Sub(sender, eventArgs) Me.ShowArrowsBySelectedMapItems(Me.selectedMapItemsField)
            Me.MapArrowStorage = New DevExpress.Xpf.Map.MapItemStorage()
            Me.DataAdapter = New DevExpress.Xpf.Map.ShapefileDataAdapter()
            Me.data = SankeyDemo.OilTradeDataGenerator.GetData()
            AddHandler Me.DataAdapter.ShapesLoaded, AddressOf Me.DataAdapter_ShapesLoaded
            Me.MapFileUri = SankeyDemo.Utils.GetFileUri("Countries.shp")
            Me.DataAdapter.FileUri = Me.MapFileUri
            Me.continentColorPairsField = SankeyDemo.ContinentCountries.GetContinentColorPairs_InteractionDemo()
            Me.continentCountriesPairs = SankeyDemo.ContinentCountries.GetContinentCountriesPairs_InteractionDemo()
            Me.Colorizer = New SankeyDemo.ContinentColorizer() With {.ContinentCountriesPairs = Me.continentCountriesPairs, .ContinentColorPairs = Me.continentColorPairsField}
        End Sub

        Private Sub DataAdapter_ShapesLoaded(ByVal sender As Object, ByVal e As DevExpress.Xpf.Map.ShapesLoadedEventArgs)
            Dim territoryNameBorderCoordPairs As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of DevExpress.Map.CoordPoint)) = Me.GetTerritoryCenterPoints(e.Shapes)
            Me.CreateArrows(territoryNameBorderCoordPairs)
            Me.DisableUnknownItems(e.Shapes)
        End Sub

        Private Sub DisableUnknownItems(ByVal mapItems As System.Collections.Generic.IList(Of DevExpress.Xpf.Map.MapItem))
            For Each mapItem In mapItems
                Dim countryName As String = mapItem.Attributes(CStr(("NAME"))).Value.ToString()
                If Me.countryNameExportPairs.ContainsKey(countryName) Then Continue For
                countryName = mapItem.Attributes(CStr(("CONTINENT"))).Value.ToString()
                If Not Me.countryNameExportPairs.ContainsKey(countryName) Then mapItem.Visible = False
            Next
        End Sub

        Private Function GetTerritoryCenterPoints(ByVal mapItems As System.Collections.Generic.IList(Of DevExpress.Xpf.Map.MapItem)) As Dictionary(Of String, System.Collections.Generic.List(Of DevExpress.Map.CoordPoint))
            Dim territoryNameCenterCoordPairs = New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of DevExpress.Map.CoordPoint))()
            Dim nodeNames As System.Collections.Generic.List(Of String) = Me.data.[Select](Function(export) export.Exporter).Union(Me.data.[Select](Function(export) export.Importer)).ToList()
            For Each mapItem In mapItems
                Dim countryName As String = mapItem.Attributes(CStr(("NAME"))).Value.ToString()
                If Not nodeNames.Contains(countryName) Then countryName = mapItem.Attributes(CStr(("CONTINENT"))).Value.ToString()
                If Not Me.countryNameMapItemPairs.ContainsKey(countryName) Then Me.countryNameMapItemPairs.Add(countryName, mapItem)
                Dim buffPoints As System.Collections.Generic.List(Of DevExpress.Map.CoordPoint) = Nothing
                If Not territoryNameCenterCoordPairs.TryGetValue(countryName, buffPoints) Then
                    buffPoints = New System.Collections.Generic.List(Of DevExpress.Map.CoordPoint)()
                    territoryNameCenterCoordPairs.Add(countryName, buffPoints)
                End If

                For Each figure In CType(CType(mapItem, DevExpress.Xpf.Map.MapPath).Data, DevExpress.Xpf.Map.MapPathGeometry).Figures
                    For Each segment As DevExpress.Xpf.Map.MapPolyLineSegment In figure.Segments
                        buffPoints.AddRange(segment.Points)
                    Next
                Next
            Next

            Return territoryNameCenterCoordPairs
        End Function

        Private Sub CreateArrows(ByVal territoryNameBorderCoordPairs As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of DevExpress.Map.CoordPoint)))
            Me.exportMapArrowPairs = New System.Collections.Generic.Dictionary(Of SankeyDemo.Export, DevExpress.Xpf.Map.MapPolyline)()
            For Each export In Me.data
                Dim startPoint As DevExpress.Xpf.Map.GeoPoint = Me.GetAveragePoint(territoryNameBorderCoordPairs(export.Exporter))
                Dim endPoint As DevExpress.Xpf.Map.GeoPoint = Me.GetAveragePoint(territoryNameBorderCoordPairs(export.Importer))
                Dim polyline = New DevExpress.Xpf.Map.MapPolyline()
                polyline.Points.Add(startPoint)
                polyline.Points.Add(endPoint)
                Me.SetPolylineDrawOptions(polyline)
                Me.MapArrowStorage.Items.Add(polyline)
                If Not Me.countryNameExportPairs.ContainsKey(export.Exporter) Then Me.countryNameExportPairs.Add(export.Exporter, New System.Collections.Generic.List(Of SankeyDemo.Export)())
                Me.countryNameExportPairs(CStr((export.Exporter))).Add(export)
                Me.exportMapArrowPairs.Add(export, polyline)
            Next
        End Sub

        Private Sub SetPolylineDrawOptions(ByVal shape As DevExpress.Xpf.Map.MapPolyline)
            shape.IsGeodesic = True
            shape.EndLineCap = New DevExpress.Xpf.Map.MapLineCap() With {.Visible = True}
            Me.SetShapeDrawOptions(shape)
        End Sub

        Private Sub SetShapeDrawOptions(ByVal shape As DevExpress.Xpf.Map.MapShape)
            shape.Fill = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(209, 28, 28))
            shape.Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(209, 28, 28))
            shape.StrokeStyle = New DevExpress.Xpf.Map.StrokeStyle() With {.Thickness = 1.5}
            shape.Visible = False
        End Sub

        Private Function GetAveragePoint(ByVal points As System.Collections.Generic.List(Of DevExpress.Map.CoordPoint)) As GeoPoint
            Dim longitudeSum As Double = 0, latitudeSum As Double = 0
            For Each point As DevExpress.Xpf.Map.GeoPoint In points
                longitudeSum += point.Longitude
                latitudeSum += point.Latitude
            Next

            Return New DevExpress.Xpf.Map.GeoPoint(latitudeSum / points.Count, longitudeSum / points.Count)
        End Function

        Private Sub ShowArrowsByHighlightedItems(ByVal activeItems As System.Collections.ObjectModel.ObservableCollection(Of Object))
            Me.HideAllUnselectedArrows()
            If activeItems Is Nothing OrElse activeItems.Count = 0 Then Return
            Dim linkTags = New System.Collections.Generic.List(Of SankeyDemo.Export)()
            For Each tag As Object In activeItems
                If TypeOf tag Is SankeyDemo.Export Then linkTags.Add(CType(tag, SankeyDemo.Export))
            Next

            Me.SetArrowVisibilityByExport(linkTags, True)
        End Sub

        Private Sub ShowArrowsBySelectedItems(ByVal selectedItems As System.Collections.ObjectModel.ObservableCollection(Of Object))
            If Not Me.updateSelectedArrows Then Return
            Me.updateSelectedArrows = False
            Me.selectedMapItemsField.Clear()
            Dim linkTags = New System.Collections.Generic.List(Of SankeyDemo.Export)()
            For Each tag As Object In selectedItems
                If TypeOf tag Is SankeyDemo.Export Then linkTags.Add(CType(tag, SankeyDemo.Export))
            Next

            For Each export As SankeyDemo.Export In linkTags
                Dim mapItem = Me.countryNameMapItemPairs(export.Exporter)
                If Not Me.selectedMapItemsField.Contains(mapItem) Then Me.selectedMapItemsField.Add(mapItem)
                mapItem = Me.countryNameMapItemPairs(export.Importer)
                If Not Me.selectedMapItemsField.Contains(mapItem) Then Me.selectedMapItemsField.Add(mapItem)
            Next

            Me.updateSelectedArrows = True
            Me.SetArrowVisibilityByExport(linkTags, True)
            Me.HideAllUnselectedArrows()
        End Sub

        Private Sub ShowArrowsBySelectedMapItems(ByVal selectedMapItems As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem))
            If Not Me.updateSelectedArrows Then Return
            Me.updateSelectedArrows = False
            Me.selectedExportItemsField.Clear()
            For Each mapItem As DevExpress.Xpf.Map.MapItem In selectedMapItems
                Dim countryName As String = mapItem.Attributes(CStr(("NAME"))).Value.ToString()
                If Not Me.countryNameExportPairs.ContainsKey(countryName) Then countryName = mapItem.Attributes(CStr(("CONTINENT"))).Value.ToString()
                If Not Me.countryNameExportPairs.ContainsKey(countryName) Then Continue For
                For Each export In Me.countryNameExportPairs(countryName)
                    Me.selectedExportItemsField.Add(export)
                Next

                Me.SetArrowVisibilityByExport(Me.countryNameExportPairs(countryName), True)
            Next

            Me.updateSelectedArrows = True
            Me.HideAllUnselectedArrows()
        End Sub

        Private Sub HideAllUnselectedArrows()
            For Each linkTagMapArrowPair In Me.exportMapArrowPairs
                If Me.selectedExportItemsField Is Nothing OrElse Not Me.selectedExportItemsField.Contains(linkTagMapArrowPair.Key) Then linkTagMapArrowPair.Value.Visible = False
            Next
        End Sub

        Private Sub SetArrowVisibilityByExport(ByVal items As System.Collections.Generic.IEnumerable(Of SankeyDemo.Export), ByVal visible As Boolean)
            For Each link As SankeyDemo.Export In items
                Me.exportMapArrowPairs(CType((link), SankeyDemo.Export)).Visible = visible
            Next
        End Sub
    End Class

    Public Class ColorValueDictionaryToColorCollectionConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim continentColorPairs As System.Collections.Generic.Dictionary(Of String, System.Windows.Media.Color) = CType(value, System.Collections.Generic.Dictionary(Of String, System.Windows.Media.Color))
            Dim colorCollection = New DevExpress.Xpf.Map.ColorCollection()
            For Each color In continentColorPairs.Values
                colorCollection.Add(color)
            Next

            Return colorCollection
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class StringKeyDictionaryToColorizerKeyItemCollectionConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim continentColorPairs As System.Collections.Generic.Dictionary(Of String, System.Windows.Media.Color) = CType(value, System.Collections.Generic.Dictionary(Of String, System.Windows.Media.Color))
            Dim keyItemCollection = New DevExpress.Xpf.Map.ColorizerKeyItemCollection()
            For Each key In continentColorPairs.Keys
                keyItemCollection.Add(New DevExpress.Xpf.Map.ColorizerKeyItem() With {.Key = key, .Name = key})
            Next

            Return keyItemCollection
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class InteractionDemoToolTipConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim text As String = ""
            Dim totalImportAndExportText As String = ""
            If TypeOf value Is DevExpress.Xpf.Charts.Sankey.SankeyNode Then
                Dim node As DevExpress.Xpf.Charts.Sankey.SankeyNode = CType(value, DevExpress.Xpf.Charts.Sankey.SankeyNode)
                If node.InputLinks IsNot Nothing AndAlso node.InputLinks.Count > 0 Then
                    totalImportAndExportText += "Total import: " & System.Linq.Enumerable.[Select](Of DevExpress.Xpf.Charts.Sankey.SankeyLink, Global.System.[Double])(node.InputLinks, CType((Function(link) CDbl((link.TotalWeight))), System.Func(Of DevExpress.Xpf.Charts.Sankey.SankeyLink, System.[Double]))).Sum() & " million tonnes" & System.Environment.NewLine
                    text += "Import:" & System.Environment.NewLine
                    For Each inputLink In node.InputLinks
                        text += inputLink.TotalWeight & " million tonnes from " & inputLink.SourceNode.Tag.ToString() & System.Environment.NewLine
                    Next
                End If

                If node.OutputLinks IsNot Nothing AndAlso node.OutputLinks.Count > 0 Then
                    If Not String.IsNullOrEmpty(text) Then text += System.Environment.NewLine
                    totalImportAndExportText += "Total export: " & System.Linq.Enumerable.[Select](Of DevExpress.Xpf.Charts.Sankey.SankeyLink, Global.System.[Double])(node.OutputLinks, CType((Function(link) CDbl((link.TotalWeight))), System.Func(Of DevExpress.Xpf.Charts.Sankey.SankeyLink, System.[Double]))).Sum() & " million tonnes" & System.Environment.NewLine
                    text += "Export:" & System.Environment.NewLine
                    For Each outputLink In node.OutputLinks
                        text += outputLink.TotalWeight & " million tonnes to " & outputLink.TargetNode.Tag.ToString() & System.Environment.NewLine
                    Next
                End If

                text = text.Remove(text.Length - System.Environment.NewLine.Length, System.Environment.NewLine.Length)
                If Not String.IsNullOrEmpty(totalImportAndExportText) Then totalImportAndExportText += System.Environment.NewLine
            ElseIf TypeOf value Is DevExpress.Xpf.Charts.Sankey.SankeyLink Then
                Dim link As DevExpress.Xpf.Charts.Sankey.SankeyLink = CType(value, DevExpress.Xpf.Charts.Sankey.SankeyLink)
                text = System.[String].Format("${0} million tonnes", link.TotalWeight)
            End If

            Return totalImportAndExportText & text
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class
End Namespace
