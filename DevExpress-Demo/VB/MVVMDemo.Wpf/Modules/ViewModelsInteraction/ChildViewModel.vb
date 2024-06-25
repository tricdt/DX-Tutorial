Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace MVVMDemo.ViewModelsInteraction

    Public Class ChildViewModel
        Inherits ViewModelBase

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return ServiceContainer.GetService(Of IMessageBoxService)()
            End Get
        End Property

        <Command>
        Public Sub ShowMessage()
            MessageBoxService.ShowMessage("Showing message using service from parent view model.", "Message", MessageButton.OK, MessageIcon.Exclamation)
        End Sub
    End Class
End Namespace
