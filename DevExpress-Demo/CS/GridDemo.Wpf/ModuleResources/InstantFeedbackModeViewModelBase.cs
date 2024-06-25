using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.DemoBase;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo {
    public class InstantFeedbackModeViewModelBase<T> where T : class {

        public class GenerateDataViewModel {
            public static GenerateDataViewModel Create() {
                return ViewModelSource.Create(() => new GenerateDataViewModel());
            }

            protected GenerateDataViewModel() { }

            public virtual int MaxValue { get; set; }
            public virtual int Progress { get; set; }
            public virtual string State { get; set; }
            public virtual bool ShowProgress { get; set; }

            internal bool StopGeneratingData { get; private set; }

            public void StopAndShow() {
                StopGeneratingData = true;
            }

            public void OnProgress(ProgressInfo progress, string state) {
                if(progress != null) {
                    MaxValue = progress.MaxValue;
                    Progress = progress.Value;
                }
                ShowProgress = progress != null;
                State = state;
            }
        }


        protected InstantFeedbackModeViewModelBase(DatabaseInfo databaseInfo, Func<IQueryable<T>> getQueryable) {
            this.databaseInfo = databaseInfo;
            this.getQueryable = getQueryable;
            AddRecordCount = 300000;
        }

        protected readonly Func<IQueryable<T>> getQueryable;
        readonly DatabaseInfo databaseInfo;
        bool unloaded;
        public virtual GenerateDataViewModel GenerateViewModel { get; set; }
        Action<ProgressInfo, string> OnProgress { get { return (progress, state) => GetOrCreateGenerateViewModel().OnProgress(progress, state); } }
        Func<bool> ShouldStopAndShow { get { return () => unloaded || GetOrCreateGenerateViewModel().StopGeneratingData; } }

        public int MinAddRecordCount { get { return 100000; } }
        public int MaxAddRecordCount { get { return 500000; } }
        public virtual int CurrentNumberOfRecords { get; set; }
        public virtual int AddRecordCount { get; set; }
        public virtual IQueryable<T> InstantFeedbackQueryableSource { get; set; }
        public virtual IQueryable<T> ServerModeQueryableSource { get; set; }

        public void OnLoaded() {
            if(File.Exists(databaseInfo.FileName)) {
                AssignSources();
            } else {
                ContinueWithAssignSources(SQLiteDataBaseGenerator.GenerateData(databaseInfo, OnProgress, ShouldStopAndShow));
            }
        }

        public virtual void OnUnloaded() {
            unloaded = true;
        }


        public void AddRecords() {
            AddRecordsCore(false);
        }
        public void RegenerateDatabase() {
            AddRecordsCore(true);
        }

        void AddRecordsCore(bool clearTable) {
            ContinueWithAssignSources(SQLiteDataBaseGenerator.AddData(databaseInfo, AddRecordCount, clearTable, OnProgress, ShouldStopAndShow));
        }

        void ContinueWithAssignSources(System.Threading.Tasks.Task task) {
            task.ContinueWith(t => AssignSources(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        void AssignSources() {
            if(unloaded)
                return;
            AssignSourcesCore();
        }

        protected virtual void AssignSourcesCore() {
            InstantFeedbackQueryableSource = getQueryable();
            ServerModeQueryableSource = getQueryable();
            CurrentNumberOfRecords = ServerModeQueryableSource.Count();
            GenerateViewModel = null;
        }

        GenerateDataViewModel GetOrCreateGenerateViewModel() {
            if(GenerateViewModel == null)
                GenerateViewModel = GenerateDataViewModel.Create();
            return GenerateViewModel;

        }
    }
}
