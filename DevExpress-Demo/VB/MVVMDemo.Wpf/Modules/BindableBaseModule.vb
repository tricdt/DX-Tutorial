Imports MVVMDemo.BindableBaseDemo
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class BindableBaseModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/BindableBase"
            Const uri As String = "17350/Common-Concepts/MVVM-Framework/ViewModels/BindableBase"
            Return New ShowcaseInfo() {LoadShowcase("Bindable Properties", uri, path, {GetType(BindablePropertiesViewModel), GetType(BindablePropertiesView)}), LoadShowcase("Bindable Properties via Fields", Nothing, path, {GetType(BindablePropertiesViaFieldsViewModel), GetType(BindablePropertiesViaFieldsView)})}
        End Function
    End Class
End Namespace
