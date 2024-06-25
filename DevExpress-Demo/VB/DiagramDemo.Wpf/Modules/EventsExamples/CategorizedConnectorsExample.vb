Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module CategorizedConnectorsExample

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            diagram.ConnectorTool = New FactoryConnectorTool("Example", Function() "Connector", Function(x) New DiagramConnector() With {.Type = ConnectorType.Curved, .BeginPointRestrictions = ConnectorPointRestrictions.KeepConnected, .EndPointRestrictions = ConnectorPointRestrictions.KeepConnected})
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
            ' Each time the end-user moves the cursor with the active Connector Tool near shapes or their connection points, the QueryConnectionPoints event is raised.
            ' The following implementation prevents the end-user from connecting items with different background colors.
            AddHandler diagram.QueryConnectionPoints, Sub(sender, e)
                If e.OppositeItem IsNot Nothing AndAlso Not Equals(e.OppositeItem.Background.ToString(), e.HoveredItem.Background.ToString()) Then
                    e.ItemConnectionBorderState = ConnectionElementState.Hidden
                    For Each p In e.ItemConnectionPointStates
                        p.State = ConnectionElementState.Hidden
                    Next
                End If
            End Sub
            diagram.Items.Add(New DiagramShape() With {.Position = New Point(50, 40), .Width = 120, .Height = 50, .Background = New SolidColorBrush(Color.FromRgb(&H01, &H73, &HC7))})
            diagram.Items.Add(New DiagramShape() With {.Position = New Point(230, 120), .Width = 120, .Height = 50, .Background = New SolidColorBrush(Color.FromRgb(&H01, &H73, &HC7))})
            diagram.Items.Add(New DiagramShape() With {.Position = New Point(50, 200), .Width = 120, .Height = 50, .Background = New SolidColorBrush(Color.FromRgb(&H01, &H73, &HC7))})
            diagram.Items.Add(New DiagramShape() With {.Position = New Point(230, 40), .Width = 120, .Height = 50, .Background = New SolidColorBrush(Color.FromRgb(&HC7, &H73, &H01))})
            diagram.Items.Add(New DiagramShape() With {.Position = New Point(50, 120), .Width = 120, .Height = 50, .Background = New SolidColorBrush(Color.FromRgb(&HC7, &H73, &H01))})
            diagram.Items.Add(New DiagramShape() With {.Position = New Point(230, 200), .Width = 120, .Height = 50, .Background = New SolidColorBrush(Color.FromRgb(&HC7, &H73, &H01))})
            diagram.ActiveTool = diagram.ConnectorTool
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
