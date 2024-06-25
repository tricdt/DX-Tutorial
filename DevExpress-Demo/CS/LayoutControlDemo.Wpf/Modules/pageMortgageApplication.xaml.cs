using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    public partial class pageMortgageApplication : LayoutControlDemoModule {
        public pageMortgageApplication() {
            InitializeComponent();
            UpdateTotalCosts();
            UpdateLoanAmount();
        }

        private void layoutGroup_SelectedTabChildChanged(object sender, ValueChangedEventArgs<FrameworkElement> e) {
            var group1 = (LayoutGroup)sender;
            LayoutGroup group2 = group1 == layoutGroup9 ? layoutGroup12 : layoutGroup9;
            group2.SelectedTabIndex = group1.SelectedTabIndex;
        }

        private void editTotalCostsItem_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateTotalCosts();
        }
        private void editLoanAmountItem_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateLoanAmount();
        }
        private void editCashToFromBorrowerItem_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateCashToFromBorrower();
        }

        private void ShowAs_Checked(object sender, RoutedEventArgs e) {
            if (groupContainer == null)
                return;
            LayoutGroupView containerView, childView;
            if (sender == checkShowAsGroupBoxes) {
                containerView = LayoutGroupView.GroupBox;
                childView = LayoutGroupView.GroupBox;
            }
            else
                if (sender == checkShowAsTabs) {
                    containerView = LayoutGroupView.Tabs;
                    childView = LayoutGroupView.Group;
                }
                else
                    return;
            groupContainer.View = containerView;
            foreach (FrameworkElement child in groupContainer.GetLogicalChildren(false))
                if (child is LayoutGroup)
                    ((LayoutGroup)child).View = childView;
        }

        void UpdateTotalCosts() {
            if (editTotalCosts == null || editPurchasePrice == null || editPrepaidItems == null ||
                editClosingCosts == null || editFees == null || editDiscount == null)
                return;
            editTotalCosts.EditValue = (double)editPurchasePrice.EditValue + (double)editPrepaidItems.EditValue +
                (double)editClosingCosts.EditValue + (double)editFees.EditValue + (double)editDiscount.EditValue;
        }
        void UpdateLoanAmount() {
            if (editLoanAmount1 == null || editLoanAmount2 == null || editLoanAmountWithoutFees == null || editFeesFinanced == null)
                return;
            editLoanAmount1.EditValue = editLoanAmount2.EditValue =
                (double)editLoanAmountWithoutFees.EditValue + (double)editFeesFinanced.EditValue;
        }
        void UpdateCashToFromBorrower() {
            if (editCashToFromBorrower == null || editTotalCosts == null || editClosingCostsPaidBySeller == null ||
                editLoanAmount2 == null || editTotalCosts.EditValue == null || editLoanAmount2.EditValue == null)
                return;
            editCashToFromBorrower.EditValue = (double)editTotalCosts.EditValue - (double)editClosingCostsPaidBySeller.EditValue -
                (double)editLoanAmount2.EditValue;
        }
    }
}
