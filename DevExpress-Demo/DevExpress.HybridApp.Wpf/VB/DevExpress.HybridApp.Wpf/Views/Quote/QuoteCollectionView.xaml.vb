Imports System.Windows.Controls
Imports DevExpress.Xpf.PivotGrid
Imports System.Globalization
Imports System.Windows
Imports System.Threading

Namespace DevExpress.DevAV.Views

    Public Partial Class QuoteCollectionView
        Inherits UserControl

        Public Sub New()
            Dim culture = New CultureInfo("en-us")
            Me.InitializeComponent()
            Thread.CurrentThread.CurrentCulture = culture
            Thread.CurrentThread.CurrentUICulture = culture
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

        Private Sub pivotGridSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            Dim pivotGrid As PivotGridControl = CType(sender, PivotGridControl)
            Dim fieldPercentage As PivotGridField = pivotGrid.Fields.GetFieldByName("fieldPercentage")
            Dim fieldQuote As PivotGridField = pivotGrid.Fields.GetFieldByName("fieldQuote")
            Dim estimatedWidth As Double = e.NewSize.Width - pivotGrid.RowTreeOffset - pivotGrid.RowTreeMinWidth - fieldQuote.MinWidth - 3
            If estimatedWidth > fieldPercentage.MinWidth Then fieldPercentage.Width = estimatedWidth
        End Sub
    End Class
End Namespace
