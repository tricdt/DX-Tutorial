Imports MVVMDemo.POCOViewModels
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class POCOViewModelsModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/POCOViewModels"
            Const uriPrefix As String = "17352/Common-Concepts/MVVM-Framework/ViewModels/POCO-ViewModels#"
            Return New ShowcaseInfo() {LoadShowcase("Overview", uriPrefix & "Basics", path, {GetType(LoginViewModel), GetType(LoginView), GetType(LoginViewModel_RuntimeGeneratedCode)}), LoadShowcase("Bindable Properties", uriPrefix & "BindableProperties", path, {GetType(PropertiesViewModel), GetType(PropertiesView), GetType(PropertiesViewModel_RuntimeGeneratedCode)}), LoadShowcase("Commands", uriPrefix & "Commands", path, {GetType(CommandsViewModel), GetType(CommandsView), GetType(CommandsViewModel_RuntimeGeneratedCode)}), LoadShowcase("Services", uriPrefix & "Services", path, {GetType(ServicesViewModel), GetType(ServicesView), GetType(ServicesViewModel_RuntimeGeneratedCode)}), LoadShowcase("IDataErrorInfo", uriPrefix & "IDataErrorInfo", path, {GetType(DataErrorInfoViewModel), GetType(DataErrorInfoView), GetType(DataErrorInfoViewModel_RuntimeGeneratedCode)}), LoadShowcase("Dependent Properties", Nothing, path, {GetType(DependentPropertiesViewModel), GetType(DependentPropertiesView), GetType(DependentPropertiesViewModel_RuntimeGeneratedCode)}), LoadShowcase("Change Notifications", Nothing, path, {GetType(ChangeNotificationsViewModel), GetType(ChangeNotificationsView), GetType(ChangeNotificationsViewModel_RuntimeGeneratedCode)})}
        End Function
    End Class
End Namespace
