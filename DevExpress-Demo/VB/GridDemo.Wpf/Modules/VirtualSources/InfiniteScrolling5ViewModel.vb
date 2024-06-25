Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks

Namespace GridDemo.VirtualSources

    Public Class InfiniteScrolling5ViewModel
        Inherits ViewModelBase

#Region "fetch rows"
        <Command>
        Public Sub FetchIssues(ByVal args As FetchRowsAsyncArgs)
            args.Result = FetchRowsAsync(args)
        End Sub

        Private Shared Async Function FetchRowsAsync(ByVal args As FetchRowsAsyncArgs) As Task(Of FetchRowsResult)
#Region "sorting"
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
#Region "filtering"
            Dim filter As IssueFilter = CType(args.Filter, IssueFilter)
#End Region
#Region "service request"
            Dim take As Integer = If(args.Take, 50)
            Dim issues = Await GetIssuesAsync(skip:=args.Skip, take:=take, sortOrder:=sortOrder, filter:=filter)
            Return New FetchRowsResult(rows:=issues, hasMoreRows:=issues.Length = take)
#End Region
        End Function

#End Region
#Region "unique filter values"
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
#Region "total summary"
        <Command>
        Public Sub GetTotalSummaries(ByVal args As GetSummariesAsyncArgs)
            args.Result = GetTotalSummariesAsync(args)
        End Sub

        Private Shared Async Function GetTotalSummariesAsync(ByVal args As GetSummariesAsyncArgs) As Task(Of Object())
            Dim filter As IssueFilter = CType(args.Filter, IssueFilter)
            Dim summaryValues = Await GetSummariesAsync(filter)
            Return args.Summaries.[Select](Function(x)
                If x.SummaryType = SummaryType.Count Then Return CObj(summaryValues.Count)
                If x.SummaryType = SummaryType.Max AndAlso Equals(x.PropertyName, "Created") Then Return summaryValues.LastCreated
                Throw New InvalidOperationException()
            End Function).ToArray()
        End Function

#End Region
#Region "!"
        <Command>
        Public Sub UpdateIssue(ByVal args As RowValidationArgs)
            args.ResultAsync = UpdateIssueAsync(TryCast(args.Item, IssueData))
        End Sub

        Private Shared Async Function UpdateIssueAsync(ByVal issue As IssueData) As Task(Of ValidationErrorInfo)
            Await UpdateRowAsync(issue)
            Return Nothing
        End Function
#End Region
    End Class
End Namespace
