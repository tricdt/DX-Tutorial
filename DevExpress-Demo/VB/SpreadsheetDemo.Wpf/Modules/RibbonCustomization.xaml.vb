Imports System.Windows
Imports DevExpress.Xpf.Core

Namespace SpreadsheetDemo

    Public Partial Class RibbonCustomization
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub About_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            ThemedMessageBox.Show("Spreadsheet Ribbon Customization", "This demo illustrates how to customize the WPF Spreadsheet's integrated ribbon UI." & Microsoft.VisualBasic.Constants.vbLf & Microsoft.VisualBasic.Constants.vbLf & "Switch to the Code View to learn how to use the Spreadsheet Control's RibbonActions collection to create, remove or modify ribbon elements.", MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub
    End Class
End Namespace
