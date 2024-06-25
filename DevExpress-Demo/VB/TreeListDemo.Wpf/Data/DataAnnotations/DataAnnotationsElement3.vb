Imports System.ComponentModel.DataAnnotations

Namespace TreeListDemo

    Public Class DataAnnotationsElement3

        <Display(AutoGenerateField:=False)>
        Public Property ID As Integer

        <Display(AutoGenerateField:=False)>
        Public Property ParentID As Integer

        <Display(Name:="First name", GroupName:="Personal")>
        Public Property FirstName As String

        <Display(Name:="Last name", GroupName:="Personal")>
        Public Property LastName As String

        <Display(GroupName:="Address", ShortName:="Address1")>
        Public Property Address As String

        <Display(GroupName:="Address", ShortName:="Address2")>
        Public Property City As String

        <Display(GroupName:="Personal", Name:="Birth date")>
        Public Property BirthDate As Date

        <Display(GroupName:="Personal")>
        Public Property Gender As Gender

        <Display(GroupName:="Job")>
        Public Property Group As String

        <Display(GroupName:="Job", Name:="Hire date")>
        Public Property HireDate As Date

        <Display(GroupName:="Contact"), DataType(DataType.PhoneNumber)>
        Public Property Phone As String

        <Display(GroupName:="Contact")>
        Public Property Email As String

        <Display(GroupName:="Job"), DataType(DataType.Currency)>
        Public Property Salary As Decimal

        <Display(GroupName:="Job")>
        Public Property Title As String

        Public Overrides Function ToString() As String
            Return "Bands Layout"
        End Function
    End Class
End Namespace
