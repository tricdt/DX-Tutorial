Imports System
Imports System.Windows.Data
Imports System.Windows.Media.Imaging

Namespace CommonDemo.TabControl.WebBrowser

    Public Class UrlToImageConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim baseUri As Uri = CType(value, Uri)
            Return New BitmapImage(New Uri(baseUri.AbsoluteUri & "favicon.ico"))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
