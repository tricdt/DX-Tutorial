Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Xpf.Core

Namespace BarsDemo

    Public Class EditWidthConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Property EditWidth As Double

        Public Property TouchScaleFactor As Double

        Public Sub New()
            TouchScaleFactor = 2R
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim walker = TryCast(value, ThemeTreeWalker)
            If walker IsNot Nothing AndAlso (walker.IsTouch OrElse Equals(walker.ThemeName, Theme.TouchlineDarkName)) Then Return EditWidth * TouchScaleFactor
            Return EditWidth
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
