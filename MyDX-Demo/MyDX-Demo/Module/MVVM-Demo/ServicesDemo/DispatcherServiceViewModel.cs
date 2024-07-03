using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class DispatcherServiceViewModel
    {
        public ObservableCollection<string> Items { get; set; }
        protected DispatcherServiceViewModel()
        {
            Items = new ObservableCollection<string>();
        }
        public Task Generate()
        {
            return Task.Factory.StartNew(GenerateCore);
        }
        void GenerateCore()
        {
            IDispatcherService service = this.GetRequiredService<IDispatcherService>();
            service.BeginInvoke(() => Items.Clear());
            for (int i = 0; i <= 10; i++)
            {
                string item = "Item " + i;
                service.BeginInvoke(() => Items.Add(item));
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }
    }
}
