Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm.DataAnnotations

Namespace PropertyGridDemo

    <MetadataType(GetType(AttributesSupportDataAnnotationsFluentAPIMetadata))>
    Public Class AttributesSupportDataAnnotationsFluentAPIData

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Email As String

        Public Property Group As String

        Public Property HireDate As Date

        Public Property Salary As Decimal

        Public Property AddressLine1 As String

        Public Property AddressLine2 As String

        Public Property Phone As String

        Public Property Title As String

        Public Property BirthDate As Date

        Public Property Gender As Gender
    End Class

    Public Class AttributesSupportDataAnnotationsFluentAPIMetadata

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of AttributesSupportDataAnnotationsFluentAPIData))
#Region "display options"
            builder.Property(Function(x) x.AddressLine1).DisplayShortName("Address line 1")
            builder.Property(Function(x) x.AddressLine2).DisplayShortName("Address line 2")
            builder.Property(Function(x) x.BirthDate).DisplayShortName("Birth date").DisplayFormatString("m", applyDisplayFormatInEditMode:=True)
            builder.Property(Function(x) x.FirstName).DisplayShortName("First name")
            builder.Property(Function(x) x.LastName).DisplayShortName("Last name")
            builder.Property(Function(x) x.Title).NullDisplayText("Title not set")
            builder.Property(Function(x) x.HireDate).DisplayName("Hire date")
            builder.[Property](Function(x) x.Phone).PhoneNumberDataType()
            builder.[Property](Function(x) x.Salary).CurrencyDataType()
            builder.[Property](Function(x) x.Email).EmailAddressDataType()
#End Region
#Region "layout"
            builder.Group("Name").ContainsProperty(Function(x) x.FirstName).ContainsProperty(Function(x) x.LastName)
            builder.Group("Job").ContainsProperty(Function(x) x.Group).ContainsProperty(Function(x) x.Title).ContainsProperty(Function(x) x.HireDate).ContainsProperty(Function(x) x.Salary)
            builder.Group("Address").ContainsProperty(Function(x) x.AddressLine1).ContainsProperty(Function(x) x.AddressLine2)
            builder.Group("Contact").ContainsProperty(Function(x) x.Email).ContainsProperty(Function(x) x.Phone)
            builder.Group("Personal").ContainsProperty(Function(x) x.Gender).ContainsProperty(Function(x) x.BirthDate)
#End Region
        End Sub
    End Class
End Namespace
