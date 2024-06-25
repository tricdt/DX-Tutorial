Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    <MetadataType(GetType(DataAnnotationsElement3Metadata))>
    Public Class DataAnnotationsElement3_FluentAPI

        Public Property LastName As String

        Public Property FirstName As String

        Public Property Gender As Gender

        Public Property BirthDate As Date

        Public Property Address As String

        Public Property City As String

        Public Property Group As String

        Public Property Title As String

        Public Property HireDate As Date

        Public Property Salary As Decimal

        Public Property Phone As String

        Public Property Email As String
    End Class

    Public Module DataAnnotationsElement3Metadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of DataAnnotationsElement3_FluentAPI))
            builder.Property(Function(p) p.FirstName).DisplayName("First name")
            builder.Property(Function(p) p.LastName).DisplayName("Last name")
            builder.Property(Function(p) p.BirthDate).DisplayName("Birth date")
            builder.Group("Personal").ContainsProperty(Function(p) p.FirstName).ContainsProperty(Function(p) p.LastName).ContainsProperty(Function(p) p.Gender).ContainsProperty(Function(p) p.BirthDate)
            builder.Property(Function(p) p.Address).DisplayShortName("Address1")
            builder.Property(Function(p) p.City).DisplayShortName("Address2")
            builder.Group("Address").ContainsProperty(Function(p) p.Address).ContainsProperty(Function(p) p.City)
            builder.Property(Function(p) p.HireDate).DisplayName("Hire date")
            builder.[Property](Function(p) p.Salary).CurrencyDataType()
            builder.Group("Job").ContainsProperty(Function(p) p.Group).ContainsProperty(Function(p) p.Title).ContainsProperty(Function(p) p.HireDate).ContainsProperty(Function(p) p.Salary)
            builder.[Property](Function(p) p.Phone).PhoneNumberDataType()
            builder.Group("Contact").ContainsProperty(Function(p) p.Phone).ContainsProperty(Function(p) p.Email)
        End Sub
    End Module
End Namespace
