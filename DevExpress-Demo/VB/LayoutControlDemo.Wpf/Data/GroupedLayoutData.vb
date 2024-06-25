Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo

    Public Class GroupedLayoutData

        Const JobGroup As String = "Job"

        Const ContactGroup As String = "Contact"

        Const AddressGroup As String = "Address"

        Const PersonalGroup As String = "Personal"

        <Display(GroupName:=AddressGroup, ShortName:="", Order:=4)>
        Public Property AddressLine1 As String

        <Display(GroupName:=AddressGroup, ShortName:="")>
        Public Property AddressLine2 As String

        <Display(GroupName:=PersonalGroup, Name:="Birth date")>
        Public Property BirthDate As Date

        <Display(GroupName:=ContactGroup)>
        Public Property Email As String

        <Display(Name:="First name", Order:=0)>
        Public Property FirstName As String

        <Display(GroupName:=PersonalGroup, Order:=5)>
        Public Property Gender As Gender

        <Display(GroupName:=JobGroup, Order:=2)>
        Public Property Group As String

        <Display(GroupName:=JobGroup, Name:="Hire date")>
        Public Property HireDate As Date

        <Display(Name:="Last name", Order:=1)>
        Public Property LastName As String

        <Display(GroupName:=ContactGroup, Order:=3), DataType(DataType.PhoneNumber)>
        Public Property Phone As String

        <Display(GroupName:=JobGroup), DataType(DataType.Currency)>
        Public Property Salary As Decimal

        <Display(GroupName:=JobGroup, Order:=21)>
        Public Property Title As String
    End Class
End Namespace
