Imports System
Imports DevExpress.XtraSpreadsheet.Services
Imports DevExpress.XtraSpreadsheet.Commands

Namespace SpreadsheetDemo

    Public Partial Class PrintTitles
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub btnPageSetup_Click(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            Dim service As ISpreadsheetCommandFactoryService = CType(spreadsheetControl1.GetService(GetType(ISpreadsheetCommandFactoryService)), ISpreadsheetCommandFactoryService)
            Dim command As SpreadsheetCommand = service.CreateCommand(SpreadsheetCommandId.PageSetupSheet)
            command.ForceExecute(command.CreateDefaultCommandUIState())
        End Sub

        Private Sub btnPrintPreview_Click(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            Dim service As ISpreadsheetCommandFactoryService = CType(spreadsheetControl1.GetService(GetType(ISpreadsheetCommandFactoryService)), ISpreadsheetCommandFactoryService)
            Dim command As SpreadsheetCommand = service.CreateCommand(SpreadsheetCommandId.FilePrintPreview)
            command.ForceExecute(command.CreateDefaultCommandUIState())
        End Sub
    End Class
End Namespace
