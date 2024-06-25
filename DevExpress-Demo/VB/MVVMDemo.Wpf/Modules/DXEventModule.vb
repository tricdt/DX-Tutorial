Imports MVVMDemo.DXEventDemo
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class DXEventModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/DXEvent"
            Const uri As String = "115778/MVVM-Framework/Productivity/DXEvent"
            Return New ShowcaseInfo() {LoadShowcase("Binding event to method", uri, path, {GetType(BindingEventToMethodView), GetType(BindingEventToMethodViewModel)}), LoadShowcase("Call multiple methods", uri, path, {GetType(MultipleCallsView)}), LoadShowcase("Using sender and event arguments", uri, path, {GetType(EventArgsView), GetType(EventArgsViewModel)})}
        End Function
    End Class
End Namespace
