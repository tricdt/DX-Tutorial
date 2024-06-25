Imports System
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows.Data
Imports System.Windows.Media

Namespace DevExpress.DevAV

    Public Class CategoryColorConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If((value Is Nothing OrElse String.IsNullOrEmpty(New Regex("^#*").Match(value.ToString()).Value)), Colors.Transparent, CType(ColorConverter.ConvertFromString(value.ToString()), Color))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(value Is Nothing, Nothing, value.ToString())
        End Function
    End Class
End Namespace
