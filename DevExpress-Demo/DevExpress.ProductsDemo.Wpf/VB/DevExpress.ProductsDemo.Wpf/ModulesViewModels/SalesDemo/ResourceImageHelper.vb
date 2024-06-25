Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media.Imaging

Namespace ProductsDemo

    Public Module ResourceImageHelper

        Public Function GetResourceImage(ByVal image As String) As BitmapImage
            Dim res As BitmapImage = New BitmapImage()
            res.BeginInit()
            res.StreamSource = GetResourceStream(image)
            res.EndInit()
            res.Freeze()
            Return res
        End Function
    End Module

    Public Class ResourceImageConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim image As String = If(TryCast(parameter, String), TryCast(value, String))
            If Equals(image, Nothing) Then Throw New NullReferenceException("parameter")
            Return GetResourceImage(image)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
