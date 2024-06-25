Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Utils

Namespace DevExpress.DevAV.Converters

    Public Class ImageConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then Return AssemblyHelper.GetResourceUri(GetType(ImageConverter).Assembly, value.ToString())
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
