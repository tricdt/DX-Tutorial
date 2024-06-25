Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Spreadsheet
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.DevAV.Reports.Spreadsheet

Namespace DevExpress.DevAV.Views

    ''' <summary>
    ''' Interaction logic for InvoiceEditorView.xaml
    ''' </summary>
    Public Partial Class OrderView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    Public Class CellTemplateSelector
        Inherits DataTemplateSelector

        Public Property AddOrderItemTemplate As DataTemplate

        Public Property DeleteOrderItemTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim cell = CType(item, CellData).Cell
            If IsInvoiceWorksheet(cell) AndAlso IsRangeForAddCommand(cell, GetInvoiceItems(cell)) Then Return AddOrderItemTemplate
            If IsInvoiceWorksheet(cell) AndAlso IsRangeForDeleteCommand(cell, GetInvoiceItems(cell)) Then Return DeleteOrderItemTemplate
            Return MyBase.SelectTemplate(item, container)
        End Function

        Private Function IsRangeForDeleteCommand(ByVal cell As Cell, ByVal invoiceItems As DefinedName) As Boolean
            Return invoiceItems IsNot Nothing AndAlso invoiceItems.Range IsNot Nothing AndAlso cell.ColumnIndex = 10 AndAlso invoiceItems.Range.RowCount > 1 AndAlso cell.RowIndex >= invoiceItems.Range.TopRowIndex AndAlso cell.RowIndex <= invoiceItems.Range.BottomRowIndex
        End Function

        Private Function IsRangeForAddCommand(ByVal cell As Cell, ByVal invoiceItems As DefinedName) As Boolean
            Return cell.ColumnIndex = 6 AndAlso cell.RowIndex = If(invoiceItems Is Nothing OrElse invoiceItems.Range Is Nothing, 21, invoiceItems.Range.BottomRowIndex + 1)
        End Function

        Private Function GetInvoiceItems(ByVal cell As Cell) As DefinedName
            Return cell.Worksheet.DefinedNames.GetDefinedName("InvoiceItems")
        End Function

        Private Function IsInvoiceWorksheet(ByVal cell As Cell) As Boolean
            Return Equals(cell.Worksheet.Name, CellsHelper.InvoiceWorksheetName)
        End Function
    End Class
End Namespace
