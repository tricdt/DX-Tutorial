Imports System
Imports System.Windows
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Module NinetyDegreeOnlyRotatableItemExample

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            ' Code Example Start
            ' Each time the end-user tries to rotate a diagram item, the ItemsRotating event is raised.
            ' The following implementation sets the fixed 90-degree angle on which the end-user can rotate items.
            AddHandler diagram.ItemsRotating, Sub(sender, e)
                For Each c As RotatingItem In e.Items
                    c.NewAngle = Math.Round(c.NewAngle / 90R) * 90R
                Next
            End Sub
            Dim item As DiagramShape = New DiagramShape() With {.Position = New Point(100, 70), .Width = 200, .Height = 160, .CanResize = False}
            diagram.Items.Add(item)
            diagram.SelectItem(item)
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
