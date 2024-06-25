using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Functions;
using DevExpress.XtraSpreadsheet;

namespace SpreadsheetDemo {
    public partial class DocumentProperties : SpreadsheetDemoModule {

        public DocumentProperties() {
            InitializeComponent();
            RegisterCustomFunctions();
            InitializeWorkbook();
            GenerateCustomPropertiesWorksheet();
            spreadsheetControl1.DocumentPropertiesChanged += new DocumentPropertiesChangedEventHandler(spreadsheetControl1_DocumentPropertiesChanged);
        }

        void spreadsheetControl1_DocumentPropertiesChanged(object sender, DocumentPropertiesChangedEventArgs e) {
            if(e.BuiltInPropertiesChanged)
                spreadsheetControl1.Document.CalculateFull();
            if(e.CustomPropertiesChanged)
                GenerateCustomPropertiesWorksheet();
        }

        void InitializeWorkbook() {
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("DocumentProperties_template.xlsx"));
            spreadsheetControl1.Document.Unit = DevExpress.Office.DocumentUnit.Point;
        }

        void RegisterCustomFunctions() {
            IWorkbook workbook = spreadsheetControl1.Document;
            DocumentPropertyFunction customFunction = new DocumentPropertyFunction();
            if(!workbook.GlobalCustomFunctions.Contains(((ICustomFunction)customFunction).Name))
                workbook.GlobalCustomFunctions.Add(customFunction);
        }

        void GenerateCustomPropertiesWorksheet() {
            if(!ShouldGenerateCustomPropertiesWorksheet())
                return;

            IWorkbook workbook = spreadsheetControl1.Document;
            Worksheet worksheet = workbook.Worksheets[1];
            DevExpress.Spreadsheet.DocumentProperties properties = workbook.DocumentProperties;
            workbook.BeginUpdate();
            try {
                int rowIndex = 3;
                Cleanup(worksheet, rowIndex);
                foreach(string propertyName in properties.Custom.Names) {
                    CellValue propertyValue = properties.Custom[propertyName];
                    worksheet[rowIndex, 1].Value = propertyName;
                    worksheet[rowIndex, 2].Formula = string.Format("=DOCPROP(\"{0}\")", propertyName);
                    if(rowIndex % 2 != 0) {
                        worksheet[rowIndex, 1].Style = workbook.Styles["PropName1"];
                        worksheet[rowIndex, 2].Style = workbook.Styles["PropValue1"];
                    }
                    else {
                        worksheet[rowIndex, 1].Style = workbook.Styles["PropName2"];
                        worksheet[rowIndex, 2].Style = workbook.Styles["PropValue2"];
                    }
                    worksheet[rowIndex, 2].NumberFormat = propertyValue.IsDateTime ? "m/d/yyyy" : string.Empty;
                    worksheet.Rows[rowIndex].Height = 21;
                    rowIndex++;
                }
                SetLastRowBorder(worksheet, rowIndex);
            }
            finally {
                workbook.EndUpdate();
            }
        }

        bool ShouldGenerateCustomPropertiesWorksheet() {
            return spreadsheetControl1.Document.Worksheets.Contains("Custom");
        }

        void Cleanup(Worksheet worksheet, int rowIndex) {
            var usedRange = worksheet.GetUsedRange();
            if(usedRange.BottomRowIndex >= rowIndex) {
                var range = worksheet.Range.FromLTRB(1, rowIndex, 2, usedRange.BottomRowIndex);
                range.ClearContents();
                range.ClearFormats();
                range.Fill.BackgroundColor = Color.White;
            }
        }

        void SetLastRowBorder(Worksheet worksheet, int rowIndex) {
            var range = worksheet.Range.FromLTRB(1, rowIndex - 1, 2, rowIndex - 1);
            range.Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            range.Borders.BottomBorder.Color = Color.Gray;
        }
    }
    #region DocumentPropertyFunction
    public class DocumentPropertyFunction : Object, ICustomFunction {
        const string functionName = "DOCPROP";
        readonly ParameterInfo[] functionParameters;

        public DocumentPropertyFunction() {
            this.functionParameters = new ParameterInfo[] { new ParameterInfo(ParameterType.Value) };
        }

        string IFunction.Name { get { return functionName; } }
        ParameterInfo[] IFunction.Parameters { get { return functionParameters; } }
        ParameterType IFunction.ReturnType { get { return ParameterType.Value; } }
        bool IFunction.Volatile { get { return true; } }

        ParameterValue IFunction.Evaluate(IList<ParameterValue> parameters, EvaluationContext context) {
            ParameterValue propertyNameParam = parameters[0];
            if(propertyNameParam.IsError)
                return propertyNameParam;
            if(propertyNameParam.IsText) {
                string propertyName = propertyNameParam.TextValue;
                DevExpress.Spreadsheet.DocumentProperties properties = context.Sheet.Workbook.DocumentProperties;
                
                if(propertyName.Equals("Application", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Application;
                if(propertyName.Equals("Manager", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Manager;
                if(propertyName.Equals("Company", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Company;
                if(propertyName.Equals("Version", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Version;
                if(propertyName.Equals("Security", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Security.ToString();
                
                if(propertyName.Equals("Title", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Title;
                if(propertyName.Equals("Subject", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Subject;
                if(propertyName.Equals("Author", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Author;
                if(propertyName.Equals("Keywords", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Keywords;
                if(propertyName.Equals("Description", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Description;
                if(propertyName.Equals("LastModifiedBy", StringComparison.InvariantCultureIgnoreCase))
                    return properties.LastModifiedBy;
                if(propertyName.Equals("Category", StringComparison.InvariantCultureIgnoreCase))
                    return properties.Category;
                if(propertyName.Equals("Created", StringComparison.InvariantCultureIgnoreCase)) {
                    if(properties.Created != DateTime.MinValue)
                        return properties.Created;
                    return string.Empty;
                }
                if(propertyName.Equals("Modified", StringComparison.InvariantCultureIgnoreCase)) {
                    if(properties.Modified != DateTime.MinValue)
                        return properties.Modified;
                    return string.Empty;
                }
                if(propertyName.Equals("Printed", StringComparison.InvariantCultureIgnoreCase)) {
                    if(properties.Printed != DateTime.MinValue)
                        return properties.Printed;
                    return string.Empty;
                }
                return properties.Custom[propertyName];
            }
            return ParameterValue.ErrorInvalidValueInFunction;
        }
        string IFunction.GetName(CultureInfo culture) {
            return functionName;
        }
    }
    #endregion
}
