Imports MVVMDemo.ViewModelBaseDemo
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class ViewModelBaseModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/ViewModelBase"
            Const uri As String = "17351/Common-Concepts/MVVM-Framework/ViewModels/ViewModelBase"
            Return New ShowcaseInfo() {LoadShowcase("Overview", uri, path, {GetType(LoginViewModel), GetType(LoginView)}), LoadShowcase("Automatic Commands Creation", uri & "#Feature5", path, {GetType(AutoCommandsViewModel), GetType(AutoCommandsView)}), LoadShowcase("Manual Commands Creation", uri & "#Feature5", path, {GetType(ManualCommandsViewModel), GetType(ManualCommandsView)}), LoadShowcase("Services", uri & "#Feature2", path, {GetType(ServicesViewModel), GetType(ServicesView)}), LoadShowcase("IDataErrorInfo", Nothing, path, {GetType(DataErrorInfoViewModel), GetType(DataErrorInfoView)})}
        End Function
    End Class
End Namespace
