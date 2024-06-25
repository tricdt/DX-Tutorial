Imports MVVMDemo.EnumItemsSource
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class EnumItemsSourceModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/EnumItemsSource"
            Const uri As String = Nothing
            Return New ShowcaseInfo() {LoadShowcase("ListBoxEdit", uri, path, {GetType(ListBoxEditView), GetType(UserRole)}), LoadShowcase("ComboBoxEdit", uri, path, {GetType(ComboBoxEditView), GetType(UserRole)}), LoadShowcase("ComboBox column in GridControl", uri, path, {GetType(GridControlView), GetType(UserRoleInfo), GetType(UserRole)})}
        End Function
    End Class
End Namespace
