Imports DevExpress.Diagram.Demos
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class ExpandAndCollapseOrgChartModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            orgChartBehavior.ItemsSource = GetOrgChartEmployees()
        End Sub

        Private Sub OnItemsGenerated(ByVal sender As Object, ByVal e As DiagramItemsGeneratedEventArgs)
            For Each item In e.GeneratedItems
                item.ThemeStyleId = OrgChartHelpers.GetStyleID(item)
            Next
        End Sub
    End Class
End Namespace
