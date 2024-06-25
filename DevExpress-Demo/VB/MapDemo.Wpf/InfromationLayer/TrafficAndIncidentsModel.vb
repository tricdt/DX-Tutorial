Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Media.Effects
Imports System.Windows.Threading
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class TrafficAndIncidentsModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private ReadOnly mainModule As MapDemo.TrafficAndIncidents

        Private ReadOnly itemsField As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem)

        Private ReadOnly trafficIncidentTimer As System.Windows.Threading.DispatcherTimer

        Private wayPointA As DevExpress.Xpf.Map.RouteWaypoint = New DevExpress.Xpf.Map.RouteWaypoint("", "")

        Private wayPointB As DevExpress.Xpf.Map.RouteWaypoint = New DevExpress.Xpf.Map.RouteWaypoint("", "")

        Private viewport As DevExpress.Map.MapBounds

        Private selectedTrafficIncidentTypeField As DevExpress.Xpf.Map.BingTrafficIncidentType

        Public ReadOnly Property Items As ObservableCollection(Of DevExpress.Xpf.Map.MapItem)
            Get
                Return Me.itemsField
            End Get
        End Property

        Public Property MapCenterPoint As CoordPoint

        Public Property SelectedTrafficIncidentType As BingTrafficIncidentType
            Get
                Return Me.selectedTrafficIncidentTypeField
            End Get

            Set(ByVal value As BingTrafficIncidentType)
                If Me.selectedTrafficIncidentTypeField <> value Then
                    Me.selectedTrafficIncidentTypeField = value
                    Me.RequestTrafficIncidents()
                End If
            End Set
        End Property

        Public Sub New(ByVal [module] As MapDemo.TrafficAndIncidents)
            Me.mainModule = [module]
            Me.MapCenterPoint = New DevExpress.Xpf.Map.GeoPoint(38.90507, -77.01909)
            Me.itemsField = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem)()
            Me.AddWaypoint("A", New DevExpress.Xpf.Map.GeoPoint(39.025538, -77.203833))
            Me.AddWaypoint("B", New DevExpress.Xpf.Map.GeoPoint(38.8501856, -76.9276787))
            Me.trafficIncidentTimer = New System.Windows.Threading.DispatcherTimer()
            Me.trafficIncidentTimer.Interval = System.TimeSpan.FromSeconds(2.5)
            AddHandler Me.trafficIncidentTimer.Tick, Sub(s, e) Me.RequestTrafficIncidents()
            Me.selectedTrafficIncidentTypeField = DevExpress.Xpf.Map.BingTrafficIncidentType.Accident Or DevExpress.Xpf.Map.BingTrafficIncidentType.Construction
        End Sub

        Private Sub RequestTrafficIncidents()
            Me.trafficIncidentTimer.[Stop]()
            Me.mainModule.IncidentLayer.ClearResults()
            If Me.SelectedTrafficIncidentType <> 0 Then Me.mainModule.IncidentDataProvider.RequestTrafficIncidents(New DevExpress.Xpf.Map.SearchBoundingBox() With {.WestLongitude = Me.viewport.Left, .NorthLatitude = Me.viewport.Top, .EastLongitude = Me.viewport.Right, .SouthLatitude = Me.viewport.Bottom}, DevExpress.Xpf.Map.BingTrafficIncidentSeverity.LowImpact Or DevExpress.Xpf.Map.BingTrafficIncidentSeverity.Minor Or DevExpress.Xpf.Map.BingTrafficIncidentSeverity.Moderate Or DevExpress.Xpf.Map.BingTrafficIncidentSeverity.Serious, Me.SelectedTrafficIncidentType)
        End Sub

        Public Sub AddWaypoint(ByVal description As String, ByVal location As DevExpress.Xpf.Map.GeoPoint)
            Dim waypoint As DevExpress.Xpf.Map.RouteWaypoint = New DevExpress.Xpf.Map.RouteWaypoint(description, location)
            Dim name As String
            If Me.wayPointB Is Nothing Then
                Me.wayPointB = waypoint
                name = "B"
            Else
                Me.wayPointA = waypoint
                Me.wayPointB = Nothing
                name = "A"
                Me.Items.Clear()
            End If

            Me.Items.Add(New DevExpress.Xpf.Map.MapPushpin() With {.Location = location, .Information = description, .TraceDepth = 0, .Text = name})
            Me.SendRouteRequest()
        End Sub

        Public Sub ProcessRouteItems(ByVal items As DevExpress.Xpf.Map.MapItem())
            Dim pushpinIndex As Integer = 0
            For Each item As DevExpress.Xpf.Map.MapItem In items
                Dim pushpin As DevExpress.Xpf.Map.MapPushpin = TryCast(item, DevExpress.Xpf.Map.MapPushpin)
                If pushpin IsNot Nothing Then
                    pushpin.Text = If(pushpinIndex = 0, "A", "B")
                    pushpinIndex += 1
                End If

                Dim polyline As DevExpress.Xpf.Map.MapPolyline = TryCast(item, DevExpress.Xpf.Map.MapPolyline)
                If polyline IsNot Nothing Then polyline.Effect = New System.Windows.Media.Effects.DropShadowEffect() With {.Direction = -90, .ShadowDepth = 1}
            Next

            Me.Items.Clear()
        End Sub

        Public Sub UpdateViewport(ByVal viewport As DevExpress.Map.MapBounds)
            Me.viewport = viewport
            Me.trafficIncidentTimer.[Stop]()
            Me.trafficIncidentTimer.Start()
        End Sub

        Public Sub SendRouteRequest()
            If Me.wayPointA IsNot Nothing AndAlso Me.wayPointB IsNot Nothing Then Me.mainModule.RouteDataProvider.CalculateRoute(New System.Collections.Generic.List(Of DevExpress.Xpf.Map.RouteWaypoint)() From {Me.wayPointA, Me.wayPointB})
        End Sub
    End Class

    Public Class BingRouteOptimizationsTypeConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Select Case CType(value, DevExpress.Xpf.Map.BingRouteOptimization)
                Case DevExpress.Xpf.Map.BingRouteOptimization.MinimizeTime
                    Return "Time"
                Case DevExpress.Xpf.Map.BingRouteOptimization.MinimizeTimeWithTraffic
                    Return "Time with Traffic"
                Case Else
                    Return "Distance"
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class BingTrafficIncidentTypeConverter
        Implements System.Windows.Data.IValueConverter

        Private Shared Function AggregateFunc(ByVal x As DevExpress.Xpf.Map.BingTrafficIncidentType, ByVal y As DevExpress.Xpf.Map.BingTrafficIncidentType) As BingTrafficIncidentType
            Return CSharpImpl.__Assign(x, y)
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim type As DevExpress.Xpf.Map.BingTrafficIncidentType = CType(value, DevExpress.Xpf.Map.BingTrafficIncidentType)
            Return System.[Enum].GetValues(GetType(DevExpress.Xpf.Map.BingTrafficIncidentType)).Cast(Of DevExpress.Xpf.Map.BingTrafficIncidentType)().Where(Function(c) type.HasFlag(c)).ToList()
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Dim list As System.Collections.Generic.List(Of Object) = TryCast(value, System.Collections.Generic.List(Of Object))
            If list Is Nothing Then Return CType(0, DevExpress.Xpf.Map.BingTrafficIncidentType)
            Return list.Cast(Of DevExpress.Xpf.Map.BingTrafficIncidentType)().Aggregate(New Global.System.Func(Of Global.DevExpress.Xpf.Map.BingTrafficIncidentType, Global.DevExpress.Xpf.Map.BingTrafficIncidentType, Global.DevExpress.Xpf.Map.BingTrafficIncidentType)(AddressOf MapDemo.BingTrafficIncidentTypeConverter.AggregateFunc))
        End Function

        Private Class CSharpImpl

            <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Class BingTrafficIncidentToStringTypeConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Select Case CType(value, DevExpress.Xpf.Map.BingTrafficIncidentType)
                Case DevExpress.Xpf.Map.BingTrafficIncidentType.DisabledVehicle
                    Return "Disabled Vehicle"
                Case DevExpress.Xpf.Map.BingTrafficIncidentType.MassTransit
                    Return "Mass Transit"
                Case DevExpress.Xpf.Map.BingTrafficIncidentType.OtherNews
                    Return "Other News"
                Case DevExpress.Xpf.Map.BingTrafficIncidentType.PlannedEvent
                    Return "Planned Event"
                Case DevExpress.Xpf.Map.BingTrafficIncidentType.RoadHazard
                    Return "Road Hazard"
            End Select

            Return value.ToString()
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class
End Namespace
