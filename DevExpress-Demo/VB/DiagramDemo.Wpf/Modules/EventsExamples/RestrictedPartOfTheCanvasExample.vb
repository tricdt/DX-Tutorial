Imports System
Imports System.Windows
Imports System.Windows.Media
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module RestrictedPartOfTheCanvasExample

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            Dim restrictedPartMarker As DiagramShape = New DiagramShape() With {.Position = New Point(180, 50), .Width = 180, .Height = 200, .StrokeDashArray = New DoubleCollection({8R, 8R}), .Stroke = New SolidColorBrush(Color.FromRgb(&HC8, &H14, &H14)), .Background = Brushes.Transparent, .StrokeThickness = 3}
            restrictedPartMarker.CanRotate = False
            restrictedPartMarker.CanResize = restrictedPartMarker.CanRotate
            restrictedPartMarker.CanSelect = restrictedPartMarker.CanResize
            restrictedPartMarker.CanMove = restrictedPartMarker.CanSelect
            ' Code Example Start
            ' Each time the end-user tries to move a diagram item, the ItemsMoving event is raised.
            ' The following implementation prevents items from being moved in a certain area of the canvas.
            AddHandler diagram.ItemsMoving, Sub(sender, e)
                For Each c As MovingItem In e.Items
                    Dim x1 As Double = restrictedPartMarker.Position.X - c.Item.Width
                    Dim x2 As Double = restrictedPartMarker.Position.X + restrictedPartMarker.Width
                    Dim y1 As Double = restrictedPartMarker.Position.Y - c.Item.Height
                    Dim y2 As Double = restrictedPartMarker.Position.Y + restrictedPartMarker.Height
                    If c.NewDiagramPosition.X > x1 AndAlso c.NewDiagramPosition.X < x2 AndAlso c.NewDiagramPosition.Y > y1 AndAlso c.NewDiagramPosition.Y < y2 Then
                        Dim coercedX As Double = If(c.NewDiagramPosition.X - x1 < (x2 - x1) / 2R, x1, x2)
                        Dim coercedY As Double = If(c.NewDiagramPosition.Y - y1 < (y2 - y1) / 2R, y1, y2)
                        If Math.Abs(coercedX - c.NewDiagramPosition.X) < Math.Abs(coercedY - c.NewDiagramPosition.Y) Then
                            c.NewDiagramPosition = New Point(coercedX, c.NewDiagramPosition.Y)
                        Else
                            c.NewDiagramPosition = New Point(c.NewDiagramPosition.X, coercedY)
                        End If
                    End If
                Next
            End Sub
            Dim item = New DiagramShape() With {.Position = New Point(50, 100), .Width = 100, .Height = 100, .CanRotate = False, .CanResize = False}
            diagram.Items.Add(item)
            diagram.Items.Add(restrictedPartMarker)
            diagram.SelectItem(item)
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
