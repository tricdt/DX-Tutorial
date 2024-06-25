Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services.Navigation

    Public Class CollectionViewModel

        Private ReadOnly Property NavigationService As INavigationService
            Get
                Return GetRequiredService(Of INavigationService)()
            End Get
        End Property

        Public Sub ShowDetail(ByVal person As PersonInfo)
            If person Is Nothing Then Return
            NavigationService.Navigate(viewType:="NavigationDetailView", param:=person, parentViewModel:=Me)
        End Sub
    End Class
End Namespace
