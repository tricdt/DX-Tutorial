Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks

Namespace GridDemo.VirtualSources

    Public Class Paging3ViewModel
        Inherits ViewModelBase

        <Command>
        Public Sub FetchIssues(ByVal args As FetchPageAsyncArgs)
            args.Result = FetchPageAsync(args)
        End Sub

        Private Shared Async Function FetchPageAsync(ByVal args As FetchPageAsyncArgs) As Task(Of FetchRowsResult)
#Region "Sorting"
            Dim sortOrder As IssueSortOrder = IssueSortOrder.Default
            If args.SortOrder.Length > 0 Then
                Dim sort = args.SortOrder.[Single]()
                If Equals(sort.PropertyName, "Created") Then
                    If sort.Direction <> ListSortDirection.Descending Then Throw New InvalidOperationException()
                    sortOrder = IssueSortOrder.CreatedDescending
                End If

                If Equals(sort.PropertyName, "Votes") Then
                    sortOrder = If(sort.Direction = ListSortDirection.Ascending, IssueSortOrder.VotesAscending, IssueSortOrder.VotesDescending)
                End If
            End If

#End Region
#Region "!"
            Dim filter As IssueFilter = CType(args.Filter, IssueFilter)
#End Region
#Region "Service request"
            Dim page As Integer = args.Skip \ args.Take
            Dim issues = Await GetIssuesPageAsync(page:=page, pageSize:=args.Take, sortOrder:=sortOrder, filter:=filter)
            Return New FetchRowsResult(rows:=issues, hasMoreRows:=issues.Length = args.Take)
#End Region
        End Function

#Region "!"
        <Command>
        Public Sub GetUniqueValues(ByVal args As GetUniqueValuesAsyncArgs)
            If Equals(args.PropertyName, "Priority") Then
                Dim values = [Enum].GetValues(GetType(Priority)).Cast(Of Object)().ToArray()
                args.Result = Threading.Tasks.Task.Factory.StartNew(Function() values)
            Else
                Throw New InvalidOperationException()
            End If
        End Sub
#End Region
    End Class
End Namespace
