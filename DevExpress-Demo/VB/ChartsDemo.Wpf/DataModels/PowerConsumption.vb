Imports System
Imports System.Data

Namespace ChartsDemo

    Public Module PowerConsumption

        Public ReadOnly Property Data As DataTable
            Get
                Dim table As DataTable = New DataTable()
                table.Columns.AddRange(New DataColumn() {New DataColumn("Branch", GetType(String)), New DataColumn("Time", GetType(Date)), New DataColumn("Power", GetType(Double))})
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 7, 0, 0), 429)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 8, 0, 0), 432)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 9, 0, 0), 301)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 10, 0, 0), 307)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 11, 0, 0), 310)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 12, 0, 0), 380)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 13, 0, 0), 384)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 14, 0, 0), 398)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 15, 0, 0), 379)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 16, 0, 0), 220)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 17, 0, 0), 321)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 18, 0, 0), 341)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 19, 0, 0), 368)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 20, 0, 0), 557)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 21, 0, 0), 523)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 22, 0, 0), 501)
                table.Rows.Add("""Center"" Branch", New DateTime(1, 1, 1, 23, 0, 0), 443)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 7, 0, 0), 260)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 8, 0, 0), 287)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 9, 0, 0), 285)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 10, 0, 0), 281)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 11, 0, 0), 294)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 12, 0, 0), 303)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 13, 0, 0), 325)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 14, 0, 0), 336)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 15, 0, 0), 325)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 16, 0, 0), 186)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 17, 0, 0), 420)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 18, 0, 0), 455)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 19, 0, 0), 481)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 20, 0, 0), 487)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 21, 0, 0), 490)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 22, 0, 0), 467)
                table.Rows.Add("""East"" Branch", New DateTime(1, 1, 1, 23, 0, 0), 409)
                Return table
            End Get
        End Property
    End Module
End Namespace
