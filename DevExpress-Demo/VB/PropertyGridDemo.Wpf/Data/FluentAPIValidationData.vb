Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace PropertyGridDemo

    <MetadataType(GetType(FluentAPIValidationMetadata))>
    Public Class FluentAPIValidationData

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Group As String

        Public Property Title As String

        Public Property HireDate As Date

        Public Property Salary As Decimal

        Public Property CreditCard As String

        Public Property AddressLine1 As String

        Public Property AddressLine2 As String

        Public Property ZipCode As String

        Public Property Phone As String

        Public Property Email As String

        Public Property Facebook As String

        Public Property Gender As Gender

        Public Property BirthDate As Date
    End Class

    Public Module FluentAPIValidationMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of FluentAPIValidationData))
#Region "validation"
            builder.[Property](Function(x) x.Email).EmailAddressDataType()
            builder.[Property](Function(x) x.Phone).PhoneNumberDataType()
            builder.[Property](Function(x) x.Facebook).UrlDataType()
            builder.Property(Function(x) x.Title).MaxLength(20)
            builder.Property(Function(x) x.HireDate).MatchesRule(Function(x) x <= Date.Today, Function() "The {0} field cannot be in the future.")
            builder.[Property](Function(x) x.Salary).InRange(0, 1000000)
            builder.[Property](Function(x) x.CreditCard).CreditCardDataType()
            builder.Property(Function(x) x.AddressLine1).Required()
            builder.Property(Function(x) x.ZipCode).MatchesRegularExpression("^\d{5}$", Function() "The {0} field is not a valid zip code.")
#End Region
#Region "display options"
            builder.[Property](Function(x) x.Salary).CurrencyDataType()
            builder.Property(Function(x) x.FirstName).DisplayName("First name")
            builder.Property(Function(x) x.LastName).DisplayName("Last name")
            builder.Property(Function(x) x.BirthDate).DisplayName("Hire date")
            builder.Property(Function(x) x.BirthDate).DisplayName("Birth date")
            builder.Property(Function(x) x.AddressLine1).DisplayName("Address line 1")
            builder.Property(Function(x) x.AddressLine2).DisplayName("Address line 2")
            builder.Property(Function(x) x.ZipCode).DisplayName("Zip code")
            builder.Property(Function(x) x.CreditCard).DisplayName("Credit card")
#End Region
#Region "layout"
            builder.Group("Name").ContainsProperty(Function(x) x.FirstName).ContainsProperty(Function(x) x.LastName)
            builder.Group("Job").ContainsProperty(Function(x) x.Group).ContainsProperty(Function(x) x.Title).ContainsProperty(Function(x) x.HireDate).ContainsProperty(Function(x) x.Salary).ContainsProperty(Function(x) x.CreditCard)
            builder.Group("Contact").ContainsProperty(Function(x) x.Phone).ContainsProperty(Function(x) x.Email).ContainsProperty(Function(x) x.Facebook)
            builder.Group("Personal").ContainsProperty(Function(x) x.Gender).ContainsProperty(Function(x) x.BirthDate)
            builder.Group("Address").ContainsProperty(Function(x) x.AddressLine1).ContainsProperty(Function(x) x.AddressLine2).ContainsProperty(Function(x) x.ZipCode)
#End Region
        End Sub
    End Module
End Namespace
