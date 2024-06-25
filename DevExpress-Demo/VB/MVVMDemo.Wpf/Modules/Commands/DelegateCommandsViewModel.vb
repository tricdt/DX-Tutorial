Imports DevExpress.Mvvm
Imports System.Windows
Imports System.Windows.Input

Namespace MVVMDemo.Commands

    Public Class DelegateCommandsViewModel
        Inherits ViewModelBase

        Private _SaveCommand As ICommand, _OpenCommand As ICommand

        Public Property SaveCommand As ICommand
            Get
                Return _SaveCommand
            End Get

            Private Set(ByVal value As ICommand)
                _SaveCommand = value
            End Set
        End Property

        Public Property OpenCommand As ICommand
            Get
                Return _OpenCommand
            End Get

            Private Set(ByVal value As ICommand)
                _OpenCommand = value
            End Set
        End Property

#Region "!"
        Public Sub New()
            CanExecuteSaveCommand = True
            SaveCommand = New DelegateCommand(Sub() MessageBox.Show("Action: Save"), Function() CanExecuteSaveCommand)
            OpenCommand = New DelegateCommand(Of String)(Sub(fileName) MessageBox.Show(String.Format("Action: Open {0}", Me.FileName)), Function(fileName) Not String.IsNullOrEmpty(fileName))
        End Sub

#End Region
        Public Property CanExecuteSaveCommand As Boolean
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
    End Class
End Namespace
