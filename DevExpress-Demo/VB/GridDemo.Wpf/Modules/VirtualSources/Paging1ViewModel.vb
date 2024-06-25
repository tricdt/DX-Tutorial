Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data
Imports System.Threading.Tasks

Namespace GridDemo.VirtualSources

    Public Class Paging1ViewModel
        Inherits ViewModelBase

        <Command>
        Public Sub FetchIssues(ByVal args As FetchPageAsyncArgs)
            args.Result = FetchPageAsync(args)
        End Sub

        Private Shared Async Function FetchPageAsync(ByVal args As FetchPageAsyncArgs) As Task(Of FetchRowsResult)
            Dim page As Integer = args.Skip \ args.Take
            Dim sortOrder As IssueSortOrder = IssueSortOrder.Default
            Dim filter As IssueFilter = Nothing
            Dim issues = Await GetIssuesPageAsync(page:=page, pageSize:=args.Take, sortOrder:=sortOrder, filter:=filter)
            Return New FetchRowsResult(rows:=issues, hasMoreRows:=issues.Length = args.Take)
        End Function
    End Class
End Namespace
