Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services.Navigation

    Public Class DetailViewModel
        Implements ISupportParameter

        Public Overridable Property Person As PersonInfo

        Private _Parameter As Object

        Private Property Parameter As Object Implements ISupportParameter.Parameter
            Get
                Return _Parameter
            End Get

            Set(ByVal value As Object)
                _Parameter = value
                Person = CType(_Parameter, PersonInfo)
            End Set
        End Property

        Private ReadOnly Property NavigationService As INavigationService
            Get
                Return GetRequiredService(Of INavigationService)()
            End Get
        End Property

        Public Sub Back()
            NavigationService.GoBack()
        End Sub
    End Class
End Namespace
