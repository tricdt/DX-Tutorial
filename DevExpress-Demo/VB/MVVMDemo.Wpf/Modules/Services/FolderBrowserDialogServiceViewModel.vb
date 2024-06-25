Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System

Namespace MVVMDemo.Services

    Public Class FolderBrowserDialogServiceViewModel

        Public Overridable Property SelectedFolder As String

        Public Sub ShowDialog(ByVal serviceName As String)
            Dim service As IFolderBrowserDialogService = GetService(Of IFolderBrowserDialogService)(serviceName)
            Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            service.StartPath = directory
            If service.ShowDialog() Then
                SelectedFolder = service.ResultPath
            End If
        End Sub
    End Class
End Namespace
