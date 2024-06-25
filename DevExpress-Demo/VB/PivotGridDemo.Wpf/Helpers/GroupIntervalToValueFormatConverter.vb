Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Class GroupIntervalToValueFormatConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim groupInterval As FieldGroupInterval? = CType(value, FieldGroupInterval?)
            If groupInterval = FieldGroupInterval.DateQuarter Then Return "Quarter {0}"
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
