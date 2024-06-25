Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel

Namespace TreeListDemo

    Public Class DataAnnotationsElement2

        <Display(AutoGenerateField:=False)>
        Public Property ID As Integer

        <Display(AutoGenerateField:=False)>
        Public Property ParentID As Integer

        <Editable(False)>
        Public Property Employer As String

        <Display(Name:="First name"), Required>
        Public Property FirstName As String

        <Display(Name:="Last name"), Required>
        Public Property LastName As String

        <Display(Name:="Full name")>
        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property

        Public Property Gender As Gender

        Public Property Age As Integer

        <ReadOnlyAttribute(True)>
        Public Property SSN As String

        Public Overrides Function ToString() As String
            Return "Attribute support"
        End Function
    End Class
End Namespace
