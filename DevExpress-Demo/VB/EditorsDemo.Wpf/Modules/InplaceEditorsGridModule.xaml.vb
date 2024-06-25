Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid

Namespace EditorsDemo

    <CodeFile("ModuleResources/DataEditorTypesDescriptions.xaml")>
    <CodeFile("ModuleResources/DataEditorTypesTemplates.xaml")>
    <CodeFile("ModuleResources/DataEditorTypesClasses.(cs)")>
    Public Partial Class InplaceEditorsGridModule
        Inherits EditorsDemoModule

        Private _ButtonEditClickCommand As ICommand

        Private list As GridMultiEditorsList

        Public Sub New()
            DataContext = Me
            ButtonEditClickCommand = New DelegateCommand(Of Object)(AddressOf PART_Editor_DefaultButtonClick)
            InitializeComponent()
            UpdateDescription()
            AssignDataSource()
        End Sub

        Private Sub AssignDataSource()
            list = New GridMultiEditorsList()
            Dim templateSelector As GridMultiEditorsTemplateSelector = New GridMultiEditorsTemplateSelector()
            For Each propertyDescriptor As PropertyDescriptor In CType(list, ITypedList).GetItemProperties(Nothing)
                Dim column As GridColumn = New GridColumn() With {.FieldName = propertyDescriptor.Name}
                If Equals(column.FieldName, "Field") Then
                    column.AllowEditing = DefaultBoolean.False
                    column.Fixed = FixedStyle.Left
                End If

                If Equals(column.FieldName, "TemplateName") Then
                    column.Visible = False
                    column.ShowInColumnChooser = False
                End If

                If Equals(column.FieldName, "EditorType") Then
                    column.Fixed = FixedStyle.Right
                    column.AllowEditing = DefaultBoolean.False
                    column.Width = 200
                End If

                If Not Equals(column.FieldName, "Field") AndAlso Not Equals(column.FieldName, "EditorType") Then column.CellTemplateSelector = templateSelector
                grid.Columns.Add(column)
            Next

            grid.ItemsSource = list
        End Sub

        Private Sub PART_Editor_DefaultButtonClick(ByVal param As Object)
            Dim listBoxEdit As ListBoxEdit = New ListBoxEdit() With {.ItemsSource = NWindContext.Create().CountriesArray, .ShowBorder = False}
            listBoxEdit.EditValue = grid.GetCellValue(grid.View.FocusedRowHandle, CType(grid.CurrentColumn, GridColumn))
            Dim closedHandler As DialogClosedDelegate = Sub(ByVal dialogResult)
                If dialogResult = True Then
                    grid.View.ShowEditor()
                    grid.View.ActiveEditor.EditValue = listBoxEdit.EditValue
                End If
            End Sub
            Call FloatingContainer.ShowDialogContent(listBoxEdit, grid.View.ActiveEditor, New Size(400, 300), New FloatingContainerParameters() With {.ClosedDelegate = closedHandler, .Title = "Select Country", .ContainerFocusable = False})
        End Sub

        Private Sub TableView_ShowGridMenu(ByVal sender As Object, ByVal e As GridMenuEventArgs)
            If e.MenuType = GridMenuType.Column Then
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNames.GroupBox})
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNamesBase.SortAscending})
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNamesBase.SortDescending})
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNamesBase.ClearSorting})
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNames.GroupColumn})
            End If
        End Sub

        Private Sub TableView_FocusedRowChanged(ByVal sender As Object, ByVal e As FocusedRowHandleChangedEventArgs)
            UpdateDescription()
        End Sub

        Private lastDescription As String

        Private Sub UpdateDescription()
            If descriptionRichTextBox Is Nothing Then Return
            Dim blocks As BlockCollection = descriptionRichTextBox.Document.Blocks
            If grid.View.FocusedRowHandle = DataControlBase.InvalidRowHandle Then Return
            Dim newDescription As String = list.FieldDescriptions(grid.View.FocusedRowHandle).TemplateName & "Description"
            If Equals(newDescription, lastDescription) Then Return
            lastDescription = newDescription
            Dim control As ContentControl = New ContentControl() With {.Template = TryCast(Resources(newDescription), ControlTemplate)}
            control.ApplyTemplate()
            Dim container As ParagraphContainer = TryCast(VisualTreeHelper.GetChild(control, 0), ParagraphContainer)
            blocks.Clear()
            blocks.Add(container.Paragraph)
        End Sub

        Private Sub TableView_ShowingEditor(ByVal sender As Object, ByVal e As ShowingEditorEventArgs)
            e.Cancel = Equals(list.FieldDescriptions(grid.View.FocusedRowHandle).TemplateName, "ProgressBarEdit")
        End Sub

        Public Property ButtonEditClickCommand As ICommand
            Get
                Return _ButtonEditClickCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ButtonEditClickCommand = value
            End Set
        End Property
    End Class

    Public Class GridMultiEditorsTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim data As GridCellData = CType(item, GridCellData)
            Dim grid As GridControl = CType(data.View, GridViewBase).Grid
            Dim editorType As String = TryCast(grid.GetCellValue(data.RowData.RowHandle.Value, "TemplateName"), String)
            Return If(String.IsNullOrEmpty(editorType), Nothing, CType(grid.Resources(editorType), DataTemplate))
        End Function
    End Class

    Public Class GridMultiEditorsList
        Inherits MultiEditorsListBaseSQLite

        Protected Overrides Sub CreateColumnCollection()
            Dim pds = New MultiEditorsListPropertyDescriptorSQLite(Table.Count + 3 - 1) {}
            pds(0) = New MultiEditorsListPropertyDescriptorSQLite(Me, 0, "Field", True)
            For n As Integer = 1 To Table.Count + 1 - 1
                pds(n) = New MultiEditorsListPropertyDescriptorSQLite(Me, n, "Product #" & n, False)
            Next

            pds(Table.Count + 1) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 1, "EditorType", True)
            pds(Table.Count + 2) = New MultiEditorsListPropertyDescriptorSQLite(Me, Table.Count + 2, "TemplateName", True)
            ColumnCollection = New PropertyDescriptorCollection(pds)
        End Sub

        Public Overrides Function GetPropertyValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
            If columnIndex = 0 Then Return FieldDescriptions(rowIndex).ColumnName
            If columnIndex = Table.Count + 1 Then Return FieldDescriptions(rowIndex).EditorDisplayName
            If columnIndex = Table.Count + 2 Then Return FieldDescriptions(rowIndex).TemplateName
            Return Table(columnIndex - 1)(FieldDescriptions(rowIndex).ColumnName)
        End Function
    End Class
End Namespace
