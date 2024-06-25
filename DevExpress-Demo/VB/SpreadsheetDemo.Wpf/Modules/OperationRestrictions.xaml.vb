Imports DevExpress.Spreadsheet.Demos
Imports DevExpress.Xpf.PropertyGrid

Namespace SpreadsheetDemo

    Public Partial Class OperationRestrictions
        Inherits SpreadsheetDemoModule

        Private optionsGroupCounter As Integer = 11

        Public Sub New()
            InitializeComponent()
            propertyGridControl1.SelectedObject = New BehaviorOptionsProvider(spreadsheetControl1.Options.Behavior)
            AddHandler propertyGridControl1.CustomExpand, AddressOf OnCustomExpand
        End Sub

        Private Sub OnCustomExpand(ByVal sender As Object, ByVal args As CustomExpandEventArgs)
            args.IsExpanded = True
            args.Handled = True
            optionsGroupCounter -= 1
            If optionsGroupCounter = 0 Then RemoveHandler propertyGridControl1.CustomExpand, AddressOf OnCustomExpand
        End Sub
    End Class
End Namespace
