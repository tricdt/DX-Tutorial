Imports MVVMDemo.AsyncCommands
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class AsyncCommandsModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/AsyncCommands"
            Const uri As String = "17354/Common-Concepts/MVVM-Framework/Commands/Asynchronous-Commands"
            Return New ShowcaseInfo() {LoadShowcase("Asynchronous Delegate Commands", uri, path, {GetType(AsyncDelegateCommandsViewModel), GetType(AsyncDelegateCommandsView)}), LoadShowcase("Asynchronous POCO Commands", uri, path, {GetType(AsyncPOCOCommandsViewModel), GetType(AsyncPOCOCommandsView), GetType(AsyncPOCOCommandsViewModel_RuntimeGeneratedCode)})}
        End Function
    End Class
End Namespace
