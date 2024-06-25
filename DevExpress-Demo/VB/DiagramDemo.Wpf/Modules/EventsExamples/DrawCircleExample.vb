Imports System
Imports System.Windows
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module DrawCircleExample

        Public Function Run() As FrameworkElement
            Dim diagram As DiagramControl = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            ' Code Example Start
            ' Each time the end-user tries to draw a diagram item, the ItemDrawing event is raised.
            ' The following implementation constrains the drawing tool to draw regular polygons (a circle in case of the Ellipse tool selected by default).
            AddHandler diagram.ItemDrawing, Sub(sender, e)
                Dim width As Double = e.EndPosition.X - e.StartPosition.X
                Dim height As Double = e.EndPosition.Y - e.StartPosition.Y
                If Math.Abs(height) > Math.Abs(width) Then
                    e.EndPosition = New Point(e.StartPosition.X + Math.Sign(width) * Math.Abs(height), e.EndPosition.Y)
                Else
                    e.EndPosition = New Point(e.EndPosition.X, e.StartPosition.Y + Math.Sign(height) * Math.Abs(width))
                End If
            End Sub
            diagram.ActiveTool = diagram.EllipseTool
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
