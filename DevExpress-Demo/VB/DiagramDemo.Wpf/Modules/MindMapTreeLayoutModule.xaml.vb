Imports System.Windows
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Demos
Imports DevExpress.Diagram.Core.Routing

Namespace DiagramDemo

    Public Partial Class MindMapTreeLayoutModule
        Inherits DiagramDemo.LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return Me.diagramControl
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()
            Me.diagramControl.LoadDemoData("MindMapTreeLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            Me.diagramControl.ApplyMindMapTreeLayout()
        End Sub

        Private Sub CreateChild(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim parent = TryCast(Me.diagramControl.PrimarySelection, DevExpress.Xpf.Diagram.DiagramItem)
            Dim size = DevExpress.Diagram.Demos.MindMapHelpers.GetSize(DevExpress.Diagram.Demos.OrgChartHelpers.GetItemLevel(parent) + 1)
            Dim child = New DevExpress.Xpf.Diagram.DiagramShape() With {.Shape = DevExpress.Diagram.Core.BasicShapes.Ellipse, .Width = size.Width, .Height = size.Height, .FontSize = DevExpress.Diagram.Demos.MindMapHelpers.GetFontSize(DevExpress.Diagram.Demos.OrgChartHelpers.GetItemLevel(parent) + 1), .Content = "New Item"}
            child.ThemeStyleId = DevExpress.Diagram.Demos.MindMapHelpers.GetMindMapStyle(child, parent)
            Me.diagramControl.Items.Add(child)
            Me.diagramControl.Items.Add(New DevExpress.Xpf.Diagram.DiagramConnector() With {.Type = DevExpress.Diagram.Core.ConnectorType.Curved, .ThemeStyleId = child.ThemeStyleId, .StrokeThickness = 3, .BeginItem = parent, .EndItem = child})
            Me.RelayoutDiagramCore()
        End Sub
    End Class
End Namespace
