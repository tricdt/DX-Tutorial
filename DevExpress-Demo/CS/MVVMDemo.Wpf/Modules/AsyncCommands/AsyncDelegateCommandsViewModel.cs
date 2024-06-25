using DevExpress.Mvvm;
using System.Threading.Tasks;

namespace MVVMDemo.AsyncCommands {
    public class AsyncDelegateCommandsViewModel : ViewModelBase {
        public AsyncCommand CalculateCommand { get; private set; }

        async Task Calculate() {
            for(int i = 0; i <= 100; i++) {
                if(CalculateCommand.IsCancellationRequested) {
                    Progress = 0;
                    return;
                }
                Progress = i;
                await Task.Delay(20);
            }

        }

        int _Progress;
        public int Progress {
            get { return _Progress; }
            set { SetValue(ref _Progress, value); }
        }

        public AsyncDelegateCommandsViewModel() {
            CalculateCommand = new AsyncCommand(Calculate);
        }

    }
}
