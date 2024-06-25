using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Mvvm.ModuleInjection;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public static class Regions {
        public static string Main { get { return "MainRegion"; } }
        public static string Navigation { get { return "NavigationRegion"; } }
    }
    public static class Modules {
        public static string Dashboard { get { return "Dashboard"; } }
        public static string Products { get { return "Products"; } }
        public static string Sectors { get { return "Sectors"; } }
        public static string Regions { get { return "Regions"; } }
        public static string Channels { get { return "Channels"; } }
    }
    public abstract class DataViewModel {
        protected DataViewModel() {
            if(this.IsInDesignMode())
                DataProvider = new SampleDataProvider();
            else DataProvider = DataSource.GetDataProvider();
        }
        public void RequestData<T>(string actionId, Func<IDataProvider, T> requestDataFunction, Action<T> callback) {
            if(this.IsInDesignMode()) {
                callback(requestDataFunction(DataProvider));
                return;
            }
            if(!tasks.ContainsKey(actionId)) {
                tasks.Add(actionId, new CancellationTokenSource());
                var task = new Task<T>(() => requestDataFunction(DataProvider), tasks[actionId].Token, TaskCreationOptions.LongRunning);
                task.ContinueWith(x => callback(x.Result), tasks[actionId].Token, TaskContinuationOptions.LongRunning, TaskScheduler.Current);
                task.Start();
                return;
            }
            if(tasks.ContainsKey(actionId)) {
                tasks[actionId].Cancel();
                tasks.Remove(actionId);
                RequestData(actionId, requestDataFunction, callback);
                return;
            }
        }
        IDataProvider DataProvider;
        Dictionary<string, CancellationTokenSource> tasks = new Dictionary<string,CancellationTokenSource>();
    }
    public abstract class PageViewModel {
        protected PageViewModel() {
            ModuleManager.DefaultManager.GetEvents(this).Navigated += OnNavigated;
            if(this.IsInDesignMode())
                Initialize();
        }
        protected abstract void Initialize();
        void OnNavigated(object sender, NavigationEventArgs e) {
            if(isInitialize) return;
            Initialize();
            isInitialize = true;
        }
        bool isInitialize = false;
    }
}
