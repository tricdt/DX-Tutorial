Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace PropertyGridDemo

    Public Class CategoryNameToImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim category = CStr(value)
            If String.IsNullOrEmpty(category) Then Return Nothing
            category = category.Replace(" ", "").Replace("/", "")
            Return GetSvgImage("Products/" & category)
        End Function

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
