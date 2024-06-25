Imports DevExpress.Diagram.Core.Layout
Imports DevExpress.Xpf.Diagram
Imports System.Windows.Controls

Namespace DiagramDemo

    Public Partial Class CircularLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("CircularLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyCircularLayout()
        End Sub
    End Class
End Namespace
