Imports DevExpress.Xpf.DemoBase.Helpers
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace DialogsDemo

    Public MustInherit Class BaseValueConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public MustOverride Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public NotOverridable Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class CountryToFlagImageConverterExtension
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then Return Nothing
            Return GetFlagImageByCountryName(CStr(value))
        End Function

        Public Shared Function GetFlagImageByCountryName(ByVal countryName As String) As Byte()
            Dim flag = CountriesData.DataSource.Find(Function(x) Equals(x.Name, countryName)).Flag
            Return flag
        End Function
    End Class

    Public Class GenderToImageConverterExtension
        Inherits BaseValueConverter

        Public Shared images As List(Of String) = New List(Of String) From {"m", "f"}

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then Return Nothing
            Return GetImagePathByGender(CStr(value))
        End Function

        Public Shared Function GetImagePathByGender(ByVal genderName As String) As String
            genderName = genderName.ToLowerInvariant()
            If Equals(genderName, "m") Then Return "pack://application:,,,/DialogsDemo;component/Images/ThemedWindow/Gender/Male.svg"
            If Equals(genderName, "f") Then Return "pack://application:,,,/DialogsDemo;component/Images/ThemedWindow/Gender/Female.svg"
            Return genderName
        End Function
    End Class

    Public Class GenderToFullStringConverterExtension
        Inherits BaseValueConverter

        Public Shared images As List(Of String) = New List(Of String) From {"m", "f"}

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then Return Nothing
            Return GetFullStringByGender(CStr(value))
        End Function

        Public Shared Function GetFullStringByGender(ByVal genderName As String) As String
            genderName = genderName.ToLowerInvariant()
            If Equals(genderName, "m") Then Return "Male"
            If Equals(genderName, "f") Then Return "Female"
            Return genderName
        End Function
    End Class

    Public Class StatusToFullStringConverterExtension
        Inherits BaseValueConverter

        Public Shared images As List(Of String) = New List(Of String) From {"s", "m"}

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then Return Nothing
            Return GetFullStringByGender(CStr(value))
        End Function

        Public Shared Function GetFullStringByGender(ByVal genderName As String) As String
            genderName = genderName.ToLowerInvariant()
            If Equals(genderName, "s") Then Return "Single"
            If Equals(genderName, "m") Then Return "Married"
            Return genderName
        End Function
    End Class
End Namespace
