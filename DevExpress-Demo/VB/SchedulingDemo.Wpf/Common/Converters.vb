Imports SchedulingDemo.ViewModels
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace SchedulingDemo

    Public Class ResourceNameToIdConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private resources As IList(Of String)

        Public Sub New()
            resources = WorkData.Calendars.[Select](Function(x) x.Name).ToList()
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim ids As IEnumerable(Of Long) = TryCast(value, IEnumerable(Of Long))
            If ids IsNot Nothing Then
                Dim names As List(Of String) = ids.[Select](Function(x) resources(CInt(x))).ToList()
                Return names
            End If

            Return New List(Of String)() From {TimeRegionsDemoViewModel.AnyResource}
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim names As List(Of Object) = TryCast(value, List(Of Object))
            If names IsNot Nothing AndAlso Not Equals(CStr(names(0)), TimeRegionsDemoViewModel.AnyResource) Then
                Dim ids As IEnumerable(Of Long) = names.[Select](Function(x) Enumerable.FirstOrDefault(Enumerable.Where(WorkData.Calendars, Function(v) Equals(v.Name, CStr(x)))).Id)
                Return ids
            End If

            Return Nothing
        End Function
    End Class
End Namespace
