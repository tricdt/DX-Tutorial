Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace DevExpress.SalesDemo.Wpf.Converters

    Friend Class BillionStringToShortStringConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is String) Then Return Nothing
            Dim billionFormatString As String = "0,,.0M"
            Dim thousandsFormatString As String = "0,.0K"
            Dim dec As Decimal
            Dim parsed As Boolean = Decimal.TryParse(CStr(value), dec)
            If parsed Then
                If dec = 0 Then
                    Return "0"
                ElseIf dec >= 0.1D * 1000000D Then
                    Return dec.ToString(billionFormatString)
                Else
                    Return dec.ToString(thousandsFormatString)
                End If
            Else
                Return Nothing
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
