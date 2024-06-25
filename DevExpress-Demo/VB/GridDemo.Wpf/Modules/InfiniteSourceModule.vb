Imports DevExpress.Xpf.DemoBase
Imports GridDemo.VirtualSources

Namespace GridDemo

    Public Class InfiniteSourceModule
        Inherits GridShowcaseBrowserModule

        Const Uri As String = "/controls-and-libraries/data-grid/bind-to-data/bind-to-any-data-source-with-virtual-sources/how-to-use-virtual-sources/"

        Friend Const Step1Uri As String = "120195" & Uri & "step-1-fetch-data-and-enable-scrolling"

        Friend Const Step2Uri As String = "120196" & Uri & "step-2-enable-sorting"

        Friend Const Step3Uri As String = "120197" & Uri & "step-3-enable-filtering"

        Friend Const Step4Uri As String = "120198" & Uri & "step-4-display-summaries"

        Friend Const Step5Uri As String = "401683" & Uri & "step-5-enable-data-editing"

        Friend Const Path As String = "Modules/VirtualSources"

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Return New ShowcaseInfo() {LoadShowcase("Step 1: Fetch Data and Enable Scrolling", Step1Uri, Path, {GetType(InfiniteScrolling1ViewModel), GetType(InfiniteScrolling1View), GetType(IssueData), GetType(IssuesService)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 2: Enable Sorting", Step2Uri, Path, {GetType(InfiniteScrolling2ViewModel), GetType(InfiniteScrolling2View), GetType(IssueSortOrder)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 3: Enable Filtering", Step3Uri, Path, {GetType(InfiniteScrolling3ViewModel), GetType(InfiniteScrolling3View), GetType(IssueFilter), GetType(IssueFilterConverter)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 4: Display Summaries", Step4Uri, Path, {GetType(InfiniteScrolling4ViewModel), GetType(InfiniteScrolling4View), GetType(IssuesSummaries)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Step 5: Enable Editing", Step5Uri, Path, {GetType(InfiniteScrolling5ViewModel), GetType(InfiniteScrolling5View), GetType(IssueData), GetType(IssuesService)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}
        End Function
    End Class
End Namespace
