Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo

    Public Class AdvancedGroupedLayoutData

        Const NameGroup As String = "<Name>"

        Const TabbedGroup As String = "{Tabs}"

        Const JobGroup As String = "Job"

        Const JobGroupPath As String = TabbedGroup & "/" & JobGroup

        Const ContactGroup As String = "Contact"

        Const ContactGroupPath As String = TabbedGroup & "/" & ContactGroup

        Const AddressGroup As String = "Address"

        Const AddressGroupPath As String = ContactGroupPath & "/" & AddressGroup

        Const PersonalGroup As String = "Personal-"

        <Display(GroupName:=AddressGroupPath, ShortName:="")>
        Public Property AddressLine1 As String

        <Display(GroupName:=AddressGroupPath, ShortName:="")>
        Public Property AddressLine2 As String

        <Display(GroupName:=PersonalGroup, Name:="Birth date")>
        Public Property BirthDate As Date

        <Display(GroupName:=ContactGroupPath, Order:=21)>
        Public Property Email As String

        <Display(GroupName:=NameGroup, Name:="First name", Order:=0)>
        Public Property FirstName As String

        <Display(GroupName:=PersonalGroup, Order:=3)>
        Public Property Gender As Gender

        <Display(GroupName:=JobGroupPath, Order:=1)>
        Public Property Group As String

        <Display(GroupName:=JobGroupPath, Name:="Hire date")>
        Public Property HireDate As Date

        <Display(GroupName:=NameGroup, Name:="Last name")>
        Public Property LastName As String

        <Display(GroupName:=ContactGroupPath, Order:=2), DataType(DataType.PhoneNumber)>
        Public Property Phone As String

        <Display(GroupName:=JobGroupPath), DataType(DataType.Currency)>
        Public Property Salary As Decimal

        <Display(GroupName:=JobGroupPath, Order:=11)>
        Public Property Title As String
    End Class
End Namespace
