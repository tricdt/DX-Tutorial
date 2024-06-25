using MVVMDemo.ViewModelsInteraction;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class ViewModelsInteractionModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/ViewModelsInteraction";
            const string supportParameterUri = "17448/Common-Concepts/MVVM-Framework/ViewModels/Passing-data-between-ViewModels-ISupportParameter";
            const string supportParentViewModelUri = "17449/Common-Concepts/MVVM-Framework/ViewModels/ViewModel-relationships-ISupportParentViewModel";
            const string messengerUri = "17474/Common-Concepts/MVVM-Framework/Messenger";
            return new ShowcaseInfo[] {
                LoadShowcase("Passing data between ViewModels (ISupportParameter)", supportParameterUri, path, new[] { typeof(MainViewModel), typeof(DetailViewModel), typeof(MainView) }),
                LoadShowcase("Passing data between POCO ViewModels (ISupportParameter)", supportParameterUri, path, new[] { typeof(MainPOCOViewModel), typeof(DetailPOCOViewModel), typeof(MainPOCOView) }),
                LoadShowcase("ViewModel parent-child relationships (ISupportParentViewModel)", supportParentViewModelUri, path, new[] { typeof(ParentViewModel), typeof(ChildViewModel), typeof(ParentView) }),
                LoadShowcase("POCO ViewModel parent-child relationships (ISupportParentViewModel)", supportParentViewModelUri, path, new[] { typeof(ParentPOCOViewModel), typeof(ChildPOCOViewModel), typeof(ParentPOCOView) }),
                LoadShowcase("Exchanging messages between view models", messengerUri, path, new[] { typeof(MessengerViewModel), typeof(MessengerView) }),
            };
        }
    }
}
