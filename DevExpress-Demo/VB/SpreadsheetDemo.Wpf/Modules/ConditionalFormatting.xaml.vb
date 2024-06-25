Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet.Demos

Namespace SpreadsheetDemo

    Public Partial Class ConditionalFormatting
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub spreadsheetControl1_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            UpdateConditionalFormatting()
        End Sub

        Private Sub OnCheckedChanged(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            UpdateConditionalFormatting()
        End Sub

        Private Sub UpdateConditionalFormatting()
            If spreadsheetControl1 Is Nothing Then Return
            spreadsheetControl1.BeginUpdate()
            Try
                Dim sheet As Worksheet = spreadsheetControl1.ActiveWorksheet
                sheet.ConditionalFormattings.Clear()
                If chkImports.IsChecked = True Then
                    ApplyTopImportsConditionalFormatting(sheet)
                    ApplyImportsYearlyChangeConditionalFormatting(sheet)
                End If

                If chkExports.IsChecked = True Then
                    ApplyTopExportsConditionalFormatting(sheet)
                    ApplyExportsYearlyChangeConditionalFormatting(sheet)
                End If

                If chkBalance.IsChecked = True Then
                    ApplyBalanceTrendConditionalFormatting(sheet)
                    ApplyBalanceChangeConditionalFormatting(sheet)
                End If

                If chkAsiaRegion.IsChecked = True Then ApplyAsiaCountriesConditionalFormatting(sheet)
            Finally
                spreadsheetControl1.EndUpdate()
            End Try
        End Sub
    End Class
End Namespace
