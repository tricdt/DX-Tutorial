Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace MVVMDemo.Services

    Public Class OpenFileDialogServiceViewModel

        Public Overridable Property OpenedFiles As String

        Public Sub ShowDialog(ByVal serviceName As String)
            Dim service As IOpenFileDialogService = GetService(Of IOpenFileDialogService)(serviceName)
            service.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg)|*.png;*.jpeg"
            Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If service.ShowDialog(directory) Then
                Dim fileNames As IEnumerable(Of String) = service.Files.[Select](Function(fileInfo) fileInfo.GetFullName())
                OpenedFiles = String.Join(Environment.NewLine, fileNames)
            End If
        End Sub
    End Class
End Namespace
