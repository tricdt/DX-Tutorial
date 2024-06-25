Imports System
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Threading
Imports DevExpress.Map
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Enum ActiveShapeType
        Polygon
        Polyline
    End Enum

    <POCOViewModel>
    Public Class ShapeSimplifierViewModel

        Public Shared MaxToleranceValue As Integer = 280

        Private ReadOnly timer As DispatcherTimer

        Private toleranceDelta As Integer = -1

        Public Overridable Property Tolerance As Double

        Public Overridable Property ActiveShape As ActiveShapeType

        Public Overridable Property PolygonData As MapItemStorage

        Public Overridable Property PolylineData As MapItemStorage

        Public Overridable Property EtalonData As MapItemStorage

        Public Overridable Property Status As String

        Public Overridable Property AutoMode As Boolean

        Public Overridable Property Simplify As ICommand

        Public Sub New()
            Tolerance = MaxToleranceValue
            timer = New DispatcherTimer() With {.Interval = TimeSpan.FromMilliseconds(30), .IsEnabled = True}
            AddHandler timer.Tick, AddressOf OnTimerTick
            AutoMode = True
            Dim adapter As ShapefileDataAdapter = New ShapefileDataAdapter() With {.FileUri = New ShapefileWorldResources().IcelandFileUri}
            adapter.LoadData()
            Dim line As MapPolyline = New MapPolyline() With {.StrokeStyle = New StrokeStyle() With {.Thickness = 3}}
            line.Points.Assign(CType(Enumerable.First(adapter.DisplayItems), ISupportCoordPoints).Points)
            Dim storage As MapItemStorage = New MapItemStorage()
            storage.Items.Add(line)
            PolylineData = storage
            Dim polygonData As MapItemStorage = New MapItemStorage()
            polygonData.Items.Add(adapter.DisplayItems.First())
            Me.PolygonData = polygonData
            Dim etalonLine As MapPolyline = New MapPolyline()
            etalonLine.Points.Assign(CType(Enumerable.First(adapter.DisplayItems), ISupportCoordPoints).Points)
            storage = New MapItemStorage()
            storage.Items.Add(etalonLine)
            EtalonData = storage
        End Sub

        Protected Sub OnToleranceChanged()
            SimplifyItems()
        End Sub

        Protected Sub OnActiveShapeChanged()
            SimplifyItems()
        End Sub

        Protected Sub OnAutoModeChanged()
            timer.IsEnabled = AutoMode
        End Sub

        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
            toleranceDelta = If(Tolerance = MaxToleranceValue, -1, If(Tolerance = 0, 1, toleranceDelta))
            Tolerance = Tolerance + toleranceDelta
        End Sub

        Private Function CalculateTolerance(ByVal value As Double) As Double
            Return Math.Max(Math.Round(100 * Math.Abs(Math.Pow(value / MaxToleranceValue, 5)), 4), 0.001)
        End Function

        Private Sub SimplifyItems()
            If Simplify IsNot Nothing Then
                Dim tolerance As Double = CalculateTolerance(Me.Tolerance)
                Simplify.Execute(New Object() {If(ActiveShape = ActiveShapeType.Polygon, PolygonData.DisplayItems, PolylineData.Items), tolerance})
                UpdateCounterText(tolerance)
            End If
        End Sub

        Private Sub UpdateCounterText(ByVal tolerance As Double)
            Dim actualStorage As MapDataAdapterBase = If(ActiveShape = ActiveShapeType.Polygon, CType(PolygonData, MapDataAdapterBase), PolylineData)
            Dim shape As ISupportCoordPoints = TryCast(actualStorage.DisplayItems.ElementAt(0), ISupportCoordPoints)
            If shape IsNot Nothing Then Status = String.Format("{0} points, {1:0.000}% tolerance", shape.Points.Count - 2, tolerance)
        End Sub
    End Class

    Public Class ActiveShapeTypeToLayerVisibilityConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value.Equals(parameter), Visibility.Visible, Visibility.Collapsed)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
