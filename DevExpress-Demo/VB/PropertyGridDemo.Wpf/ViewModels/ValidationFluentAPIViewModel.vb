Imports System

Namespace PropertyGridDemo

    Public Class ValidationFluentAPIViewModel

        Public Overridable Property Data As FluentAPIValidationData

        Public Sub New()
            Data = New FluentAPIValidationData() With {.FirstName = "Anita", .LastName = "Benson", .Group = "Inventory Management", .Title = "Purchasing Manager Purchasing Manager", .HireDate = Date.Today.AddDays(1), .Salary = -10000D, .CreditCard = "1234 5678 1234 5678", .Phone = "(713) 863 813", .Email = "@gmail.com", .Facebook = "ttps://www.facebook.com/anitabenson", .AddressLine1 = "", .AddressLine2 = "Seattle, 77025, USA", .ZipCode = "1234", .Gender = Gender.Female, .BirthDate = New DateTime(1985, 3, 18)}
        End Sub
    End Class
End Namespace
