Imports MVVMDemo.ViewModelsInteraction
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo

    Public Partial Class ViewModelsInteractionModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const path As String = "Modules/ViewModelsInteraction"
            Const supportParameterUri As String = "17448/Common-Concepts/MVVM-Framework/ViewModels/Passing-data-between-ViewModels-ISupportParameter"
            Const supportParentViewModelUri As String = "17449/Common-Concepts/MVVM-Framework/ViewModels/ViewModel-relationships-ISupportParentViewModel"
            Const messengerUri As String = "17474/Common-Concepts/MVVM-Framework/Messenger"
            Return New ShowcaseInfo() {LoadShowcase("Passing data between ViewModels (ISupportParameter)", supportParameterUri, path, {GetType(MainViewModel), GetType(DetailViewModel), GetType(MainView)}), LoadShowcase("Passing data between POCO ViewModels (ISupportParameter)", supportParameterUri, path, {GetType(MainPOCOViewModel), GetType(DetailPOCOViewModel), GetType(MainPOCOView)}), LoadShowcase("ViewModel parent-child relationships (ISupportParentViewModel)", supportParentViewModelUri, path, {GetType(ParentViewModel), GetType(ChildViewModel), GetType(ParentView)}), LoadShowcase("POCO ViewModel parent-child relationships (ISupportParentViewModel)", supportParentViewModelUri, path, {GetType(ParentPOCOViewModel), GetType(ChildPOCOViewModel), GetType(ParentPOCOView)}), LoadShowcase("Exchanging messages between view models", messengerUri, path, {GetType(MessengerViewModel), GetType(MessengerView)})}
        End Function
    End Class
End Namespace
