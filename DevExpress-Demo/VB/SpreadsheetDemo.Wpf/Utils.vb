Imports System
Imports System.IO
Imports System.Windows
Imports System.Diagnostics
Imports DevExpress.Xpf.Core
Imports System.Windows.Forms
Imports DevExpress.DemoData.Helpers
Imports System.Reflection

Namespace SpreadsheetDemo

    Public Module DocumentLoadHelper

        Public Function GetDocument(ByVal path As String) As Stream
            Dim assembly = Reflection.Assembly.GetExecutingAssembly()
            Return assembly.GetManifestResourceStream(path)
        End Function
    End Module

#Region "DemoUtils"
    Public Class DemoUtils

        Public Shared Function GetRelativePath(ByVal name As String) As String
            name = "Data\" & name
            Dim path As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If File.Exists(path & s & name) Then
                    Return IO.Path.GetFullPath(path & s & name)
                Else
                    s += "..\"
                End If
            Next

            Return ""
        End Function

        Public Shared Function GetRelativeDirectoryPath(ByVal name As String) As String
            name = "Data\" & name
            Dim path As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If Directory.Exists(path & s & name) Then
                    Return path & s & name
                Else
                    s += "..\"
                End If
            Next

            Return ""
        End Function

        Public Shared Function GetDataStream(ByVal fileName As String) As Stream
            Dim path As String = GetRelativePath(fileName)
            If Not String.IsNullOrEmpty(path) Then
                Dim fileAccess As FileAccess = If((File.GetAttributes(path) And FileAttributes.ReadOnly) <> 0, FileAccess.Read, FileAccess.ReadWrite)
                Return New FileStream(path, FileMode.Open, fileAccess)
            End If

            Return Nothing
        End Function

        Public Shared Sub SetDatabasePath()
            Const dbName As String = "nwind.mdb"
            Const pathToDbTag As String = "|pathToDb|"
            Dim path As String = GetRelativePath(dbName)
            If String.IsNullOrEmpty(path) Then Return
            Dim connectionString As String = TryCast(Global.SpreadsheetDemo.Properties.Settings.Default("nwindConnectionString"), String)
            If String.IsNullOrEmpty(connectionString) Then Return
            connectionString = connectionString.Replace(pathToDbTag, path)
            Global.SpreadsheetDemo.Properties.Settings.Default("nwindConnectionString") = connectionString
        End Sub
    End Class

#End Region
#Region "DialogHelper"
    Public Class DialogHelper

        Public Shared Function ShowQuestionDialog(ByVal message As String) As MessageBoxResult
            Return ThemedMessageBox.Show(Forms.Application.ProductName, message, MessageBoxButton.OKCancel, MessageBoxImage.Question)
        End Function

        Public Shared Sub ShowErrorDialog(ByVal message As String)
            ThemedMessageBox.Show(Forms.Application.ProductName, message, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
    End Class

#End Region
#Region "FileOpenHelper"
    Public Class FileOpenHelper

        Public Shared Sub ShowFile(ByVal fileName As String)
            If Not File.Exists(fileName) Then Return
#If DXCORE3
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName);
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
#Else
            Call Process.Start(fileName)
#End If
        End Sub
    End Class

#End Region
#Region "FileSaveHelper"
    Public Class FileSaveHelper

        Public Shared Function GetSaveFileName(ByVal filter As String, ByVal defaultName As String) As String
            Dim sfDialog As SaveFileDialog = New SaveFileDialog()
            sfDialog.Filter = filter
            sfDialog.FileName = defaultName
            If sfDialog.ShowDialog() <> DialogResult.OK Then Return String.Empty
            Return sfDialog.FileName
        End Function
    End Class
#End Region
End Namespace
