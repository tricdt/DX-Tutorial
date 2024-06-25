using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    
    
    public partial class CalculatedFields : PivotGridDemoModule {
        public CalculatedFields() {
            InitializeComponent();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(true, false);
            teBonusName.Text = GetUniqueFieldName();
        }
        PivotGridField GetNewInvisibleBonusField() {
            PivotGridField newBonusField = new PivotGridField(string.Empty, FieldArea.DataArea) {
                ValueTemplate = (DataTemplate) Resources["UnboundFieldTemplate"],
                Name = GetUniqueFieldName(),
                Caption = teBonusName.Text,
                Visible = false,
                DataBinding = new ExpressionDataBinding(beExpression.Text)
            };
            return newBonusField;
        }
        string GetUniqueFieldName() {
            string fieldName = "NewBonus";
            int uniqueId = Enumerable.Range(0, int.MaxValue)
                .First(i => !pivotGrid.Fields.Select(x => x.Name).Contains(fieldName + i.ToString()));
            return fieldName + uniqueId.ToString();
        }
        void beExpression_Click(object sender, RoutedEventArgs e) {
            PivotGridField newBonus = GetNewInvisibleBonusField();
            pivotGrid.Fields.Add(newBonus);
            pivotGrid.ShowUnboundExpressionEditor(newBonus);
            beExpression.Text = ((ExpressionDataBinding)newBonus.DataBinding).Expression;
            pivotGrid.Fields.Remove(newBonus);
        }
        void btnAddField_Click(object sender, RoutedEventArgs e) {
            PivotGridField newBonus = GetNewInvisibleBonusField();
            newBonus.Visible = true;
            pivotGrid.Fields.Add(newBonus);
            teBonusName.Text = GetUniqueFieldName();
            beExpression.Text = string.Empty;
        }
        void teBonusName_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e) {
            btnAddField.IsEnabled = !string.IsNullOrEmpty(e.NewValue as string);
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            FieldValueElementData fieldValue = ((Control)sender).DataContext as FieldValueElementData;
            if(fieldValue != null && fieldValue.Field != null)
                pivotGrid.ShowUnboundExpressionEditor(fieldValue.Field);
        }
        void removeBonus_ItemClick(object sender, ItemClickEventArgs e) {
            FieldValueElementData fieldValue = ((BarButtonItem)sender).Tag as FieldValueElementData;
            if(fieldValue != null && fieldValue.Field != null)
                pivotGrid.Fields.Remove(fieldValue.Field);
        }
    }
}
