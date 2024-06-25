Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Xpf.Gauges

Namespace DevExpress.SalesDemo.Wpf.Converters

    Public Class DecimalToRangeValueConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim doubleValue As Double = System.Convert.ToDouble(value)
            Dim valueType As RangeValueType = If(parameter Is Nothing, RangeValueType.Absolute, CType(parameter, RangeValueType))
            Return New RangeValue(doubleValue, valueType)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim rv As RangeValue = CType(value, RangeValue)
            Return rv.Value
        End Function
    End Class
End Namespace
