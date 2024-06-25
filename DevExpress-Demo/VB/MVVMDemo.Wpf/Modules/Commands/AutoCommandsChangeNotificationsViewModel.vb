Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.Commands

    Public Class AutoCommandsChangeNotificationsViewModel
        Inherits ViewModelBase

        <Command(UseCommandManager:=False)>
        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub

        Public Function CanSave() As Boolean
            Return CanExecuteSaveCommand
        End Function

        Public Property CanExecuteSaveCommand As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
#Region "!"
                If SetValue(value) Then RaiseCanExecuteChanged(Sub() Save())
#End Region  ' }
            End Set
        End Property

        Public Sub New()
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
