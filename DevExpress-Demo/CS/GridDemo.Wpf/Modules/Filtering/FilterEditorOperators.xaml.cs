using System;
using System.Windows.Controls;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core.FilteringUI;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;

namespace GridDemo.Filtering {
    public partial class FilterEditorOperators : UserControl {
        const string CustomFunctionName = "LastYears";

        static FilterEditorOperators() {
            #region !
            var currentYear = DateTime.Now.Year;
            CriteriaOperator.RegisterCustomFunction(CustomFunctionFactory.Create(CustomFunctionName, (DateTime date, int threshold) => {
                return currentYear >= date.Year && currentYear - date.Year <= threshold;
            }));
            #endregion
        }

        public FilterEditorOperators() {
            InitializeComponent();
        }

        #region !
        void OnQueryOperators(object sender, FilterEditorQueryOperatorsEventArgs e) {
            if(e.FieldName == "OrderDate") {
                e.Operators.Clear();
                e.Operators.Add(new FilterEditorOperatorItem(FilterEditorOperatorType.Equal));
                e.Operators.Add(new FilterEditorOperatorItem(FilterEditorOperatorType.NotEqual));

                var customFunctionEditSettings = new BaseEditSettings[] { new TextEditSettings { MaskType = MaskType.Numeric, Mask = "D", MaskUseAsDisplayFormat = true } };
                e.Operators.Add(new FilterEditorOperatorItem(CustomFunctionName, customFunctionEditSettings) { Caption = "Last Years" });
            }
        }
        #endregion
    }
}
