using DevExpress.Xpf.DemoBase;
using GanttDemo.Examples;

namespace GanttDemo {
    public class CodeExamples : ShowcaseBrowserModule {
        const string path = "Modules/Examples";
        const string helpPath = "/controls-and-libraries/gantt-control/";
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }

        protected override IShowcaseInfo[] CreateShowcases() {
            return new[] {
                LoadShowcase("Bind to Data", "400336" + helpPath + "bind-to-data/bind-to-hierarchical-data", path + "/BindToData", new[] { typeof(BindToData), typeof(BindToDataViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Bind to SelfReference Data", "400337" + helpPath + "bind-to-data/bind-to-selfreference-data", path + "/BindToSelfReferenceData", new[] { typeof(BindToSelfReferenceData), typeof(BindToSelfReferenceDataViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Built-in Types", "400335" + helpPath + "bind-to-data#retrieve-tasks", path + "/BuiltinTypes", new[] { typeof(BuiltinTypes), typeof(ProjectTaskViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Customize Timescale", "400347" + helpPath + "visual-elements/timescale-rulers", path + "/CustomizeTimescale", new[] { typeof(CustomizeTimescale), typeof(CustomDateTimeRangeFormatter), typeof(ProjectTaskViewModel) }, showCodeBehind: true, codeOrder: SourceCodeOrder.CodeBehindFirst, useNewDocsSite: true),
                LoadShowcase("Customize Working Time", "400451" + helpPath + "working-and-nonworking-time-ranges#working-and-non-working-time-rules", path + "/CustomizeWorkingTime", new[] { typeof(CustomizeWorkingTime), typeof(ProjectTaskViewModel) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Customize Items Appearance", "400346" + helpPath + "visual-elements/task", path + "/CustomizeItems", new[] { typeof(CustomizeItems), typeof(ProjectTaskViewModel) }, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Customize Summary Task Progress", "DevExpress.Xpf.Gantt.GanttView.CalculateSummaryTaskProgress", path + "/CustomizeSummaryTaskProgress", new[] { typeof(CustomizeSummaryTaskProgress) }, showCodeBehind: true, codeOrder: SourceCodeOrder.CodeBehindFirst, useNewDocsSite: true),
                LoadShowcase("Milestone Summary", "DevExpress.Xpf.Gantt.GanttView.CalculateSummaryTaskBounds", path + "/MilestoneSummary", new[] { typeof(MilestoneSummary) }, showCodeBehind: true, codeOrder: SourceCodeOrder.CodeBehindFirst, useNewDocsSite: true),
                LoadShowcase("Custom Edit Snapping", null, path + "/CustomSnapping", new[] { typeof(CustomSnapping) }, showCodeBehind: true, codeOrder: SourceCodeOrder.CodeBehindFirst, useNewDocsSite: true),
                LoadShowcase("Edit Limits", null, path + "/EditLimits", new[] { typeof(EditLimits) }, showCodeBehind: true, codeOrder: SourceCodeOrder.CodeBehindFirst, useNewDocsSite: true),
                LoadShowcase("Assign Resources to Tasks without Links", null, path + "/BindResourcesWithoutLinks", new[] { typeof(BindResourcesWithoutLinks), typeof(BindResourcesWithoutLinksViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Color Tasks by Resources", null, path + "/TaskColorByResource", new[] { typeof(TaskColorByResource), typeof(TaskColorByResourceViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Group Resources in the Editor", null, path + "/EditorGrouping", new[] { typeof(EditorGrouping), typeof(PartsProductionViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Custom Resources Editor", null, path + "/CustomResourcesEditor", new[] { typeof(CustomResourcesEditor), typeof(PartsProductionViewModel) }, showCodeBehind: false, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true)
            };
        }

        protected virtual void LoadDemoData() { }
    }
}
