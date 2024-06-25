Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Helpers
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Xpf.Editors.Native

Namespace PivotGridDemo

    Public Partial Class FieldsCustomization
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            pivotGrid.ShowFieldList()
        End Sub

        Private Sub ShowHideFieldList_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.IsFieldListVisible = Not pivotGrid.IsFieldListVisible
        End Sub

        Private Sub customizationStyle_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim IsFieldListVisible As Boolean = pivotGrid.IsFieldListVisible
            gbAdvancedCustSettings.MaxWidth = gbCustSettings.ActualWidth
            pivotGrid.FieldListStyle = CType([Enum].Parse(GetType(FieldListStyle), customizationStyle.SelectedItem.ToString()), FieldListStyle)
            pivotGrid.IsFieldListVisible = IsFieldListVisible
        End Sub

        Private Sub OnAllowCustomizationFormChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If Not IsLoaded Then Return
            pivotGrid.IsFieldListVisible = pivotGrid.AllowCustomizationForm
        End Sub

        Private Sub OnCustomizationLayoutsEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Dim layout As FieldListAllowedLayouts = CType(customizationLayouts.SelectedItems(0), FieldListAllowedLayouts)
            For Each layout2 As FieldListAllowedLayouts In customizationLayouts.SelectedItems
                layout = layout Or layout2
            Next

            pivotGrid.FieldListAllowedLayouts = layout
            EnsureCurrentLayoutItems(True)
        End Sub

        Private Sub OnCustomizationLayoutsPopupContentSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            customizationLayouts.GetOkButton().IsEnabled = GetListBox().SelectedItems.Count > 0
        End Sub

        Private Function GetListBox() As ListBox
            Return CType(LookUpEditHelper.GetVisualClient(customizationLayouts).InnerEditor, ListBox)
        End Function

        Private Sub OnCurrentLayoutEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            EnsureCurrentLayoutItems(False)
        End Sub

        Private Sub EnsureCurrentLayoutItems(ByVal includeCurrent As Boolean)
            currentLayout.Items.BeginUpdate()
            currentLayout.Items.Clear()
            For Each layout2 As FieldListAllowedLayouts In customizationLayouts.SelectedItems
                currentLayout.Items.Add(ToFieldListAllowedLayouts(layout2))
            Next

            If includeCurrent AndAlso Not currentLayout.Items.Contains(pivotGrid.FieldListLayout) Then currentLayout.Items.Add(pivotGrid.FieldListLayout)
            currentLayout.Items.EndUpdate()
        End Sub

        Private Function ToFieldListAllowedLayouts(ByVal layout As FieldListAllowedLayouts) As FieldListLayout
            Select Case layout
                Case FieldListAllowedLayouts.BottomPanelOnly1by4
                    Return FieldListLayout.BottomPanelOnly1by4
                Case FieldListAllowedLayouts.BottomPanelOnly2by2
                    Return FieldListLayout.BottomPanelOnly2by2
                Case FieldListAllowedLayouts.StackedDefault
                    Return FieldListLayout.StackedDefault
                Case FieldListAllowedLayouts.StackedSideBySide
                    Return FieldListLayout.StackedSideBySide
                Case FieldListAllowedLayouts.TopPanelOnly
                    Return FieldListLayout.TopPanelOnly
                Case Else
                    Return FieldListLayout.StackedDefault
            End Select
        End Function
    End Class
End Namespace
