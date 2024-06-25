Imports MVVMDemo.DXBindingDemo
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class DXBindingModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/DXBinding"
            Const uri As String = "115771/MVVM-Framework/Productivity/DXBinding"
            Return New ShowcaseInfo() {LoadShowcase("Binding Expressions", uri, path, {GetType(BindingExpressionsView), GetType(BindingExpressionsViewModel)}), LoadShowcase("Functions", uri, path, {GetType(FunctionsView), GetType(FunctionsViewModel), GetType(RegistrationHelper)}), LoadShowcase("Binding Sources", uri, path, {GetType(SourcesView)}), LoadShowcase("Back Conversion", uri, path, {GetType(BackConversionView), GetType(BackConversionViewModel)})}
        End Function
    End Class
End Namespace
