Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.ViewModelsInteraction

    Public Class MainPOCOViewModel

        Public Sub ShowDetail(ByVal item As String)
            Dim service As IDialogService = GetRequiredService(Of IDialogService)()
            service.ShowDialog(dialogButtons:=MessageButton.OK, title:="Detail", documentType:=Nothing, parameter:=item, parentViewModel:=Me)
        End Sub
    End Class
End Namespace
