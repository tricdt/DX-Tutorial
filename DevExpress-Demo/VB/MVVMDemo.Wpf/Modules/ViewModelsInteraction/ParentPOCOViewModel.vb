Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.ViewModelsInteraction

    Public Class ParentPOCOViewModel

        Public Sub ShowDetail()
            Dim service As IDialogService = GetRequiredService(Of IDialogService)()
            service.ShowDialog(dialogButtons:=MessageButton.OK, title:="Detail", documentType:=Nothing, parameter:=Nothing, parentViewModel:=Me)
        End Sub
    End Class
End Namespace
