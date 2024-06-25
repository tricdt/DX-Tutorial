Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.ViewModelBaseDemo

    Public Class LoginViewModel
        Inherits ViewModelBase

        Public Property UserName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property Status As String
            Get
                Return GetValue(Of String)()
            End Get

            Private Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        <Command>
        Public Sub Login()
            Status = "User: " & UserName
        End Sub

        Public Function CanLogin() As Boolean
            Return Not String.IsNullOrEmpty(UserName)
        End Function
    End Class
End Namespace
