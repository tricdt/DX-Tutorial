Imports System.Linq
Imports System.Windows
Imports DevExpress.Diagram.Core
Imports DevExpress.Diagram.Core.Native
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module ExpandContainerOnDragOver

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            ' Code Example Start
            ' Each time the end-user tries to move a diagram item, the ItemsMoving event is raised.
            ' The following implementation expands a collapsed container when the end-user is dragging an item over it.
            Dim shape = New DiagramShape() With {.Position = New Point(150, 130), .Width = 100, .Height = 80, .Content = "Drag me over the container"}
            Dim container = New DiagramContainer With {.Position = New Point(100, 50), .Width = 200, .Height = 150, .ShowHeader = True, .Header = "Collapsed container", .CanCollapse = True, .IsCollapsed = True, .CanCopy = False, .CanMove = False}
            Dim containerCollapseState = False
            AddHandler diagram.ItemsMoving, Sub(s, e)
                If e.ActionSource <> ItemsActionSource.Mouse Then Return
                If e.Stage = DiagramActionStage.Start Then
                    containerCollapseState = container.IsCollapsed
                ElseIf e.Stage = DiagramActionStage.Canceled Then
                    container.IsCollapsed = containerCollapseState
                ElseIf containerCollapseState Then
                    Dim containerBounds = container.DiagramBounds()
                    container.IsCollapsed = Not e.Items.Any(Function(x) containerBounds.IntersectsWith(New Rect(x.NewDiagramPosition, x.Item.RenderSize)))
                End If
            End Sub
            diagram.Items.Add(shape)
            diagram.Items.Add(container)
            diagram.SelectItem(shape)
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
