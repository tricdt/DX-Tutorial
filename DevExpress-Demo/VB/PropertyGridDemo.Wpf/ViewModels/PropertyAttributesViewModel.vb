Imports System

Namespace PropertyGridDemo

    Public Class PropertyAttributesViewModel

        Public Overridable Property Data As AttributesSupportData

        Public Sub New()
            Data = New AttributesSupportData() With {.ID = 123456, .FirstName = "Nancy", .LastName = "Davolio", .Age = 36, .Gender = Gender.Female, .HireDate = New DateTime(2002, 2, 2), .SSN = "123-45-6789"}
        End Sub
    End Class
End Namespace
