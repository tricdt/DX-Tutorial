Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <POCOViewModel>
    Public Class GpxDataAdapterViewModel

        Private highlightedPoint As MapDot

        Public Overridable Property Storage As MapItemStorage

        Public Overridable Property Activities As DataTable

        Public Overridable Property Info As GpxTrackInfo

        Public Overridable Property CurrentTrackUri As Uri

        Public Overridable Property CurrentActivity As DataRowView

        Public Overridable Property IsMovingPointVisible As Boolean

        Public Overridable Property MovingPointLocation As GeoPoint

        Public Sub New()
            FillActivities()
        End Sub

        Private Sub PopulatePoints(ByVal track As MapPath)
            Dim titleTemplate As DataTemplate = XamlHelper.GetTemplate("<Grid> <TextBlock Text=""{Binding Path=Text}"" Foreground=""White"" HorizontalAlignment=""Center"" VerticalAlignment=""Center""/> </Grid>")
            Dim points As CoordPointCollection = CType(CType(track.Data, MapPathGeometry).Figures(0).Segments(0), MapPolyLineSegment).Points
            Dim storage As MapItemStorage = New MapItemStorage()
            highlightedPoint = New MapDot() With {.Visible = False, .Size = 14}
            highlightedPoint.Location = points.First()
            storage.Items.Add(highlightedPoint)
            storage.Items.Add(New MapDot() With {.Location = points.First(), .Size = 20, .TitleOptions = New ShapeTitleOptions() With {.Pattern = "A", .VisibilityMode = VisibilityMode.Visible, .Template = titleTemplate}})
            storage.Items.Add(New MapDot() With {.Location = points.Last(), .Size = 20, .TitleOptions = New ShapeTitleOptions() With {.Pattern = "B", .VisibilityMode = VisibilityMode.Visible, .Template = titleTemplate}})
            Me.Storage = storage
        End Sub

        Private Sub FillActivities()
            Dim xmlDataSet As DataSet = New DataSet("XML DataSet")
            xmlDataSet.ReadXml(Application.GetResourceStream(New Uri(CreateGpxDataPath("Activities.xml"), UriKind.RelativeOrAbsolute)).Stream)
            Dim activities As DataTable = xmlDataSet.Tables("Activity")
            activities.Columns("Data").ColumnMapping = MappingType.Hidden
            Me.Activities = activities
            CurrentActivity = activities.DefaultView(0)
        End Sub

        Private Function CreateGpxDataPath(ByVal fileName As Object) As String
            Return String.Format("/MapDemo;component/Data/Gpx/{0}", fileName)
        End Function

        Protected Sub OnIsMovingPointVisibleChanged()
            highlightedPoint.Visible = IsMovingPointVisible
        End Sub

        Protected Sub OnMovingPointLocationChanged()
            If MovingPointLocation IsNot Nothing Then highlightedPoint.Location = MovingPointLocation
        End Sub

        Protected Sub OnCurrentActivityChanged()
            Dim path As String = String.Empty
            If CurrentActivity IsNot Nothing Then path = TryCast(CurrentActivity("Data"), String)
            Dim uri As Uri = If(String.IsNullOrEmpty(path), Nothing, New Uri(CreateGpxDataPath(path & ".gpx"), UriKind.RelativeOrAbsolute))
            CurrentTrackUri = uri
        End Sub

        Public Sub OnShapesLoaded(ByVal sender As Object, ByVal e As ShapesLoadedEventArgs)
            PopulatePoints(TryCast(e.Shapes.First(), MapPath))
            Info = Calculate(TryCast(sender, IListSource))
        End Sub
    End Class

    Public Class GpxTrackInfo

        Public Property Duration As TimeSpan

        Public Property Distance As Double

        Public Property AverageHeartRate As Integer

        Public Property MinHeartRate As Integer

        Public Property MaxHeartRate As Integer

        Public Property AveragePace As TimeSpan

        Public Property MaxPace As TimeSpan
    End Class

    Public Module TrackInfoCalculator

        Public Function Calculate(ByVal source As IListSource) As GpxTrackInfo
            Dim info As GpxTrackInfo = New GpxTrackInfo()
            Dim data As DataView = TryCast(source.GetList(), DataView)
            If data Is Nothing Then Return info
            info.Duration = CDate(data(data.Count - 1)("time")) - CDate(data(0)("time"))
            info.Distance =(CDbl(data(data.Count - 1)("gpxdata:distance")) - CDbl(data(0)("gpxdata:distance")))
            CalculatePace(data, info)
            CalculateHeartRate(data, info)
            info.Distance /= 1000
            Return info
        End Function

        Private Sub CalculateHeartRate(ByVal data As DataView, ByVal info As GpxTrackInfo)
            Dim heartRate As String = "gpxtpx:hr"
            If Not data.Table.Columns.Contains(heartRate) Then Return
            Dim minHeartRate As Integer = Convert.ToInt32(data(0)(heartRate)), maxHeartRate As Integer = minHeartRate, heartRateSum As Integer = 0
            For Each row As DataRowView In data
                minHeartRate = Math.Min(minHeartRate, Convert.ToInt32(row(heartRate)))
                maxHeartRate = Math.Max(maxHeartRate, Convert.ToInt32(row(heartRate)))
                heartRateSum += Convert.ToInt32(row(heartRate))
            Next

            info.MinHeartRate = minHeartRate
            info.MaxHeartRate = maxHeartRate
            info.AverageHeartRate = CInt(heartRateSum \ data.Count)
        End Sub

        Private Sub CalculatePace(ByVal data As DataView, ByVal info As GpxTrackInfo)
            If Not data.Table.Columns.Contains("Pace") Then data.Table.Columns.Add("Pace")
            Dim distance As String = "gpxdata:distance", time As String = "time"
            Dim window As Integer = 10
            Dim minTicks As Long = Long.MaxValue
            For i As Integer = 1 To window - 1
                Dim d As Double = CDbl(data(i)(distance)) - CDbl(data(i - 1)(distance))
                Dim pace As TimeSpan = TimeSpan.FromMilliseconds(0)
                If d > Double.Epsilon Then
                    Dim dTime As TimeSpan = CDate(data(i)(time)) - CDate(data(i - 1)(time))
                    pace = TimeSpan.FromMinutes(dTime.TotalMinutes / (d * 0.001))
                    minTicks = Math.Min(minTicks, pace.Ticks)
                End If

                data(i)("Pace") = pace
            Next

            For j As Integer = window To data.Count - 1
                Dim d As Double = CDbl(data(j)(distance)) - CDbl(data(j - window)(distance))
                Dim dTime As TimeSpan = CDate(data(j)(time)) - CDate(data(j - window)(time))
                Dim pace As TimeSpan = TimeSpan.FromMinutes(dTime.TotalMinutes / (d * 0.001))
                minTicks = Math.Min(minTicks, pace.Ticks)
                data(j)("Pace") = pace
            Next

            info.AveragePace = TimeSpan.FromMinutes(info.Duration.TotalMinutes / (info.Distance * 0.001))
            info.MaxPace = TimeSpan.FromTicks(minTicks)
        End Sub
    End Module

    Public Class MapCrosshairBehavior
        Inherits Behavior(Of ChartControl)

        Public Shared ReadOnly IsMovingPointVisibleProperty As DependencyProperty = DependencyProperty.Register("IsMovingPointVisible", GetType(Boolean), GetType(MapCrosshairBehavior), New PropertyMetadata(False))

        Public Shared ReadOnly MovingPointLocationProperty As DependencyProperty = DependencyProperty.Register("MovingPointLocation", GetType(GeoPoint), GetType(MapCrosshairBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property IsMovingPointVisible As Boolean
            Get
                Return CBool(GetValue(IsMovingPointVisibleProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsMovingPointVisibleProperty, value)
            End Set
        End Property

        Public Property MovingPointLocation As GeoPoint
            Get
                Return CType(GetValue(MovingPointLocationProperty), GeoPoint)
            End Get

            Set(ByVal value As GeoPoint)
                SetValue(MovingPointLocationProperty, value)
            End Set
        End Property

        Private Sub OnCustomDrawCrosshair(ByVal sender As Object, ByVal e As CustomDrawCrosshairEventArgs)
            If e.CrosshairElementGroups.Count > 0 Then
                Dim sourceItem As DataRowView = CType(e.CrosshairElementGroups(0).CrosshairElements(0).SeriesPoint.Tag, DataRowView)
                MovingPointLocation = New GeoPoint(CDbl(sourceItem("lat")), CDbl(sourceItem("lon")))
            End If
        End Sub

        Private Sub OnMouseMove(ByVal sender As Object, ByVal e As Input.MouseEventArgs)
            IsMovingPointVisible = AssociatedObject.CalcHitInfo(e.GetPosition(AssociatedObject)).InDiagram
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.MouseMove, AddressOf OnMouseMove
            AddHandler AssociatedObject.CustomDrawCrosshair, AddressOf OnCustomDrawCrosshair
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.MouseMove, AddressOf OnMouseMove
            RemoveHandler AssociatedObject.CustomDrawCrosshair, AddressOf OnCustomDrawCrosshair
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
