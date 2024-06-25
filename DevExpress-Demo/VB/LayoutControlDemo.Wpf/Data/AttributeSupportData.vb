Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo

    Public Class AttributeSupportData

        <Display(AutoGenerateField:=False)>
        Public Property ID As Integer

        <DisplayFormat(DataFormatString:="d2", ApplyFormatInEditMode:=True)>
        Public Property Age As Integer

        <Editable(False)>
        Public Property Employer As String

        <Display(Name:="First name", Order:=0), Required>
        Public Property FirstName As String

        <Display(Name:="Full name", Order:=2)>
        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property

        <Display(ShortName:="Sex", Order:=3)>
        Public Property Gender As Gender

        <Display(Name:="Last name", Order:=1), Required>
        Public Property LastName As String

        <ReadOnlyAttribute(True)>
        Public Property SSN As String

        <DisplayFormat(NullDisplayText:="Department not set")>
        Public Property Department As String
    End Class
End Namespace
