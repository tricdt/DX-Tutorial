Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.Themes
Imports DevExpress.Data.TreeList

Namespace TreeListDemo

    Public Partial Class ScrollBarAnnotations
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub view_ValidateCell(ByVal sender As Object, ByVal e As TreeList.TreeListCellValidationEventArgs)
            If e.CellValue IsNot Nothing Then
                Select Case e.Column.FieldName
                    Case "SeptemberFromPriorYear", "MarchFromPriorYear"
                        Dim numb As Double = Double.Parse(e.CellValue.ToString())
                        If numb <= -0.3 Then
                            e.IsValid = False
                            e.SetError("'Change from Prior Year' is less than or equals to -30%")
                        End If

                    Case "MarketShare"
                        Dim val As Double = Double.Parse(e.CellValue.ToString())
                        If val < 0.12 Then
                            e.IsValid = False
                            e.SetError("Market Share is less than 12%")
                        End If

                    Case Else
                        Return
                End Select
            End If
        End Sub

        Private changedRows As HashSet(Of Integer) = New HashSet(Of Integer)()

        Private Sub view_ScrollBarAnnotationsCreating(ByVal sender As Object, ByVal e As ScrollBarAnnotationsCreatingEventArgs)
            If changedRows IsNot Nothing Then
                Dim info As ScrollBarAnnotationInfo = New ScrollBarAnnotationInfo(Brushes.Green, ScrollBarAnnotationAlignment.Left, 4, 3)
                e.CustomScrollBarAnnotations = New List(Of ScrollBarAnnotationRowInfo)(changedRows.[Select](Function(x) New ScrollBarAnnotationRowInfo(view.GetNodeByKeyValue(x).RowHandle, info)))
            End If
        End Sub

        Private Sub view_NodeChanged(ByVal sender As Object, ByVal e As TreeList.TreeListNodeChangedEventArgs)
            If e.ChangeType = NodeChangeType.Content AndAlso changedRows IsNot Nothing Then changedRows.Add(CType(e.Node.Content, SalesData).ID)
        End Sub

        Private Sub listAnnotaions_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            listAnnotaions.SelectAll()
        End Sub
    End Class

    Public Class ToScrollBarAnnotationsModeConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim list = TryCast(value, IEnumerable)
            If list Is Nothing Then Return ScrollBarAnnotationMode.None
            Dim result As ScrollBarAnnotationMode = ScrollBarAnnotationMode.None
            For Each item In list
                result = result Or CType(item, AnnotationsDataContext.AnnotationsItem).Mode
            Next

            Return result
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ColorAnnotationConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim key = TryCast(values(0), TableViewThemeKeyExtension)
            If key Is Nothing Then Return values(3)
            Dim treeWalker = TryCast(values(1), ThemeTreeWalker)
            Dim frameworkElement = TryCast(values(2), FrameworkElement)
            Dim converter As ThemeResourceConverter = New ThemeResourceConverter()
            Return converter.Convert(New Object() {treeWalker, frameworkElement}, targetType, key, culture)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class AnnotationsDataContext

        Private _AnnotationsItems As List(Of TreeListDemo.AnnotationsDataContext.AnnotationsItem)

        Public Class AnnotationsItem

            Public Property Name As String

            Public Property Mode As ScrollBarAnnotationMode

            Public Property ResourceKey As Object

            Public Property CustomColor As Brush
        End Class

        Public Property AnnotationsItems As List(Of AnnotationsItem)
            Get
                Return _AnnotationsItems
            End Get

            Private Set(ByVal value As List(Of AnnotationsItem))
                _AnnotationsItems = value
            End Set
        End Property

        Public Sub New()
            AnnotationsItems = New List(Of AnnotationsItem)() From {New AnnotationsItem() With {.Name = "Focused Row", .Mode = ScrollBarAnnotationMode.FocusedRow, .ResourceKey = New TableViewThemeKeyExtension() With {.ResourceKey = TableViewThemeKeys.AnnotationFocusedRowBrush}}, New AnnotationsItem() With {.Name = "Selected Rows", .Mode = ScrollBarAnnotationMode.Selected, .ResourceKey = New TableViewThemeKeyExtension() With {.ResourceKey = TableViewThemeKeys.AnnotationSelectionBrush}}, New AnnotationsItem() With {.Name = "Search Results", .Mode = ScrollBarAnnotationMode.SearchResult, .ResourceKey = New TableViewThemeKeyExtension() With {.ResourceKey = TableViewThemeKeys.AnnotationSearchBrush}}, New AnnotationsItem() With {.Name = "Invalid Cells", .Mode = ScrollBarAnnotationMode.InvalidCells, .ResourceKey = New TableViewThemeKeyExtension() With {.ResourceKey = TableViewThemeKeys.AnnotationErrorBrush}}, New AnnotationsItem() With {.Name = "Modified Rows", .Mode = ScrollBarAnnotationMode.Custom, .ResourceKey = Nothing, .CustomColor = Brushes.Green}}
        End Sub
    End Class
End Namespace
