Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Demos.DayAndNightLineCalculator
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class MapProjections
        Inherits MapDemo.MapDemoModule

        Private ReadOnly viewModelField As MapDemo.DayAndNightViewModel

        Private ReadOnly Property ViewModel As DayAndNightViewModel
            Get
                Return Me.viewModelField
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()
            Me.viewModelField = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New MapDemo.DayAndNightViewModel(Me.Map))
            Me.DataContext = Me.viewModelField
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.SetCurrentDateTime()
        End Sub

        Private Sub ButtonBackwardClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.SetPreviousDateTime()
        End Sub

        Private Sub ButtonForwardClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.SetNextDateTime()
        End Sub

        Private Sub OnProjectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.SelectedItemChangedEventArgs)
            If Me.ViewModel IsNot Nothing Then Call System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(New System.Action(Sub() Me.ViewModel.Update()))
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class DayAndNightViewModel
        Inherits DevExpress.Mvvm.BindableBase

        Const DiscreteHoursStep As Double = 0.5

        Const SteadilyHoursStep As Double = 24.5

        Private ReadOnly mapField As DevExpress.Xpf.Map.MapControl

        Private ReadOnly gridDataField As DevExpress.Xpf.Map.MapItemStorage = New DevExpress.Xpf.Map.MapItemStorage()

        Private ReadOnly excludeFromSouth As System.Collections.Generic.List(Of DevExpress.Xpf.Map.MapItem) = New System.Collections.Generic.List(Of DevExpress.Xpf.Map.MapItem)()

        Private timer As System.Windows.Threading.DispatcherTimer

        Protected ReadOnly Property Map As MapControl
            Get
                Return Me.mapField
            End Get
        End Property

        Public Overridable Property SunPosition As GeoPoint

        Public Overridable Property MoonPosition As GeoPoint

        Public Overridable Property DayAndNightLineVertices As CoordPointCollection

        Public Overridable Property IsSteady As Boolean

        Public Overridable Property DataContext As Object

        Public ReadOnly Property GridData As MapItemStorage
            Get
                Return Me.gridDataField
            End Get
        End Property

        Protected Sub OnIsSteadyChanged()
            Me.timer.IsEnabled = Me.IsSteady
        End Sub

        Public Overridable Property ActualDateTime As DateTime

        Protected Sub OnActualDateTimeChanged()
            Me.UpdateDayAndNightLine()
        End Sub

        Public Sub New(ByVal map As DevExpress.Xpf.Map.MapControl)
            Me.mapField = map
            Me.InitializeTimer()
            AddHandler Me.Map.Layers(CInt((0))).DataLoaded, AddressOf Me.DayAndNightViewModel_DataLoaded
            AddHandler Me.Map.Layers(CInt((0))).Unloaded, AddressOf Me.DayAndNightViewModel_Unloaded
            Me.IsSteady = True
            Me.SunPosition = New DevExpress.Xpf.Map.GeoPoint()
            Me.MoonPosition = New DevExpress.Xpf.Map.GeoPoint()
            Me.DayAndNightLineVertices = New DevExpress.Xpf.Map.CoordPointCollection()
            Me.ActualDateTime = System.DateTime.UtcNow
            Me.GenerateGrid()
        End Sub

        Private Sub DayAndNightViewModel_DataLoaded(ByVal sender As Object, ByVal e As DevExpress.Xpf.Map.DataLoadedEventArgs)
            For Each item As DevExpress.Xpf.Map.MapShape In CType(e, DevExpress.Xpf.Map.MapItemsLoadedEventArgs).Items
                Dim bounds As DevExpress.Map.MapBounds = item.GetBounds()
                If bounds.Width > 359.0 AndAlso bounds.Bottom < -89.9 Then Me.excludeFromSouth.Add(item)
            Next

            Me.ZoomToFit()
        End Sub

        Private Sub InitializeTimer()
            Me.timer = New System.Windows.Threading.DispatcherTimer With {.Interval = System.TimeSpan.FromMilliseconds(100), .IsEnabled = True}
            AddHandler Me.timer.Tick, AddressOf Me.OnTimerTick
        End Sub

        Private Sub UpdateDayAndNightLine()
            Dim sun3DPosition As Double() = DevExpress.Demos.DayAndNightLineCalculator.DayAndNightLineCalculator.CalculateSunPosition(Me.ActualDateTime)
            Dim sunPosition As DevExpress.Xpf.Map.GeoPoint = New DevExpress.Xpf.Map.GeoPoint(sun3DPosition(1), sun3DPosition(0))
            Dim moonPosition As DevExpress.Xpf.Map.GeoPoint = Me.GetOppositePoint(sunPosition)
            Dim dayAndNightLineVertices As DevExpress.Xpf.Map.CoordPointCollection = Me.GetDayAndNightLineVertices(sunPosition, 0.1)
            Dim isNorthNight As Boolean = DevExpress.Demos.DayAndNightLineCalculator.DayAndNightLineCalculator.CalculateIsNorthNight(sun3DPosition)
            If isNorthNight Then
                Me.AddNorthContour(dayAndNightLineVertices)
            Else
                Me.AddSouthContour(dayAndNightLineVertices)
            End If

            Me.SunPosition = sunPosition
            Me.MoonPosition = moonPosition
            Me.DayAndNightLineVertices = dayAndNightLineVertices
        End Sub

        Private Function GetOppositePoint(ByVal sunLocation As DevExpress.Xpf.Map.GeoPoint) As GeoPoint
            Dim lat As Double = -sunLocation.Latitude
            Dim lon As Double = sunLocation.Longitude + 180
            If lon > 180 Then lon -= 360
            Return New DevExpress.Xpf.Map.GeoPoint(lat, lon)
        End Function

        Private Function GetDayAndNightLineVertices(ByVal sunLocation As DevExpress.Xpf.Map.GeoPoint, ByVal [step] As Double) As CoordPointCollection
            Dim result As DevExpress.Xpf.Map.CoordPointCollection = New DevExpress.Xpf.Map.CoordPointCollection()
            Dim latitudes As System.Collections.Generic.IList(Of Double) = DevExpress.Demos.DayAndNightLineCalculator.DayAndNightLineCalculator.GetDayAndNightLineLatitudes(sunLocation.Latitude, sunLocation.Longitude, [step])
            Dim lon As Double = -180
            For Each lat As Double In latitudes
                result.Add(New DevExpress.Xpf.Map.GeoPoint(lat, lon))
                lon += [step]
            Next

            Return result
        End Function

        Private Sub AddNorthContour(ByVal dayAndNightLineVertices As DevExpress.Xpf.Map.CoordPointCollection)
            Dim initLat As Double = System.Math.Ceiling(CType(dayAndNightLineVertices(CInt((dayAndNightLineVertices.Count - 1))), DevExpress.Xpf.Map.GeoPoint).Latitude)
            For latForward As Double = initLat To 90.0
                dayAndNightLineVertices.Add(New DevExpress.Xpf.Map.GeoPoint(latForward, 180))
            Next

            For lon As Double = 180 To -180 Step -1
                dayAndNightLineVertices.Add(New DevExpress.Xpf.Map.GeoPoint(90, lon))
            Next

            initLat = System.Math.Ceiling(CType(dayAndNightLineVertices(CInt((0))), DevExpress.Xpf.Map.GeoPoint).Latitude)
            For latBackward As Double = 90 To initLat Step -1
                dayAndNightLineVertices.Add(New DevExpress.Xpf.Map.GeoPoint(latBackward, -180))
            Next
        End Sub

        Private Sub AddSouthContour(ByVal dayAndNightLineVertices As DevExpress.Xpf.Map.CoordPointCollection)
            Dim initLat As Double = System.Math.Ceiling(CType(dayAndNightLineVertices(CInt((dayAndNightLineVertices.Count - 1))), DevExpress.Xpf.Map.GeoPoint).Latitude)
            For lat As Double = initLat To -90.0 Step -1
                dayAndNightLineVertices.Add(New DevExpress.Xpf.Map.GeoPoint(lat, 180))
            Next

            For lon As Double = 180 To -180 Step -1
                dayAndNightLineVertices.Add(New DevExpress.Xpf.Map.GeoPoint(-90, lon))
            Next

            initLat = System.Math.Ceiling(CType(dayAndNightLineVertices(CInt((0))), DevExpress.Xpf.Map.GeoPoint).Latitude)
            For lat As Double = -90 To initLat
                dayAndNightLineVertices.Add(New DevExpress.Xpf.Map.GeoPoint(lat, -180))
            Next
        End Sub

        Private Function GetNextDateTime(ByVal dt As System.DateTime) As DateTime
            Return dt.AddHours(If(Me.IsSteady, MapDemo.DayAndNightViewModel.SteadilyHoursStep, MapDemo.DayAndNightViewModel.DiscreteHoursStep))
        End Function

        Private Function GetPreviousDateTime(ByVal dt As System.DateTime) As DateTime
            Return dt.AddHours(-MapDemo.DayAndNightViewModel.DiscreteHoursStep)
        End Function

        Private Sub GenerateGrid()
            Dim gridBrush As System.Windows.Media.Brush = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 255, 255, 255))
            gridBrush.Freeze()
            Call MapDemo.DayAndNightViewModel.GenerateLatitudes(Me.gridDataField, gridBrush)
            Call MapDemo.DayAndNightViewModel.GenerateLongitudes(Me.gridDataField, gridBrush)
        End Sub

        Private Sub SetVisibleEx(ByVal value As Boolean)
            Me.excludeFromSouth.ForEach(Sub(item) item.Visible = value)
        End Sub

        Private Sub ZoomToFit()
            Me.Map.EnableZooming = True
            Me.Map.ZoomToFitLayerItems(0.3)
            Me.Map.EnableZooming = False
        End Sub

        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.ActualDateTime = Me.GetNextDateTime(Me.ActualDateTime)
        End Sub

        Private Sub DayAndNightViewModel_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.timer.[Stop]()
            RemoveHandler Me.timer.Tick, AddressOf Me.OnTimerTick
        End Sub

        Private Shared Sub GenerateLatitudes(ByVal gridData As DevExpress.Xpf.Map.MapItemStorage, ByVal gridColor As System.Windows.Media.Brush)
            For i As Double = -90 To 90 Step 10
                Dim points As DevExpress.Xpf.Map.CoordPointCollection = New DevExpress.Xpf.Map.CoordPointCollection()
                For x As Integer = -180 To 180
                    points.Add(New DevExpress.Xpf.Map.GeoPoint(i, x))
                Next

                Dim line As DevExpress.Xpf.Map.MapPolyline = New DevExpress.Xpf.Map.MapPolyline() With {.Points = points, .StrokeStyle = New DevExpress.Xpf.Map.StrokeStyle() With {.Thickness = 1}, .Stroke = gridColor, .IsGeodesic = False}
                gridData.Items.Add(line)
            Next
        End Sub

        Private Shared Sub GenerateLongitudes(ByVal gridData As DevExpress.Xpf.Map.MapItemStorage, ByVal gridColor As System.Windows.Media.Brush)
            For i As Double = -180 To 180 Step 10
                Dim points As DevExpress.Xpf.Map.CoordPointCollection = New DevExpress.Xpf.Map.CoordPointCollection()
                For y As Integer = -90 To 90
                    points.Add(New DevExpress.Xpf.Map.GeoPoint(y, i))
                Next

                Dim line As DevExpress.Xpf.Map.MapPolyline = New DevExpress.Xpf.Map.MapPolyline() With {.Points = points, .StrokeStyle = New DevExpress.Xpf.Map.StrokeStyle() With {.Thickness = 1}, .Stroke = gridColor, .IsGeodesic = False}
                gridData.Items.Add(line)
            Next
        End Sub

        Public Sub SetCurrentDateTime()
            Me.IsSteady = False
            Me.ActualDateTime = System.DateTime.UtcNow
        End Sub

        Public Sub SetPreviousDateTime()
            Me.IsSteady = False
            Me.ActualDateTime = Me.GetPreviousDateTime(Me.ActualDateTime)
        End Sub

        Public Sub SetNextDateTime()
            Me.IsSteady = False
            Me.ActualDateTime = Me.GetNextDateTime(Me.ActualDateTime)
        End Sub

        Public Sub Update()
            Dim cs = CType(Me.Map.CoordinateSystem, DevExpress.Xpf.Map.GeoMapCoordinateSystem)
            Me.SetVisibleEx(Not(TypeOf cs.Projection Is MapDemo.NorthPole))
            Me.ZoomToFit()
            If TypeOf cs.Projection Is DevExpress.Xpf.Map.LambertAzimuthalEqualAreaProjectionBase Then Me.Map.CenterPoint = New DevExpress.Xpf.Map.GeoPoint(CType(cs.Projection, DevExpress.Xpf.Map.LambertAzimuthalEqualAreaProjectionBase).OriginLatitude, CType(cs.Projection, DevExpress.Xpf.Map.LambertAzimuthalEqualAreaProjectionBase).CentralMeridian)
        End Sub
    End Class

    Public Class Projection

        Public Property Name As String

        Public Property PrjInstance As ProjectionBase

        Public Property ParentPrjName As String
    End Class

    Public Class SouthPole
        Inherits DevExpress.Xpf.Map.LambertAzimuthalEqualAreaProjectionBase

        Protected Overrides ReadOnly Property IsPredefined As Boolean
            Get
                Return False
            End Get
        End Property

        Public Sub New()
            Me.OriginLatitude = -90.0
            Me.BoundingBox = New DevExpress.Map.MapBounds(-180.0, -90.0, 180.0, 0.0)
        End Sub

        Public Overrides Function MapUnitToGeoPoint(ByVal mapPoint As DevExpress.Xpf.Map.MapUnit) As GeoPoint
            Dim res As DevExpress.Xpf.Map.GeoPoint = MyBase.MapUnitToGeoPoint(mapPoint)
            If mapPoint.X > 0.5 AndAlso mapPoint.Y > 0.5 Then res = New DevExpress.Xpf.Map.GeoPoint(res.GetY(), 180.0 + res.GetX())
            If mapPoint.X <= 0.5 AndAlso mapPoint.Y > 0.5 Then res = New DevExpress.Xpf.Map.GeoPoint(res.GetY(), res.GetX() - 180.0)
            Return res
        End Function
    End Class

    Public Class NorthPole
        Inherits DevExpress.Xpf.Map.LambertAzimuthalEqualAreaProjectionBase

        Protected Overrides ReadOnly Property IsPredefined As Boolean
            Get
                Return False
            End Get
        End Property

        Public Sub New()
            Me.OriginLatitude = 90.0
            Me.BoundingBox = New DevExpress.Map.MapBounds(-180.0, 0.0, 180.0, 90.0)
        End Sub

        Public Overrides Function MapUnitToGeoPoint(ByVal mapPoint As DevExpress.Xpf.Map.MapUnit) As GeoPoint
            Dim res As DevExpress.Xpf.Map.GeoPoint = MyBase.MapUnitToGeoPoint(mapPoint)
            If mapPoint.X >= 0.5 AndAlso mapPoint.Y < 0.5 Then res = New DevExpress.Xpf.Map.GeoPoint(res.GetY(), 180.0 + res.GetX())
            If mapPoint.X < 0.5 AndAlso mapPoint.Y < 0.5 Then res = New DevExpress.Xpf.Map.GeoPoint(res.GetY(), res.GetX() - 180.0)
            Return res
        End Function
    End Class
End Namespace
