Imports DevExpress.DemoData.Models
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemosHelpers
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace TreeListDemo

    Public Partial Class DataEditorTypes
        Inherits TreeListDemoModule

        Private list As MultiEditorsList

        Public Sub New()
            InitializeComponent()
            UpdateDescription()
            AssignDataSource()
            treeListView.ExpandAllNodes()
        End Sub

        Private Sub AssignDataSource()
            list = New MultiEditorsList()
            Dim templateSelector As MultiEditorsTemplateSelector = New MultiEditorsTemplateSelector()
            For Each propertyDescriptor As PropertyDescriptor In CType(list, ITypedList).GetItemProperties(Nothing)
                Dim column As TreeListColumn = New TreeListColumn() With {.FieldName = propertyDescriptor.Name}
                If Equals(column.FieldName, "Field") Then
                    column.AllowEditing = DefaultBoolean.False
                    column.Fixed = FixedStyle.Right
                End If

                If Equals(column.FieldName, "TemplateName") Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If

                If Equals(column.FieldName, "EditorType") Then
                    column.Fixed = FixedStyle.Left
                    column.AllowEditing = DefaultBoolean.False
                    column.Width = 200
                End If

                If Equals(column.FieldName, "Id") Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If

                If Equals(column.FieldName, "ParentId") Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If

                If Not Equals(column.FieldName, "Field") AndAlso Not Equals(column.FieldName, "EditorType") Then column.CellTemplateSelector = templateSelector
                treeList.Columns.Add(column)
            Next

            treeList.ItemsSource = list
        End Sub

        Private Sub PART_Editor_DefaultButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim listBoxEdit As ListBoxEdit = New ListBoxEdit() With {.ItemsSource = NWindContext.Create().CountriesArray, .ShowBorder = False}
            listBoxEdit.EditValue = treeList.GetCellValue(treeListView.FocusedRowHandle, CType(treeList.CurrentColumn, TreeListColumn))
            Dim closedHandler As DialogClosedDelegate = Sub(ByVal dialogResult)
                If dialogResult = True Then
                    treeListView.ShowEditor()
                    treeListView.ActiveEditor.EditValue = listBoxEdit.EditValue
                End If
            End Sub
            Call FloatingContainer.ShowDialogContent(listBoxEdit, treeListView.ActiveEditor, New Size(400, 300), New FloatingContainerParameters() With {.ClosedDelegate = closedHandler, .Title = "Select Country", .ContainerFocusable = False})
        End Sub

        Private lastDescription As String

        Private Sub UpdateDescription()
            If descriptionRichTextBox Is Nothing Then Return
            Dim blocks As BlockCollection = descriptionRichTextBox.Document.Blocks
            If treeListView.FocusedRowHandle = DataControlBase.InvalidRowHandle Then Return
            Dim newDescription As String = list.FieldDescriptions(treeListView.FocusedRowHandle).TemplateName & "Description"
            If Equals(newDescription, lastDescription) Then Return
            lastDescription = newDescription
            Dim control As ContentControl = New ContentControl() With {.Template = TryCast(Resources(newDescription), ControlTemplate)}
            control.ApplyTemplate()
            Dim container As ParagraphContainer = TryCast(VisualTreeHelper.GetChild(control, 0), ParagraphContainer)
            blocks.Clear()
            blocks.Add(container.Paragraph)
        End Sub

        Private Sub TableView_ShowingEditor(ByVal sender As Object, ByVal e As TreeList.TreeListShowingEditorEventArgs)
            e.Cancel = Equals(list.FieldDescriptions(treeListView.FocusedRowHandle).TemplateName, "ProgressBarEdit")
        End Sub

        Private Sub CurrentItemChanged(ByVal sender As Object, ByVal e As CurrentItemChangedEventArgs)
            UpdateDescription()
        End Sub
    End Class

    Public Class MultiEditorsList
        Inherits MultiEditorsListBaseSQLite

        Protected Overrides Sub CreateColumnCollection()
            Dim pds As MultiEditorsListPropertyDescriptorSQLite() = New MultiEditorsListPropertyDescriptorSQLite(Table.Count + 5 - 1) {}
            pds(0) = New MultiEditorsListPropertyDescriptorSQLite(Me, 0, "EditorType", True)
            For n As Integer = 1 To Table.Count + 1 - 1
                pds(n) = New MultiEditorsListPropertyDescriptorSQLite(Me, n, "Product #" & n, False)
            Next

            pds(Table.Count + 1) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 1, "Field", True)
            pds(Table.Count + 2) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 2, "TemplateName", True)
            pds(Table.Count + 3) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 3, "Id", True)
            pds(Table.Count + 4) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 4, "ParentId", True)
            ColumnCollection = New PropertyDescriptorCollection(pds)
        End Sub

        Public Overrides Function GetPropertyValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
            If columnIndex = 0 Then Return FieldDescriptions(rowIndex).EditorDisplayName
            If columnIndex = Table.Count + 1 Then Return FieldDescriptions(rowIndex).ColumnName
            If columnIndex = Table.Count + 2 Then Return FieldDescriptions(rowIndex).TemplateName
            If columnIndex = Table.Count + 3 Then Return FieldDescriptions(rowIndex).Id
            If columnIndex = Table.Count + 4 Then Return FieldDescriptions(rowIndex).ParentId
            Return Table(columnIndex - 1)(FieldDescriptions(rowIndex).ColumnName)
        End Function
    End Class

    Public Class MultiEditorsTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim data As GridCellData = CType(item, GridCellData)
            Dim grid As TreeListControl = CType(data.View.DataControl, TreeListControl)
            Dim editorType As String = TryCast(grid.GetCellValue(data.RowData.RowHandle.Value, "TemplateName"), String)
            Return If(String.IsNullOrEmpty(editorType), Nothing, CType(grid.Resources(editorType), DataTemplate))
        End Function
    End Class

    Public Class ProductCategoryToImageConverter
        Inherits MarkupExtension
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

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
