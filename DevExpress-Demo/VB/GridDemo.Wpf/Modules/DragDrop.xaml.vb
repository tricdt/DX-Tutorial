Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DragDropViewModel.(cs)")>
    Public Partial Class DragDrop
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnDragRecordOver(ByVal sender As Object, ByVal e As DragRecordOverEventArgs)
            If e.IsFromOutside AndAlso GetType(Employee).IsAssignableFrom(e.GetRecordType()) Then e.Effects = System.Windows.DragDropEffects.Move
            e.Handled = True
        End Sub
    End Class
End Namespace
