using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Docs.Text;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Spreadsheet.Functions;

namespace SpreadsheetDemo {
    public partial class CustomFunction : SpreadsheetDemoModule {
        public CustomFunction() {
            InitializeComponent();

            RegisterCustomFunction();
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("NumberInWords_template.xlsx"));
            spreadsheetControl1.CellValueChanged += spreadsheetControl1_CellValueChanged;
        }

        void RegisterCustomFunction() {
            NumberInWordsFunction customFunction = new NumberInWordsFunction();
            if (!spreadsheetControl1.Document.GlobalCustomFunctions.Contains(((ICustomFunction)customFunction).Name))
                spreadsheetControl1.Document.GlobalCustomFunctions.Add(customFunction);
        }

        private void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
            Worksheet sheet = spreadsheetControl1.Document.Worksheets[0];
            if (e.SheetName == sheet.Name && e.RowIndex == 3 && e.ColumnIndex == 2) {
                sheet.Columns[2].AutoFit();
                sheet.Columns[4].AutoFit();
            }
        }
    }
}

public class NumberInWordsFunction : Object, ICustomFunction {
    const string functionName = "SPELLNUMBER";
    readonly ParameterInfo[] functionParameters;
    static List<CultureInfo> cultureInfoParamTable = CreateCultureInfoParamTable();

    public NumberInWordsFunction() {
        this.functionParameters = new ParameterInfo[] { 
            new ParameterInfo(ParameterType.Value), 
            new ParameterInfo(ParameterType.Value), 
            new ParameterInfo(ParameterType.Value, ParameterAttributes.Optional) };
    }

    string IFunction.Name { get { return functionName; } }
    ParameterInfo[] IFunction.Parameters { get { return functionParameters; } }
    ParameterType IFunction.ReturnType { get { return ParameterType.Value; } }
    bool IFunction.Volatile { get { return true; } }

    ParameterValue IFunction.Evaluate(IList<ParameterValue> parameters, EvaluationContext context) {
        bool isOrdinal = false;
        ParameterValue numberValue = parameters[0];
        if (numberValue.IsError)
            return numberValue;

        ParameterValue cultureValue = parameters[1];
        if (cultureValue.IsError)
            return cultureValue;
        if (cultureValue.NumericValue < 1 || cultureValue.NumericValue > cultureInfoParamTable.Count)
            return ParameterValue.ErrorNumber;

        if (parameters.Count == 3) {
            ParameterValue ordinalValue = parameters[1];
            if (ordinalValue.IsError)
                return ordinalValue;

            isOrdinal = ordinalValue.BooleanValue;
        }

        long number = 0;
        try {
            number = Convert.ToInt64(numberValue.NumericValue);
        } catch (OverflowException) {
            return ParameterValue.ErrorNumber;
        }
        if (number < 0)
            return ParameterValue.ErrorNumber;

        CultureInfo culture = cultureInfoParamTable[(int)cultureValue.NumericValue - 1];
        if (isOrdinal)
            return NumberInWords.Ordinal.ConvertToText(number, culture);
        else
            return NumberInWords.Cardinal.ConvertToText(number, culture);
    }
    string IFunction.GetName(CultureInfo culture) {
        return functionName;
    }
    static List<CultureInfo> CreateCultureInfoParamTable() {
        List<CultureInfo> result = new List<CultureInfo>();
        result.Add(CultureInfo.GetCultureInfo("en-US"));
        result.Add(CultureInfo.GetCultureInfo("en-GB"));
        result.Add(CultureInfo.GetCultureInfo("fr-FR"));
        result.Add(CultureInfo.GetCultureInfo("de-DE"));
        result.Add(CultureInfo.GetCultureInfo("el-GR"));
        result.Add(CultureInfo.GetCultureInfo("hi-IN"));
        result.Add(CultureInfo.GetCultureInfo("it-IT"));
        result.Add(CultureInfo.GetCultureInfo("pt-PT"));
        result.Add(CultureInfo.GetCultureInfo("ru-RU"));
        result.Add(CultureInfo.GetCultureInfo("es-ES"));
        result.Add(CultureInfo.GetCultureInfo("sv-SE"));
        result.Add(CultureInfo.GetCultureInfo("th-TH"));
        result.Add(CultureInfo.GetCultureInfo("tr-TR"));
        result.Add(CultureInfo.GetCultureInfo("uk-UA"));
        return result;
    }
}
