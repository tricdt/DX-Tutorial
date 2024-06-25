Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services.DocumentManager

    Public Class CollectionViewModel

        Public Sub ShowDetail(ByVal person As PersonInfo)
            If person Is Nothing Then Return
            Dim service As IDocumentManagerService = GetRequiredService(Of IDocumentManagerService)()
            Dim document As IDocument = service.FindDocument(parameter:=person, parentViewModel:=Me)
            If document Is Nothing Then
                document = service.CreateDocument(documentType:="DocumentDetailView", parameter:=person, parentViewModel:=Me)
                document.Title = person.FullName
            End If

            document.Show()
        End Sub
    End Class
End Namespace
