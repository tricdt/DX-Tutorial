Imports System
Imports System.Data

Namespace ChartsDemo

    Public Class FuelPrices

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Public Function GetData() As DataTable
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("Date", GetType(Date)), New DataColumn("Price", GetType(Decimal))})
            table.Rows.Add(New DateTime(2016, 1, 1, 0, 0, 0), 2.143D)
            table.Rows.Add(New DateTime(2016, 2, 1, 0, 0, 0), 1.998D)
            table.Rows.Add(New DateTime(2016, 3, 1, 0, 0, 0), 2.090D)
            table.Rows.Add(New DateTime(2016, 4, 1, 0, 0, 0), 2.152D)
            table.Rows.Add(New DateTime(2016, 5, 1, 0, 0, 0), 2.315D)
            table.Rows.Add(New DateTime(2016, 6, 1, 0, 0, 0), 2.423D)
            table.Rows.Add(New DateTime(2016, 7, 1, 0, 0, 0), 2.405D)
            table.Rows.Add(New DateTime(2016, 8, 1, 0, 0, 0), 2.351D)
            table.Rows.Add(New DateTime(2016, 9, 1, 0, 0, 0), 2.394D)
            table.Rows.Add(New DateTime(2016, 10, 1, 0, 0, 0), 2.454D)
            table.Rows.Add(New DateTime(2016, 11, 1, 0, 0, 0), 2.439D)
            table.Rows.Add(New DateTime(2016, 12, 1, 0, 0, 0), 2.510D)
            Return table
        End Function
    End Class
End Namespace
