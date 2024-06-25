Imports ControlsDemo.BreadcrumbSamples
Imports DevExpress.Xpf.DemoBase

Namespace ControlsDemo.Modules

    Public Class BreadcrumbSamplesModule
        Inherits ShowcaseBrowserModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/BreadcrumbSamples"
            Return New ShowcaseInfo() {LoadShowcase("Self-Referential Data", Nothing, path, {GetType(SelfReferentialDataView), GetType(SelfReferentialDataViewModel)}), LoadShowcase("Hierarchical Data", Nothing, path, {GetType(HierarchicalDataView), GetType(HierarchicalDataViewModel)}), LoadShowcase("Different-Type Items", Nothing, path, {GetType(DifferentTypeItemsView), GetType(NWindObjectHelper)}, showCodeBehind:=True)}
        End Function
    End Class
End Namespace
