Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Map
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Class PuzzleLayoutGenerator

        Const HeightPadding As Double = 0.01

        Const WidthPadding As Double = 0.01

        Private ReadOnly items As IList(Of Tuple(Of MapPath, Rect))

        Private ReadOnly availableBounds As Rect

        Private ReadOnly projection As ProjectionBase = New SphericalMercatorProjection()

        Public Sub New(ByVal items As IEnumerable(Of MapItem))
            Me.items = New List(Of Tuple(Of MapPath, Rect))()
            For Each item As MapPath In items
                Dim itemBoundingBox As Rect = CalculateBoundingBox(item)
                Me.items.Add(New Tuple(Of MapPath, Rect)(item, itemBoundingBox))
            Next

            Dim leftTop As MapUnit = projection.GeoPointToMapUnit(New GeoPoint(15, -180))
            Dim rightBottom As MapUnit = projection.GeoPointToMapUnit(New GeoPoint(-62, -90))
            availableBounds = New Rect(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y)
        End Sub

        Private Function CalculateBoundingBox(ByVal item As ISupportCoordPoints) As Rect
            Dim maxLat As Double = Double.NegativeInfinity
            Dim minLat As Double = Double.PositiveInfinity
            Dim maxLon As Double = Double.NegativeInfinity
            Dim minLon As Double = Double.PositiveInfinity
            For Each point As GeoPoint In item.Points
                If maxLat < point.Latitude Then maxLat = point.Latitude
                If minLat > point.Latitude Then minLat = point.Latitude
                If maxLon < point.Longitude Then maxLon = point.Longitude
                If minLon > point.Longitude Then minLon = point.Longitude
            Next

            Dim corner1 As MapUnit = projection.GeoPointToMapUnit(New GeoPoint(maxLat, minLon))
            Dim corner2 As MapUnit = projection.GeoPointToMapUnit(New GeoPoint(minLat, maxLon))
            Return New Rect(corner1.X, corner1.Y, corner2.X - corner1.X, corner2.Y - corner1.Y)
        End Function

        Public Function GeneratePathInfos() As IEnumerable(Of MapPathInfo)
            Dim rnd As Random = New Random(Date.Now.Millisecond)
            Dim unusedItems As List(Of Tuple(Of MapPath, Rect)) = New List(Of Tuple(Of MapPath, Rect))(items)
            Dim result As List(Of MapPathInfo) = New List(Of MapPathInfo)()
            Dim availableHeight As Double = availableBounds.Height
            Dim y As Double = availableBounds.Top
            Dim x As Double = availableBounds.Left
            Dim maxWidth As Double = 0
            While unusedItems.Count > 0
                Dim availableItems As List(Of Tuple(Of MapPath, Rect)) = New List(Of Tuple(Of MapPath, Rect))()
                For Each item As Tuple(Of MapPath, Rect) In unusedItems
                    If item.Item2.Height < availableHeight Then availableItems.Add(item)
                Next

                If availableItems.Count > 0 Then
                    Dim index As Integer = rnd.Next(availableItems.Count)
                    Dim pair As Tuple(Of MapPath, Rect) = availableItems(index)
                    Dim gameCenter As MapUnit = New MapUnit(x + pair.Item2.Width / 2.0R, y + pair.Item2.Height / 2.0R)
                    result.Add(New MapPathInfo(pair.Item1, PuzzleViewModel.GetItemLocation(pair.Item1), CType(pair.Item1.GetCenter(), GeoPoint), projection.MapUnitToGeoPoint(gameCenter)))
                    unusedItems.Remove(pair)
                    availableHeight -=(pair.Item2.Height + HeightPadding)
                    y += pair.Item2.Height + HeightPadding
                    If pair.Item2.Width > maxWidth Then maxWidth = pair.Item2.Width
                Else
                    availableHeight = availableBounds.Height
                    x += maxWidth + WidthPadding
                    y = availableBounds.Top
                    maxWidth = 0
                End If
            End While

            Return result
        End Function
    End Class

    Public Class BindFunctionToSourceBehavior
        Inherits Behavior(Of MapControl)

        Public Shared ReadOnly FunctionProperty As DependencyProperty = DependencyProperty.Register("Function", GetType(Func(Of CoordPoint, Point)), GetType(BindFunctionToSourceBehavior))

        Public Property [Function] As Func(Of CoordPoint, Point)
            Get
                Return CType(GetValue(FunctionProperty), Func(Of CoordPoint, Point))
            End Get

            Set(ByVal value As Func(Of CoordPoint, Point))
                SetValue(FunctionProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            [Function] = Function(point) AssociatedObject.CoordPointToScreenPoint(point)
        End Sub
    End Class
End Namespace
