Imports System
Imports System.Windows.Data

Namespace DevExpress.DevAV.Converters

    Public Class AbsoluteToLocalConverter
        Implements IValueConverter

        Public Property MaxValue As Double

        Public Property MinValue As Double

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Double) Then Return value
            Return CDbl(value) * (MaxValue - MinValue) + MinValue
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
