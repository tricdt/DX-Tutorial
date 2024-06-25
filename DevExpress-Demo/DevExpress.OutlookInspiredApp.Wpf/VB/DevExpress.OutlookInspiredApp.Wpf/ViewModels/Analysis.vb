Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV.DevAVDbDataModel
Imports System.Runtime.CompilerServices

Namespace DevExpress.DevAV.ViewModels

    Public Module AnalysisPeriod

        Public ReadOnly Start As System.DateTime = New System.DateTime(System.DateTime.Now.Year - 3, 10, 1)

        Public ReadOnly [End] As System.DateTime = New System.DateTime(System.DateTime.Now.Year, 09, 30)

        Public Function MonthOffsetFromStart(ByVal dateTime As System.DateTime) As Integer
            Return(dateTime.Year - DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Year) * 12 + dateTime.Month - DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Month
        End Function

        Public Class Item

            Public Property Total As Decimal

            Public Property Year As Integer

            Public Property Month As Integer

            Public ReadOnly Property [Date] As DateTime
                Get
                    Return New System.DateTime(Me.Year, Me.Month, 1)
                End Get
            End Property
        End Class
    End Module

    Public Module ProductsAnalysis

#If DXCORE3
        public static IEnumerable<Item> GetFinancialReport(this IDevAVDbUnitOfWork UnitOfWork) {
            var orders = UnitOfWork.Orders;
            var orderItems =
                from oi in UnitOfWork.OrderItems
                join o in orders on oi.OrderId equals o.Id
                where (o.OrderDate >= AnalysisPeriod.Start && o.OrderDate <= AnalysisPeriod.End)
                select new {
                    Product = oi.Product,
                    Total = oi.Total,
                    FY = ((o.OrderDate.Year - AnalysisPeriod.Start.Year) * 12 + (o.OrderDate.Month - AnalysisPeriod.Start.Month)) / 12
                };
            return
                from oi in orderItems
                group oi by new { oi.Product.Id, oi.Product.Name, oi.FY } into g
                select new Item {
                    ProductName = g.Key.Name,
                    Year = AnalysisPeriod.Start.Year + g.Key.FY,
                    Month = AnalysisPeriod.Start.Month,
                    Total = g.Sum(o => o.Total)
                };
        }
        public static IEnumerable<Item> GetFinancialData(this IDevAVDbUnitOfWork UnitOfWork) {
            var orders = UnitOfWork.Orders;
            var orderItems =
                from oi in UnitOfWork.OrderItems
                join o in orders on oi.OrderId equals o.Id
                where (o.OrderDate >= AnalysisPeriod.Start && o.OrderDate <= AnalysisPeriod.End)
                select new {
                    Product = oi.Product,
                    Date = o.OrderDate,
                    Total = oi.Total
                };
            return
                from oi in orderItems
                group oi by new { oi.Product.Category, oi.Date.Year, oi.Date.Month } into g
                select new Item {
                    ProductCategory = g.Key.Category,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(o => o.Total)
                };
        }
#Else
        <Extension()>
        Public Function GetFinancialReport(ByVal UnitOfWork As DevExpress.DevAV.DevAVDbDataModel.IDevAVDbUnitOfWork) As IEnumerable(Of DevExpress.DevAV.ViewModels.ProductsAnalysis.Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems Join o In orders On oi.OrderId Equals o.Id Where(o.OrderDate >= DevExpress.DevAV.ViewModels.AnalysisPeriod.Start AndAlso o.OrderDate <= DevExpress.DevAV.ViewModels.AnalysisPeriod.[End]) Select New With {.Product = oi.Product, .Total = oi.Total, .FY =((o.OrderDate.Year - DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Year) * 12 + (o.OrderDate.Month - DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Month)) \ 12}
            Return From oi In orderItems Group oi By __groupByKey1__ = New With {oi.Product, oi.FY} Into g = Group Select New DevExpress.DevAV.ViewModels.ProductsAnalysis.Item With {.ProductName = __groupByKey1__.Product.Name, .Year = DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Year + __groupByKey1__.FY, .Month = DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Month, .Total = If(System.Linq.Enumerable.[Select](g, Function(o) CType(CType(o.Total, Decimal?), System.[Decimal]?)).Sum(), 0)}
        End Function

        <Extension()>
        Public Function GetFinancialData(ByVal UnitOfWork As DevExpress.DevAV.DevAVDbDataModel.IDevAVDbUnitOfWork) As IEnumerable(Of DevExpress.DevAV.ViewModels.ProductsAnalysis.Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems Join o In orders On oi.OrderId Equals o.Id Where(o.OrderDate >= DevExpress.DevAV.ViewModels.AnalysisPeriod.Start AndAlso o.OrderDate <= DevExpress.DevAV.ViewModels.AnalysisPeriod.[End]) Select New With {.Product = oi.Product, .[Date] = o.OrderDate, .Total = oi.Total}
            Return From oi In orderItems Group oi By __groupByKey2__ = New With {oi.Product.Category, oi.[Date].Year, oi.[Date].Month} Into g = Group Select New DevExpress.DevAV.ViewModels.ProductsAnalysis.Item With {.ProductCategory = __groupByKey2__.Category, .Year = __groupByKey2__.Year, .Month = __groupByKey2__.Month, .Total = If(System.Linq.Enumerable.[Select](g, Function(o) CType(CType(o.Total, Decimal?), System.[Decimal]?)).Sum(), 0)}
        End Function

#End If
        Public Class Item
            Inherits DevExpress.DevAV.ViewModels.AnalysisPeriod.Item

            Public Property ProductName As String

            Public Property ProductCategory As ProductCategory
        End Class
    End Module

    Public Module CustomersAnalysis

#If DXCORE3
        public static IEnumerable<Item> GetSalesReport(this IDevAVDbUnitOfWork UnitOfWork) {
            var orders = UnitOfWork.Orders;
            var orderItems =
                from oi in UnitOfWork.OrderItems
                join o in orders on oi.OrderId equals o.Id
                where (o.OrderDate >= AnalysisPeriod.Start && o.OrderDate <= AnalysisPeriod.End)
                select new {
                    Customer = o.Customer,
                    Total = oi.Total,
                    FY = ((o.OrderDate.Year - AnalysisPeriod.Start.Year) * 12 + (o.OrderDate.Month - AnalysisPeriod.Start.Month)) / 12
                };
            return
                from oi in orderItems
                group oi by new { oi.Customer.Id, oi.Customer.Name, oi.FY } into g
                select new Item {
                    CustomerName = g.Key.Name,
                    Year = AnalysisPeriod.Start.Year + g.Key.FY,
                    Month = AnalysisPeriod.Start.Month,
                    Total = g.Sum(o => o.Total)
                };
        }
        public static IEnumerable<Item> GetSalesData(this IDevAVDbUnitOfWork UnitOfWork) {
            var orders = UnitOfWork.Orders;
            var storeStates = UnitOfWork.CustomerStores.ToDictionary(t => t.Id, t => t.Address.State);
            var data =
                from oi in UnitOfWork.OrderItems
                join o in orders on oi.OrderId equals o.Id
                where (o.OrderDate >= AnalysisPeriod.Start && o.OrderDate <= AnalysisPeriod.End)
                group oi by new { StoreId = o.Store.Id, o.OrderDate.Year, o.OrderDate.Month } into g
                select new {
                    StoreId = g.Key.StoreId,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(o => o.Total)
                };
            return
                data.ToList().Select(t => new Item() {
                    State = storeStates[t.StoreId],
                    Year = t.Year,
                    Month = t.Month,
                    Total = t.Total
                });
        }
#Else
        <Extension()>
        Public Function GetSalesReport(ByVal UnitOfWork As DevExpress.DevAV.DevAVDbDataModel.IDevAVDbUnitOfWork) As IEnumerable(Of DevExpress.DevAV.ViewModels.CustomersAnalysis.Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems Join o In orders On oi.OrderId Equals o.Id Where(o.OrderDate >= DevExpress.DevAV.ViewModels.AnalysisPeriod.Start AndAlso o.OrderDate <= DevExpress.DevAV.ViewModels.AnalysisPeriod.[End]) Select New With {.Customer = o.Customer, .Total = oi.Total, .FY =((o.OrderDate.Year - DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Year) * 12 + (o.OrderDate.Month - DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Month)) \ 12}
            Return From oi In orderItems Group oi By __groupByKey3__ = New With {oi.Customer, oi.FY} Into g = Group Select New DevExpress.DevAV.ViewModels.CustomersAnalysis.Item With {.CustomerName = __groupByKey3__.Customer.Name, .Year = DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Year + __groupByKey3__.FY, .Month = DevExpress.DevAV.ViewModels.AnalysisPeriod.Start.Month, .Total = If(System.Linq.Enumerable.[Select](g, Function(o) CType(CType(o.Total, Decimal?), System.[Decimal]?)).Sum(), 0)}
        End Function

        <Extension()>
        Public Function GetSalesData(ByVal UnitOfWork As DevExpress.DevAV.DevAVDbDataModel.IDevAVDbUnitOfWork) As IEnumerable(Of DevExpress.DevAV.ViewModels.CustomersAnalysis.Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems Join o In orders On oi.OrderId Equals o.Id Where(o.OrderDate >= DevExpress.DevAV.ViewModels.AnalysisPeriod.Start AndAlso o.OrderDate <= DevExpress.DevAV.ViewModels.AnalysisPeriod.[End]) Select New With {.State = o.Store.Address.State, .[Date] = o.OrderDate, .Total = oi.Total}
            Return From oi In orderItems Group oi By __groupByKey4__ = New With {oi.State, oi.[Date].Year, oi.[Date].Month} Into g = Group Select New DevExpress.DevAV.ViewModels.CustomersAnalysis.Item With {.State = __groupByKey4__.State, .Year = __groupByKey4__.Year, .Month = __groupByKey4__.Month, .Total = If(System.Linq.Enumerable.[Select](g, Function(o) CType(CType(o.Total, Decimal?), System.[Decimal]?)).Sum(), 0)}
        End Function

#End If
        Public Class Item
            Inherits DevExpress.DevAV.ViewModels.AnalysisPeriod.Item

            Public Property CustomerName As String

            Public Property State As StateEnum
        End Class
    End Module
End Namespace
