Imports System.Windows.Controls

Namespace EditorsDemo.FilterBehavior

    Public Partial Class FilterBehaviorFilteringPanel
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            chart.DataSource = New DevAVBranchesSales().GetList()
        End Sub
    End Class
End Namespace
