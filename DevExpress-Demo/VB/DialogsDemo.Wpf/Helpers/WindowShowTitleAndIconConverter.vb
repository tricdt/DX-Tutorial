Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace CommonDemo.Helpers

    Public Class WindowShowTitleAndIconConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Return CBool(values(0))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Return New Object() {CBool(value), CBool(value)}
        End Function
    End Class
End Namespace
