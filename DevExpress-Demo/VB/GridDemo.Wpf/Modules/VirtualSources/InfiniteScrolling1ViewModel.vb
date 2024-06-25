Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data
Imports System.Threading.Tasks

Namespace GridDemo.VirtualSources

    Public Class InfiniteScrolling1ViewModel
        Inherits ViewModelBase

        <Command>
        Public Sub FetchIssues(ByVal args As FetchRowsAsyncArgs)
            args.Result = FetchRowsAsync(args)
        End Sub

        Private Shared Async Function FetchRowsAsync(ByVal args As FetchRowsAsyncArgs) As Task(Of FetchRowsResult)
            Dim take As Integer = If(args.Take, 50)
            Dim sortOrder As IssueSortOrder = IssueSortOrder.Default
            Dim filter As IssueFilter = Nothing
            Dim issues = Await GetIssuesAsync(skip:=args.Skip, take:=take, sortOrder:=sortOrder, filter:=filter)
            Return New FetchRowsResult(rows:=issues, hasMoreRows:=issues.Length = take)
        End Function
    End Class
End Namespace
