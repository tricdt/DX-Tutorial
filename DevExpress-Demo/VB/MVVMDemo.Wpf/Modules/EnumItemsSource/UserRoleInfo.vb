Imports System
Imports System.Linq

Namespace MVVMDemo.EnumItemsSource

    Public Class UserRoleInfo

#Region "UserRoles"
        Public Shared ReadOnly Property UserRoles As MVVMDemo.EnumItemsSource.UserRoleInfo()
            Get
                Dim roles As MVVMDemo.EnumItemsSource.UserRole() = System.[Enum].GetValues(GetType(MVVMDemo.EnumItemsSource.UserRole)).Cast(Of MVVMDemo.EnumItemsSource.UserRole)().ToArray()
                Return MVVMDemo.PersonInfo.Persons.[Select](Function(person, i) New MVVMDemo.EnumItemsSource.UserRoleInfo() With {.FullName = person.FullName, .UserRole = roles(i)}).ToArray()
            End Get
        End Property

#End Region
        Public Property FullName As String

        Public Property UserRole As UserRole
    End Class
End Namespace
