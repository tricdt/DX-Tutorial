Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Demos

Namespace DiagramDemo

    Public Partial Class FloorPlanModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeStencils()
            diagramControl.DocumentSource = GetDataFilePath("OfficePlan.xml")
        End Sub

        Private homeObjectsStencil As DiagramStencil

        Private Sub InitializeStencils()
            homeObjectsStencil = CreatePredefinedSvgStencil("HomeObjects", "Home Objects", True)
            diagramControl.Stencils = CreateExtendedStencilCollection(homeObjectsStencil)
        End Sub

        Private Sub diagramControl_ItemInitializing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Diagram.DiagramItemInitializingEventArgs)
            Call InitializeSvgShape(homeObjectsStencil, TryCast(e.Item, IDiagramShape))
        End Sub
    End Class
End Namespace
