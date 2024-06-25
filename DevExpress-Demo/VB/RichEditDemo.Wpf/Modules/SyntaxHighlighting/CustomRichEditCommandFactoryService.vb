Imports DevExpress.Utils
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Class CustomRichEditCommandFactoryService
        Implements IRichEditCommandFactoryService

        Private ReadOnly service As IRichEditCommandFactoryService

        Public Sub New(ByVal service As IRichEditCommandFactoryService)
            Guard.ArgumentNotNull(service, "service")
            Me.service = service
        End Sub

        Private Function CreateCommand(ByVal id As RichEditCommandId) As RichEditCommand Implements IRichEditCommandFactoryService.CreateCommand
            If id.Equals(RichEditCommandId.InsertColumnBreak) OrElse id.Equals(RichEditCommandId.InsertLineBreak) OrElse id.Equals(RichEditCommandId.InsertPageBreak) Then Return service.CreateCommand(RichEditCommandId.InsertParagraph)
            Return service.CreateCommand(id)
        End Function
    End Class
End Namespace
