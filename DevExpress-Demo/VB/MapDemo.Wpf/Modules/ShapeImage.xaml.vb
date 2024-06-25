Imports DevExpress.Map
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class ShapeImage
        Inherits MapDemoModule

        Private initialCenterPoint As CoordPoint

        Public Sub New()
            InitializeComponent()
            initialCenterPoint = map.CenterPoint
        End Sub

        Private Sub ImageLayer_ViewportChanged(ByVal sender As Object, ByVal e As ViewportChangedEventArgs)
            If Not e.IsAnimated AndAlso e.ZoomLevel <= 14 Then
                Dim center As CoordPoint = If(initialCenterPoint, map.CenterPoint)
                Dim xOffset As Double =(e.BottomRight.GetX() - e.TopLeft.GetX()) / 2R
                Dim yOffset As Double =(e.BottomRight.GetY() - e.TopLeft.GetY()) / 2R
                map.ScrollArea = New MapBounds(center.Offset(-xOffset, -yOffset), center.Offset(xOffset, yOffset))
            End If
        End Sub
    End Class
End Namespace
