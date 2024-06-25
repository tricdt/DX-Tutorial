Imports MVVMDemo.Commands
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class CommandsModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/Commands"
            Const uri As String = "17353/Common-Concepts/MVVM-Framework/Commands/Delegate-Commands"
            Const viewModelBaseUri As String = "17351/Common-Concepts/MVVM-Framework/ViewModels/ViewModelBase"
            Const pocoUri As String = "17352/Common-Concepts/MVVM-Framework/ViewModels/POCO-ViewModels#"
            Return New ShowcaseInfo() {LoadShowcase("Delegate Commands", uri, path, {GetType(DelegateCommandsViewModel), GetType(DelegateCommandsView)}), LoadShowcase("Delegate Commands Change Notifications", uri, path, {GetType(ChangeNotificationsViewModel), GetType(ChangeNotificationsView)}), LoadShowcase("Automatic Commands Creation", viewModelBaseUri & "#Feature5", path, {GetType(AutoCommandsViewModel), GetType(AutoCommandsView)}), LoadShowcase("Automatic Commands Change Notifications", viewModelBaseUri & "#Feature5", path, {GetType(AutoCommandsChangeNotificationsViewModel), GetType(AutoCommandsChangeNotificationsView)}), LoadShowcase("POCO Commands", pocoUri & "Commands", path, {GetType(POCOCommandsViewModel), GetType(POCOCommandsView), GetType(POCOCommandsViewModel_RuntimeGeneratedCode)}), LoadShowcase("POCO Commands Change Notifications", pocoUri & "Commands", path, {GetType(POCOCommandsChangeNotificationsViewModel), GetType(POCOCommandsChangeNotificationsView), GetType(POCOCommandsChangeNotificationsViewModel_RuntimeGeneratedCode)})}
        End Function
    End Class
End Namespace
