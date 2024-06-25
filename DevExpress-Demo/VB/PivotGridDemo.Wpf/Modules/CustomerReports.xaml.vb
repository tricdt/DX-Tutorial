Imports System.ComponentModel.DataAnnotations

Namespace PivotGridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CustomerReportsViewModel.(cs)")>
    Public Partial Class CustomerReports
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Enum CustomerReportType
        Customers
        <Display(Name:="Products (filtering)")>
        Products
        <Display(Name:="Top 2 Products")>
        Top2Products
        <Display(Name:="Top 10 Customers")>
        Top10Customers
    End Enum
End Namespace
