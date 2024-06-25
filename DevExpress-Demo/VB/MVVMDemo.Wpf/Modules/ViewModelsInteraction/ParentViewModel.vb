Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.ViewModelsInteraction

    Public Class ParentViewModel
        Inherits ViewModelBase

        <Command>
        Public Sub ShowDetail()
            Dim service As IDialogService = ServiceContainer.GetService(Of IDialogService)()
            service.ShowDialog(dialogButtons:=MessageButton.OK, title:="Detail", documentType:=Nothing, parameter:=Nothing, parentViewModel:=Me)
        End Sub
    End Class
End Namespace
