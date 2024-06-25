Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.POCOViewModels

    Public Class ServicesViewModel

        Public Overridable Property UserName As String

#Region "!"
        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetRequiredService(Of IMessageBoxService)()
            End Get
        End Property

        Public Sub Login()
            MessageBoxService.ShowMessage("User: " & UserName, "Login", MessageButton.OK, MessageIcon.Information)
        End Sub

#End Region
        Public Function CanLogin() As Boolean
            Return Not String.IsNullOrEmpty(UserName)
        End Function
    End Class
End Namespace
