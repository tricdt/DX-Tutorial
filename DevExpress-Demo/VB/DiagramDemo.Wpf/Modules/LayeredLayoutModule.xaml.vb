Imports DevExpress.Diagram.Core.Layout
Imports DevExpress.Xpf.Diagram
Imports System.Windows.Controls

Namespace DiagramDemo

    Public Partial Class LayeredLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("LayeredLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplySugiyamaLayout()
        End Sub
    End Class
End Namespace
