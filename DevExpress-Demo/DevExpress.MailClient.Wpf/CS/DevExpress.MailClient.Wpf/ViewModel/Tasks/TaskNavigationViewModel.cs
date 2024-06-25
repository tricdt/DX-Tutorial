using DevExpress.MailClient.Helpers;
using DevExpress.Mvvm.POCO;

namespace DevExpress.MailClient.ViewModel {
    public class TasksNavigationViewModel : NavigationViewModelBase {
        public static TasksNavigationViewModel Create() {
            return ViewModelSource.Create(() => new TasksNavigationViewModel());
        }
        protected TasksNavigationViewModel() : base(ModuleType.Tasks) {
            HeaderViewModel = HeaderViewModel.Create();
            HeaderViewModel.Init(this);
        }

        protected override void Initialize() {
            Header = "Tasks";
            Icon = FilePathHelper.GetDXImageUri("Outlook Inspired/Task");
        }
        protected override void InitializeInDesignMode() {
            ContentViewModel = TasksViewModel.Create();
        }
    }
}
