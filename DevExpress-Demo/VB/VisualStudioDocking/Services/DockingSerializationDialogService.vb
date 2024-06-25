Imports System
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Docking
Imports Microsoft.Win32

Namespace VisualStudioDocking

    Public Interface IDockingSerializationDialogService

        Sub SaveLayout()

        Sub LoadLayout()

    End Interface

    Public Class DockingSerializationDialogService
        Inherits DevExpress.Mvvm.UI.ServiceBase
        Implements VisualStudioDocking.IDockingSerializationDialogService

        Const filter As String = "Configuration (*.xml)|*.xml|All files (*.*)|*.*"

        Public Property DockLayoutManager As DockLayoutManager

        Public Sub LoadLayout() Implements Global.VisualStudioDocking.IDockingSerializationDialogService.LoadLayout
            Dim openFileDialog As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog() With {.Filter = VisualStudioDocking.DockingSerializationDialogService.filter}
            Dim openResult = openFileDialog.ShowDialog()
            If openResult.HasValue AndAlso openResult.Value Then Me.DockLayoutManager.RestoreLayoutFromXml(openFileDialog.FileName)
        End Sub

        Public Sub SaveLayout() Implements Global.VisualStudioDocking.IDockingSerializationDialogService.SaveLayout
            Dim saveFileDialog As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog() With {.Filter = VisualStudioDocking.DockingSerializationDialogService.filter}
            Dim saveResult = saveFileDialog.ShowDialog()
            If saveResult.HasValue AndAlso saveResult.Value Then Me.DockLayoutManager.SaveLayoutToXml(saveFileDialog.FileName)
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Me.DockLayoutManager = TryCast(Me.AssociatedObject, DevExpress.Xpf.Docking.DockLayoutManager)
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            Me.DockLayoutManager = Nothing
        End Sub
    End Class
End Namespace
