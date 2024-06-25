Imports System
Imports System.Data

Namespace ChartsDemo

    Public Class DevAVSalesByLast10Years

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Public Function GetData() As DataTable
            Dim lastYear As Integer = Date.Now.Year - 1
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("Year", GetType(Date)), New DataColumn("Region", GetType(String)), New DataColumn("Sales", GetType(Decimal))})
            table.Rows.Add(New DateTime(lastYear - 10, 12, 31), "North America", 3.010D)
            table.Rows.Add(New DateTime(lastYear - 10, 12, 31), "Europe", 3.032D)
            table.Rows.Add(New DateTime(lastYear - 10, 12, 31), "Australia", 1.31D)
            table.Rows.Add(New DateTime(lastYear - 9, 12, 31), "North America", 3.212D)
            table.Rows.Add(New DateTime(lastYear - 9, 12, 31), "Europe", 3.050D)
            table.Rows.Add(New DateTime(lastYear - 9, 12, 31), "Australia", 1.64D)
            table.Rows.Add(New DateTime(lastYear - 8, 12, 31), "North America", 3.223D)
            table.Rows.Add(New DateTime(lastYear - 8, 12, 31), "Europe", 3.054D)
            table.Rows.Add(New DateTime(lastYear - 8, 12, 31), "Australia", 1.70D)
            table.Rows.Add(New DateTime(lastYear - 7, 12, 31), "North America", 3.001D)
            table.Rows.Add(New DateTime(lastYear - 7, 12, 31), "Europe", 2.775D)
            table.Rows.Add(New DateTime(lastYear - 7, 12, 31), "Australia", 1.083D)
            table.Rows.Add(New DateTime(lastYear - 6, 12, 31), "North America", 2.612D)
            table.Rows.Add(New DateTime(lastYear - 6, 12, 31), "Europe", 2.066D)
            table.Rows.Add(New DateTime(lastYear - 6, 12, 31), "Australia", 0.88D)
            table.Rows.Add(New DateTime(lastYear - 5, 12, 31), "North America", 2.666D)
            table.Rows.Add(New DateTime(lastYear - 5, 12, 31), "Europe", 2.078D)
            table.Rows.Add(New DateTime(lastYear - 5, 12, 31), "Australia", 1.09D)
            table.Rows.Add(New DateTime(lastYear - 4, 12, 31), "North America", 3.665D)
            table.Rows.Add(New DateTime(lastYear - 4, 12, 31), "Europe", 3.888D)
            table.Rows.Add(New DateTime(lastYear - 4, 12, 31), "Australia", 2.01D)
            table.Rows.Add(New DateTime(lastYear - 3, 12, 31), "North America", 3.555D)
            table.Rows.Add(New DateTime(lastYear - 3, 12, 31), "Europe", 3.008D)
            table.Rows.Add(New DateTime(lastYear - 3, 12, 31), "Australia", 1.85D)
            table.Rows.Add(New DateTime(lastYear - 2, 12, 31), "North America", 3.485D)
            table.Rows.Add(New DateTime(lastYear - 2, 12, 31), "Europe", 3.088D)
            table.Rows.Add(New DateTime(lastYear - 2, 12, 31), "Australia", 1.78D)
            table.Rows.Add(New DateTime(lastYear - 1, 12, 31), "North America", 3.747D)
            table.Rows.Add(New DateTime(lastYear - 1, 12, 31), "Europe", 3.357D)
            table.Rows.Add(New DateTime(lastYear - 1, 12, 31), "Australia", 1.957D)
            table.Rows.Add(New DateTime(lastYear, 12, 31), "North America", 4.182D)
            table.Rows.Add(New DateTime(lastYear, 12, 31), "Europe", 3.725D)
            table.Rows.Add(New DateTime(lastYear, 12, 31), "Australia", 2.272D)
            Return table
        End Function
    End Class
End Namespace
