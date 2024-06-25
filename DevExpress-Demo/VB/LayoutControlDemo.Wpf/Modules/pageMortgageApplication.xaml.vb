Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    Public Partial Class pageMortgageApplication
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
            UpdateTotalCosts()
            UpdateLoanAmount()
        End Sub

        Private Sub layoutGroup_SelectedTabChildChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs(Of FrameworkElement))
            Dim group1 = CType(sender, LayoutGroup)
            Dim group2 As LayoutGroup = If(group1 Is layoutGroup9, layoutGroup12, layoutGroup9)
            group2.SelectedTabIndex = group1.SelectedTabIndex
        End Sub

        Private Sub editTotalCostsItem_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateTotalCosts()
        End Sub

        Private Sub editLoanAmountItem_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateLoanAmount()
        End Sub

        Private Sub editCashToFromBorrowerItem_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateCashToFromBorrower()
        End Sub

        Private Sub ShowAs_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If groupContainer Is Nothing Then Return
            Dim containerView, childView As LayoutGroupView
            If sender Is checkShowAsGroupBoxes Then
                containerView = LayoutGroupView.GroupBox
                childView = LayoutGroupView.GroupBox
            ElseIf sender Is checkShowAsTabs Then
                containerView = LayoutGroupView.Tabs
                childView = LayoutGroupView.Group
            Else
                Return
            End If

            groupContainer.View = containerView
            For Each child As FrameworkElement In groupContainer.GetLogicalChildren(False)
                If TypeOf child Is LayoutGroup Then CType(child, LayoutGroup).View = childView
            Next
        End Sub

        Private Sub UpdateTotalCosts()
            If editTotalCosts Is Nothing OrElse editPurchasePrice Is Nothing OrElse editPrepaidItems Is Nothing OrElse editClosingCosts Is Nothing OrElse editFees Is Nothing OrElse editDiscount Is Nothing Then Return
            editTotalCosts.EditValue = CDbl(editPurchasePrice.EditValue) + CDbl(editPrepaidItems.EditValue) + CDbl(editClosingCosts.EditValue) + CDbl(editFees.EditValue) + CDbl(editDiscount.EditValue)
        End Sub

        Private Sub UpdateLoanAmount()
            If editLoanAmount1 Is Nothing OrElse editLoanAmount2 Is Nothing OrElse editLoanAmountWithoutFees Is Nothing OrElse editFeesFinanced Is Nothing Then Return
            editLoanAmount2.EditValue = CDbl(editLoanAmountWithoutFees.EditValue) + CDbl(editFeesFinanced.EditValue)
            editLoanAmount1.EditValue = editLoanAmount2.EditValue
        End Sub

        Private Sub UpdateCashToFromBorrower()
            If editCashToFromBorrower Is Nothing OrElse editTotalCosts Is Nothing OrElse editClosingCostsPaidBySeller Is Nothing OrElse editLoanAmount2 Is Nothing OrElse editTotalCosts.EditValue Is Nothing OrElse editLoanAmount2.EditValue Is Nothing Then Return
            editCashToFromBorrower.EditValue = CDbl(editTotalCosts.EditValue) - CDbl(editClosingCostsPaidBySeller.EditValue) - CDbl(editLoanAmount2.EditValue)
        End Sub
    End Class
End Namespace
