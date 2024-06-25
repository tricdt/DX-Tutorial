Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.DemoData.Helpers

Namespace PivotGridDemo

    Public Class DrillDownViewModel

        Public Sub New()
            CanPerformDrillDown = True
        End Sub

        Public Property CanPerformDrillDown As Boolean

        Public Function CanShowDrillDown(ByVal cellInfo As CellInfo) As Boolean
            Return CanPerformDrillDown AndAlso cellInfo IsNot Nothing
        End Function

        Public Sub ShowDrillDown(ByVal cellInfo As CellInfo)
            Me.GetService(Of IDialogService)("DrillDownTemplate").ShowDialog(MessageButton.OK, "Drill Down Results", cellInfo)
        End Sub
    End Class
End Namespace
