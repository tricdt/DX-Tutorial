Imports DevExpress.Xpf.DemoBase
Imports GanttDemo.Examples

Namespace GanttDemo

    Public Class CodeExamples
        Inherits ShowcaseBrowserModule

        Const path As String = "Modules/Examples"

        Const helpPath As String = "/controls-and-libraries/gantt-control/"

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Return {LoadShowcase("Bind to Data", "400336" & helpPath & "bind-to-data/bind-to-hierarchical-data", path & "/BindToData", {GetType(BindToData), GetType(BindToDataViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Bind to SelfReference Data", "400337" & helpPath & "bind-to-data/bind-to-selfreference-data", path & "/BindToSelfReferenceData", {GetType(BindToSelfReferenceData), GetType(BindToSelfReferenceDataViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Built-in Types", "400335" & helpPath & "bind-to-data#retrieve-tasks", path & "/BuiltinTypes", {GetType(BuiltinTypes), GetType(ProjectTaskViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Customize Timescale", "400347" & helpPath & "visual-elements/timescale-rulers", path & "/CustomizeTimescale", {GetType(CustomizeTimescale), GetType(CustomDateTimeRangeFormatter), GetType(ProjectTaskViewModel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.CodeBehindFirst, useNewDocsSite:=True), LoadShowcase("Customize Working Time", "400451" & helpPath & "working-and-nonworking-time-ranges#working-and-non-working-time-rules", path & "/CustomizeWorkingTime", {GetType(CustomizeWorkingTime), GetType(ProjectTaskViewModel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Customize Items Appearance", "400346" & helpPath & "visual-elements/task", path & "/CustomizeItems", {GetType(CustomizeItems), GetType(ProjectTaskViewModel)}, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Customize Summary Task Progress", "DevExpress.Xpf.Gantt.GanttView.CalculateSummaryTaskProgress", path & "/CustomizeSummaryTaskProgress", {GetType(CustomizeSummaryTaskProgress)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.CodeBehindFirst, useNewDocsSite:=True), LoadShowcase("Milestone Summary", "DevExpress.Xpf.Gantt.GanttView.CalculateSummaryTaskBounds", path & "/MilestoneSummary", {GetType(MilestoneSummary)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.CodeBehindFirst, useNewDocsSite:=True), LoadShowcase("Custom Edit Snapping", Nothing, path & "/CustomSnapping", {GetType(CustomSnapping)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.CodeBehindFirst, useNewDocsSite:=True), LoadShowcase("Edit Limits", Nothing, path & "/EditLimits", {GetType(EditLimits)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.CodeBehindFirst, useNewDocsSite:=True), LoadShowcase("Assign Resources to Tasks without Links", Nothing, path & "/BindResourcesWithoutLinks", {GetType(BindResourcesWithoutLinks), GetType(BindResourcesWithoutLinksViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Color Tasks by Resources", Nothing, path & "/TaskColorByResource", {GetType(TaskColorByResource), GetType(TaskColorByResourceViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Group Resources in the Editor", Nothing, path & "/EditorGrouping", {GetType(EditorGrouping), GetType(PartsProductionViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Custom Resources Editor", Nothing, path & "/CustomResourcesEditor", {GetType(CustomResourcesEditor), GetType(PartsProductionViewModel)}, showCodeBehind:=False, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}
        End Function

        Protected Overridable Sub LoadDemoData()
        End Sub
    End Class
End Namespace
