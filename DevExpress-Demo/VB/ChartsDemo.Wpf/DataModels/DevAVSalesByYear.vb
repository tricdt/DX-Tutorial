Imports System.Data
Imports System.Linq

Namespace ChartsDemo

    Public Class DevAVSalesByYear

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Public ReadOnly Property Series1Source As DataTable
            Get
                Return GetData().AsEnumerable().Where(Function(r) r.Field(Of Integer)("Year") = Date.Now.Year - 1).CopyToDataTable()
            End Get
        End Property

        Public ReadOnly Property Series2Source As DataTable
            Get
                Return GetData().AsEnumerable().Where(Function(r) r.Field(Of Integer)("Year") = Date.Now.Year - 2).CopyToDataTable()
            End Get
        End Property

        Public ReadOnly Property Series3Source As DataTable
            Get
                Return GetData().AsEnumerable().Where(Function(r) r.Field(Of Integer)("Year") = Date.Now.Year - 3).CopyToDataTable()
            End Get
        End Property

        Public ReadOnly Property Series1DisplayName As String
            Get
                Return(Date.Now.Year - 1).ToString()
            End Get
        End Property

        Public ReadOnly Property Series2DisplayName As String
            Get
                Return(Date.Now.Year - 2).ToString()
            End Get
        End Property

        Public ReadOnly Property Series3DisplayName As String
            Get
                Return(Date.Now.Year - 3).ToString()
            End Get
        End Property

        Public Function GetData() As DataTable
            Dim lastYear As Integer = Date.Now.Year - 1
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("Year", GetType(Integer)), New DataColumn("Region", GetType(String)), New DataColumn("Sales", GetType(Decimal))})
            table.Rows.Add(lastYear - 2, "Asia", 4.23D)
            table.Rows.Add(lastYear - 2, "North America", 3.485D)
            table.Rows.Add(lastYear - 2, "Europe", 3.088D)
            table.Rows.Add(lastYear - 2, "Australia", 1.78D)
            table.Rows.Add(lastYear - 2, "South America", 1.602D)
            table.Rows.Add(lastYear - 1, "Asia", 4.768D)
            table.Rows.Add(lastYear - 1, "North America", 3.747D)
            table.Rows.Add(lastYear - 1, "Europe", 3.357D)
            table.Rows.Add(lastYear - 1, "Australia", 1.957D)
            table.Rows.Add(lastYear - 1, "South America", 1.823D)
            table.Rows.Add(lastYear, "Asia", 5.289D)
            table.Rows.Add(lastYear, "North America", 4.182D)
            table.Rows.Add(lastYear, "Europe", 3.725D)
            table.Rows.Add(lastYear, "Australia", 2.272D)
            table.Rows.Add(lastYear, "South America", 2.117D)
            Return table
        End Function
    End Class
End Namespace
