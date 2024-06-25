Imports System
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.DevAV.ViewModels

Namespace DevExpress.DevAV

    Public Class SelectedFilterItemConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is FilterCategory Then Return Binding.DoNothing
            Return value
        End Function
    End Class
End Namespace
