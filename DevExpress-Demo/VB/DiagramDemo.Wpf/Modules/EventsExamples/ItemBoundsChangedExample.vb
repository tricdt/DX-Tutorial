Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module ItemBoundsChangedExample

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300), .EnableProportionalResizing = False}
            AddHandler diagram.Loaded, Sub(s, e)
                Enumerable.First(diagram.Items.OfType(Of DiagramContentItem)(), Function(x) Equals(x.Tag, Orientation.Vertical)).Content = New VRuler()
                Enumerable.First(diagram.Items.OfType(Of DiagramContentItem)(), Function(x) Equals(x.Tag, Orientation.Horizontal)).Content = New HRuler()
                diagram.FitToPage()
            End Sub
            AddHandler diagram.QueryItemsAction, Sub(sender, e)
                If e.Action <> ItemsActionKind.Resize AndAlso e.Items.Any(Function(x) TypeOf x.Tag Is Orientation) Then
                    e.Allow = False
                    Return
                End If

                If e.Items.Any(Function(x) Not(TypeOf x.Tag Is Orientation)) Then e.Allow = False
            End Sub
            AddHandler diagram.QueryItemSnapping, Sub(sender, e)
                If diagram.Items.Any(Function(x) TypeOf x.Tag Is Orientation) Then e.Allow = False
            End Sub
            AddHandler diagram.ItemsResizing, Sub(sender, e)
                For Each c As ResizingItem In e.Items
                    If TypeOf c.Item.Tag Is Orientation AndAlso c.NewDiagramPosition <> c.OldDiagramPosition Then
                        c.NewSize = c.OldSize
                        c.NewDiagramPosition = c.OldDiagramPosition
                    ElseIf Equals(c.Item.Tag, Orientation.Vertical) Then
                        c.NewSize = New Size(c.OldSize.Width, c.NewSize.Height)
                    ElseIf Equals(c.Item.Tag, Orientation.Horizontal) Then
                        c.NewSize = New Size(c.NewSize.Width, c.OldSize.Height)
                    End If
                Next
            End Sub
            ' Code Example Start
            ' Each time the end-user tries to modify the bounds a diagram item, the ItemsBoundsChanged event is raised.
            ' The following implementation allows the end-user to change the size of the ellipse by resizing the ruler items that are tagged by their orientation.
            Dim item As DiagramShape = New DiagramShape() With {.Position = New Point(65, 65), .Width = 200, .Height = 160, .CanRotate = False, .CanMove = False}
            AddHandler diagram.ItemBoundsChanged, Sub(sender, e)
                If Equals(e.Item.Tag, Orientation.Vertical) Then
                    item.Height = e.NewSize.Height
                    item.Position = New Point(item.Position.X, e.NewPosition.Y)
                ElseIf Equals(e.Item.Tag, Orientation.Horizontal) Then
                    item.Width = e.NewSize.Width
                    item.Position = New Point(e.NewPosition.X, item.Position.Y)
                End If
            End Sub
            diagram.Items.Add(item)
            Dim vRuler As DiagramContentItem = New DiagramContentItem() With {.Position = New Point(25, 65), .Width = 20, .Height = 160, .Background = Brushes.Transparent, .Tag = Orientation.Vertical}
            Dim hRuler As DiagramContentItem = New DiagramContentItem() With {.Position = New Point(65, 25), .Width = 200, .Height = 20, .Background = Brushes.Transparent, .Tag = Orientation.Horizontal}
            diagram.Items.Add(vRuler)
            diagram.Items.Add(hRuler)
            diagram.SelectItem(hRuler)
            ' Code Example End
            Return diagram
        End Function
    End Module

    Public Class VRuler
        Inherits Decorator

        Protected Overrides Sub OnRender(ByVal drawingContext As DrawingContext)
            MyBase.OnRender(drawingContext)
            Dim pen = New Pen(Brushes.Black, 1)
            drawingContext.DrawRectangle(Brushes.Black, pen, New Rect(0, 0, 2, RenderSize.Height))
            For y As Double = 0R To RenderSize.Height - 1 Step 10R
                drawingContext.DrawRectangle(Brushes.Black, pen, New Rect(0, y, 8, 2))
            Next

            For y1 As Double = 0R To RenderSize.Height - 1 Step 20R
                drawingContext.DrawRectangle(Brushes.Black, pen, New Rect(0, y1, 12, 2))
            Next
        End Sub
    End Class

    Public Class HRuler
        Inherits Decorator

        Protected Overrides Sub OnRender(ByVal drawingContext As DrawingContext)
            MyBase.OnRender(drawingContext)
            Dim pen = New Pen(Brushes.Black, 1)
            drawingContext.DrawRectangle(Brushes.Black, pen, New Rect(0, 0, RenderSize.Width, 2))
            For x As Double = 0R To RenderSize.Width - 1 Step 10R
                drawingContext.DrawRectangle(Brushes.Black, pen, New Rect(x, 0, 2, 8))
            Next

            For x1 As Double = 0R To RenderSize.Width - 1 Step 20R
                drawingContext.DrawRectangle(Brushes.Black, pen, New Rect(x1, 0, 2, 12))
            Next
        End Sub
    End Class
End Namespace
