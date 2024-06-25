using DevExpress.Mvvm.POCO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMDemo.AsyncCommands {
    public class AsyncPOCOCommandsViewModel {
        public virtual int Progress { get; set; }

        public async Task Calculate() {
            for(int i = 0; i <= 100; i++) {
                if(this.GetAsyncCommand(x => x.Calculate()).IsCancellationRequested) {
                    Progress = 0;
                    return;
                }
                Progress = i;
                await Task.Delay(20);
            }

        }
    }
}
