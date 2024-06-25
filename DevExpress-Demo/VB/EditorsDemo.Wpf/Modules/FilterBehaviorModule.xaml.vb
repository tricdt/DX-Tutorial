Imports DevExpress.Xpf.DemoBase
Imports EditorsDemo.FilterBehavior

Namespace EditorsDemo

    Public Partial Class FilterBehaviorModule
        Inherits EditorsShowcaseBrowserModule

        Friend Const Path As String = "Modules/FilterBehavior"

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const Version As String = "?v=" & AssemblyInfo.VersionShort
            Const Uri As String = "DevExpress.Xpf.Core.FilteringUI.FilterBehavior" & Version & "#"
            Return New IShowcaseInfo() {LoadShowcase("Chart Filtering", Uri & "filter-elements-and-chartcontrol", Path, {GetType(FilterBehaviorChartControl), GetType(FilterBehaviorChartControlViewModel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Scheduler Filtering", Uri & "filter-elements-and-scheduler", Path, {GetType(FilterBehaviorScheduler)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("ListBoxEdit Filtering", Uri & "filter-elements-and-data-editors", Path, {GetType(FilterBehaviorListBoxEdit), GetType(FilterBehaviorListBoxEditViewModel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("CollectionView Filtering", Uri & "filter-elements-and-collectionview", Path, {GetType(FilterBehaviorCollecitonView), GetType(CollectionViewFilterBehavior), GetType(FilterBehaviorCollecitonViewViewModel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("View Model Data Filtering", Uri & "viewmodel-data-filtering", Path, {GetType(FilterBehaviorViewModelData), GetType(FilterBehaviorViewModelDataViewModel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Standalone Filter Editor", Uri & "standalone-filter-editor", Path, {GetType(FilterBehaviorFilterEditor)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Filter Panel", Uri & "standalone-filter-panel", Path, {GetType(FilterBehaviorFilteringPanel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}
        End Function
    End Class
End Namespace
