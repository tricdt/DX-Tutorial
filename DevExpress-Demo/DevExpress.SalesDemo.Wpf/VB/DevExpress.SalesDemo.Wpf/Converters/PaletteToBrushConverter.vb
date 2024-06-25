Imports DevExpress.Xpf.Charts
Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media

Namespace DevExpress.SalesDemo.Wpf.Converters

    Public Class PaletteToBrushConverter
        Implements IValueConverter

        Public Property Palette As CustomPalette

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim index = CInt(value)
            If index >= Palette.Count Then index = 0
            Dim result As SolidColorBrush = New SolidColorBrush(Palette(index))
            Return result
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
