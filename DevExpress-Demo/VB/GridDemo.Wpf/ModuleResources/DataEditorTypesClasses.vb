Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports DevExpress.Xpf.Grid
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo

    Public Class MultiEditorsTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim data As GridCellData = CType(item, GridCellData)
            Dim grid As GridControl = CType(data.View, GridViewBase).Grid
            Dim editorType As String = TryCast(grid.GetCellValue(data.RowData.RowHandle.Value, "TemplateName"), String)
            Return If(String.IsNullOrEmpty(editorType), Nothing, CType(grid.Resources(editorType), DataTemplate))
        End Function
    End Class

    Public Class MultiEditorsList
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
