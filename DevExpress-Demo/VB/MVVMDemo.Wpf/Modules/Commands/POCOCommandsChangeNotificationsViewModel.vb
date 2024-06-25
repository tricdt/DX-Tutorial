Imports DevExpress.Mvvm.POCO
Imports System.Windows

Namespace MVVMDemo.Commands

    Public Class POCOCommandsChangeNotificationsViewModel

        Public Overridable Property CanExecuteSaveCommand As Boolean

#Region "!"
        Protected Sub OnCanExecuteSaveCommandChanged()
            RaiseCanExecuteChanged(Sub(x) x.Save())
        End Sub

#End Region
        Public Sub Save()
            MessageBox.Show("Action: Save")
        End Sub

        Public Function CanSave() As Boolean
            Return CanExecuteSaveCommand
        End Function

        Protected Sub New()
            CanExecuteSaveCommand = True
        End Sub
    End Class
End Namespace
