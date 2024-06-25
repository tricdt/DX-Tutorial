using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Xpf.Spreadsheet.Extensions.Internal;

namespace SpreadsheetDemo {
    public partial class DataEntryForm : SpreadsheetDemoModule {
        public DataEntryForm() {
            InitializeComponent();
            LoadInvoiceTemplate();
        }

        void LoadInvoiceTemplate() {
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("DevAVInvoicing.xltx"));
            InitializeDateFields();
            BindCustomEditors();
        }

        void InitializeDateFields() {
            IWorkbook workbook = spreadsheetControl1.Document;
            Worksheet invoice = workbook.Worksheets["Invoice"];
            invoice["B4"].Value = DateTime.Today;
            invoice["F18"].Value = DateTime.Today.AddDays(14);
        }

        void BindCustomEditors() {
            IWorkbook workbook = spreadsheetControl1.Document;
            Worksheet invoice = workbook.Worksheets["Invoice"];
            Worksheet customers = workbook.Worksheets["Customers"];
            Worksheet stores = workbook.Worksheets["Stores"];
            Worksheet employees = workbook.Worksheets["Employees"];
            Worksheet products = workbook.Worksheets["Products"];

            
            invoice.CustomCellInplaceEditors.Add(invoice["B4"], CustomCellInplaceEditorType.DateEdit);

            
            invoice.CustomCellInplaceEditors.Add(invoice["B10:E10"], CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(customers["A2:A21"]));

            
            invoice.CustomCellInplaceEditors.Add(invoice["H12:M12"], CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(stores["D5:D204"]), true);

            
            invoice.CustomCellInplaceEditors.Add(invoice["B18:C18"], CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(employees["I2:I52"]), true);
            
            invoice.CustomCellInplaceEditors.Add(invoice["F18:G18"], CustomCellInplaceEditorType.DateEdit);
            
            CellValue[] shipVia = { "Air", "Ground", "Rail" };
            invoice.CustomCellInplaceEditors.Add(invoice["H18:I18"], CustomCellInplaceEditorType.ComboBox, ValueObject.CreateListSource(shipVia));
            
            invoice.CustomCellInplaceEditors.Add(invoice["J18:K18"], CustomCellInplaceEditorType.Custom, "FOBSpinEdit");
            
            invoice.CustomCellInplaceEditors.Add(invoice["L18:M18"], CustomCellInplaceEditorType.Custom, "TermsSpinEdit");

            
            invoice.CustomCellInplaceEditors.Add(invoice["B22:B25"], CustomCellInplaceEditorType.Custom, "QtySpinEdit");
            
            invoice.CustomCellInplaceEditors.Add(invoice["C22:F25"], CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(products["B2:B20"]));
            
            invoice.CustomCellInplaceEditors.Add(invoice["I22:J25"], CustomCellInplaceEditorType.Custom, "DiscountSpinEdit");

            
            invoice.CustomCellInplaceEditors.Add(invoice["K27:M27"], CustomCellInplaceEditorType.Custom, "ShippingSpinEdit");
        }

        void spreadsheetControl1_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            spreadsheetControl1.Dispatcher.BeginInvoke(new Action(InitializeSelectedCell), DispatcherPriority.Render);
        }
        void InitializeSelectedCell() {
            spreadsheetControl1.SelectedCell = spreadsheetControl1.ActiveWorksheet["B10"];
        }


        void spreadsheetControl1_CustomCellEdit(object sender, DevExpress.Xpf.Spreadsheet.SpreadsheetCustomCellEditEventArgs e) {
            if (!e.ValueObject.IsText)
                return;
            if (e.ValueObject.TextValue == "FOBSpinEdit")
                e.EditSettings = CreateSpinEditSettings(0, 500, 5);
            else if (e.ValueObject.TextValue == "TermsSpinEdit")
                e.EditSettings = CreateSpinEditSettings(5, 30, 1);
            else if (e.ValueObject.TextValue == "QtySpinEdit")
                e.EditSettings = CreateSpinEditSettings(1, 100, 1);
            else if (e.ValueObject.TextValue == "DiscountSpinEdit")
                e.EditSettings = CreateSpinEditSettings(0, 1000, 10);
            else if (e.ValueObject.TextValue == "ShippingSpinEdit")
                e.EditSettings = CreateSpinEditSettings(10, 1000, 5);
        }

        SpinEditSettings CreateSpinEditSettings(int minValue, int maxValue, int increment) {
            SpinEditSettings settings = new SpinEditSettings();
            settings.MinValue = minValue;
            settings.MaxValue = maxValue;
            settings.Increment = increment;
            settings.IsFloatValue = false;
            return settings;
        }

        void ActivateEditor() {
            Worksheet sheet = spreadsheetControl1.ActiveWorksheet;
            if (sheet.Name == "Invoice") {
                IList<CustomCellInplaceEditor> editors = sheet.CustomCellInplaceEditors.GetCustomCellInplaceEditors(sheet.Selection);
                if (editors.Count == 1)
                    spreadsheetControl1.OpenCellEditor(DevExpress.XtraSpreadsheet.CellEditorMode.Edit);
            }
        }

        void spreadsheetControl1_SelectionChanged(object sender, EventArgs e) {
            ActivateEditor();
        }

        void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
            if (e.Action == DevExpress.XtraSpreadsheet.CellValueChangedAction.UndoRedo || e.OldValue == e.Cell.Value ||
                e.Cell.GetReferenceA1(ReferenceElement.IncludeSheetName) != "Invoice!B10")
                return;
            Worksheet invoice = e.Worksheet;
            Worksheet customerStores = spreadsheetControl1.Document.Worksheets["Stores"];
            
            string customerId = invoice["B11"].Value.TextValue;
            Table storesTable = customerStores.Tables[0];
            storesTable.AutoFilter.Clear();
            storesTable.AutoFilter.Columns[1].ApplyFilterCriteria(customerId);
            
            var range = storesTable.DataRange;
            for (int rowOffset = 0; rowOffset < range.RowCount; rowOffset++) {
                if (range[rowOffset, 1].Value.TextValue == customerId) {
                    invoice["H12"].Value = range[rowOffset, 3].Value.TextValue;
                    return;
                }
            }
            invoice["H12"].Value = CellValue.Empty;
        }

        void RemoveOrderItem(Worksheet sheet, int rowIndex) {
            if (spreadsheetControl1.IsCellEditorActive)
                spreadsheetControl1.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
            sheet.Rows.Remove(rowIndex, 1);
            DefinedName invoiceItems = sheet.DefinedNames.GetDefinedName("InvoiceItems");
            if (invoiceItems != null && rowIndex > invoiceItems.Range.BottomRowIndex)
                rowIndex = invoiceItems.Range.BottomRowIndex;
            spreadsheetControl1.SelectedCell = sheet[rowIndex, 2];
            ActivateEditor();
        }

        void AddOrderItem(Worksheet sheet) {
            if (spreadsheetControl1.IsCellEditorActive)
                spreadsheetControl1.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Cancel);
            AddRecord(sheet);
            ActivateEditor();
        }

        
        void AddRecord(Worksheet sheet) {
            spreadsheetControl1.BeginUpdate();
            try {
                DefinedName invoiceItems = sheet.DefinedNames.GetDefinedName("InvoiceItems");
                int rowIndex = invoiceItems.Range.BottomRowIndex;
                sheet.Rows.Insert(rowIndex);
                sheet.Rows[rowIndex].Height = sheet.Rows[rowIndex + 1].Height;
                var range = invoiceItems.Range;
                var itemRange = sheet.Range.FromLTRB(range.LeftColumnIndex, range.BottomRowIndex, range.RightColumnIndex + 1, range.BottomRowIndex);
                if (range.RowCount == 1) {
                    sheet["K24"].FormulaInvariant = "=SUM(K22:K23)";
                    invoiceItems.Range = sheet.Range.FromLTRB(range.LeftColumnIndex, range.TopRowIndex - 1, range.RightColumnIndex, range.BottomRowIndex).GetRangeWithAbsoluteReference();
                    RecreateCustomEditors();
                }
                MoveUpLastRecord(itemRange);
                InitializeRecord(itemRange);
                spreadsheetControl1.SelectedCell = itemRange[1];
            }
            finally {
                spreadsheetControl1.EndUpdate();
            }
        }

        void RecreateCustomEditors() {
            IWorkbook workbook = spreadsheetControl1.Document;
            Worksheet invoice = workbook.Worksheets["Invoice"];
            Worksheet products = workbook.Worksheets["Products"];

            
            invoice.CustomCellInplaceEditors.Remove(invoice["B23:M23"]);

            
            invoice.CustomCellInplaceEditors.Add(invoice["B22:B23"], CustomCellInplaceEditorType.Custom, "QtySpinEdit");
            
            invoice.CustomCellInplaceEditors.Add(invoice["C22:F23"], CustomCellInplaceEditorType.ComboBox, ValueObject.FromRange(products["B2:B20"]));
            
            invoice.CustomCellInplaceEditors.Add(invoice["I22:J23"], CustomCellInplaceEditorType.Custom, "DiscountSpinEdit");
        }

        
        void MoveUpLastRecord(CellRange itemRange) {
            var range = itemRange.Offset(-1, 0);
            range.CopyFrom(itemRange, PasteSpecial.All, true);
        }

        
        void InitializeRecord(CellRange itemRange) {
            itemRange[0].Value = 1; 
            itemRange[1].Value = CellValue.Empty; 
            itemRange[7].Value = 0; 
            itemRange[12].Value = " ";
        }

        void spreadsheetControl1_ProtectionWarning(object sender, System.ComponentModel.HandledEventArgs e) {
            e.Handled = true;
        }

        void spreadsheetControl1_EmptyDocumentCreated(object sender, EventArgs e) {
            LoadInvoiceTemplate();
        }

        void spreadsheetControl1_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Cell cell = spreadsheetControl1.GetCellFromPoint(e.GetPosition(spreadsheetControl1).ToDrawingPoint());
            if (cell == null || cell.Worksheet.Name != "Invoice")
                return;
            Worksheet sheet = cell.Worksheet;
            DefinedName invoiceItems = sheet.DefinedNames.GetDefinedName("InvoiceItems");
            
            if (cell.ColumnIndex >= 2 && cell.ColumnIndex <= 3 &&
                cell.RowIndex == (invoiceItems == null ? 21 : invoiceItems.Range.BottomRowIndex + 1)) {
                AddOrderItem(sheet);

            }
            
            if (invoiceItems != null && cell.ColumnIndex >= 13 && cell.ColumnIndex <= 15 && invoiceItems.Range.RowCount > 1 &&
                cell.RowIndex >= invoiceItems.Range.TopRowIndex && cell.RowIndex <= invoiceItems.Range.BottomRowIndex) {
                RemoveOrderItem(sheet, cell.RowIndex);
            }
        }
    }

    public class ManageOrderItemTemplateSelector : DataTemplateSelector {

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            CellData data = item as CellData;
            SpreadsheetControl control = (SpreadsheetControl)container;
            if (CanApplyDeleteOrderItemTemplate(data.Cell))
                return control.TryFindResource("DeleteOrderItemTemplate") as DataTemplate;
            if (CanApplyAddOrderItemTemplate(data.Cell))
                return control.TryFindResource("AddOrderItemTemplate") as DataTemplate;
            return base.SelectTemplate(item, container);
        }

        bool CanApplyAddOrderItemTemplate(Cell cell) {
            Worksheet sheet = cell.Worksheet;
            if (sheet.Name != "Invoice")
                return false;
            DefinedName invoiceItems = sheet.DefinedNames.GetDefinedName("InvoiceItems");
            return cell.ColumnIndex == 2 && cell.RowIndex == (invoiceItems == null ? 21 : invoiceItems.Range.BottomRowIndex + 1);
        }

        bool CanApplyDeleteOrderItemTemplate(Cell cell) {
            Worksheet sheet = cell.Worksheet;
            if (sheet.Name != "Invoice")
                return false;
            DefinedName invoiceItems = sheet.DefinedNames.GetDefinedName("InvoiceItems");
            return invoiceItems != null && cell.ColumnIndex == 13 && invoiceItems.Range.RowCount > 1 &&
                cell.RowIndex >= invoiceItems.Range.TopRowIndex && cell.RowIndex <= invoiceItems.Range.BottomRowIndex;
        }
    }

}
