Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.Commands

    Public Class AutoCommandsViewModel
        Inherits ViewModelBase

        Public Property CanExecuteSaveCommand As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

#Region "!"
        <Command>
        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub

        Public Function CanSave() As Boolean
            Return CanExecuteSaveCommand
        End Function

#End Region
        Public Property FileName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

#Region "!"
        <Command>
        Public Sub Open(ByVal fileName As String)
            MessageBox.Show(String.Format("Action: Open {0}", fileName))
        End Sub

        Public Function CanOpen(ByVal fileName As String) As Boolean
            Return Not String.IsNullOrEmpty(fileName)
        End Function

#End Region
        Public Sub New()
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
