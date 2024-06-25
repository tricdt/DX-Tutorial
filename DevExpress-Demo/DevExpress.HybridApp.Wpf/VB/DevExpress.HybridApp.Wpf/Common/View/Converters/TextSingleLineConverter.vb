Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace DevExpress.DevAV.Common.View

    Public Class TextSingleLineConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim text As String = TryCast(value, String)
            Return If(Equals(text, Nothing), Nothing, text.Replace(Environment.NewLine, " "))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
