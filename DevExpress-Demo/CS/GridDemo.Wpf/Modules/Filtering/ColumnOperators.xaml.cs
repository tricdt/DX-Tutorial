using System;
using System.Windows.Controls;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core.FilteringUI;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;

namespace GridDemo.Filtering {
    public partial class ColumnOperators : UserControl {
        #region !
        const string CustomFunctionName = "LastYears";

        static ColumnOperators() {
            var currentYear = DateTime.Now.Year;
            CriteriaOperator.RegisterCustomFunction(CustomFunctionFactory.Create(CustomFunctionName, (DateTime date, int threshold) => {
                return currentYear >= date.Year && currentYear - date.Year <= threshold;
            }));
        }

        void OnExcelStyleFilterQueryOperators(object sender, ExcelStyleFilterElementQueryOperatorsEventArgs e) {
            if(e.FieldName == "OrderDate") {
                e.Operators.Clear();
                e.Operators.Add(new ExcelStyleFilterElementOperatorItem(ExcelStyleFilterElementOperatorType.Equal));
                e.Operators.Add(new ExcelStyleFilterElementOperatorItem(ExcelStyleFilterElementOperatorType.NotEqual));

                var customFunctionEditSettings = new BaseEditSettings[] { new TextEditSettings { MaskType = MaskType.Numeric, Mask = "D", MaskUseAsDisplayFormat = true } };
                e.Operators.Add(new ExcelStyleFilterElementOperatorItem(CustomFunctionName, customFunctionEditSettings) { Caption = "Last Years" });
            }
        }
        #endregion

        public ColumnOperators() {
            InitializeComponent();
        }
    }
}
