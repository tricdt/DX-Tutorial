Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace RibbonDemo

    Public Class ImageScaleValueConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return(CInt((CDbl(value) * 100))).ToString() & "%"
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim val As String = TryCast(value, String)
            Return Double.Parse(val.Substring(0, val.Length - 1)) / 100
        End Function
#End Region
    End Class
End Namespace
