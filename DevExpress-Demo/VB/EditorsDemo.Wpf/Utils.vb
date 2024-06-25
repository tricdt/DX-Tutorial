Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.ComponentModel
Imports System.Windows.Media
Imports DevExpress.Xpf.Editors
Imports System.Collections.Generic
Imports GridDemo
Imports System.Diagnostics
Imports DevExpress.Xpf.Core
Imports System.Globalization

Namespace EditorsDemo.Utils

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

    Public Module ImageHelper

        Public Function GetSvgImage(ByVal imageName As String) As ImageSource
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(String.Format("pack://application:,,,/EditorsDemo;component/Images/{0}.svg", imageName)), .Size = New Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Module

    Public Class NullableToStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private nullString As String = "Null"

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return nullString
            Return value.ToString()
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class DecimalToConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = TryCast(parameter, Type)
            If target Is Nothing Then Return value
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
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
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = Type.GetType(ToType, False)
            If target Is Nothing Then Return value
            Return System.Convert.ChangeType(value, target, culture)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim target As Type = Type.GetType(FromType, False)
            If target Is Nothing Then Return value
            Return System.Convert.ChangeType(value, target, culture)
        End Function
#End Region
    End Class

    Public Class CategoryDataToImageSourceConverter
        Inherits BytesToImageSourceConverter

        Private Shared cachedImages As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Dim categoryData As CategoryData = TryCast(value, CategoryData)
            If value Is Nothing Then Return Nothing
            If cachedImages.ContainsKey(categoryData.Name) Then
                Return cachedImages(categoryData.Name)
            Else
                Dim image As Object = MyBase.Convert(categoryData.Picture, targetType, parameter, culture)
                cachedImages.Add(categoryData.Name, image)
                Return image
            End If
        End Function
    End Class

    Public Class CategoryNameToImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim category = CStr(value)
            If String.IsNullOrEmpty(category) Then Return Nothing
            category = category.Replace(" ", "").Replace("/", "")
            Return GetSvgImage("Products/" & category)
        End Function

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
