Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace ChartsDemo

    Friend MustInherit Class ForwardOnlyValueConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public MustOverride Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
