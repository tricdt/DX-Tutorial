Imports DevExpress.Mvvm
Imports System.Windows

Namespace MVVMDemo.Commands

    Public Class ChangeNotificationsViewModel
        Inherits ViewModelBase

        Private _SaveCommand As DelegateCommand

        Public Property SaveCommand As DelegateCommand
            Get
                Return _SaveCommand
            End Get

            Private Set(ByVal value As DelegateCommand)
                _SaveCommand = value
            End Set
        End Property

        Public Property CanExecuteSaveCommand As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
#Region "!"
                If SetValue(value) Then SaveCommand.RaiseCanExecuteChanged()
#End Region
            End Set
        End Property

        Public Sub New()
            SaveCommand = New DelegateCommand(Sub() MessageBox.Show("Action: Save"), Function() CanExecuteSaveCommand, useCommandManager:=False)
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
