Imports DevExpress.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    Public Class ValidationData

        Const ZipCodeRegExp As String = "^\d{5}$"

        Private Const ZipCodeErrorMessage As String = "The {0} field is not a valid zip code."

        Public Shared Function IsHireDateValid(ByVal [date] As Date) As ValidationResult
            Return If([date] <= Date.Today, ValidationResult.Success, New ValidationResult(String.Format("The {0} field cannot be in the future.", "HireDate")))
        End Function

        <Required>
        Public Property FirstName As String

        <Required>
        Public Property LastName As String

        <StringLength(30)>
        Public Property Title As String

        <CustomValidation(GetType(ValidationData), "IsHireDateValid")>
        Public Property HireDate As Date

        <Range(GetType(Decimal), "0", "1000000"), DataType(DataType.Currency)>
        Public Property Salary As Decimal

        <DemoCreditCard>
        Public Property CreditCard As String

        <Required>
        Public Property Address As String

        <RegularExpression(ZipCodeRegExp, ErrorMessage:=ZipCodeErrorMessage)>
        Public Property ZipCode As String

        <DemoPhone>
        Public Property Phone As String

        <DemoEmailAddress>
        Public Property Email As String

        <DemoUrl>
        Public Property Facebook As String
    End Class
End Namespace
