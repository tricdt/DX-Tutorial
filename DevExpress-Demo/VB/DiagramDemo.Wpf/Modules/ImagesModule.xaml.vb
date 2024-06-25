Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class ImagesModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler diagramControl.MouseDoubleClick, Sub(o, e)
                If e.ChangedButton <> System.Windows.Input.MouseButton.Left Then Return
                Dim imageItem = TryCast(diagramControl.CalcHitItem(e.GetPosition(diagramControl)), DiagramImage)
                If imageItem Is Nothing OrElse Not Equals(imageItem.Tag, GetType(DiagramImage).Name) Then Return
                diagramControl.SelectItem(imageItem)
                diagramControl.LoadImage()
            End Sub
        End Sub
    End Class
End Namespace
