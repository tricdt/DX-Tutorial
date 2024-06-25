Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.ViewModelBaseDemo

    Public Class ServicesViewModel
        Inherits ViewModelBase

        Public Property UserName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

#Region "!"
        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return ServiceContainer.GetService(Of IMessageBoxService)()
            End Get
        End Property

        <Command>
        Public Sub Login()
            MessageBoxService.ShowMessage("User: " & UserName, "Login", MessageButton.OK, MessageIcon.Information)
        End Sub

#End Region
        Public Function CanLogin() As Boolean
            Return Not String.IsNullOrEmpty(UserName)
        End Function
    End Class
End Namespace
