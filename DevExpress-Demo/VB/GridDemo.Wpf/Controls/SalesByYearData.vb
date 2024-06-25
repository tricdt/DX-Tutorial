Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Dynamic

Namespace GridDemo

    Public Module SalesByYearData

        Private _Columns As List(Of GridDemo.ColumnDescription)

        Public ReadOnly Property Data As List(Of ExpandoObject)
            Get
                Dim lData = New List(Of ExpandoObject)()
                Dim random As Random = New Random()
                For yearIndex As Integer = 10 To 0 + 1 Step -1
                    Dim year As Integer = Date.Now.Year - yearIndex
                    For month As Integer = 1 To 12
                        Dim row As IDictionary(Of String, Object) = New ExpandoObject()
                        row("Date") = New DateTime(year, month, 1)
                        For columnIndex As Integer = 0 To Columns.Count - 1
                            row(Columns(columnIndex).PropertyName) = random.Next(30000)
                        Next

                        lData.Add(CType(row, ExpandoObject))
                    Next
                Next

                Return lData
            End Get
        End Property

        Public Property Columns As List(Of ColumnDescription)
            Get
                Return _Columns
            End Get

            Private Set(ByVal value As List(Of ColumnDescription))
                _Columns = value
            End Set
        End Property

        Sub New()
            Dim columns = New List(Of ColumnDescription)()
            For Each employee In NWindContext.Create().Employees
                Dim name As String = employee.FirstName & " " & employee.LastName
                columns.Add(New ColumnDescription(name))
            Next

            SalesByYearData.Columns = columns
        End Sub
    End Module
End Namespace
