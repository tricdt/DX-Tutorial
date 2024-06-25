Imports DevExpress.Diagram.Core.Layout
Imports DevExpress.Xpf.Diagram
Imports System.Windows.Controls

Namespace DiagramDemo

    Public Partial Class TreeLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("TreeLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyTreeLayout()
        End Sub
    End Class
End Namespace
