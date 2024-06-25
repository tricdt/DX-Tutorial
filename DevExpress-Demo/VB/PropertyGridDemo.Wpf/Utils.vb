Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Windows.Media.Imaging

Namespace PropertyGridDemo.Utils

    Public Class FormatWrapper

        Public Sub New()
        End Sub

        Public Sub New(ByVal name As String, ByVal format As String)
            FormatName = name
            FormatString = format
        End Sub

        Public Property FormatName As String

        Public Property FormatString As String
    End Class

    Public Class BaseKindHelper(Of T)

        Public Function GetEnumMemberList() As Array
            Return [Enum].GetValues(GetType(T))
        End Function
    End Class

    Public Class ClickModeKindHelper
        Inherits BaseKindHelper(Of ClickMode)

    End Class

    Public Class TextWrappingKindHelper
        Inherits BaseKindHelper(Of TextWrapping)

    End Class

    Public Class ScrollBarVisibilityKindHelper
        Inherits BaseKindHelper(Of ScrollBarVisibility)

    End Class

    Public Class CharacterCasingKindHelper
        Inherits BaseKindHelper(Of CharacterCasing)

    End Class

    Public Class NullableToStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private nullString As String = "Null"

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return nullString
            Return value.ToString()
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class DecimalToConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = TryCast(parameter, Type)
            If target Is Nothing Then Return value
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return System.Convert.ToDecimal(value)
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class IConvertibleConverter
        Implements IValueConverter

        Public Property ToType As String

        Public Property FromType As String

#Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = Type.GetType(ToType, False)
            If target Is Nothing Then Return value
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim target As Type = Type.GetType(FromType, False)
            If target Is Nothing Then Return value
            Return System.Convert.ChangeType(value, target, culture)
        End Function
#End Region
    End Class

    Public Class ResourceStringToImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim valueString = TryCast(value, String)
            If String.IsNullOrEmpty(valueString) Then Return Nothing
            Dim uriPrefix = "pack://application:,,,/PropertyGridDemo;component/"
            Dim uri = New Uri(String.Format("{0}{1}", uriPrefix, valueString), UriKind.RelativeOrAbsolute)
            Return New BitmapImage(uri)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ResourceStringToImageSourceConverterExtension
        Inherits MarkupExtension

        Private Shared instance As ResourceStringToImageSourceConverter

        Shared Sub New()
            instance = New ResourceStringToImageSourceConverter()
        End Sub

        Public Sub New()
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return instance
        End Function
    End Class
End Namespace
