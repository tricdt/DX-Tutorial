Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXCommandDemo

    Public Class MultipleCallsViewModel
        Inherits BindableBase

        Public Property IsReadonly As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

        Public Property FileName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Sub Open()
            MessageBox.Show(String.Format("Action: Open {0}", FileName))
        End Sub

        Public Function CanOpen() As Boolean
            Return Not String.IsNullOrEmpty(FileName)
        End Function

        Public Sub Clear()
            FileName = Nothing
        End Sub
    End Class
End Namespace
