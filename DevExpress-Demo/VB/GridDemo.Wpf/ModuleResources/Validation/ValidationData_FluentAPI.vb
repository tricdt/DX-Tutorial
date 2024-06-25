Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    <MetadataType(GetType(ValidationFluentAPIMetadata))>
    Public Class ValidationData_FluentAPI

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Title As String

        Public Property HireDate As Date

        Public Property Salary As Decimal

        Public Property CreditCard As String

        Public Property Address As String

        Public Property ZipCode As String

        Public Property Phone As String

        Public Property Email As String

        Public Property Facebook As String
    End Class

    Public Module ValidationFluentAPIMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of ValidationData_FluentAPI))
#Region "validation"
            builder.Property(Function(x) x.FirstName).Required()
            builder.Property(Function(x) x.LastName).Required()
            builder.[Property](Function(x) x.Email).EmailAddressDataType()
            builder.[Property](Function(x) x.Phone).PhoneNumberDataType()
            builder.[Property](Function(x) x.Facebook).UrlDataType()
            builder.Property(Function(x) x.Title).MaxLength(30)
            builder.Property(Function(x) x.HireDate).MatchesRule(Function(x) x <= Date.Today, Function() "The {0} field cannot be in the future.")
            builder.[Property](Function(x) x.Salary).InRange(0, 1000000)
            builder.[Property](Function(x) x.CreditCard).CreditCardDataType()
            builder.Property(Function(x) x.Address).Required()
            builder.Property(Function(x) x.ZipCode).MatchesRegularExpression("^\d{5}$", Function() "The {0} field is not a valid zip code.")
#End Region
#Region "display options"
            builder.[Property](Function(x) x.Salary).CurrencyDataType()
#End Region
        End Sub
    End Module
End Namespace
