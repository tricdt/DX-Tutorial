Imports DevExpress.Xpf.PropertyGrid

Namespace RichEditDemo

    Public Partial Class OperationRestrictions
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
            propertyGridControl.SelectedObject = New RichEditOptionsProvider(richEdit.Options)
        End Sub

        Private Sub PropertyGridControl_CellValueChanged(ByVal sender As Object, ByVal args As CellValueChangedEventArgs)
            If String.IsNullOrEmpty(RichEditControl.DocumentSaveOptions.CurrentFileName) Then Return
            If args.Row.FullPath.Contains("DocumentCapabilities") Then RichEditControl.LoadDocument(RichEditControl.DocumentSaveOptions.CurrentFileName)
        End Sub
    End Class
End Namespace
