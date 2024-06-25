Imports System.Windows
Imports DevExpress.Xpf.Diagram
Imports ResizeMode = DevExpress.Diagram.Core.ResizeMode

Namespace DiagramDemo

    Public Module MaxWidthExample

        Public Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300), .EnableProportionalResizing = False}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            ' Code Example Start
            ' Each time the end-user tries to resize a diagram item, the ItemsResizing event is raised.
            ' The following implementation sets the maximum width based on the item's content.
            AddHandler diagram.ItemsResizing, Sub(sender, e)
                For Each c As ResizingItem In e.Items
                    Dim maxWidth As Double = 0R
                    If TypeOf c.Item Is DiagramShape Then Call Double.TryParse(CType(c.Item, DiagramShape).Content, maxWidth)
                    Dim widthOver As Double = c.NewSize.Width - maxWidth
                    If widthOver <= 0R Then Continue For
                    c.NewSize = New Size(maxWidth, c.NewSize.Height)
                    If e.Mode = ResizeMode.Left OrElse e.Mode = ResizeMode.TopLeft OrElse e.Mode = ResizeMode.BottomLeft Then c.NewDiagramPosition = New Point(c.NewDiagramPosition.X + widthOver, c.NewDiagramPosition.Y)
                Next
            End Sub
            diagram.Items.Add(New DiagramShape() With {.Content = "300", .Position = New Point(60, 75), .Width = 280, .Height = 150, .CanRotate = False, .FontSize = 24})
            ' Code Example End
            Return diagram
        End Function
    End Module
End Namespace
