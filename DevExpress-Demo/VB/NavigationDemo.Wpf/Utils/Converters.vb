Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Xpf.Core.Internal

Namespace NavigationDemo.Utils

    Public Class CollectionConverter(Of T)
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If value Is Nothing Then Return Enumerable.Empty(Of T)()
            Return CType(value, List(Of Object)).Cast(Of T)().ToList()
        End Function
    End Class

    Public Class Int32CollectionConverter
        Inherits CollectionConverter(Of Integer)

    End Class

    Public Class Int64CollectionConverter
        Inherits CollectionConverter(Of Long)

    End Class

    Public Class DeltaValueToImageSourceConverter
        Inherits MarkupExtension
        Implements IMultiValueConverter

        <TypeConverter(GetType(SvgImageSourceConverter))>
        Public Property NegativeImageSource As ImageSource

        <TypeConverter(GetType(SvgImageSourceConverter))>
        Public Property PositiveImageSource As ImageSource

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim delta = CDbl(values(0))
            Dim deltaChange = CInt(values(1))
            Dim mode = CType(values(2), StockIndicatorMode)
            If mode = StockIndicatorMode.Delta AndAlso delta < 0 OrElse mode = StockIndicatorMode.DeltaChange AndAlso deltaChange < 0 Then Return NegativeImageSource
            If mode = StockIndicatorMode.Delta AndAlso delta > 0 OrElse mode = StockIndicatorMode.DeltaChange AndAlso deltaChange > 0 Then Return PositiveImageSource
            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class FilterStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim item = CType(value, FilterItem)
            Return If(item Is Nothing, String.Empty, item.FilterString)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class NavigationWidthConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Property StartupWidth As Double

        Private isExpanded As Boolean = True

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            isExpanded = CBool(value)
            Return If(isExpanded, New GridLength(StartupWidth), GridLength.Auto)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If isExpanded Then StartupWidth = CType(value, GridLength).Value
            Return Binding.DoNothing
        End Function
    End Class
End Namespace
