Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace MVVMDemo.Services

    Public Class OpenFolderDialogServiceViewModel

        Public Overridable Property OpenedFolders As String

        Public Sub ShowDialog()
            Dim service As IOpenFolderDialogService = GetService(Of IOpenFolderDialogService)()
            Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If service.ShowDialog(Nothing, directory) Then
                Dim folderNames As IEnumerable(Of String) = service.Folders.[Select](Function(folderInfo) folderInfo.Path)
                OpenedFolders = String.Join(Environment.NewLine, folderNames)
            End If
        End Sub
    End Class
End Namespace
