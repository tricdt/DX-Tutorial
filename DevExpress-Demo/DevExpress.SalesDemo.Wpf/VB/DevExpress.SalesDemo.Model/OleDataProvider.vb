Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb

Namespace DevExpress.SalesDemo.Model

    Public Class OleDataProvider
        Implements IDataProvider, IDisposable

        Protected Enum GroupColumn
            None
            Channel
            Product
            Region
            Sector
        End Enum

        Public Shared connection As DbConnection

        Public Sub New()
        End Sub

        Public Sub New(ByVal dbPath As String)
            connection = New OleDbConnection(GetConnectionString(dbPath))
            Call connection.Open()
        End Sub

        Public Shared Function GetConnectionString(ByVal dbPath As String) As String
            Return String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", dbPath)
        End Function

        Protected Function BuildQueryBySales(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As String
            Dim groupBy As String = GetGroupBy(period, "Sale_Date")
            Dim groupByExpression As String = ""
            If Not String.IsNullOrEmpty(groupBy) Then groupByExpression = String.Format(", {0} AS SaleDate", groupBy)
            Dim query As String = String.Format("SELECT
                                                'TOTAL' AS Name,
	                                            SUM(Total_Cost) AS TotalCost, 
	                                            SUM(Units) AS Units
                                                {0}
                                           FROM 
                                                Sales
                                           WHERE 
                                                Sale_Date BETWEEN @PeriodStart and @PeriodEnd
                                           ", groupByExpression)
            If Not String.IsNullOrEmpty(groupBy) Then query += " GROUP BY " & groupBy
            Return query
        End Function

        Protected Overridable Function CreateQueryBySales(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As DbCommand
            Dim query As String = BuildQueryBySales(start, [end], period)
            Dim command As OleDbCommand = New OleDbCommand(query)
            command.Parameters.Add(New OleDbParameter("@PeriodStart", OleDbType.Date)).Value = start
            command.Parameters.Add(New OleDbParameter("@PeriodEnd", OleDbType.Date)).Value = [end]
            command.Parameters.Add(New OleDbParameter("@DiffBase", OleDbType.BSTR)).Value = "0"
            Return command
        End Function

        Protected Overridable Function [Cstr](ByVal value As String) As String
            Return String.Format("Cstr({0})", value)
        End Function

        Protected Function BuildJoinQuery(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod, ByVal column As GroupColumn) As String
            Dim groupBy As String = GetGroupBy(period, "Sales.Sale_Date")
            Dim groupByField As String = String.Empty
            Dim groupByCondition As String = String.Empty
            If Not String.IsNullOrEmpty(groupBy) Then
                groupByField = ", " & groupBy & " AS SaleDate"
                groupByCondition = ", " & groupBy
            End If

            Dim keyColumn As String = String.Empty
            Dim tableName As String = String.Empty
            Dim columnName As String = String.Empty
            If column <> GroupColumn.None Then
                Dim name As String = [Enum].GetName(GetType(GroupColumn), column)
                keyColumn = name & "Id"
                tableName = name & "s"
                columnName = name & "_Name"
            End If

            Dim query As String = String.Format("SELECT
	                                        {1} as Name,
	                                        SUM(Sales.Total_Cost) as TotalCost, 
	                                        SUM(Sales.Units) as Units
                                            {2}
                                        FROM 
                                            Sales
                                        INNER JOIN 
                                            {3} as LinkedTable on Sales.{0} = LinkedTable.Id
                                        WHERE 
                                            Sales.Sale_Date BETWEEN @PeriodStart and @PeriodEnd  
                                        GROUP BY
                                            {1} {4}", keyColumn, [Cstr]("LinkedTable." & columnName), groupByField, tableName, groupByCondition)
            Return query
        End Function

        Protected Overridable Function CreateJoinQuery(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod, ByVal column As GroupColumn) As DbCommand
            Dim query As String = BuildJoinQuery(start, [end], period, column)
            Dim command As OleDbCommand = New OleDbCommand(query)
            command.Parameters.Add(New OleDbParameter("@PeriodStart", OleDbType.Date)).Value = start
            command.Parameters.Add(New OleDbParameter("@PeriodEnd", OleDbType.Date)).Value = [end]
            Return command
        End Function

        Private Shared Function GetGroupBy(ByVal period As GroupingPeriod, ByVal column As String) As String
            Select Case period
                Case GroupingPeriod.Hour
                    Return String.Format("DATEADD(""h"", DATEDIFF(""h"", 0," & column & "), 0)")
                Case GroupingPeriod.Day
                    Return String.Format("DATEADD(""d"", DATEDIFF(""d"", 0," & column & "), 0)")
            End Select

            Return String.Empty
        End Function

        Private Shared MinDate As Date = New DateTime(1900, 1, 1, 0, 0, 0)

        Private Shared MaxDate As Date = New DateTime(2100, 12, 31, 11, 59, 59)

        Private Function ExecuteQuery(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod, ByVal column As GroupColumn) As IEnumerable(Of SalesGroup)
            If start < MinDate Then start = MinDate
            If [end] > MaxDate Then [end] = MaxDate
            Dim result As IList(Of SalesGroup) = Nothing
            If connection.State <> ConnectionState.Open AndAlso connection.State <> ConnectionState.Connecting Then Call connection.Open()
            Dim command As DbCommand
            If column = GroupColumn.None Then
                command = CreateQueryBySales(start, [end], period)
            Else
                command = CreateJoinQuery(start, [end], period, column)
            End If

            command.Connection = connection
            command.Prepare()
            result = New List(Of SalesGroup)()
            Using reader = command.ExecuteReader()
                While reader.Read()
                    Dim name As String = reader("Name").ToString()
                    Dim totalCost As Decimal = 0
                    Dim units As Integer = 0
                    Dim startDate As Date = start
                    Dim endDate As Date = [end]
                    If Not(TypeOf reader("TotalCost") Is DBNull) Then
                        totalCost = Convert.ToDecimal(reader("TotalCost"))
                        units = Convert.ToInt32(reader("Units"))
                        If reader.FieldCount > 3 Then
                            startDate = Convert.ToDateTime(reader("SaleDate"))
                            endDate = GetEndDate(startDate, [end], period)
                        End If
                    End If

                    result.Add(New SalesGroup(name, totalCost, units, startDate, endDate))
                End While
            End Using

            Return result
        End Function

        Private Function GetEndDate(ByVal startDate As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As Date
            If period = GroupingPeriod.Day Then
                Return startDate.AddDays(1).AddMilliseconds(-1)
            ElseIf period = GroupingPeriod.Hour Then
                Return startDate.AddHours(1).AddMilliseconds(-1)
            End If

            Return [end]
        End Function

#Region "IDataProvider"
        Public Function GetTotalSalesByRange(ByVal start As Date, ByVal [end] As Date) As SalesGroup Implements IDataProvider.GetTotalSalesByRange
            Dim salesGroups = ExecuteQuery(start, [end], GroupingPeriod.All, GroupColumn.None)
            If salesGroups IsNot Nothing Then
                For Each salesGroup In salesGroups
                    Return salesGroup
                Next
            End If

            Return Nothing
        End Function

        Public Function GetSales(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSales
            Return ExecuteQuery(start, [end], period, GroupColumn.None)
        End Function

        Public Function GetSalesByChannel(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesByChannel
            Return ExecuteQuery(start, [end], period, GroupColumn.Channel)
        End Function

        Public Function GetSalesByProduct(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesByProduct
            Return ExecuteQuery(start, [end], period, GroupColumn.Product)
        End Function

        Public Function GetSalesByRegion(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesByRegion
            Return ExecuteQuery(start, [end], period, GroupColumn.Region)
        End Function

        Public Function GetSalesBySector(ByVal start As Date, ByVal [end] As Date, ByVal period As GroupingPeriod) As IEnumerable(Of SalesGroup) Implements IDataProvider.GetSalesBySector
            Return ExecuteQuery(start, [end], period, GroupColumn.Sector)
        End Function

#End Region
#Region "IDisposable"
        Public Sub Dispose() Implements IDisposable.Dispose
            If connection.State = ConnectionState.Open Then Call connection.Close()
            Call connection.Dispose()
        End Sub
#End Region
    End Class
End Namespace
