Imports DevExpress.DemoData
Imports System.Linq
Imports System.Windows.Controls

Namespace GridDemo.Filtering

    Public Partial Class RangeElementRangeControlTemplate
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = NWindDataProvider.Invoices.Where(Function(x) x.Quantity < 20).ToList()
        End Sub
    End Class
End Namespace
