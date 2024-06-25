Imports DevExpress.Data.Filtering
Imports System.Windows.Controls

Namespace GridDemo.Filtering

    Public Partial Class FilterPanel
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            grid.AddMRUFilter(CriteriaOperator.Parse("[ShipCity] = 'Bergamo'"))
        End Sub
    End Class
End Namespace
