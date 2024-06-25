using DevExpress.Xpf.DemoBase;
using GridDemo.VirtualSources;

namespace GridDemo {
    public class InfiniteSourceModule : GridShowcaseBrowserModule {
        const string Uri = "/controls-and-libraries/data-grid/bind-to-data/bind-to-any-data-source-with-virtual-sources/how-to-use-virtual-sources/";
        internal const string Step1Uri = "120195" + Uri + "step-1-fetch-data-and-enable-scrolling";
        internal const string Step2Uri = "120196" + Uri + "step-2-enable-sorting";
        internal const string Step3Uri = "120197" + Uri + "step-3-enable-filtering";
        internal const string Step4Uri = "120198" + Uri + "step-4-display-summaries";
        internal const string Step5Uri = "401683" + Uri + "step-5-enable-data-editing";
        internal const string Path = "Modules/VirtualSources";

        protected override IShowcaseInfo[] CreateShowcases() {
            return new ShowcaseInfo[] {
                LoadShowcase("Step 1: Fetch Data and Enable Scrolling", Step1Uri, Path, new[] { typeof(InfiniteScrolling1ViewModel), typeof(InfiniteScrolling1View), typeof(IssueData), typeof(IssuesService) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 2: Enable Sorting", Step2Uri, Path, new[] { typeof(InfiniteScrolling2ViewModel), typeof(InfiniteScrolling2View), typeof(IssueSortOrder) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 3: Enable Filtering", Step3Uri, Path, new[] { typeof(InfiniteScrolling3ViewModel), typeof(InfiniteScrolling3View), typeof(IssueFilter), typeof(IssueFilterConverter) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 4: Display Summaries", Step4Uri, Path, new[] { typeof(InfiniteScrolling4ViewModel), typeof(InfiniteScrolling4View), typeof(IssuesSummaries) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 5: Enable Editing", Step5Uri, Path, new[] {  typeof(InfiniteScrolling5ViewModel), typeof(InfiniteScrolling5View), typeof(IssueData), typeof(IssuesService) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
            };
        }
    }
}
