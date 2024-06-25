Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Threading
Imports System.Windows.Controls
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Spreadsheet
Imports DevExpress.Xpf.Spreadsheet.Extensions.Internal

Namespace SpreadsheetDemo

    Public Partial Class DataEntryForm
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            LoadInvoiceTemplate()
        End Sub

        Private Sub LoadInvoiceTemplate()
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("DevAVInvoicing.xltx"))
            InitializeDateFields()
            BindCustomEditors()
        End Sub

        Private Sub InitializeDateFields()
            Dim workbook As IWorkbook = spreadsheetControl1.Document
            Dim invoice As Worksheet = workbook.Worksheets("Invoice")
            invoice("B4").Value = Date.Today
            invoice("F18").Value = Date.Today.AddDays(14)
        End Sub

        Private Sub BindCustomEditors()
            Dim workbook As IWorkbook = spreadsheetControl1.Document
            Dim invoice As Worksheet = workbook.Worksheets("Invoice")
            Dim customers As Worksheet = workbook.Worksheets("Customers")
            Dim stores As Worksheet = workbook.Worksheets("Stores")
            Dim employees As Worksheet = workbook.Worksheets("Employees")
            Dim products As Worksheet = workbook.Worksheets("Products")
            invoice.CustomCellInplaceEditors.Add(invoice("B4"), CustomCellInplaceEditorType.DateEdit)
            invoice.CustomCellInplaceEditors.Add(invoice("B10:E10"), CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(customers("A2:A21")))
            invoice.CustomCellInplaceEditors.Add(invoice("H12:M12"), CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(stores("D5:D204")), True)
            invoice.CustomCellInplaceEditors.Add(invoice("B18:C18"), CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(employees("I2:I52")), True)
            invoice.CustomCellInplaceEditors.Add(invoice("F18:G18"), CustomCellInplaceEditorType.DateEdit)
            Dim shipVia As CellValue() = {"Air", "Ground", "Rail"}
            invoice.CustomCellInplaceEditors.Add(invoice("H18:I18"), CustomCellInplaceEditorType.ComboBox, ValueObject.CreateListSource(shipVia))
            invoice.CustomCellInplaceEditors.Add(invoice("J18:K18"), CustomCellInplaceEditorType.Custom, "FOBSpinEdit")
            invoice.CustomCellInplaceEditors.Add(invoice("L18:M18"), CustomCellInplaceEditorType.Custom, "TermsSpinEdit")
            invoice.CustomCellInplaceEditors.Add(invoice("B22:B25"), CustomCellInplaceEditorType.Custom, "QtySpinEdit")
            invoice.CustomCellInplaceEditors.Add(invoice("C22:F25"), CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(products("B2:B20")))
            invoice.CustomCellInplaceEditors.Add(invoice("I22:J25"), CustomCellInplaceEditorType.Custom, "DiscountSpinEdit")
            invoice.CustomCellInplaceEditors.Add(invoice("K27:M27"), CustomCellInplaceEditorType.Custom, "ShippingSpinEdit")
        End Sub

        Private Sub spreadsheetControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            spreadsheetControl1.Dispatcher.BeginInvoke(New Action(AddressOf InitializeSelectedCell), DispatcherPriority.Render)
        End Sub

        Private Sub InitializeSelectedCell()
            spreadsheetControl1.SelectedCell = spreadsheetControl1.ActiveWorksheet("B10")
        End Sub

        Private Sub spreadsheetControl1_CustomCellEdit(ByVal sender As Object, ByVal e As SpreadsheetCustomCellEditEventArgs)
            If Not e.ValueObject.IsText Then Return
            If Equals(e.ValueObject.TextValue, "FOBSpinEdit") Then
                e.EditSettings = CreateSpinEditSettings(0, 500, 5)
            ElseIf Equals(e.ValueObject.TextValue, "TermsSpinEdit") Then
                e.EditSettings = CreateSpinEditSettings(5, 30, 1)
            ElseIf Equals(e.ValueObject.TextValue, "QtySpinEdit") Then
                e.EditSettings = CreateSpinEditSettings(1, 100, 1)
            ElseIf Equals(e.ValueObject.TextValue, "DiscountSpinEdit") Then
                e.EditSettings = CreateSpinEditSettings(0, 1000, 10)
            ElseIf Equals(e.ValueObject.TextValue, "ShippingSpinEdit") Then
                e.EditSettings = CreateSpinEditSettings(10, 1000, 5)
            End If
        End Sub

        Private Function CreateSpinEditSettings(ByVal minValue As Integer, ByVal maxValue As Integer, ByVal increment As Integer) As SpinEditSettings
            Dim settings As SpinEditSettings = New SpinEditSettings()
            settings.MinValue = minValue
            settings.MaxValue = maxValue
            settings.Increment = increment
            settings.IsFloatValue = False
            Return settings
        End Function

        Private Sub ActivateEditor()
            Dim sheet As Worksheet = spreadsheetControl1.ActiveWorksheet
            If Equals(sheet.Name, "Invoice") Then
                Dim editors As IList(Of CustomCellInplaceEditor) = sheet.CustomCellInplaceEditors.GetCustomCellInplaceEditors(sheet.Selection)
                If editors.Count = 1 Then spreadsheetControl1.OpenCellEditor(DevExpress.XtraSpreadsheet.CellEditorMode.Edit)
            End If
        End Sub

        Private Sub spreadsheetControl1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            ActivateEditor()
        End Sub

        Private Sub spreadsheetControl1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs)
            If e.Action = DevExpress.XtraSpreadsheet.CellValueChangedAction.UndoRedo OrElse e.OldValue Is e.Cell.Value OrElse Not Equals(e.Cell.GetReferenceA1(ReferenceElement.IncludeSheetName), "Invoice!B10") Then Return
            Dim invoice As Worksheet = e.Worksheet
            Dim customerStores As Worksheet = spreadsheetControl1.Document.Worksheets("Stores")
            Dim customerId As String = invoice("B11").Value.TextValue
            Dim storesTable As Table = customerStores.Tables(0)
            storesTable.AutoFilter.Clear()
            storesTable.AutoFilter.Columns(1).ApplyFilterCriteria(customerId)
            Dim range = storesTable.DataRange
            For rowOffset As Integer = 0 To range.RowCount - 1
                If Equals(range(rowOffset, 1).Value.TextValue, customerId) Then
                    invoice("H12").Value = range(rowOffset, 3).Value.TextValue
                    Return
                End If
            Next

            invoice("H12").Value = CellValue.Empty
        End Sub

        Private Sub RemoveOrderItem(ByVal sheet As Worksheet, ByVal rowIndex As Integer)
            If spreadsheetControl1.IsCellEditorActive Then spreadsheetControl1.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel)
            sheet.Rows.Remove(rowIndex, 1)
            Dim invoiceItems As DefinedName = sheet.DefinedNames.GetDefinedName("InvoiceItems")
            If invoiceItems IsNot Nothing AndAlso rowIndex > invoiceItems.Range.BottomRowIndex Then rowIndex = invoiceItems.Range.BottomRowIndex
            spreadsheetControl1.SelectedCell = sheet(rowIndex, 2)
            ActivateEditor()
        End Sub

        Private Sub AddOrderItem(ByVal sheet As Worksheet)
            If spreadsheetControl1.IsCellEditorActive Then spreadsheetControl1.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel)
            AddRecord(sheet)
            ActivateEditor()
        End Sub

        Private Sub AddRecord(ByVal sheet As Worksheet)
            spreadsheetControl1.BeginUpdate()
            Try
                Dim invoiceItems As DefinedName = sheet.DefinedNames.GetDefinedName("InvoiceItems")
                Dim rowIndex As Integer = invoiceItems.Range.BottomRowIndex
                sheet.Rows.Insert(rowIndex)
                sheet.Rows(rowIndex).Height = sheet.Rows(rowIndex + 1).Height
                Dim range = invoiceItems.Range
                Dim itemRange = sheet.Range.FromLTRB(range.LeftColumnIndex, range.BottomRowIndex, range.RightColumnIndex + 1, range.BottomRowIndex)
                If range.RowCount = 1 Then
                    sheet("K24").FormulaInvariant = "=SUM(K22:K23)"
                    invoiceItems.Range = sheet.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex - 1, range.RightColumnIndex, range.BottomRowIndex).GetRangeWithAbsoluteReference()
                    RecreateCustomEditors()
                End If

                MoveUpLastRecord(itemRange)
                InitializeRecord(itemRange)
                spreadsheetControl1.SelectedCell = itemRange(1)
            Finally
                spreadsheetControl1.EndUpdate()
            End Try
        End Sub

        Private Sub RecreateCustomEditors()
            Dim workbook As IWorkbook = spreadsheetControl1.Document
            Dim invoice As Worksheet = workbook.Worksheets("Invoice")
            Dim products As Worksheet = workbook.Worksheets("Products")
            invoice.CustomCellInplaceEditors.Remove(invoice("B23:M23"))
            invoice.CustomCellInplaceEditors.Add(invoice("B22:B23"), CustomCellInplaceEditorType.Custom, "QtySpinEdit")
            invoice.CustomCellInplaceEditors.Add(invoice("C22:F23"), CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(products("B2:B20")))
            invoice.CustomCellInplaceEditors.Add(invoice("I22:J23"), CustomCellInplaceEditorType.Custom, "DiscountSpinEdit")
        End Sub

        Private Sub MoveUpLastRecord(ByVal itemRange As CellRange)
            Dim range = itemRange.Offset(-1, 0)
            range.CopyFrom(itemRange, PasteSpecial.All, True)
        End Sub

        Private Sub InitializeRecord(ByVal itemRange As CellRange)
            itemRange(0).Value = 1
            itemRange(1).Value = CellValue.Empty
            itemRange(7).Value = 0
            itemRange(12).Value = " "
        End Sub

        Private Sub spreadsheetControl1_ProtectionWarning(ByVal sender As Object, ByVal e As System.ComponentModel.HandledEventArgs)
            e.Handled = True
        End Sub

        Private Sub spreadsheetControl1_EmptyDocumentCreated(ByVal sender As Object, ByVal e As EventArgs)
            LoadInvoiceTemplate()
        End Sub

        Private Sub spreadsheetControl1_PreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As Input.MouseButtonEventArgs)
            Dim cell As Cell = spreadsheetControl1.GetCellFromPoint(e.GetPosition(spreadsheetControl1).ToDrawingPoint())
            If cell Is Nothing OrElse Not Equals(cell.Worksheet.Name, "Invoice") Then Return
            Dim sheet As Worksheet = cell.Worksheet
            Dim invoiceItems As DefinedName = sheet.DefinedNames.GetDefinedName("InvoiceItems")
            If cell.ColumnIndex >= 2 AndAlso cell.ColumnIndex <= 3 AndAlso cell.RowIndex = If(invoiceItems Is Nothing, 21, invoiceItems.Range.BottomRowIndex + 1) Then
                AddOrderItem(sheet)
            End If

            If invoiceItems IsNot Nothing AndAlso cell.ColumnIndex >= 13 AndAlso cell.ColumnIndex <= 15 AndAlso invoiceItems.Range.RowCount > 1 AndAlso cell.RowIndex >= invoiceItems.Range.TopRowIndex AndAlso cell.RowIndex <= invoiceItems.Range.BottomRowIndex Then
                RemoveOrderItem(sheet, cell.RowIndex)
            End If
        End Sub
    End Class

    Public Class ManageOrderItemTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim data As CellData = TryCast(item, CellData)
            Dim control As SpreadsheetControl = CType(container, SpreadsheetControl)
            If CanApplyDeleteOrderItemTemplate(data.Cell) Then Return TryCast(control.TryFindResource("DeleteOrderItemTemplate"), DataTemplate)
            If CanApplyAddOrderItemTemplate(data.Cell) Then Return TryCast(control.TryFindResource("AddOrderItemTemplate"), DataTemplate)
            Return MyBase.SelectTemplate(item, container)
        End Function

        Private Function CanApplyAddOrderItemTemplate(ByVal cell As Cell) As Boolean
            Dim sheet As Worksheet = cell.Worksheet
            If Not Equals(sheet.Name, "Invoice") Then Return False
            Dim invoiceItems As DefinedName = sheet.DefinedNames.GetDefinedName("InvoiceItems")
            Return cell.ColumnIndex = 2 AndAlso cell.RowIndex = If(invoiceItems Is Nothing, 21, invoiceItems.Range.BottomRowIndex + 1)
        End Function

        Private Function CanApplyDeleteOrderItemTemplate(ByVal cell As Cell) As Boolean
            Dim sheet As Worksheet = cell.Worksheet
            If Not Equals(sheet.Name, "Invoice") Then Return False
            Dim invoiceItems As DefinedName = sheet.DefinedNames.GetDefinedName("InvoiceItems")
            Return invoiceItems IsNot Nothing AndAlso cell.ColumnIndex = 13 AndAlso invoiceItems.Range.RowCount > 1 AndAlso cell.RowIndex >= invoiceItems.Range.TopRowIndex AndAlso cell.RowIndex <= invoiceItems.Range.BottomRowIndex
        End Function
    End Class
End Namespace
