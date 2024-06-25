Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.POCOViewModels

    Public Class LoginViewModel

        ' These properties will be converted to bindable ones
        Public Overridable Property UserName As String

        Public Overridable Property Status As String

        ' LoginCommand will be created for the Login and CanLogin methods: 
        Public Sub Login()
            Status = "User: " & UserName
        End Sub

        Public Function CanLogin() As Boolean
            Return Not String.IsNullOrEmpty(UserName)
        End Function

        ' We recommend that you do not use public constructors to prevent creating a View Model instance without the ViewModelSource 
        Protected Sub New()
        End Sub

        ' A helper method that uses the ViewModelSource class for creating a LoginViewModel instance
        Public Shared Function Create() As LoginViewModel
            Return ViewModelSource.Create(Function() New LoginViewModel())
        End Function
    End Class
End Namespace
