Imports System.Data

Namespace ChartsDemo

    Public Class DevAVSalesMixByRegion

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Public Function GetData() As DataTable
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("ProductCategory", GetType(String)), New DataColumn("Region", GetType(String)), New DataColumn("Sales", GetType(Decimal))})
            table.Rows.Add("Video players", "Asia", 853D)
            table.Rows.Add("Video players", "Australia", 321D)
            table.Rows.Add("Video players", "Europe", 655D)
            table.Rows.Add("Video players", "North America", 1325D)
            table.Rows.Add("Video players", "South America", 653D)
            table.Rows.Add("Automation", "Asia", 172D)
            table.Rows.Add("Automation", "Australia", 255D)
            table.Rows.Add("Automation", "Europe", 981D)
            table.Rows.Add("Automation", "North America", 963D)
            table.Rows.Add("Automation", "South America", 123D)
            table.Rows.Add("Monitors", "Asia", 1011D)
            table.Rows.Add("Monitors", "Australia", 359D)
            table.Rows.Add("Monitors", "Europe", 721D)
            table.Rows.Add("Monitors", "North America", 565D)
            table.Rows.Add("Monitors", "South America", 532D)
            table.Rows.Add("Projectors", "Asia", 998D)
            table.Rows.Add("Projectors", "Australia", 222D)
            table.Rows.Add("Projectors", "Europe", 865D)
            table.Rows.Add("Projectors", "North America", 787D)
            table.Rows.Add("Projectors", "South America", 332D)
            table.Rows.Add("Televisions", "Asia", 1356D)
            table.Rows.Add("Televisions", "Australia", 232D)
            table.Rows.Add("Televisions", "Europe", 1323D)
            table.Rows.Add("Televisions", "North America", 1125D)
            table.Rows.Add("Televisions", "South America", 865D)
            Return table
        End Function
    End Class
End Namespace
