Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Threading
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class MapElements
        Inherits MapDemo.MapDemoModule

        Private dataGenerator As MapDemo.FlightMapDataGenerator

        Public Sub New()
            Me.InitializeComponent()
            Me.dataGenerator = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New MapDemo.FlightMapDataGenerator())
            Me.DataContext = Me.dataGenerator
            Me.dataGenerator.SpeedScale = System.Convert.ToDouble(Me.tbSpeedScale.Value)
            AddHandler ModuleUnloaded, AddressOf Me.MapElements_Unloaded
        End Sub

        Private Sub MapElements_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.dataGenerator.Dispose()
        End Sub

        Private Sub tbSpeedScale_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Me.dataGenerator IsNot Nothing Then Me.dataGenerator.SpeedScale = System.Convert.ToDouble(e.NewValue)
        End Sub
    End Class

    Public Class PlaneTrajectory

        Private _TrajectoryPresentation As MapPolyline

        Private Class TrajectoryPart

            Private ReadOnly startPointField As DevExpress.Xpf.Map.GeoPoint

            Private ReadOnly endPointField As DevExpress.Xpf.Map.GeoPoint

            Private ReadOnly flightTimeField As Double

            Private ReadOnly courseField As Double

            Public ReadOnly Property StartPoint As GeoPoint
                Get
                    Return Me.startPointField
                End Get
            End Property

            Public ReadOnly Property EndPoint As GeoPoint
                Get
                    Return Me.endPointField
                End Get
            End Property

            Public ReadOnly Property FlightTime As Double
                Get
                    Return Me.flightTimeField
                End Get
            End Property

            Public ReadOnly Property Course As Double
                Get
                    Return Me.courseField
                End Get
            End Property

            Public Sub New(ByVal projection As DevExpress.Xpf.Map.ProjectionBase, ByVal startPoint As DevExpress.Xpf.Map.GeoPoint, ByVal endPoint As DevExpress.Xpf.Map.GeoPoint, ByVal speedInKmH As Double)
                Me.startPointField = startPoint
                Me.endPointField = endPoint
                Dim sizeInKm As System.Windows.Size = projection.GeoToKilometersSize(startPoint, New System.Windows.Size(System.Math.Abs(startPoint.Longitude - endPoint.Longitude), System.Math.Abs(startPoint.Latitude - endPoint.Latitude)))
                Dim partlength As Double = System.Math.Sqrt(sizeInKm.Width * sizeInKm.Width + sizeInKm.Height * sizeInKm.Height)
                Me.flightTimeField = partlength / speedInKmH
                Me.courseField = System.Math.Atan2(endPoint.Longitude - startPoint.Longitude, endPoint.Latitude - startPoint.Latitude) * 180 / System.Math.PI
            End Sub

            Public Function GetPointByCurrentFlightTime(ByVal currentFlightTime As Double) As GeoPoint
                If currentFlightTime > Me.FlightTime Then Return Me.endPointField
                Dim ratio As Double = currentFlightTime / Me.FlightTime
                Return New DevExpress.Xpf.Map.GeoPoint(Me.startPointField.Latitude + ratio * (Me.endPointField.Latitude - Me.startPointField.Latitude), Me.startPointField.Longitude + ratio * (Me.endPointField.Longitude - Me.startPointField.Longitude))
            End Function
        End Class

        Private ReadOnly trajectory As System.Collections.Generic.List(Of MapDemo.PlaneTrajectory.TrajectoryPart) = New System.Collections.Generic.List(Of MapDemo.PlaneTrajectory.TrajectoryPart)()

        Private ReadOnly projection As DevExpress.Xpf.Map.SphericalMercatorProjection = New DevExpress.Xpf.Map.SphericalMercatorProjection()

        Public ReadOnly Property StartPoint As GeoPoint
            Get
                Return If((Me.trajectory.Count > 0), Me.trajectory(CInt((0))).StartPoint, New DevExpress.Xpf.Map.GeoPoint(0, 0))
            End Get
        End Property

        Public ReadOnly Property EndPoint As GeoPoint
            Get
                Return If((Me.trajectory.Count > 0), Me.trajectory(CInt((Me.trajectory.Count - 1))).EndPoint, New DevExpress.Xpf.Map.GeoPoint(0, 0))
            End Get
        End Property

        Public ReadOnly Property FlightTime As Double
            Get
                Dim result As Double = 0.0
                For Each part As MapDemo.PlaneTrajectory.TrajectoryPart In Me.trajectory
                    result += part.FlightTime
                Next

                Return result
            End Get
        End Property

        Public Property TrajectoryPresentation As MapPolyline
            Get
                Return _TrajectoryPresentation
            End Get

            Private Set(ByVal value As MapPolyline)
                _TrajectoryPresentation = value
            End Set
        End Property

        Public Sub New(ByVal points As DevExpress.Xpf.Map.CoordPointCollection, ByVal speedInKmH As Double)
            Me.TrajectoryPresentation = New DevExpress.Xpf.Map.MapPolyline() With {.Points = points, .IsGeodesic = True}
            Dim geoPoints As System.Collections.Generic.List(Of DevExpress.Xpf.Map.GeoPoint) = Me.TrajectoryPresentation.ActualPoints.Cast(Of DevExpress.Xpf.Map.GeoPoint)().ToList()
            For i As Integer = 0 To geoPoints.Count - 1 - 1
                Me.trajectory.Add(New MapDemo.PlaneTrajectory.TrajectoryPart(Me.projection, geoPoints(i), geoPoints(i + 1), speedInKmH))
            Next
        End Sub

        Public Function GetPointByCurrentFlightTime(ByVal currentFlightTime As Double) As GeoPoint
            Dim time As Double = 0.0
            For i As Integer = 0 To Me.trajectory.Count - 1 - 1
                If Me.trajectory(CInt((i))).FlightTime > currentFlightTime - time Then Return Me.trajectory(CInt((i))).GetPointByCurrentFlightTime(currentFlightTime - time)
                time += Me.trajectory(CInt((i))).FlightTime
            Next

            Return Me.trajectory(CInt((Me.trajectory.Count - 1))).GetPointByCurrentFlightTime(currentFlightTime - time)
        End Function

        Public Function GetCourseByCurrentFlightTime(ByVal currentFlightTime As Double) As Double
            Dim time As Double = 0.0
            For i As Integer = 0 To Me.trajectory.Count - 1 - 1
                If Me.trajectory(CInt((i))).FlightTime > currentFlightTime - time Then Return Me.trajectory(CInt((i))).Course
                time += Me.trajectory(CInt((i))).FlightTime
            Next

            Return Me.trajectory(CInt((Me.trajectory.Count - 1))).Course
        End Function
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class PlaneInfo
        Inherits DevExpress.Mvvm.BindableBase

        Public Sub New()
            Me.CurrentFlightTime = 0
            Me.Course = 0
        End Sub

        Public Overridable Property CurrentFlightTime As Double

        Public Overridable Property Position As GeoPoint

        Public Overridable Property Course As Double

        Protected Sub OnCurrentFlightTimeChanged()
            Me.UpdatePosition(Me.CurrentFlightTime)
        End Sub

        Private Shared Function ConvertPlaneNameToFilePath(ByVal PlaneName As String) As String
            Dim result As String = PlaneName.Replace(" ", "")
            result = "../Images/Planes/" & result.Replace("-", "") & ".png"
            Return result
        End Function

        Private isLandedField As Boolean = False

        Private ReadOnly planeIDField As String

        Private ReadOnly nameField As String

        Private ReadOnly endPointNameField As String

        Private ReadOnly startPointNameField As String

        Private ReadOnly speedInKmHField As Double

        Private ReadOnly flightAltitudeField As Double

        Private ReadOnly imagePathField As String

        Private ReadOnly trajectoryField As MapDemo.PlaneTrajectory

        Public ReadOnly Property PlaneID As String
            Get
                Return Me.planeIDField
            End Get
        End Property

        Public ReadOnly Property Name As String
            Get
                Return Me.nameField
            End Get
        End Property

        Public ReadOnly Property EndPointName As String
            Get
                Return Me.endPointNameField
            End Get
        End Property

        Public ReadOnly Property StartPointName As String
            Get
                Return Me.startPointNameField
            End Get
        End Property

        Public ReadOnly Property StartPoint As GeoPoint
            Get
                Return Me.trajectoryField.StartPoint
            End Get
        End Property

        Public ReadOnly Property EndPoint As GeoPoint
            Get
                Return Me.trajectoryField.EndPoint
            End Get
        End Property

        Public ReadOnly Property SpeedKmH As Double
            Get
                Return If(Me.isLandedField, 0.0, Me.speedInKmHField)
            End Get
        End Property

        Public ReadOnly Property FlightAltitude As Double
            Get
                Return If(Me.isLandedField, 0.0, Me.flightAltitudeField)
            End Get
        End Property

        Public ReadOnly Property ImagePath As String
            Get
                Return Me.imagePathField
            End Get
        End Property

        Public ReadOnly Property IsLanded As Boolean
            Get
                Return Me.isLandedField
            End Get
        End Property

        Public ReadOnly Property TotalFlightTime As Double
            Get
                Return Me.trajectoryField.FlightTime
            End Get
        End Property

        Public ReadOnly Property TrajectoryPresentation As MapPolyline
            Get
                Return Me.trajectoryField.TrajectoryPresentation
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal id As String, ByVal endPointName As String, ByVal startPointName As String, ByVal speedInKmH As Double, ByVal flightAltitude As Double, ByVal points As DevExpress.Xpf.Map.CoordPointCollection)
            Me.nameField = name
            Me.planeIDField = id
            Me.endPointNameField = endPointName
            Me.startPointNameField = startPointName
            Me.speedInKmHField = speedInKmH
            Me.flightAltitudeField = flightAltitude
            Me.trajectoryField = New MapDemo.PlaneTrajectory(points, speedInKmH)
            Me.imagePathField = MapDemo.PlaneInfo.ConvertPlaneNameToFilePath(name)
            Me.UpdatePosition(Me.CurrentFlightTime)
        End Sub

        Private Sub UpdatePosition(ByVal flightTime As Double)
            Me.isLandedField = flightTime >= Me.trajectoryField.FlightTime
            Me.Position = Me.trajectoryField.GetPointByCurrentFlightTime(flightTime)
            Me.Course = Me.trajectoryField.GetCourseByCurrentFlightTime(flightTime)
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class AirportInfo

        Public Overridable Property Location As GeoPoint
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class FlightMapDataGenerator
        Inherits DevExpress.Mvvm.BindableBase
        Implements System.IDisposable

        Public Overridable Property Planes As ObservableCollection(Of MapDemo.PlaneInfo)

        Public Overridable Property Airports As ObservableCollection(Of MapDemo.AirportInfo)

        Public Overridable Property ActualAirPaths As ObservableCollection(Of DevExpress.Xpf.Map.MapItem)

        Public Overridable Property SelectedPlaneInfo As PlaneInfo

        Public Overridable Property SpeedScale As Double

        Protected Sub OnSelectedPlaneInfoChanged()
            Me.Airports = If(Me.SelectedPlaneInfo IsNot Nothing, New System.Collections.ObjectModel.ObservableCollection(Of MapDemo.AirportInfo)() From {New MapDemo.AirportInfo() With {.Location = Me.SelectedPlaneInfo.StartPoint}, New MapDemo.AirportInfo() With {.Location = Me.SelectedPlaneInfo.EndPoint}}, Nothing)
            Me.ActualAirPaths = If(Me.SelectedPlaneInfo IsNot Nothing, New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem)() From {Me.SelectedPlaneInfo.TrajectoryPresentation}, Nothing)
        End Sub

        Const mSecPerHour As Double = 3600000

        Private ReadOnly timer As System.Windows.Threading.DispatcherTimer = New System.Windows.Threading.DispatcherTimer()

        Private ReadOnly planesInfo As System.Collections.ObjectModel.ObservableCollection(Of MapDemo.PlaneInfo) = New System.Collections.ObjectModel.ObservableCollection(Of MapDemo.PlaneInfo)()

        Private lastTime As System.DateTime

        Public Sub New()
            Me.LoadFromXML()
            AddHandler Me.timer.Tick, New System.EventHandler(AddressOf Me.OnTimedEvent)
            Me.timer.Interval = New System.TimeSpan(0, 0, 2)
            Me.lastTime = System.DateTime.Now
            Me.timer.Start()
            If Me.Planes IsNot Nothing Then Me.SelectedPlaneInfo = Me.Planes(1)
        End Sub

        Private Sub LoadFromXML()
            Dim document As System.Xml.Linq.XDocument = MapDemo.DataLoader.LoadXmlFromResources("/Data/FlightMap.xml")
            If document IsNot Nothing Then
                For Each element As System.Xml.Linq.XElement In document.Element(CType(("Planes"), System.Xml.Linq.XName)).Elements()
                    Dim points As DevExpress.Xpf.Map.CoordPointCollection = New DevExpress.Xpf.Map.CoordPointCollection()
                    For Each infoElement As System.Xml.Linq.XElement In element.Element(CType(("Path"), System.Xml.Linq.XName)).Elements()
                        points.Add(New DevExpress.Xpf.Map.GeoPoint(System.Convert.ToDouble(infoElement.Element(CType(("Latitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(infoElement.Element(CType(("Longitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture)))
                    Next

                    Dim info As MapDemo.PlaneInfo = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New MapDemo.PlaneInfo(element.Element(CType(("PlaneName"), System.Xml.Linq.XName)).Value, element.Element(CType(("PlaneID"), System.Xml.Linq.XName)).Value, element.Element(CType(("EndPointName"), System.Xml.Linq.XName)).Value, element.Element(CType(("StartPointName"), System.Xml.Linq.XName)).Value, System.Convert.ToInt32(element.Element(CType(("Speed"), System.Xml.Linq.XName)).Value), System.Convert.ToInt32(element.Element(CType(("Altitude"), System.Xml.Linq.XName)).Value), points))
                    info.CurrentFlightTime = System.Convert.ToDouble(element.Element(CType(("CurrentFlightTime"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture)
                    Me.planesInfo.Add(info)
                Next
            End If

            Me.Planes = Me.planesInfo
        End Sub

        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As System.EventArgs)
            Dim currentTime As System.DateTime = System.DateTime.Now
            Dim interval As System.TimeSpan = currentTime.Subtract(Me.lastTime)
            For Each info As MapDemo.PlaneInfo In Me.planesInfo
                If Not info.IsLanded Then info.CurrentFlightTime += Me.SpeedScale * interval.TotalMilliseconds / MapDemo.FlightMapDataGenerator.mSecPerHour
            Next

            Me.lastTime = currentTime
        End Sub

        Public Sub Dispose() Implements Global.System.IDisposable.Dispose
            Me.timer.[Stop]()
            RemoveHandler Me.timer.Tick, AddressOf Me.OnTimedEvent
        End Sub
    End Class
End Namespace
