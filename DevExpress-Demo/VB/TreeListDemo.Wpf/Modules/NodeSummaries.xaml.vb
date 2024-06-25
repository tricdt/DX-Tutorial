Imports System.Linq
Imports System.Windows.Controls

Namespace TreeListDemo

    Public Partial Class NodeSummaries
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnShowingNodeFooter(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListShowingNodeFooterEventArgs)
            If Not chkHideSingleNodeFooters.IsChecked.Value Then Return
            If e.IsRootNode OrElse e.Node.Nodes.Where(Function(node) node.IsVisible).Count() = 1 Then e.Allow = False
        End Sub
    End Class
End Namespace
