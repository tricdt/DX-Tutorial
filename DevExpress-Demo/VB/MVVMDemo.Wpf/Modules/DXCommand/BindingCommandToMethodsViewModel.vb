Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.DXCommandDemo

    Public Class BindingCommandToMethodsViewModel
        Inherits BindableBase

        Public Property CanExecuteSaveCommand As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub

        Public Property FileName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Sub Open(ByVal fileName As String)
            MessageBox.Show(String.Format("Action: Open {0}", fileName))
        End Sub

        Public Sub New()
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
