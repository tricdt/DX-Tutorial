Imports DevExpress.Internal
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows

Namespace DialogsDemo.Helpers

    Public Module DialogsDemoHelper

        Public Function GetDialogsDataPath(ByVal relativePath As String) As String
            Return Path.GetFullPath(DataDirectoryHelper.GetFile("Dialogs\" & relativePath, DataDirectoryHelper.DataFolderName))
        End Function

        Public Function GetPhotosPath(ByVal relativePath As String) As String
            Return Path.GetFullPath(DataDirectoryHelper.GetFile("Photos\" & relativePath, DataDirectoryHelper.DataFolderName))
        End Function

        Public Function GetDataStream(ByVal dataFileName As String) As Stream
            Dim path As String = GetDialogsDataPath(dataFileName)
            Return File.OpenRead(path)
        End Function

        Public Function GetMessageBoxResults(ByVal buttons As MessageBoxButton) As IList(Of MessageBoxResult)
            Dim resultButtons = New List(Of MessageBoxResult)()
            Select Case buttons
                Case MessageBoxButton.OK
                    resultButtons.Add(MessageBoxResult.OK)
                Case MessageBoxButton.OKCancel
                    resultButtons.Add(MessageBoxResult.OK)
                    resultButtons.Add(MessageBoxResult.Cancel)
                Case MessageBoxButton.YesNoCancel
                    resultButtons.Add(MessageBoxResult.Yes)
                    resultButtons.Add(MessageBoxResult.No)
                    resultButtons.Add(MessageBoxResult.Cancel)
                Case MessageBoxButton.YesNo
                    resultButtons.Add(MessageBoxResult.Yes)
                    resultButtons.Add(MessageBoxResult.No)
            End Select

            Return resultButtons
        End Function
    End Module
End Namespace
