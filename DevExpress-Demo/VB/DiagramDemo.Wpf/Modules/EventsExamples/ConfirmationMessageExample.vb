Imports System.Windows
Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module ConfirmationMessageExample

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            AddHandler diagram.QueryItemsAction, Sub(sender, e)
                If e.Action = ItemsActionKind.Move Then e.Allow = False
            End Sub
            AddHandler diagram.QueryConnectionPoints, Sub(sender, e)
                If Equals(e.OppositeItem, e.HoveredItem) Then
                    e.ItemConnectionBorderState = ConnectionElementState.Hidden
                    For Each p As ConnectionPoint In e.ItemConnectionPointStates
                        p.State = ConnectionElementState.Hidden
                    Next
                End If
            End Sub
            AddHandler diagram.ItemsChanged, Sub(sender, e)
                If e.Action = ItemsChangedAction.Added AndAlso TypeOf e.Item Is DiagramConnector Then
                    CType(e.Item, DiagramConnector).CanChangeRoute = False
                    e.Item.StrokeThickness = 2
                    e.Item.StrokeId = DiagramThemeColorId.Black_3
                End If
            End Sub
            ' Code Example Start
            ' Each time the end-user tries to modify a connector, the ConnectionChanging event is raised.
            ' The following implementation invokes the confirmation dialog window prompting the user to confirm the action.
            AddHandler diagram.ConnectionChanging, Sub(sender, e)
                Dim result = ThemedMessageBox.Show(Window.GetWindow(diagram), "Confirmation", "Confirm the connection changing action.", MessageBoxButton.OKCancel, Nothing, MessageBoxImage.None)
                If result <> MessageBoxResult.OK Then e.Cancel = True
            End Sub
            Dim item1 As DiagramShape = New DiagramShape() With {.Shape = BasicShapes.Parallelogram, .Position = New Point(150, 40), .Width = 120, .Height = 80}
            diagram.Items.Add(item1)
            Dim item2 As DiagramShape = New DiagramShape() With {.Shape = BasicShapes.Ellipse, .Position = New Point(250, 200), .Width = 120, .Height = 80}
            diagram.Items.Add(item2)
            diagram.Items.Add(New DiagramShape() With {.Shape = BasicShapes.Hexagon, .Position = New Point(30, 200), .Width = 120, .Height = 80})
            Dim connector As DiagramConnector = New DiagramConnector() With {.BeginItem = item1, .EndItem = item2}
            diagram.Items.Add(connector)
            diagram.SelectItem(connector)
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
