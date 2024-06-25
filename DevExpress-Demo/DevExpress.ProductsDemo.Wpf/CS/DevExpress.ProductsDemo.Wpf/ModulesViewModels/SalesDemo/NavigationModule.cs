using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DevExpress.SalesDemo.Model;

namespace ProductsDemo {
    public abstract class NavigationModule : ViewModelBase {
        public NavigationModule() {
            IsActive = true;
        }
        public abstract string Caption { get; }
        public abstract string Description { get; }
        public abstract BitmapImage Glyph { get; }
        bool isActive = false;
        public bool IsActive {
            get { return isActive; }
            set { SetProperty(ref isActive, value, "IsActive", OnIsActiveChanged); }
        }
        public IDataProvider DataProvider { get; private set; }
        bool isDataLoading = false;
        public bool IsDataLoading {
            get { return isDataLoading; }
            private set { SetProperty(ref isDataLoading, value, "IsDataLoading"); }
        }
        bool isDataLoaded = false;
        public bool IsDataLoaded {
            get { return isDataLoaded; }
            private set { SetProperty(ref isDataLoaded, value, "IsDataLoaded"); }
        }

        protected abstract void SaveAndClearData();
        protected abstract void RestoreData();
        protected abstract void InitializeData();
        protected void SaveAndClearPropertyValue<T>(ref T storage, string propName, T nullValue = default(T)) {
            T storageValue = storage;
            T resultValue = default(T);
            if(PropertyCache == null)
                PropertyCache = new Dictionary<string, object>();
            if(PropertyCache.ContainsKey(propName)) PropertyCache.Remove(propName);
            PropertyCache.Add(propName, storageValue);
            resultValue = nullValue;
            storage = resultValue;
            RaisePropertyChanged(propName);
        }
        protected void SavePropertyValue<T>(T storage, string propName) {
            if(PropertyCache == null)
                PropertyCache = new Dictionary<string, object>();
            if(PropertyCache.ContainsKey(propName)) PropertyCache.Remove(propName);
            PropertyCache.Add(propName, storage);
        }
        protected void RestorePropertyValue<T>(out T storage, string propName, bool doRaisePropertyChanged) {
            T resultValue = default(T);
            if(PropertyCache != null && PropertyCache.ContainsKey(propName)) {
                resultValue = (T)PropertyCache[propName];
                PropertyCache.Remove(propName);
            }
            storage = resultValue;
            if(doRaisePropertyChanged)
                RaisePropertyChanged(propName);
        }
        protected override void OnInitializeInDesignMode() {
            base.OnInitializeInDesignMode();
            DataProvider = new SampleDataProvider();
            InitializeData();
        }
        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            DataProvider = DataSource.GetDataProvider();
        }
        void OnIsActiveChanged() {
            if(ViewModelBase.IsInDesignMode) return;
            if(IsDataLoading) return;
            if(IsActive && !IsDataLoaded) {
                InitializeInBackground();
                return;
            }
            if(IsActive == false)
                SaveAndClearData();
            else RestoreData();
        }
        void InitializeInBackground() {
            if(IsDataLoading || IsDataLoaded) return;
            IsDataLoading = true;
            var t = DoInBackground(InitializeData);
            t.ContinueWith(x => {
                IsDataLoading = false;
                IsDataLoaded = true;
            });
            t.Start();
        }
        Task DoInBackground(Action action) {
            return new Task(action);
        }
        Dictionary<string, object> PropertyCache;
    }
}
