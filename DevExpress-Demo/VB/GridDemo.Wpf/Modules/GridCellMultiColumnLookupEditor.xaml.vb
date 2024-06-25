Imports DevExpress.Xpf.Grid.LookUp
Imports System
Imports System.Windows.Threading

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorResources.xaml")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorClasses.(cs)")>
    Public Partial Class GridCellMultiColumnLookupEditor
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Show()
            MyBase.Show()
            ShowLookUp()
        End Sub

        Protected Overrides Sub Clear()
            view.CloseEditor()
            MyBase.Clear()
        End Sub

        Private Sub ShowLookUp()
            grid.CurrentColumn = grid.Columns("CustomerID")
            view.ShowEditor()
            Dispatcher.BeginInvoke(New Action(Sub()
                Dim lookUpEdit As LookUpEdit = TryCast(view.ActiveEditor, LookUpEdit)
                If lookUpEdit IsNot Nothing Then lookUpEdit.ShowPopup()
            End Sub), DispatcherPriority.ApplicationIdle)
        End Sub
    End Class
End Namespace
