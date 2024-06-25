Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Serialization
Imports System.IO
Imports System.Reflection
Imports System.Windows

Namespace BarsDemo

    Public Partial Class SaveAndRestoreLayout
        Inherits BarsDemoModule

        Private wManager As IWorkspaceManager

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
            DXSerializer.SetSerializationID(barManager, "BarManager")
            isolatedStorageSettingsGroup.Visibility = Visibility.Collapsed
            wManager = WorkspaceManager.GetWorkspaceManager(barManager)
            wManager.TransitionEffect = TransitionEffect.Ripple
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            loadLocalStorageButton.IsEnabled = False
        End Sub

        Protected Overridable Sub ShowMessageBox(ByVal message As String)
            MessageBox.Show(message, "Layout", MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub

        Protected Overridable Sub serializeButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            stream = New MemoryStream()
            barManager.SaveLayoutToStream(stream)
            ShowMessageBox("Layout has been saved")
            deserializeButton.IsEnabled = stream.Length > 0
        End Sub

        Protected Overridable Sub deserializeButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If stream Is Nothing Then Return
            Dim memoryStream As MemoryStream = New MemoryStream()
            stream.Seek(0, SeekOrigin.Begin)
            stream.CopyTo(memoryStream)
            stream.Seek(0, SeekOrigin.Begin)
            RestoreLayout("UserSaved", stream)
            stream.Dispose()
            stream = memoryStream
        End Sub

        Private Sub RestoreLayout(ByVal name As String, ByVal file As Stream)
            If useWManagerCheck.IsChecked.HasValue AndAlso useWManagerCheck.IsChecked.Value Then
                If transitionComboBox.SelectedItem IsNot Nothing Then wManager.TransitionEffect = CType(transitionComboBox.SelectedItem, TransitionEffect)
                wManager.LoadWorkspace(name, file)
                wManager.ApplyWorkspace(name)
            Else
                barManager.RestoreLayoutFromStream(file)
                ShowMessageBox("Layout has been restored")
            End If
        End Sub

        Protected Overridable Sub loadSampleLayoutButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim thisExe As Assembly = Assembly.GetExecutingAssembly()
            Dim name As String = String.Format("BarsDemo.Layouts.{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower())
            Dim file As Stream = thisExe.GetManifestResourceStream(name)
            RestoreLayout(name, file)
        End Sub

        Protected Overridable Sub saveLocalStorageButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        End Sub

        Protected Overridable Sub loadLocalStorageButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        End Sub

        Private stream As Stream

        Private Sub transitionComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If barManager IsNot Nothing Then
                Dim wm As IWorkspaceManager = WorkspaceManager.GetWorkspaceManager(barManager)
                If wm IsNot Nothing Then wm.TransitionEffect = CType(transitionComboBox.SelectedItem, TransitionEffect)
            End If
        End Sub
    End Class
End Namespace
