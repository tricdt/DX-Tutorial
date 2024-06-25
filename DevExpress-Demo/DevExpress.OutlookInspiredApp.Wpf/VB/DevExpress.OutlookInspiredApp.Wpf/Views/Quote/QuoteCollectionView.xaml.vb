Imports DevExpress.Xpf.PivotGrid
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class QuoteCollectionView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub CustomSummary(ByVal sender As Object, ByVal e As PivotCustomSummaryEventArgs)
            Select Case e.DataField.FieldName
                Case "Percentage"
                    Dim quoteSummary As Decimal = 0
                    Dim opportunitySummary As Decimal = 0
                    For Each row As PivotDrillDownDataRow In e.CreateDrillDownDataSource()
                        quoteSummary += CDec(row("Total"))
                        opportunitySummary += CDec(row("MoneyOpportunity"))
                    Next

                    If quoteSummary <> 0 Then e.CustomValue = 100D * opportunitySummary / quoteSummary
            End Select
        End Sub
    End Class
End Namespace
