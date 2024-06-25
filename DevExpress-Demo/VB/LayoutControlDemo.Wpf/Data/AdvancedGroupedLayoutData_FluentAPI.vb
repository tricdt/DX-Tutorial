Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations
Imports System.Windows.Controls

Namespace LayoutControlDemo

    <MetadataType(GetType(AdvancedGroupedLayoutMetadata))>
    Public Class AdvancedGroupedLayoutData_FluentAPI

        Public Property AddressLine1 As String

        Public Property AddressLine2 As String

        Public Property BirthDate As Date

        Public Property Email As String

        Public Property FirstName As String

        Public Property Gender As Gender

        Public Property Group As String

        Public Property HireDate As Date

        Public Property LastName As String

        Public Property Phone As String

        Public Property Salary As Decimal

        Public Property Title As String
    End Class

    Public Module AdvancedGroupedLayoutMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of AdvancedGroupedLayoutData_FluentAPI))
            builder.DataFormLayout().Group("Name", Orientation.Horizontal).ContainsProperty(Function(x) x.FirstName).ContainsProperty(Function(x) x.LastName).EndGroup().TabbedGroup("Tabs").Group("Job").ContainsProperty(Function(x) x.Group).ContainsProperty(Function(x) x.Title).ContainsProperty(Function(x) x.HireDate).ContainsProperty(Function(x) x.Salary).EndGroup().Group("Contact").ContainsProperty(Function(x) x.Phone).ContainsProperty(Function(x) x.Email).GroupBox("Address").ContainsProperty(Function(x) x.AddressLine1).ContainsProperty(Function(x) x.AddressLine2).EndGroup().EndGroup().EndGroup().GroupBox("Personal", Orientation.Horizontal).ContainsProperty(Function(x) x.Gender).ContainsProperty(Function(x) x.BirthDate).EndGroup()
            builder.Property(Function(x) x.AddressLine1).DisplayShortName(String.Empty)
            builder.Property(Function(x) x.AddressLine2).DisplayShortName(String.Empty)
            builder.Property(Function(x) x.BirthDate).DisplayName("Birth date")
            builder.Property(Function(x) x.FirstName).DisplayName("First name")
            builder.Property(Function(x) x.LastName).DisplayName("Last name")
            builder.Property(Function(x) x.HireDate).DisplayName("Hire date")
            builder.[Property](Function(x) x.Phone).PhoneNumberDataType()
            builder.[Property](Function(x) x.Salary).CurrencyDataType()
        End Sub
    End Module
End Namespace
