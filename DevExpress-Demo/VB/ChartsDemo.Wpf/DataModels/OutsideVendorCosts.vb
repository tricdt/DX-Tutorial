Imports System
Imports System.Data

Namespace ChartsDemo

    Public Class OutsideVendorCosts

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Public Function GetData() As DataTable
            Dim lastYear As Integer = Date.Now.Year - 1
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("Year", GetType(Date)), New DataColumn("Company", GetType(String)), New DataColumn("Costs", GetType(Decimal))})
            table.Rows.Add(New DateTime(lastYear - 6, 12, 31), "DevAV North", 362.5D)
            table.Rows.Add(New DateTime(lastYear - 5, 12, 31), "DevAV North", 348.4D)
            table.Rows.Add(New DateTime(lastYear - 4, 12, 31), "DevAV North", 279.0D)
            table.Rows.Add(New DateTime(lastYear - 3, 12, 31), "DevAV North", 230.9D)
            table.Rows.Add(New DateTime(lastYear - 2, 12, 31), "DevAV North", 203.5D)
            table.Rows.Add(New DateTime(lastYear - 1, 12, 31), "DevAV North", 197.1D)
            table.Rows.Add(New DateTime(lastYear - 6, 12, 31), "DevAV South", 277.0D)
            table.Rows.Add(New DateTime(lastYear - 5, 12, 31), "DevAV South", 328.5D)
            table.Rows.Add(New DateTime(lastYear - 4, 12, 31), "DevAV South", 297.0D)
            table.Rows.Add(New DateTime(lastYear - 3, 12, 31), "DevAV South", 255.3D)
            table.Rows.Add(New DateTime(lastYear - 2, 12, 31), "DevAV South", 173.5D)
            table.Rows.Add(New DateTime(lastYear - 1, 12, 31), "DevAV South", 131.8D)
            Return table
        End Function
    End Class
End Namespace
