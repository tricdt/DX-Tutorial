using DevExpress.Xpf.DemoBase;
using EditorsDemo.FilterBehavior;

namespace EditorsDemo {
    public partial class FilterBehaviorModule : EditorsShowcaseBrowserModule {
        internal const string Path = "Modules/FilterBehavior";

        protected override IShowcaseInfo[] CreateShowcases() {
            const string Version = "?v=" + AssemblyInfo.VersionShort;
            const string Uri = "DevExpress.Xpf.Core.FilteringUI.FilterBehavior" + Version + "#";
            return new IShowcaseInfo[] {
                LoadShowcase("Chart Filtering", Uri + "filter-elements-and-chartcontrol", Path, new[] { typeof(FilterBehaviorChartControl), typeof(FilterBehaviorChartControlViewModel) }, 
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Scheduler Filtering", Uri + "filter-elements-and-scheduler", Path, new[] { typeof(FilterBehaviorScheduler) },
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("ListBoxEdit Filtering", Uri + "filter-elements-and-data-editors", Path, new[] { typeof(FilterBehaviorListBoxEdit), typeof(FilterBehaviorListBoxEditViewModel) }, 
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("CollectionView Filtering", Uri + "filter-elements-and-collectionview", Path, new[] { typeof(FilterBehaviorCollecitonView), typeof(CollectionViewFilterBehavior), typeof(FilterBehaviorCollecitonViewViewModel) },
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("View Model Data Filtering", Uri + "viewmodel-data-filtering", Path, new[] { typeof(FilterBehaviorViewModelData), typeof(FilterBehaviorViewModelDataViewModel) },
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Standalone Filter Editor", Uri + "standalone-filter-editor", Path, new[] { typeof(FilterBehaviorFilterEditor) },
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Filter Panel", Uri + "standalone-filter-panel", Path, new[] { typeof(FilterBehaviorFilteringPanel) },
                    showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
            };
        }
    }
}
