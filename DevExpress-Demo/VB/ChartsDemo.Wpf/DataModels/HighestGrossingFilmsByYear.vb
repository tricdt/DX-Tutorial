Imports System.Data

Namespace ChartsDemo

    Public Class HighestGrossingFilmsByYear

        Public ReadOnly Property Data As DataTable
            Get
                Return GetData()
            End Get
        End Property

        Public Function GetData() As DataTable
            Dim table As DataTable = New DataTable()
            table.Columns.AddRange(New DataColumn() {New DataColumn("Year", GetType(Integer)), New DataColumn("Budget", GetType(Decimal)), New DataColumn("Grosses", GetType(Decimal)), New DataColumn("Title", GetType(String))})
            table.Rows.Add(2007, 300, 0.963, "Pirates of the Caribbean: At World's End")
            table.Rows.Add(2008, 185, 1.004, "The Dark Knight")
            table.Rows.Add(2009, 237, 2.788, "Avatar")
            table.Rows.Add(2010, 200, 1.067, "Toy Story 3")
            table.Rows.Add(2011, 250, 1.341, "Harry Potter and the Deathly Hallows Part 2")
            table.Rows.Add(2012, 220, 1.519, "Marvel's The Avengers")
            table.Rows.Add(2013, 150, 1.276, "Frozen")
            table.Rows.Add(2014, 210, 1.104, "Transformers: Age of Extinction")
            table.Rows.Add(2015, 245, 2.068, "Star Wars: The Force Awakens")
            table.Rows.Add(2016, 250, 1.153, "Captain America: Civil War")
            Return table
        End Function
    End Class
End Namespace
