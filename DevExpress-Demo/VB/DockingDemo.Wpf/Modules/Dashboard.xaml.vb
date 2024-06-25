Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors.Settings

Namespace DockingDemo

    <CodeFile("ViewModels/DashboardViewModel.(cs)")>
    Public Partial Class Dashboard
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class ChartPaletteToMapColorsConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim chartColors As Palette = CType(value, Palette)
            Dim rangeStops As DoubleCollection = CType(parameter, DoubleCollection)
            Dim colorsCount As Integer = CInt(rangeStops(rangeStops.Count - 1) - rangeStops(0)) + 1
            Dim mapColors As DevExpress.Xpf.Map.ColorCollection = New DevExpress.Xpf.Map.ColorCollection()
            For i As Integer = 0 To colorsCount - 1
                mapColors.Add(chartColors(i))
            Next

            Return mapColors
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class RevenueFormatTextEditSettings
        Inherits TextEditSettings

        Public Overrides Function GetDisplayTextFromEditor(ByVal editValue As Object) As String
            Return "$" & Format(CDec(editValue))
        End Function

        Private Function Format(ByVal source As Decimal) As String
            Dim [integer] = Math.Truncate(source)
            If [integer] > 999999999 Then
                Return [integer].ToString("0,,,.###B", CultureInfo.InvariantCulture)
            ElseIf [integer] > 999999 Then
                Return [integer].ToString("0,,.##M", CultureInfo.InvariantCulture)
            ElseIf [integer] > 999 Then
                Return [integer].ToString("0,.#K", CultureInfo.InvariantCulture)
            Else
                Return [integer].ToString("", CultureInfo.InvariantCulture)
            End If
        End Function
    End Class

    Public Class UniformGridReorderBehavior
        Inherits Behavior(Of UniformGrid)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.SizeChanged, AddressOf AssociatedObject_SizeChanged
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.SizeChanged, AddressOf AssociatedObject_SizeChanged
        End Sub

        Private Sub AssociatedObject_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            If AssociatedObject.ActualWidth < 200 Then
                AssociatedObject.Rows = 6
                AssociatedObject.Columns = 1
            ElseIf AssociatedObject.ActualWidth < 340 Then
                AssociatedObject.Rows = 3
                AssociatedObject.Columns = 2
            Else
                AssociatedObject.Rows = 2
                AssociatedObject.Columns = 3
            End If
        End Sub
    End Class
End Namespace
