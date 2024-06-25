Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.ViewModelsInteraction

    Public Class MainViewModel
        Inherits ViewModelBase

        <Command>
        Public Sub ShowDetail(ByVal item As String)
            Dim service As IDialogService = ServiceContainer.GetService(Of IDialogService)()
            service.ShowDialog(dialogButtons:=MessageButton.OK, title:="Detail", documentType:=Nothing, parameter:=item, parentViewModel:=Me)
        End Sub
    End Class
End Namespace
