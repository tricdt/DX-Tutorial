Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm.DataAnnotations
Imports CreditCardAttribute = System.ComponentModel.DataAnnotations.CreditCardAttribute

Namespace PropertyGridDemo

    <MetadataType(GetType(DataAnnotationValidationMetadata))>
    Public Class DataAnnotationValidationData

        Const ZipCodeRegExp As String = "^\d{5}$"

        Private Const ZipCodeErrorMessage As String = "The {0} field is not a valid zip code."

        Public Property FirstName As String

        Public Property LastName As String

        Public Property Group As String

        <StringLength(20)>
        Public Property Title As String

        <CustomValidation(GetType(DataAnnotationValidationMetadata), "IsHireDateValid")>
        Public Property HireDate As Date

        <Range(0, 1000000)>
        Public Property Salary As Decimal

        <CreditCardAttribute>
        Public Property CreditCard As String

        <Required>
        Public Property AddressLine1 As String

        Public Property AddressLine2 As String

        <RegularExpression(ZipCodeRegExp, ErrorMessage:=ZipCodeErrorMessage)>
        Public Property ZipCode As String

        <Phone>
        Public Property Phone As String

        <EmailAddress>
        Public Property Email As String

        <Url>
        Public Property Facebook As String

        Public Property Gender As Gender

        Public Property BirthDate As Date
    End Class

    Public Module DataAnnotationValidationMetadata

        Public Function IsHireDateValid(ByVal [date] As Date) As ValidationResult
            Return If([date] <= Date.Today, ValidationResult.Success, New ValidationResult(String.Format("The {0} field cannot be in the future.", "HireDate")))
        End Function

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of DataAnnotationValidationData))
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
