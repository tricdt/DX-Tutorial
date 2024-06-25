Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class OrgChartLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("OrgChartLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyOrgChartLayout()
        End Sub
    End Class
End Namespace
