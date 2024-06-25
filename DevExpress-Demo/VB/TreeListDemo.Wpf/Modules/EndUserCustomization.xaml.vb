Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Linq
Imports DevExpress.Xpf.Core

Namespace TreeListDemo

    Public Partial Class EndUserCustomization
        Inherits TreeListDemoModule

        Private maxId As Integer

        Public Sub New()
            InitializeComponent()
            maxId = EmployeesWithPhotoData.DataSource.Max(Function(x) x.Id)
        End Sub

        Private Sub lbSummary_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If lbSummary.SelectedIndex = 0 Then
                TreeListControl.View.TotalSummaryPosition = TotalSummaryPosition.Bottom
                TreeListControl.View.ShowFixedTotalSummary = False
            Else
                TreeListControl.View.TotalSummaryPosition = TotalSummaryPosition.None
                TreeListControl.View.ShowFixedTotalSummary = True
            End If
        End Sub

        Private Sub OnInitNewNode(ByVal sender As Object, ByVal e As TreeList.TreeListNodeEventArgs)
            maxId += 1
            view.SetNodeValue(e.Node, "Id", maxId)
        End Sub
    End Class

    Public Class EmployeeCategoryImageSelector
        Inherits TreeListNodeImageSelector
        Implements IValueConverter

        Shared Sub New()
            ImageCache = New Dictionary(Of String, ImageSource)()
        End Sub

        Private Shared ImageCache As Dictionary(Of String, ImageSource)

        Public Overrides Function [Select](ByVal rowData As TreeList.TreeListRowData) As ImageSource
            Dim empl As Employee = TryCast(rowData.Row, Employee)
            If empl Is Nothing Then Return Nothing
            Return GetImageByGroupName(empl.GroupName)
        End Function

        Public Shared Function GetImageByGroupName(ByVal groupName As String) As ImageSource
            If Equals(groupName, Nothing) Then Return Nothing
            If ImageCache.ContainsKey(groupName) Then Return ImageCache(groupName)
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(GetImagePathByGroupName(groupName)), .Size = New Size(16, 16)}
            Dim image = CType(extension.ProvideValue(Nothing), ImageSource)
            ImageCache.Add(groupName, image)
            Return image
        End Function

        Public Shared images As List(Of String) = New List(Of String) From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}

        Public Shared Function GetImagePathByGroupName(ByVal groupName As String) As String
            groupName = groupName.ToLowerInvariant()
            For Each item As String In images
                If groupName.Contains(item) Then
                    Return "pack://application:,,,/TreeListDemo;component/Images/Categories/" & item & ".svg"
                End If
            Next

            Return groupName
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return GetImagePathByGroupName(CStr(value))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

#Region "Converters"
    Public Class NavigationStyleToIsEnabledConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return True
            Return CType(value, GridViewNavigationStyle) <> GridViewNavigationStyle.Row
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class HeaderToImageConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Return "/TreeListDemo;component/Images/HeaderIcons/" & (CStr(value)).Replace(" ", String.Empty) & ".svg"
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
#End Region
End Namespace
