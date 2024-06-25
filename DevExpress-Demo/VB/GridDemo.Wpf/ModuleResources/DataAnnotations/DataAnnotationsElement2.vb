Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    Public Class DataAnnotationsElement2

        <Display(AutoGenerateField:=False)>
        Public Property ID As Integer

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

        <DisplayFormat(DataFormatString:="d2", ApplyFormatInEditMode:=True)>
        Public Property Age As Integer

        <ReadOnlyAttribute(True)>
        Public Property SSN As String

        <DisplayFormat(NullDisplayText:="Department not set")>
        Public Property Department As String
    End Class
End Namespace
