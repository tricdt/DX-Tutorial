Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Layout.Core

Namespace DockingDemo

    Public Partial Class Serialization
        Inherits DockingDemoModule

        Private wManager As DevExpress.Xpf.Core.IWorkspaceManager

        Private memoryStream As Stream

        Public Sub New()
            InitializeComponent()
            wManager = DevExpress.Xpf.Core.WorkspaceManager.GetWorkspaceManager(dockManager)
        End Sub

        Protected Overridable Sub SaveLayout()
            Call Ref.Dispose(memoryStream)
            memoryStream = New MemoryStream()
            dockManager.SaveLayoutToStream(memoryStream)
            deserializeButton.IsEnabled = True
        End Sub

        Private Sub serializeButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SaveLayout()
        End Sub

        Private Sub deserializeButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If memoryStream Is Nothing Then Return
            memoryStream.Seek(0, SeekOrigin.Begin)
            RestoreLayout("UserLayout", memoryStream)
        End Sub

        Private Sub loadSampleLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim thisExe As Assembly = GetType(Serialization).Assembly
            Dim name As String = String.Format("{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower())
            Using resourceStream As Stream = DevExpress.Utils.AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/", thisExe) & name, True)
                RestoreLayout(name, resourceStream)
            End Using
        End Sub

        Private Sub RestoreLayout(ByVal name As String, ByVal stream As Stream)
            If useWManagerCheck.IsChecked = True Then
                If transitionComboBox.EditValue IsNot Nothing Then wManager.TransitionEffect = CType(transitionComboBox.EditValue, DevExpress.Xpf.Core.TransitionEffect)
                wManager.LoadWorkspace(name, stream)
                wManager.ApplyWorkspace(name)
            Else
                dockManager.RestoreLayoutFromStream(stream)
            End If
        End Sub
    End Class
End Namespace
