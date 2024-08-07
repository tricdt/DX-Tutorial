Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks

Namespace GridDemo.VirtualSources

    Public Class InfiniteScrolling2ViewModel
        Inherits ViewModelBase

        <Command>
        Public Sub FetchIssues(ByVal args As FetchRowsAsyncArgs)
            args.Result = FetchRowsAsync(args)
        End Sub

        Private Shared Async Function FetchRowsAsync(ByVal args As FetchRowsAsyncArgs) As Task(Of FetchRowsResult)
#Region "!"
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
#Region "Service request"
            Dim take As Integer = If(args.Take, 50)
            Dim filter As IssueFilter = Nothing
            Dim issues = Await GetIssuesAsync(skip:=args.Skip, take:=take, sortOrder:=sortOrder, filter:=filter)
            Return New FetchRowsResult(rows:=issues, hasMoreRows:=issues.Length = take)
#End Region
        End Function
    End Class
End Namespace
