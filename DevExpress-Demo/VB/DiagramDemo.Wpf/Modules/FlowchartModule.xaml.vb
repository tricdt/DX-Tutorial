Imports System.Windows.Controls

Namespace DiagramDemo

    Public Partial Class FlowchartModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("Flowchart.xml")
        End Sub
    End Class
End Namespace
