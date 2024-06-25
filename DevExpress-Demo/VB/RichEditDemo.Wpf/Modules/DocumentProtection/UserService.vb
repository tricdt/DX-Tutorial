Imports System.Collections.Generic
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo

    Public Class UserService
        Implements IUserListService

        Private ReadOnly usersField As List(Of String) = New List(Of String)()

        Public ReadOnly Property Users As List(Of String)
            Get
                Return usersField
            End Get
        End Property

        Private Function GetUsers() As IList(Of String) Implements IUserListService.GetUsers
            Return Users
        End Function

        Public Sub Update(ByVal userList As List(Of String))
            usersField.Clear()
            usersField.AddRange(userList)
        End Sub
    End Class
End Namespace
