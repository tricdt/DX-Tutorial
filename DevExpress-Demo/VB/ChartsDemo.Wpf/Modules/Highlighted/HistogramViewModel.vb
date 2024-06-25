Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    <POCOViewModel>
    Public Class HistogramViewModel

        Public Shared Function Create() As HistogramViewModel
            Return ViewModelSource.Create(Function() New HistogramViewModel())
        End Function

        Public Overridable Property Palette As Palette

        Public Overridable Property IntervalDivisionMode As IntervalDivisionMode

        Public Overridable Property Cluster1Brush As SolidColorBrush

        Public Overridable Property Cluster2Brush As SolidColorBrush

        Public Overridable Property Cluster3Brush As SolidColorBrush

        Public Sub New()
            Palette = New OfficePalette()
            IntervalDivisionMode = IntervalDivisionMode.Width
        End Sub

        Protected Sub OnIntervalDivisionModeChanged()
            Select Case IntervalDivisionMode
                Case IntervalDivisionMode.Auto
                Case IntervalDivisionMode.Width
                Case IntervalDivisionMode.Count
                Case Else
                    Throw New InvalidEnumArgumentException(String.Format("The {0} enum value is unknown", IntervalDivisionMode))
            End Select
        End Sub

        Protected Sub OnPaletteChanged()
            If Palette Is Nothing Then Return
            Cluster1Brush = New SolidColorBrush(Color.FromArgb(100, Palette(0).R, Palette(0).G, Palette(0).B))
            Cluster2Brush = New SolidColorBrush(Color.FromArgb(100, Palette(1).R, Palette(1).G, Palette(1).B))
            Cluster3Brush = New SolidColorBrush(Color.FromArgb(100, Palette(2).R, Palette(2).G, Palette(2).B))
        End Sub
    End Class

    Friend Class IntervalDivisionModeToScaleOptionsConverter
        Implements IValueConverter

        Public Property AutoIntervalNumericScaleOptions As IntervalNumericScaleOptions

        Public Property WidthIntervalNumericScaleOptions As WidthIntervalNumericScaleOptions

        Public Property CountIntervalNumericScaleOptions As CountIntervalNumericScaleOptions

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value.GetType() IsNot GetType(IntervalDivisionMode) Then Return value
            Dim scaleMode As IntervalDivisionMode = CType(value, IntervalDivisionMode)
            Select Case scaleMode
                Case IntervalDivisionMode.Auto
                    Return AutoIntervalNumericScaleOptions
                Case IntervalDivisionMode.Width
                    Return WidthIntervalNumericScaleOptions
                Case IntervalDivisionMode.Count
                    Return CountIntervalNumericScaleOptions
                Case Else
                    Throw New InvalidEnumArgumentException(String.Format("The {0} enum value is unknown", scaleMode))
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not targetType Is GetType(IntervalDivisionMode) Then Return value
            If TypeOf value Is WidthIntervalNumericScaleOptions Then Return IntervalDivisionMode.Width
            If TypeOf value Is CountIntervalNumericScaleOptions Then Return IntervalDivisionMode.Count
            If TypeOf value Is IntervalNumericScaleOptions Then Return IntervalDivisionMode.Auto
            Throw New ArgumentException("An instance of the IntervalNumericScaleOptions, WidthIntervalNumericScaleOptions or CountIntervalNumericScaleOptions class is expected.")
        End Function
    End Class
End Namespace
