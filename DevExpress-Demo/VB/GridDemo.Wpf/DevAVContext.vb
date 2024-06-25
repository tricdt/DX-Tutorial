Imports System.Data.Entity

Namespace DevExpress.DevAV

    Public Class DevAVDbContext
        Inherits DevAVDbBase

        Shared Sub New()
            Database.SetInitializer(Of DevAVDbContext)(Nothing)
        End Sub
    End Class
End Namespace
