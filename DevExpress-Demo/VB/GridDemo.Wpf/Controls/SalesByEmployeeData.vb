Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Dynamic
Imports System.Linq

Namespace GridDemo

    Public Class SalesByEmployeeData

        Private _Data As List(Of System.Dynamic.ExpandoObject), _Bands As GridDemo.BandDescription(), _Columns As GridDemo.ColumnDescription()

        Public Property Data As List(Of ExpandoObject)
            Get
                Return _Data
            End Get

            Private Set(ByVal value As List(Of ExpandoObject))
                _Data = value
            End Set
        End Property

        Public Property Bands As BandDescription()
            Get
                Return _Bands
            End Get

            Private Set(ByVal value As BandDescription())
                _Bands = value
            End Set
        End Property

        Public Property Columns As ColumnDescription()
            Get
                Return _Columns
            End Get

            Private Set(ByVal value As ColumnDescription())
                _Columns = value
            End Set
        End Property

        Public Sub New()
            GenerateBandsAndColumns()
            GenerateData()
        End Sub

        Private Sub GenerateBandsAndColumns()
            Dim bands As List(Of BandDescription) = New List(Of BandDescription)(12)
            bands.Add(New BandDescription("Employee Info", New ColumnDescription() {New ColumnDescription("Employee", Nothing, "EmployeeColumn"), New ColumnDescription("GroupName", Nothing, "GroupNameColumn")}, FixedStyle.Left, True))
            bands.Add(New BandDescription("Total Info", New ColumnDescription() {New ColumnDescription("Total", Nothing, "TotalColumn")}, FixedStyle.Right, True))
            bands.AddRange(Enumerable.Range(0, 10).Reverse().[Select](Function(yearIndex)
                Dim yearBandName =(Date.Now.Year - yearIndex).ToString()
                Dim quarters = Enumerable.Range(1, 4).[Select](Function(quarter) New ColumnDescription(yearBandName & "-Q" & quarter, "Q" & quarter)).Concat({New ColumnDescription(yearBandName & "-YearTotal", "Year Total")}).ToArray()
                Return New BandDescription(yearBandName, quarters)
            End Function))
            Me.Bands = bands.ToArray()
            Columns = Me.Bands.SelectMany(Function(x) x.Columns).ToArray()
        End Sub

        Private Sub GenerateData()
            Dim random As Random = New Random()
            Data = New List(Of ExpandoObject)()
            For Each employee In EmployeesWithPhotoData.DataSource
                Dim row As IDictionary(Of String, Object) = New ExpandoObject()
                row("Employee") = employee.FirstName & " " & employee.LastName
                row("GroupName") = employee.GroupName
                Dim total = 0R
                For Each band In Bands.Where(Function(x) x.Fixed = FixedStyle.None)
                    Dim yearTotal = 0R
                    For Each column In band.Columns.Take(band.Columns.Length - 1)
                        Dim value = random.Next(100000)
                        yearTotal += value
                        total += value
                        row(column.PropertyName) = value
                    Next

                    row(Enumerable.Last(band.Columns).PropertyName) = yearTotal
                Next

                row("Total") = total
                Data.Add(CType(row, ExpandoObject))
            Next
        End Sub
    End Class
End Namespace
