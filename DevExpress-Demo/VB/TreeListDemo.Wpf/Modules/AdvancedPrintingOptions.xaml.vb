Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid

Namespace TreeListDemo

    Public Partial Class AdvancedPrintingOptions
        Inherits PrintTreeListDemoModule

        Public Sub New()
            InitializeComponent()
            DXTabControl = tabControl
            AddHandler Loaded, AddressOf AdvancedPrintingOptions_Loaded
        End Sub

        Private Sub AdvancedPrintingOptions_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf AdvancedPrintingOptions_Loaded
            ExpandNodes()
            OnShowPrintPreviewInNewTab("TreeList Preview")
        End Sub

        Private Sub ExpandNodes()
            View.Nodes(0).IsExpanded = True
            View.Nodes(0).Nodes(0).IsExpanded = True
            View.Nodes(0).Nodes(1).IsExpanded = True
        End Sub

        Private Sub printStyleChooser_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If printStyleChooser.SelectedIndex > 0 Then
                treeView.PrintColumnHeaderStyle = CType(Resources("customPrintColumnHeaderStyle"), Style)
                treeView.PrintCellStyle = CType(Resources("customPrintCellStyle"), Style)
                treeView.PrintRowIndentStyle = CType(Resources("customIndentStyle"), Style)
                treeView.PrintTotalSummaryStyle = CType(Resources("customPrintTotalSummaryStyle"), Style)
            Else
                treeView.ClearValue(TreeListView.PrintColumnHeaderStyleProperty)
                treeView.ClearValue(DataViewBase.PrintCellStyleProperty)
                treeView.ClearValue(DataViewBase.PrintRowIndentStyleProperty)
                treeView.ClearValue(DataViewBase.PrintTotalSummaryStyleProperty)
            End If
        End Sub
    End Class
End Namespace
