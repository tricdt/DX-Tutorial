Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    Public Class DataAnnotationsElement4

        <Display(Name:="Id", AutoGenerateField:=False), Editable(False)>
        Public Property OrderId As Integer

        <Display(Name:="Category", GroupName:="Product Details"), Editable(False)>
        Public Property ProductCategory As CategoryData

        <Display(Name:="Name", GroupName:="Product Details"), Editable(False)>
        Public Property ProductName As String

        <Display(Name:="Customer", GroupName:="Order Details/Main"), Editable(False)>
        Public Property CustomerName As String

        <Display(Name:="Date", GroupName:="Order Details/Main")>
        Public Property OrderDate As Date

        <Display(GroupName:="Order Details/Status")>
        Public Property Quantity As Integer

        <Display(GroupName:="Order Details/Status"), DataType(DataType.Currency)>
        Public Property Price As Decimal

        <Display(Name:="Is ready", GroupName:="Order Details/Status")>
        Public Property IsReady As Boolean
    End Class
End Namespace
