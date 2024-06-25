Imports System
Imports System.Linq.Expressions

Namespace DevExpress.DevAV.ViewModels

    Public Interface ISupportFiltering(Of TEntity As Class)

        Property FilterExpression As Expression(Of Func(Of TEntity, Boolean))

    End Interface
End Namespace
