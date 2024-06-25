Imports System
Imports System.Linq

Namespace MVVMDemo.DXDataTemplateSelector

    Public Class UserRoleInfo

#Region "UserRoles"
        Public Shared ReadOnly Property UserRoles As MVVMDemo.DXDataTemplateSelector.UserRoleInfo()
            Get
                Dim roles As MVVMDemo.DXDataTemplateSelector.UserRole() = System.[Enum].GetValues(GetType(MVVMDemo.DXDataTemplateSelector.UserRole)).Cast(Of MVVMDemo.DXDataTemplateSelector.UserRole)().ToArray()
                Return MVVMDemo.PersonInfo.Persons.[Select](Function(person, i) New MVVMDemo.DXDataTemplateSelector.UserRoleInfo() With {.FullName = person.FullName, .UserRole = roles(i)}).ToArray()
            End Get
        End Property

#End Region
        Public Property FullName As String

        Public Property UserRole As UserRole

        Public Function IsAdmin() As Boolean
            Return Me.UserRole = MVVMDemo.DXDataTemplateSelector.UserRole.Admin
        End Function
    End Class

    Public Enum UserRole
        Admin
        Moderator
        User
    End Enum
End Namespace
