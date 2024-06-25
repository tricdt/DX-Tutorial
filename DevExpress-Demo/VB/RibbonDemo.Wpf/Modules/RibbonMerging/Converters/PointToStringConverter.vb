Imports System
Imports System.Windows
Imports System.Windows.Data

Namespace RibbonDemo

    Public Class PointToStringConverter
        Implements IValueConverter

        Public Property FormatString As String

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Point) Then Return Nothing
            Dim p As Point = CType(value, Point)
            Return String.Format(If(FormatString, "{0},{1}"), If(p.X <> -1, Math.Round(p.X).ToString(), ""), If(p.X <> -1, Math.Round(p.Y).ToString(), ""))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
