Imports System
Imports System.Windows
Imports System.Windows.Data

Namespace RibbonDemo

    Public Class EnumValueToImageSourceConverter
        Implements IValueConverter

        Public Property Prefix As String

        Public Property Folder As String

        Public Property Suffix As String

        Public Property EnumType As Type

        Public Sub New()
            Suffix = String.Empty
            Folder = Suffix
            Prefix = Folder
        End Sub

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return DependencyProperty.UnsetValue
            Dim fileName As String = String.Format("{0}/{1}{2}{3}", Folder, Prefix, [Enum].GetName(EnumType, value), Suffix)
            Return New Uri(String.Format("/RibbonDemo;component/Images/{0}.png", fileName), UriKind.Relative)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class TextMarkerStyleValueToImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return DependencyProperty.UnsetValue
            Dim fileName As String = String.Format("Bullets-{0}-76x76", [Enum].GetName(GetType(TextMarkerStyle), value))
            Return New Uri(String.Format("/RibbonDemo;component/Images/Icons/{0}.png", fileName), UriKind.Relative)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
