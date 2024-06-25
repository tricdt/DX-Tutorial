Imports System
Imports System.Data

Namespace ChartsDemo

    Public Class PopulationGenerator

        Const PointsCount As Integer = 150

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Private Function GetData() As DataTable
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("Population", GetType(String)), New DataColumn("Argument", GetType(Integer)), New DataColumn("Value", GetType(Integer))})
            Dim random As Random = New Random(0)
            Generate(table, "Population 1", random, 140, 1280, 100, 1240, PointsCount)
            Generate(table, "Population 2", random, 500, 1600, 1000, 2100, PointsCount)
            Generate(table, "Population 3", random, 450, 950, 1550, 2050, PointsCount)
            Generate(table, "Population 4", random, 800, 1700, 300, 1200, PointsCount)
            Return table
        End Function

        Private Sub Generate(ByVal table As DataTable, ByVal name As String, ByVal random As Random, ByVal xPlus As Integer, ByVal xMinus As Integer, ByVal yPlus As Integer, ByVal yMinus As Integer, ByVal count As Integer)
            For Each point As NumericPoint In PointGenerator.GenerateCluster(random, xPlus, xMinus, yPlus, yMinus, count)
                table.Rows.Add(name, point.Argument, point.Value)
            Next
        End Sub
    End Class
End Namespace
