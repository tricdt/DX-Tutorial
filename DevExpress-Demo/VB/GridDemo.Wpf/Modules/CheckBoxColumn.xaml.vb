Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Xpf.Core

Namespace GridDemo

    Public Partial Class CheckBoxColumn
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class ProductIdToImageConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim categoryId As Integer = 0
            If value Is Nothing OrElse Not Integer.TryParse(value.ToString(), categoryId) Then Return Nothing
            Dim name As String = Nothing
            Select Case categoryId
                Case 1
                    name = "Beverages"
                Case 2
                    name = "condiments"
                Case 3
                    name = "confections"
                Case 4
                    name = "DairyProduct"
                Case 5
                    name = "grains_cereals"
                Case 6
                    name = "MeatPoultry"
                Case 7
                    name = "produce"
                Case 8
                    name = "Seafood"
                Case Else
                    Return Nothing
            End Select

            Dim extension As SvgImageSourceExtension = New SvgImageSourceExtension() With {.Uri = New Uri(String.Format("pack://application:,,,/GridDemo;component/Images/Products/{0}.svg", name))}
            Return extension.ProvideValue(Nothing)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
