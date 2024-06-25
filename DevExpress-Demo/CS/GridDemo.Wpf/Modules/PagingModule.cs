using DevExpress.Xpf.DemoBase;
using GridDemo.VirtualSources;

namespace GridDemo {
    public class PagingModule : GridShowcaseBrowserModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            return new ShowcaseInfo[] {
                LoadShowcase("Step 1: Fetch Data and Enable Paging", InfiniteSourceModule.Step1Uri, InfiniteSourceModule.Path, new[] { typeof(Paging1ViewModel), typeof(Paging1View), typeof(IssueData), typeof(IssuesService) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 2: Enable Sorting", InfiniteSourceModule.Step2Uri, InfiniteSourceModule.Path, new[] { typeof(Paging2ViewModel), typeof(Paging2View), typeof(IssueSortOrder) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 3: Enable Filtering", InfiniteSourceModule.Step3Uri, InfiniteSourceModule.Path, new[] { typeof(Paging3ViewModel), typeof(Paging3View), typeof(IssueFilter), typeof(IssueFilterConverter) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Step 4: Display Summaries", InfiniteSourceModule.Step4Uri, InfiniteSourceModule.Path, new[] { typeof(Paging4ViewModel), typeof(Paging4View), typeof(IssuesSummaries) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
            };
        }
    }
}
