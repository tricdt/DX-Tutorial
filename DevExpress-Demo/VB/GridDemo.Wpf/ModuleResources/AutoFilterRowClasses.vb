Imports System
Imports System.Windows
Imports System.Windows.Data

Namespace GridDemo

    Public Class AutoFilterConditionVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim strValue As String = CStr(value)
            Return If(Equals(strValue, "ToId") OrElse Equals(strValue, "HasAttachment") OrElse Equals(strValue, "Sent"), Visibility.Collapsed, Visibility.Visible)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
