Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Map
Imports DevExpress.Xpf.Map.Native
Imports System.Runtime.InteropServices

Namespace MapDemo

    Public Class DriveModel
        Inherits BindableBase

        Private _DriveItems As ObservableCollection(Of DevExpress.Xpf.Map.MapItem)

        Const driveSpeed As Double = 100.0

        Const driveTicksPerSecond As Double = 100

        Const driveTimeQuant As Double = 1 / driveTicksPerSecond

        Private drivePushpin As MapPushpin

        Private drivePath As MapPolyline

        Private parentModel As RouteModel

        Private routePath As List(Of GeoPoint)

        Private routeNodeIndex As Integer

        Private baseLocation As GeoPoint

        Private targetLocation As GeoPoint

        Private basePoint As MapUnit

        Private targetPoint As MapUnit

        Private distance As Double

        Private currentDistance As Double

        Private currentDirectionField As Double

        Private currentPoint As MapUnit

        Private itineraryItems As List(Of BingItineraryItem)

        Private routePushpins As List(Of MapPushpin)

        Private animationTimerField As DispatcherTimer

        Private actionTextField As String

        Public Event CurrentLocationChanged As PropertyChangedEventHandler

        Public Sub RaiseCurrentLocationChanged()
            RaiseEvent CurrentLocationChanged(Me, New PropertyChangedEventArgs("CurrentLocation"))
        End Sub

        Public Property DriveItems As ObservableCollection(Of MapItem)
            Get
                Return _DriveItems
            End Get

            Private Set(ByVal value As ObservableCollection(Of MapItem))
                _DriveItems = value
            End Set
        End Property

        Public Property ActionText As String
            Get
                Return actionTextField
            End Get

            Set(ByVal value As String)
                If SetProperty(actionTextField, value, "ActionText") Then parentModel.RaiseDriveModelChanged()
            End Set
        End Property

        Public ReadOnly Property CurrentLocation As GeoPoint
            Get
                Return parentModel.Navigator.RouteLayer.MapUnitToGeoPoint(currentPoint)
            End Get
        End Property

        Public ReadOnly Property CurrentDirection As Double
            Get
                Return currentDirectionField
            End Get
        End Property

        Shared Public Function DistanceBetweenPoints(ByVal a As MapUnit, ByVal b As MapUnit) As Double
            Dim vector As MapUnit = New MapUnit(b.X - a.X, b.Y - a.Y)
            Return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y)
        End Function

        Shared Public Function KilometerPerHourToMapUnitsPerSecond(ByVal kmh As Double) As Double
            Return kmh / 40000.0 / 360.0
        End Function

        Public Sub New(ByVal parentModel As RouteModel)
            Me.parentModel = parentModel
            routePath = parentModel.RoutePath
            itineraryItems = New List(Of BingItineraryItem)()
            itineraryItems.AddRange(parentModel.ItineraryItems)
            routePushpins = New List(Of MapPushpin)()
            routePushpins.AddRange(parentModel.RoutePushpins)
            drivePushpin = New MapPushpin()
            drivePushpin.Location = routePath(0)
            drivePushpin.MarkerTemplate = parentModel.Navigator.DriveMarkerTemplate
            drivePath = New MapPolyline()
            drivePath.Stroke = parentModel.Navigator.DriveBrush
            drivePath.StrokeStyle = New StrokeStyle() With {.Thickness = 4, .StartLineCap = PenLineCap.Round, .EndLineCap = PenLineCap.Round}
            drivePath.Points.Add(targetLocation)
            targetLocation = routePath(0)
            DriveItems = New ObservableCollection(Of MapItem)()
            DriveItems.Add(drivePath)
            DriveItems.Add(drivePushpin)
            animationTimerField = New DispatcherTimer()
            animationTimerField.Interval = TimeSpan.FromSeconds(driveTimeQuant)
            AddHandler animationTimerField.Tick, New EventHandler(AddressOf AnimationTimer)
            Advance()
        End Sub

        Private Sub CheckItinerary()
            Dim routeLayer As InformationLayer = parentModel.Navigator.RouteLayer
            Dim location As GeoPoint = routeLayer.MapUnitToGeoPoint(currentPoint)
            Dim currentItem As BingItineraryItem = itineraryItems(0)
            Dim geoSize As Size = New Size(Math.Abs(location.Latitude - currentItem.Location.Latitude), Math.Abs(location.Longitude - currentItem.Location.Longitude))
            Dim metricSize As Size = routeLayer.GeoToKilometersSize(location, geoSize)
            Dim distance As Double = Math.Sqrt(metricSize.Width * metricSize.Width + metricSize.Height * metricSize.Height)
            If distance < 0.005 Then
                If itineraryItems.Count > 1 Then
                    itineraryItems.Remove(currentItem)
                Else
                    distance = 0.0
                End If
            End If

            If distance > 0.0 Then
                ActionText = CommonUtils.GetUserFriendlyEnumString(itineraryItems(0).Maneuver) & " after " & If(distance > 0.9, String.Format("{0:0} km", Math.Ceiling(distance)), String.Format("{0:0} m", Math.Ceiling(distance * 10) * 100))
            Else
                ActionText = "Finish! Click Stop and Clear to set a new route."
            End If
        End Sub

        Private Sub CheckRoutePushpins(ByVal location As GeoPoint)
            For Each pushpin As MapPushpin In routePushpins
                Dim pushpinLocation As GeoPoint = CType(pushpin.Location, GeoPoint)
                If Math.Abs(pushpinLocation.Latitude - location.Latitude) < 0.00001 AndAlso Math.Abs(pushpinLocation.Longitude - location.Longitude) < 0.00001 Then pushpin.Brush = parentModel.Navigator.DriveBrush
            Next
        End Sub

        Private Function Advance() As Boolean
            If routeNodeIndex < routePath.Count - 1 Then
                routeNodeIndex += 1
                Dim routeLayer As InformationLayer = parentModel.Navigator.RouteLayer
                baseLocation = targetLocation
                currentPoint = routeLayer.GeoPointToMapUnit(baseLocation)
                targetLocation = routePath(routeNodeIndex)
                basePoint = routeLayer.GeoPointToMapUnit(baseLocation)
                targetPoint = routeLayer.GeoPointToMapUnit(targetLocation)
                distance = DistanceBetweenPoints(targetPoint, basePoint)
                currentDistance = 0
                drivePath.Points(drivePath.Points.Count - 1) = baseLocation
                drivePath.Points.Add(baseLocation)
                CheckItinerary()
                CheckRoutePushpins(baseLocation)
                If Not animationTimerField.IsEnabled Then animationTimerField.Start()
                Return True
            Else
                If routePushpins.Count > 0 Then routePushpins(routePushpins.Count - 1).Brush = parentModel.Navigator.DriveBrush
                If animationTimerField.IsEnabled Then animationTimerField.Stop()
                drivePushpin.Visible = False
                Return False
            End If
        End Function

        Private Sub AnimationTimer(ByVal sender As Object, ByVal e As EventArgs)
            Dim scaledTime As Double = driveTimeQuant
            While scaledTime > 0.0
                Dim quant As Double = Math.Min(scaledTime, driveTimeQuant)
                Dim excess As Double = Update(quant * KilometerPerHourToMapUnitsPerSecond(driveSpeed))
                If excess > 0.0 Then
                    If Not Advance() Then
                        CheckItinerary()
                        PlaceItems()
                        Return
                    End If

                    excess = Update(excess)
                End If

                PlaceItems()
                CheckItinerary()
                scaledTime -= quant
            End While
        End Sub

        Private Sub PlaceItems()
            PlaceItems(parentModel.Navigator.RouteLayer.MapUnitToGeoPoint(currentPoint))
            RaisePropertyChanged("CurrentLocation")
            RaiseCurrentLocationChanged()
        End Sub

        Private Sub PlaceItems(ByVal location As GeoPoint)
            drivePath.Points(drivePath.Points.Count - 1) = location
            drivePushpin.Location = location
        End Sub

        Private Function GetDirection(<Out> ByRef angle As Double) As MapUnit
            Dim direction As MapUnit = New MapUnit(targetPoint.X - basePoint.X, targetPoint.Y - basePoint.Y)
            Dim length As Double = Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y)
            If length > 0.0 Then
                Dim oneByLength As Double = 1 / length
                direction.X *= oneByLength
                direction.Y *= oneByLength
            End If

            angle = Math.Atan2(direction.Y, direction.X) * 180.0 / Math.PI
            If angle < 0.0 Then angle += 360.0
            Return direction
        End Function

        Private Function CalculateNavAngle(ByVal direction As Double) As Double
            Dim angle As Double = direction
            Const delta As Double = 2.0
            If Math.Abs(direction - currentDirectionField) > delta Then
                Dim a As Double =(360.0 + direction - currentDirectionField) Mod 360
                angle = currentDirectionField + If(a > 180, -delta, delta)
            End If

            angle = angle Mod 360
            If angle < 0 Then angle += 360
            Return angle
        End Function

        Private Function Update(ByVal distanceToGo As Double) As Double
            currentDistance += distanceToGo
            If currentDistance > distance Then
                currentPoint = targetPoint
                Return currentDistance - distance
            End If

            Dim direction As Double = 0
            Dim offset As MapUnit = GetDirection(direction)
            offset.X *= currentDistance
            offset.Y *= currentDistance
            currentPoint = New MapUnit(basePoint.X + offset.X, basePoint.Y + offset.Y)
            currentDirectionField = CalculateNavAngle(direction)
            Return 0.0
        End Function

        Public Sub Cleanup()
            For Each pushpin As MapPushpin In routePushpins
                pushpin.Brush = parentModel.Navigator.DefaultBrush
            Next

            If animationTimerField.IsEnabled Then animationTimerField.Stop()
        End Sub
    End Class
End Namespace
