Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Xpf.Grid
Imports System.Linq
Imports System.Windows

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InplaceEditFormResources.xaml")>
    Public Partial Class InplaceEditForm
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = New VehiclesContext().Models.ToList()
        End Sub

        Private Sub OnTemplateValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If tableView Is Nothing Then Return
            Dim index As Integer = templatesListBox.SelectedIndex
            If index = 0 Then
                tableView.ClearValue(TableView.EditFormTemplateProperty)
            ElseIf index = 1 Then
                tableView.EditFormTemplate = CType(Resources("CustomEditFormTemplate"), DataTemplate)
            End If
        End Sub
    End Class
End Namespace
