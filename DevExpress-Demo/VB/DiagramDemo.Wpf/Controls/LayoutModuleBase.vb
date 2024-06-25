Imports DevExpress.Xpf.Diagram
Imports System.Windows

Namespace DiagramDemo

    Public MustInherit Class LayoutModuleBase
        Inherits DiagramDemoModule

        Protected MustOverride ReadOnly Property Diagram As DiagramControl

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoad
        End Sub

        Private Sub OnLoad(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RelayoutDiagram()
        End Sub

        Protected Sub SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RelayoutDiagram()
        End Sub

        Protected Sub EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            RelayoutDiagram()
        End Sub

        Private Sub RelayoutDiagram()
            If Not IsLoaded Then Return
            RelayoutDiagramCore()
            UpdateLayout()
            Diagram.FitToPage()
        End Sub

        Protected MustOverride Sub RelayoutDiagramCore()
    End Class
End Namespace
