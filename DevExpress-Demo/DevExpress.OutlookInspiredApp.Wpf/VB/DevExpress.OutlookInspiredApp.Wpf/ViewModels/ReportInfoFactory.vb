Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.Reports
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.ViewModels

    Public Module ReportInfoFactory

#Region "Employee"
        Public Function EmployeeTaskList(ByVal tasks As IEnumerable(Of EmployeeTask)) As IReportInfo
            Return GetReportInfo(SortByDateViewModel.Create(), Function(p) ReportFactory.EmployeeTaskList(tasks, p.SortDirection = SortByDatePrintMode.SortByDueDate))
        End Function

        Public Function EmployeeProfile(ByVal employee As Employee) As IReportInfo
            Return GetReportInfo(EmployeeEvaluationsPrintModeViewModel.Create(), Function(p) If(employee Is Nothing, Nothing, ReportFactory.EmployeeProfile(employee, p.EmployeeEvaluationsPrintMode <> EmployeeEvaluationsPrintMode.ExcludeEvaluations)))
        End Function

        Public Function EmployeeSummary(ByVal employees As IEnumerable(Of Employee)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.EmployeeSummary(employees, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function

        Public Function EmployeeDirectory(ByVal employees As IEnumerable(Of Employee)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.EmployeeDirectory(employees, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function

#End Region
#Region "Customer"
        Public Function CusomerProfile(ByVal customer As Customer) As IReportInfo
            Return GetReportInfo(CustomerContactsPrintModeViewModel.Create(), Function(p) If(customer Is Nothing, Nothing, ReportFactory.CustomerProfile(customer, p.CustomerContactsPrintMode <> CustomerContactsPrintMode.ExcludeContacts)))
        End Function

        Public Function CustomerContactsDirectory(ByVal customer As Customer) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.CustomerContactsDirectory(customer.Employees, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function

        Public Function CustomerSalesDetail(ByVal orders As IEnumerable(Of Order)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesDetail(orders, orders.SelectMany(Function(x) x.OrderItems).ToArray(), p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function CustomerSalesDetailReport(ByVal orders As IEnumerable(Of CustomerSaleDetailOrderInfo)) As IReportInfo
            Dim orderItems = orders.SelectMany(Function(x) x.OrderItems).ToArray()
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesDetailReport(orders, orderItems, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function CustomerSalesSummary(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesSummary(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function CustomerSalesSummaryReport(ByVal sales As IEnumerable(Of CustomerSaleDetailOrderItemInfo)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesSummaryReport(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function CustomerLocationsDirectory(ByVal customers As IEnumerable(Of Customer)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.CustomerLocationsDirectory(customers, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function

#End Region
#Region "Order"
        Public Function SalesInvoice(ByVal order As Order) As IReportInfo
            Return GetReportInfo(InvoiceOptionsViewModel.Create(), Function(p) ReportFactory.SalesInvoice(order, p.IsOptionSelected("ShowHeader"), p.IsOptionSelected("ShowFooter"), p.IsOptionSelected("ShowStatus"), p.IsOptionSelected("ShowComments")))
        End Function

        Public Function SalesOrdersSummary(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.SalesOrdersSummary(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function SalesOrdersSummaryReport(ByVal sales As IEnumerable(Of SaleSummaryInfo)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.SalesOrdersSummaryReport(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function SalesAnalysis(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SelectYearsViewModel.Create(), Function(p) ReportFactory.SalesAnalysis(sales, p.Years))
        End Function

        Public Function SalesAnalysisReport(ByVal sales As IEnumerable(Of SaleAnalysisInfo)) As IReportInfo
            Return GetReportInfo(SelectYearsViewModel.Create(), Function(p) ReportFactory.SalesAnalysisReport(sales, p.Years))
        End Function

        Public Function SalesRevenueReport(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.SalesRevenueReport(sales, p.SortDirection = SortByPrintMode.SortByOrderDate))
        End Function

        Public Function SalesRevenueAnalysisReport(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.SalesRevenueAnalysisReport(sales, p.SortDirection = SortByPrintMode.SortByOrderDate))
        End Function

#End Region
#Region "Task"
        Public Function TaskListReport(ByVal tasks As IEnumerable(Of EmployeeTask)) As IReportInfo
            Return GetReportInfo(SortByDateViewModel.Create(), Function(p) ReportFactory.TaskListReport(tasks, p.SortDirection = SortByDatePrintMode.SortByDueDate))
        End Function

        Public Function TaskDetailReport(ByVal task As EmployeeTask) As IReportInfo
            Return New ParameterlessReportInfo(ReportFactory.TaskDetailReport(task))
        End Function

#End Region
#Region "Product"
        Public Function ProductProfile(ByVal product As Product) As IReportInfo
            Return GetReportInfo(ProductImagesPrintModeViewModel.Create(), Function(p) ReportFactory.ProductProfile(product, p.ProductImagesPrintMode = ProductImagesPrintMode.DisplayProductImages))
        End Function

        Public Function ProductOrders(ByVal sales As IEnumerable(Of OrderItem), ByVal states As IList(Of State)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.ProductOrders(sales, states, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function

        Public Function ProductSalesSummary(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SelectYearsViewModel.Create(), Function(p) ReportFactory.ProductSalesSummary(sales, p.Years))
        End Function

        Public Function ProductTopSalesPerson(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.ProductTopSalesPerson(sales, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function

#End Region
        Private Function GetReportInfo(Of TParametersViewModel)(ByVal parametersViewModel As TParametersViewModel, ByVal reportFactory As Func(Of TParametersViewModel, IReport)) As IReportInfo
            Return New ReportInfo(Of TParametersViewModel)(parametersViewModel, reportFactory)
        End Function
    End Module

    Public Class SortDirectionViewModel

        Public Shared Function Create() As SortDirectionViewModel
            Return ViewModelSource.Create(Function() New SortDirectionViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property SortDirection As SortOrderPrintMode
    End Class

    Public Class SortDirectionAndDateRangeViewModel
        Inherits SortDirectionViewModel

        Public Overloads Shared Function Create() As SortDirectionAndDateRangeViewModel
            Return ViewModelSource.Create(Function() New SortDirectionAndDateRangeViewModel())
        End Function

        Protected Sub New()
            FromDate = New DateTime(2011, 1, 1)
            ToDate = New DateTime(2013, 1, 1)
        End Sub

        Public Overridable Property ToDate As Date

        Public Overridable Property FromDate As Date
    End Class

    Public Class SortByDateViewModel

        Public Shared Function Create() As SortByDateViewModel
            Return ViewModelSource.Create(Function() New SortByDateViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property SortDirection As SortByDatePrintMode
    End Class

    Public Class SortByViewModel

        Public Shared Function Create() As SortByViewModel
            Return ViewModelSource.Create(Function() New SortByViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property SortDirection As SortByPrintMode
    End Class

    Public Class SortByAndDateRangeViewModel
        Inherits SortByViewModel

        Public Overloads Shared Function Create() As SortByAndDateRangeViewModel
            Return ViewModelSource.Create(Function() New SortByAndDateRangeViewModel())
        End Function

        Protected Sub New()
            Dim releasePeriod As Integer = AssemblyInfo.VersionId Mod 10
            Dim releaseYear As Integer = CInt(AssemblyInfo.VersionId \ 10) + 2000
            ToDate = New DateTime(releaseYear + 1, If(releasePeriod = 1, 1, 6), 1)
            FromDate = New DateTime(ToDate.Year - 3, 1, 1)
        End Sub

        Public Overridable Property ToDate As Date

        Public Overridable Property FromDate As Date
    End Class

    Public Class EmployeeEvaluationsPrintModeViewModel

        Public Shared Function Create() As EmployeeEvaluationsPrintModeViewModel
            Return ViewModelSource.Create(Function() New EmployeeEvaluationsPrintModeViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property EmployeeEvaluationsPrintMode As EmployeeEvaluationsPrintMode
    End Class

    Public Class CustomerContactsPrintModeViewModel

        Public Shared Function Create() As CustomerContactsPrintModeViewModel
            Return ViewModelSource.Create(Function() New CustomerContactsPrintModeViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property CustomerContactsPrintMode As CustomerContactsPrintMode
    End Class

    Public Class SelectYearsViewModel

        Public Shared Function Create() As SelectYearsViewModel
            Return ViewModelSource.Create(Function() New SelectYearsViewModel())
        End Function

        Protected Sub New()
            AvailableYears = New List(Of String)() From {GetYear(-3), GetYear(-2), GetYear(-1), GetYear()}
            Years = String.Format("{0},{1}", GetYear(-2), GetYear(-1))
        End Sub

        Private Function GetYear(ByVal Optional offset As Integer = 0) As String
            Dim releaseYear As Integer = CInt(AssemblyInfo.VersionId \ 10) + 2000
            Return(releaseYear + offset).ToString()
        End Function

        Public Property AvailableYears As List(Of String)

        Public Overridable Property Years As String
    End Class

    Public Class InvoiceOptionsViewModel

        Public Shared Function Create() As InvoiceOptionsViewModel
            Return ViewModelSource.Create(Function() New InvoiceOptionsViewModel())
        End Function

        Protected Sub New()
            AvailableOptions = New List(Of Tuple(Of String, String))() From {New Tuple(Of String, String)("ShowHeader", "Show Header"), New Tuple(Of String, String)("ShowFooter", "Show Footer"), New Tuple(Of String, String)("ShowStatus", "Show Status"), New Tuple(Of String, String)("ShowComments", "Show Comments")}
            SelectedOptions = New List(Of Object)() From {AvailableOptions(0)}
        End Sub

        Public Property AvailableOptions As List(Of Tuple(Of String, String))

        Public Overridable Property SelectedOptions As List(Of Object)

        Public Function IsOptionSelected(ByVal id As String) As Boolean
            If SelectedOptions Is Nothing Then Return False
            Return SelectedOptions.Cast(Of Tuple(Of String, String))().Any(Function(x) Equals(x.Item1, id))
        End Function
    End Class

    Public Class ProductImagesPrintModeViewModel

        Public Shared Function Create() As ProductImagesPrintModeViewModel
            Return ViewModelSource.Create(Function() New ProductImagesPrintModeViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property ProductImagesPrintMode As ProductImagesPrintMode
    End Class

    Public Enum EmployeeEvaluationsPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/PrintExcludeEvaluations.svg")>
        ExcludeEvaluations
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/PrintIncludeEvaluations.svg")>
        IncludeEvaluations
    End Enum

    Public Enum CustomerContactsPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/PrintIncludeEvaluations.svg")>
        IncludeContacts
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/PrintExcludeEvaluations.svg")>
        ExcludeContacts
    End Enum

    Public Enum ProductImagesPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/ShowProduct.svg")>
        DisplayProductImages
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/HideProduct.svg")>
        HideProductImages
    End Enum

    Public Enum SortOrderPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/SortAsc.svg")>
        AscendingOrder
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/SortDesc.svg")>
        DescencingOrder
    End Enum

    Public Enum SortByDatePrintMode
        <Display(Name:="Sort by Due Date"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/ShowDueDate.svg")>
        SortByDueDate
        <Display(Name:="Sort by Start Date"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/ShowStartDate.svg")>
        SortByStartDate
    End Enum

    Public Enum SortByPrintMode
        <Display(Name:="Sort by Order Date"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/SortByOrderDate.svg")>
        SortByOrderDate
        <Display(Name:="Sort by Invoice #"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/SortByInvoice.svg")>
        SortByInvoice
    End Enum
End Namespace
