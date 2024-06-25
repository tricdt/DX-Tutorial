using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MVVMDemo.Services {
    public class DispatcherServiceViewModel {
        public ObservableCollection<string> Items { get; set; }

        public Task Generate() {
            return Task.Factory.StartNew(GenerateCore);
        }
        void GenerateCore() {
            IDispatcherService service = this.GetRequiredService<IDispatcherService>();
            service.BeginInvoke(() => Items.Clear());
            for(int i = 0; i <= 10; i++) {
                string item = "Item " + i;
                service.BeginInvoke(() => Items.Add(item));
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }

        protected DispatcherServiceViewModel() {
            Items = new ObservableCollection<string>();
        }
    }
}
