Imports System
Imports System.Collections.Generic
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace DevExpress.DevAV

    Public Class SelectedItemsConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim result = New List(Of Object)()
            For Each item In CType(value, List(Of Employee))
                result.Add(item)
            Next

            Return result
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim result As List(Of Employee) = New List(Of Employee)()
            If value IsNot Nothing Then
                For Each item As Object In CType(value, List(Of Object))
                    If TryCast(item, Employee) IsNot Nothing Then result.Add(CType(item, Employee))
                Next
            End If

            Return result
        End Function
    End Class
End Namespace
