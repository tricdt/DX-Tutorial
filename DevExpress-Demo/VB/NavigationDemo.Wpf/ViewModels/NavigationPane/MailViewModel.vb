Imports System.Collections.Generic
Imports DevExpress.Mvvm

Namespace NavigationDemo

    Public Class MailViewModel
        Inherits ViewModelBase

        Public Overridable Property ItemsSource As IList(Of MailItem)

        Public Overridable Property SentGroupIndex As Integer

        Public Overridable Property ReceiveGroupIndex As Integer

        Public Overridable Property FolderId As NavigationId

        Public Sub New()
            SentGroupIndex = -1
            ReceiveGroupIndex = -1
        End Sub

        Protected Overrides Sub OnParameterChanged(ByVal parameter As Object)
            If parameter IsNot Nothing Then UpdateIds(CType(parameter, NavigationId))
        End Sub

        Private Sub UpdateIds(ByVal id As NavigationId)
            FolderId = id
            Select Case id
                Case NavigationId.Inbox
                    SentGroupIndex = -1
                    ReceiveGroupIndex = 0
                Case NavigationId.Outbox
                    SentGroupIndex = 0
                    ReceiveGroupIndex = -1
                Case NavigationId.SentItems
                    SentGroupIndex = 0
                    ReceiveGroupIndex = -1
                Case NavigationId.DeletedItems
                    SentGroupIndex = -1
                    ReceiveGroupIndex = 0
                Case NavigationId.Drafts
                    SentGroupIndex = 0
                    ReceiveGroupIndex = -1
            End Select

            ItemsSource = MailData(id)
        End Sub
    End Class
End Namespace
