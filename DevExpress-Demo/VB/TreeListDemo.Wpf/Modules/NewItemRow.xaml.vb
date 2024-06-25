Imports DevExpress.Xpf.Grid
Imports System.Windows.Controls

Namespace TreeListDemo

    Public Partial Class NewItemRow
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnInitNewNode(ByVal sender As Object, ByVal e As TreeList.TreeListNodeEventArgs)
            view.SetNodeValue(e.Node, "ID", view.TotalNodesCount + 1)
            view.SetNodeValue(e.Node, "StartDate", Date.Now)
            view.SetNodeValue(e.Node, "DueDate", Date.Now)
        End Sub
    End Class
End Namespace
