Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXCommandDemo

    Public Class ValuesFromControlsViewModel
        Inherits BindableBase

        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub

        Public Sub Open(ByVal fileName As String)
            MessageBox.Show(String.Format("Action: Open {0}", fileName))
        End Sub

        Public Function CanOpen(ByVal fileName As String) As Boolean
            Return Not String.IsNullOrEmpty(fileName)
        End Function
    End Class
End Namespace
