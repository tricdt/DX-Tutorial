Imports DevExpress.Xpf.DemoBase
Imports GridDemo.VirtualSources

Namespace GridDemo

    Public Class PagingModule
        Inherits GridShowcaseBrowserModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Return New ShowcaseInfo() {LoadShowcase("Step 1: Fetch Data and Enable Paging", InfiniteSourceModule.Step1Uri, InfiniteSourceModule.Path, {GetType(Paging1ViewModel), GetType(Paging1View), GetType(IssueData), GetType(IssuesService)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 2: Enable Sorting", InfiniteSourceModule.Step2Uri, InfiniteSourceModule.Path, {GetType(Paging2ViewModel), GetType(Paging2View), GetType(IssueSortOrder)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 3: Enable Filtering", InfiniteSourceModule.Step3Uri, InfiniteSourceModule.Path, {GetType(Paging3ViewModel), GetType(Paging3View), GetType(IssueFilter), GetType(IssueFilterConverter)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 4: Display Summaries", InfiniteSourceModule.Step4Uri, InfiniteSourceModule.Path, {GetType(Paging4ViewModel), GetType(Paging4View), GetType(IssuesSummaries)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}
        End Function
    End Class
End Namespace
