Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee
Imports EmployeesWithPhotoData = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData
Imports Order = DevExpress.DemoData.Models.Order

Namespace GridDemo

    Public Module EmployeesWithChartSource

        Public ReadOnly Property Employees As List(Of GridDemo.EmployeeWithChart)
            Get
                Dim employeesWithChart = New System.Collections.Generic.List(Of GridDemo.EmployeeWithChart)()
                Dim lEmployees As System.Collections.Generic.List(Of DevExpress.Xpf.DemoBase.DataClasses.Employee) = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData.DataSource
                Dim dict As System.Collections.Generic.Dictionary(Of Integer, Integer) = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData.OrdersRelations
                Dim context As DevExpress.DemoData.Models.NWindContext = DevExpress.DemoData.Models.NWindContext.Create()
                Dim orders As System.Collections.Generic.List(Of DevExpress.DemoData.Models.Order) = context.Orders.ToList()
                Dim invoices As System.Collections.Generic.List(Of DevExpress.DemoData.Models.Invoice) = context.Invoices.ToList()
                For Each empl As DevExpress.Xpf.DemoBase.DataClasses.Employee In lEmployees
                    Dim chartPoints As System.Collections.Generic.List(Of GridDemo.ChartPoint) = New System.Collections.Generic.List(Of GridDemo.ChartPoint)()
                    For Each order As DevExpress.DemoData.Models.Order In orders.Where(Function(o) dict(CInt(o.OrderID)) = empl.Id)
                        Dim cp As GridDemo.ChartPoint = New GridDemo.ChartPoint()
                        cp.ArgumentMember = order.OrderDate
                        System.Linq.Enumerable.ToList(Of DevExpress.DemoData.Models.Invoice)(System.Linq.Enumerable.Where(Of DevExpress.DemoData.Models.Invoice)(invoices, CType((Function(invoice) CBool((invoice.OrderID = order.OrderID))), System.Func(Of DevExpress.DemoData.Models.Invoice, System.[Boolean])))).ForEach(Sub(invoice) cp.ValueMember += System.Convert.ToInt32(invoice.Quantity * invoice.UnitPrice))
                        chartPoints.Add(cp)
                    Next

                    employeesWithChart.Add(New GridDemo.EmployeeWithChart(empl, chartPoints))
                Next

                Return employeesWithChart
            End Get
        End Property
    End Module

    Public Class EmployeeWithChart

        Private _JobTitle As String, _CountryRegionName As String, _BirthDate As DateTime, _EmailAddress As String, _FullName As String, _ChartSource As List(Of GridDemo.ChartPoint)

        Public Sub New(ByVal employee As DevExpress.Xpf.DemoBase.DataClasses.Employee, ByVal chartSource As System.Collections.Generic.List(Of GridDemo.ChartPoint))
            Me.ChartSource = chartSource
            Me.FullName = System.[String].Format("{0} {1}", employee.FirstName, employee.LastName)
            Me.JobTitle = employee.JobTitle
            Me.CountryRegionName = employee.CountryRegionName
            Me.BirthDate = employee.BirthDate
            Me.EmailAddress = employee.EmailAddress
        End Sub

        Public Property JobTitle As String
            Get
                Return _JobTitle
            End Get

            Private Set(ByVal value As String)
                _JobTitle = value
            End Set
        End Property

        Public Property CountryRegionName As String
            Get
                Return _CountryRegionName
            End Get

            Private Set(ByVal value As String)
                _CountryRegionName = value
            End Set
        End Property

        Public Property BirthDate As DateTime
            Get
                Return _BirthDate
            End Get

            Private Set(ByVal value As DateTime)
                _BirthDate = value
            End Set
        End Property

        Public Property EmailAddress As String
            Get
                Return _EmailAddress
            End Get

            Private Set(ByVal value As String)
                _EmailAddress = value
            End Set
        End Property

        Public Property FullName As String
            Get
                Return _FullName
            End Get

            Private Set(ByVal value As String)
                _FullName = value
            End Set
        End Property

        Public Property ChartSource As List(Of GridDemo.ChartPoint)
            Get
                Return _ChartSource
            End Get

            Private Set(ByVal value As List(Of GridDemo.ChartPoint))
                _ChartSource = value
            End Set
        End Property
    End Class

    Public Class ChartPoint

        Private _ArgumentMember As System.DateTime?

        Public Property ArgumentMember As System.DateTime?
            Get
                Return _ArgumentMember
            End Get

            Friend Set(ByVal value As System.DateTime?)
                _ArgumentMember = value
            End Set
        End Property

        Public Property ValueMember As Integer
    End Class
End Namespace
