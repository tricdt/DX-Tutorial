Imports System.Windows

Namespace MVVMDemo.Commands

    Public Class POCOCommandsViewModel

        Public Overridable Property CanExecuteSaveCommand As Boolean

        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub

        Public Function CanSave() As Boolean
            Return CanExecuteSaveCommand
        End Function

        Public Overridable Property FileName As String

        Public Sub Open(ByVal fileName As String)
            MessageBox.Show(String.Format("Action: Open {0}", fileName))
        End Sub

        Public Function CanOpen(ByVal fileName As String) As Boolean
            Return Not String.IsNullOrEmpty(fileName)
        End Function

        Protected Sub New()
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
