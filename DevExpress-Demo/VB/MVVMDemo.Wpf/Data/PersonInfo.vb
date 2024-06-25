Imports System.ComponentModel.DataAnnotations

Namespace MVVMDemo

    Public Class PersonInfo

        Public Shared ReadOnly Property Persons As PersonInfo()
            Get
                Return {New PersonInfo() With {.FirstName = "Gregory", .LastName = "Price"}, New PersonInfo() With {.FirstName = "Irma", .LastName = "Marshal"}, New PersonInfo() With {.FirstName = "John", .LastName = "Powell"}}
            End Get
        End Property

        Public Property FirstName As String

        Public Property LastName As String

        <Display(AutoGenerateField:=False)>
        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " "c & LastName
            End Get
        End Property
    End Class
End Namespace
