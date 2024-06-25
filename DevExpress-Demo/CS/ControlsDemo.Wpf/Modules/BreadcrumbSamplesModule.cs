using System;
using System.Linq;
using ControlsDemo.BreadcrumbSamples;
using DevExpress.Xpf.DemoBase;

namespace ControlsDemo.Modules {
    public class BreadcrumbSamplesModule : ShowcaseBrowserModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/BreadcrumbSamples";
            return new ShowcaseInfo[] {
                LoadShowcase("Self-Referential Data", null, path, new[] { typeof(SelfReferentialDataView), typeof(SelfReferentialDataViewModel) }),
                LoadShowcase("Hierarchical Data", null, path, new[] { typeof(HierarchicalDataView), typeof(HierarchicalDataViewModel) }),
                LoadShowcase("Different-Type Items", null, path, new[] { typeof(DifferentTypeItemsView), typeof(NWindObjectHelper) }, showCodeBehind:true),
            };
        }
    }
}
