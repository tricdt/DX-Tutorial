Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.ViewModelsInteraction

    Public Class ChildPOCOViewModel

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetRequiredService(Of IMessageBoxService)()
            End Get
        End Property

        Public Sub ShowMessage()
            MessageBoxService.ShowMessage("Showing message using service from parent view model.", "Message", MessageButton.OK, MessageIcon.Exclamation)
        End Sub
    End Class
End Namespace
