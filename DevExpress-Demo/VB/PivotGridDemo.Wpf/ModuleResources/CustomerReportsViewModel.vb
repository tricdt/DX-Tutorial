Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native

Namespace PivotGridDemo

    <POCOViewModel>
    Public Class CustomerReportsViewModel

        Const allString As String = "(All)"

        Public Sub New()
            AllYears = CustomerReports.[Select](Function(x) x.OrderDate.Value.Year).Distinct().OrderBy(Function(x) x).Cast(Of Object)()
            AllQuarters = New Object() {allString}.Concat(CustomerReports.[Select](Function(x)(x.OrderDate.Value.Month - 1) \ 3 + 1).Distinct().OrderBy(Function(x) x).Cast(Of Object)())
            SelectedQuarter = AllQuarters.FirstOrDefault()
            SelectedYear = AllYears.FirstOrDefault()
        End Sub

        Public Property AllQuarters As IEnumerable(Of Object)

        Public Property AllYears As IEnumerable(Of Object)

        Public ReadOnly Property CustomerReports As IList(Of Models.CustomerReport)
            Get
                Return NWindDataProvider.CustomerReports
            End Get
        End Property

        Public Overridable Property QuartersFilter As Object

        <BindableProperty(OnPropertyChangedMethodName:="OnSelectedQuarterChanged")>
        Public Overridable Property SelectedQuarter As Object

        <BindableProperty(OnPropertyChangedMethodName:="OnSelectedYearChanged")>
        Public Overridable Property SelectedYear As Object

        Public Overridable Property YearsFilter As Object

        Protected Sub OnSelectedQuarterChanged()
            QuartersFilter = If(Equals(SelectedQuarter, allString), AllQuarters.Skip(1), {SelectedQuarter})
        End Sub

        Protected Sub OnSelectedYearChanged()
            YearsFilter = {SelectedYear}
        End Sub
    End Class
End Namespace
