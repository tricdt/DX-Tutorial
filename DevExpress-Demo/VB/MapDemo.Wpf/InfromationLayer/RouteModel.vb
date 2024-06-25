Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media.Effects
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Enum RouteModelState
        Normal
        Drive
    End Enum

    <POCOViewModel>
    Public Class RouteModel
        Inherits ViewModelBase

        Private helpersField As ObservableCollection(Of MapItem)

        Private waypointsField As List(Of RouteWaypoint)

        Private routePathField As List(Of GeoPoint)

        Private waypointIndex As Integer

        Private searchPushpinsCount As Integer

        Public ReadOnly Property Helpers As ObservableCollection(Of MapItem)
            Get
                Return helpersField
            End Get
        End Property

        Public Overridable Property IsCalculating As Boolean

        Public Overridable Property State As RouteModelState

        Public Overridable Property DriveModel As DriveModel

        Public Overridable Property Navigator As DirectionsNavigation

        Public ReadOnly Property RoutePath As List(Of GeoPoint)
            Get
                Return routePathField
            End Get
        End Property

        Public Overridable Property DrivePathDistance As Double

        Public Overridable Property ItineraryItems As List(Of BingItineraryItem)

        Public Overridable Property RoutePushpins As List(Of MapPushpin)

        Public ReadOnly Property ActionText As String
            Get
                If State = RouteModelState.Drive AndAlso DriveModel IsNot Nothing Then
                    Return DriveModel.ActionText
                Else
                    If waypointsField.Count = 0 Then
                        If searchPushpinsCount > 0 Then
                            Return "Click the  pushpin to set a start point."
                        Else
                            Return "Click the map or use Search to find a location."
                        End If
                    ElseIf waypointsField.Count = 1 Then
                        Return "Set a finish point to calculate a route."
                    Else
                        Return "Set another finish point or click Drive."
                    End If
                End If
            End Get
        End Property

        Public Overridable Property MapCenter As CoordPoint

        Public Overridable Property MapAngle As Double

        Public Overridable Property InMotion As Boolean

        Public Overridable Property EnableRotation As Boolean

        Public ReadOnly Property Waypoints As List(Of RouteWaypoint)
            Get
                Return waypointsField
            End Get
        End Property

        Public Sub New(ByVal navigator As DirectionsNavigation)
            MapCenter = New GeoPoint(34.158506, -118.255629)
            Me.Navigator = navigator
            helpersField = New ObservableCollection(Of MapItem)()
            waypointsField = New List(Of RouteWaypoint)()
            RoutePushpins = New List(Of MapPushpin)()
            ItineraryItems = New List(Of BingItineraryItem)()
            EnableRotation = True
        End Sub

        Private Sub SendRouteRequest()
            IsCalculating = True
            If Waypoints.Count > 1 Then Navigator.RouteProvider.CalculateRoute(Waypoints)
        End Sub

        Private Function NextWaypointLetter() As String
            Dim letter As String = "" & Microsoft.VisualBasic.ChrW(Microsoft.VisualBasic.AscW("A"c) + waypointIndex Mod 26)
            waypointIndex += 1
            Return letter
        End Function

        Private Sub ExtractItineraryItems(ByVal result As BingRouteResult)
            ItineraryItems.Clear()
            For Each leg As BingRouteLeg In result.Legs
                For Each item As BingItineraryItem In leg.Itinerary
                    ItineraryItems.Add(item)
                Next
            Next
        End Sub

        Private Sub BeginDrive()
            If RoutePath IsNot Nothing AndAlso RoutePath.Count > 1 Then
                StopDrive()
                DriveModel = New DriveModel(Me)
                AddHandler DriveModel.CurrentLocationChanged, AddressOf OnCurrentLocationChanged
                InMotion = True
            End If
        End Sub

        Private Sub OnCurrentLocationChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
            MapCenter = DriveModel.CurrentLocation
            MapAngle = If(EnableRotation, DriveModel.CurrentDirection + 90.0, 0)
        End Sub

        Private Sub StopDrive()
            If DriveModel IsNot Nothing Then
                DriveModel.Cleanup()
                DriveModel = Nothing
                MapCenter = CType(MapCenter, GeoPoint)
                MapAngle = 0
            End If

            InMotion = False
        End Sub

        Protected Sub OnStateChanged()
            Select Case State
                Case RouteModelState.Drive
                    BeginDrive()
                Case RouteModelState.Normal
                    StopDrive()
            End Select

            RaisePropertyChanged("ActionText")
        End Sub

        Private Sub CalculatePathDistance()
            DrivePathDistance = 0
            If routePathField IsNot Nothing Then
                Dim lastPoint As MapUnit? = Nothing
                For Each node As GeoPoint In routePathField
                    If lastPoint IsNot Nothing Then
                        Dim currentPoint As MapUnit = Navigator.RouteLayer.GeoPointToMapUnit(node)
                        DrivePathDistance += DriveModel.DistanceBetweenPoints(currentPoint, lastPoint.Value)
                        lastPoint = currentPoint
                    Else
                        lastPoint = Navigator.RouteLayer.GeoPointToMapUnit(node)
                    End If
                Next
            End If
        End Sub

        Friend Sub RaiseDriveModelChanged()
            RaisePropertyChanged("ActionText")
        End Sub

        Friend Sub RaiseSearchPushpinsChanged(ByVal count As Integer)
            searchPushpinsCount = count
            RaisePropertyChanged("ActionText")
        End Sub

        Public Sub AddWaypoint(ByVal description As String, ByVal location As GeoPoint)
            Dim waypoint As RouteWaypoint = New RouteWaypoint(description, location)
            If Not waypointsField.Contains(waypoint) Then
                Dim pushpin As MapPushpin = New MapPushpin()
                pushpin.Location = location
                pushpin.Information = description
                pushpin.Text = NextWaypointLetter()
                pushpin.TraceDepth = 0
                pushpin.State = MapPushpinState.Busy
                Helpers.Add(pushpin)
                waypointsField.Add(waypoint)
                SendRouteRequest()
            End If

            RaisePropertyChanged("ActionText")
            RaisePropertyChanged("Waypoints")
        End Sub

        Public Sub ProcessRouteItems(ByVal items As MapItem())
            waypointIndex = 0
            RoutePushpins.Clear()
            For Each item As MapItem In items
                Dim pushpin As MapPushpin = TryCast(item, MapPushpin)
                If pushpin IsNot Nothing Then
                    pushpin.Text = NextWaypointLetter()
                    RoutePushpins.Add(pushpin)
                End If

                Dim polyline As MapPolyline = TryCast(item, MapPolyline)
                If polyline IsNot Nothing Then polyline.Effect = New DropShadowEffect() With {.Direction = -90, .ShadowDepth = 1}
            Next

            Helpers.Clear()
        End Sub

        Public Sub ProcessRouteResult(ByVal result As BingRouteResult)
            If result IsNot Nothing Then
                ExtractItineraryItems(result)
                routePathField = result.RoutePath
            Else
                routePathField = Nothing
            End If

            CalculatePathDistance()
        End Sub

        Public Sub Clear()
            If RoutePath IsNot Nothing Then RoutePath.Clear()
            Helpers.Clear()
            RoutePushpins.Clear()
            ItineraryItems.Clear()
            DrivePathDistance = 0.0
            waypointsField.Clear()
            waypointIndex = 0
            RaiseSearchPushpinsChanged(0)
            RaisePropertyChanged("Waypoints")
        End Sub

        Public Sub RotationStateChanged()
            EnableRotation = Not EnableRotation
        End Sub
    End Class

    Public Class RouteModelStateToButtonTextConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing AndAlso (value.GetType() Is GetType(RouteModelState)) Then
                Dim state As RouteModelState = CType(value, RouteModelState)
                Select Case state
                    Case RouteModelState.Drive
                        Return "Stop"
                    Case RouteModelState.Normal
                        Return "Drive"
                End Select
            End If

            Return String.Empty
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class RouteModelNormalStateToBoolConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing AndAlso (value.GetType() Is GetType(RouteModelState)) Then Return CType(value, RouteModelState) = RouteModelState.Normal
            Return False
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class WaypointsCountToDriveAbilityConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing AndAlso (value.GetType() Is GetType(Integer)) Then Return(CInt(value)) > 1
            Return False
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class InvertDoubleConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is Double, CDbl(value) * -1R, value)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
