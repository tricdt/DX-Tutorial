Imports DevExpress.Data.Filtering
Imports DevExpress.DemoData
Imports System.Linq
Imports System.Windows.Controls

Namespace GridDemo.Filtering

    Public Partial Class DateRangeElementRangeControlTemplate
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = NWindDataProvider.InvoicesUpToDate.Where(Function(x) x.OrderDate.HasValue AndAlso x.OrderDate.Value >= Date.Today.AddDays(-20))
            grid.FilterCriteria = New OperandProperty("OrderDate") >= Date.Today.AddDays(-5) And New OperandProperty("OrderDate") < Date.Today.AddDays(1)
        End Sub
    End Class
End Namespace
