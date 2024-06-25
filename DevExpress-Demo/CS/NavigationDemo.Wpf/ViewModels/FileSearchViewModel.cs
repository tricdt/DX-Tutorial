using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace NavigationDemo {
    public class FileSearchViewModel {
        #region Static

        static IEnumerable<DirectoryInfo> GetNestedDirectories(IEnumerable<DirectoryInfo> directories, Func<DirectoryInfo, bool> predicate) {
            return directories.SelectMany(x => {
                var childDirectories = new DirectoryInfo[0];
                try {
                    childDirectories = x.GetDirectories().Where(predicate).ToArray();
                } 
                catch { }
                return new[] { x }.Concat(GetNestedDirectories(childDirectories, predicate));
            });
        }

        #endregion

        const int maximumFilesCount = 2000;
        readonly ObservableCollection<string> _selectedPaths;
        ModifiedIntervalKind dateTimeKind = ModifiedIntervalKind.Undefined;
        #region Properties

        protected virtual IFolderBrowserDialogService FolderBrowserDialogService { get { return this.GetService<IFolderBrowserDialogService>(); } }

        public virtual string SearchPattern { get; set; }

        public ObservableCollection<string> SelectedPaths { get { return _selectedPaths; } }
        public virtual string SelectedPath { get; set; }

        public virtual SpecifiedDateKind DateKind { get; set; }

        public virtual DateTime FromDate { get; set; }
        public virtual DateTime ToDate { get; set; }

        public virtual bool IsSearchSubfolders { get; set; }
        public virtual bool IsSearchHiddenFilesAndFolders { get; set; }
        public virtual bool IsSearchSystemFolders { get; set; }
        public virtual bool IsReadOnlyFiles { get; set; }

        public virtual bool Searching { get; set; }
        [DevExpress.Mvvm.DataAnnotations.BindableProperty]
        public virtual ObservableCollection<FileInfo> SearchResult { get; set; }

        #endregion

        public FileSearchViewModel() {
            _selectedPaths = new ObservableCollection<string>();
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            IsSearchSystemFolders = true;
            IsSearchSubfolders = true;
        }

        #region Command Methods

        public void OnInitialized() {
            SearchPattern = ".exe";
            SetSelectedPath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            this.GetAsyncCommand(x => x.StartSearch()).Execute(null);
        }
        public void OnUnloaded() {
            if(CanCancel())
                Cancel();
            SearchResult.Clear();
            _selectedPaths.Clear();
            SelectedPath = null;
        }
        public virtual void SelectPath() {
            if(FolderBrowserDialogService.ShowDialog()) {
                SetSelectedPath(FolderBrowserDialogService.ResultPath);
            }
        }
        public void SetSelectedPath(string path) {
            SelectedPaths.Add(path);
            SelectedPath = path;
        }
        public Task StartSearch() {
            Searching = true;
            return Task.Factory.StartNew(() => {
                WalkFolderTree();
                Searching = false;
            });
        }
        public virtual bool CanStartSearch() {
            return !(string.IsNullOrEmpty(SearchPattern) || string.IsNullOrEmpty(SelectedPath) || Searching);
        }
        public virtual void Cancel() {
            this.GetAsyncCommand(x => x.StartSearch()).CancelCommand.Execute(null);
        }
        public virtual bool CanCancel() {
            return Searching;
        }
        public virtual void SelectDateTimeKind(ModifiedIntervalKind kind) {
            dateTimeKind = kind;
        }

        #endregion
        #region Filtering Predicates

        bool AllowFolderIterate(FileSystemInfo info) {
            return GetHiddenElementPredicate(info) && (IsSearchSystemFolders || !info.Attributes.HasFlag(FileAttributes.System));
        }
        bool GetHiddenElementPredicate(FileSystemInfo info) {
            return IsSearchHiddenFilesAndFolders || !info.Attributes.HasFlag(FileAttributes.Hidden);
        }

        bool GetDateTimePredicate(FileInfo info) {
            var result = true;
            DateTime? start = null;
            DateTime? finish = null;
            GetDateTimeBounds(out start, out finish);
            if(start.HasValue && finish.HasValue)
                if(dateTimeKind != ModifiedIntervalKind.SpecifiedDates || DateKind == SpecifiedDateKind.Modified)
                    result = DateTimeIsInRange(info.LastWriteTime, start, finish);
                else
                    result = DateTimeIsInRange(DateKind == SpecifiedDateKind.Accessed ? info.LastAccessTime : info.CreationTime, start, finish);
            return result;
        }
        void GetDateTimeBounds(out DateTime? start, out DateTime? finish) {
            start = null;
            finish = null;
            switch(dateTimeKind) {
                case ModifiedIntervalKind.LastWeek:
                    start = DateTime.Now - TimeSpan.FromDays(7);
                    finish = DateTime.Now;
                    break;
                case ModifiedIntervalKind.PastMonth:
                    int year = DateTime.Now.Month > 1 ? DateTime.Now.Year : DateTime.Now.Year - 1;
                    int month = DateTime.Now.Month > 1 ? DateTime.Now.Month - 1 : 12;
                    start = new DateTime(year, month, 1);
                    finish = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    break;
                case ModifiedIntervalKind.PastYear:
                    start = DateTime.Now - TimeSpan.FromDays(365);
                    finish = DateTime.Now;
                    break;
                case ModifiedIntervalKind.SpecifiedDates:
                    start = FromDate;
                    finish = ToDate;
                    break;
                default:
                    break;
            }
        }
        bool DateTimeIsInRange(DateTime dateTime, DateTime? start, DateTime? finish) {
            return dateTime >= start.Value && dateTime <= finish.Value;
        }

        #endregion

        void WalkFolderTree() {
            var directories = GetSearchDirectories();
            var searchResult = new List<FileInfo>();
            foreach(var directoryInfo in directories) {
                if(this.GetAsyncCommand(y => y.StartSearch()).IsCancellationRequested) break;
                if(!AllowFolderIterate(directoryInfo)) continue;

                IEnumerable<FileInfo> files = null;
                try {
                    files = directoryInfo.GetFiles(string.Format("*{0}*", SearchPattern), SearchOption.TopDirectoryOnly)
                            .Where(x => GetDateTimePredicate(x) && GetHiddenElementPredicate(x) && (!IsReadOnlyFiles || x.IsReadOnly)).ToArray();
                }
                catch {
                    continue;
                }

                searchResult.AddRange(files);
                if(searchResult.Count > maximumFilesCount)
                    Cancel();
            }
            SearchResult = new ObservableCollection<FileInfo>(searchResult);
        }
        IEnumerable<DirectoryInfo> GetSearchDirectories() {
            var rootInfo = new[] { new DirectoryInfo(SelectedPath) };
            if(!IsSearchSubfolders)
                return rootInfo;
            return GetNestedDirectories(rootInfo, x => GetHiddenElementPredicate(x));
        }
    }

    public enum SpecifiedDateKind {
        [Display(Name = "Modified Date")]
        Modified,
        [Display(Name = "Created Date")]
        Created,
        [Display(Name = "Accessed Date")]
        Accessed
    }

    public enum ModifiedIntervalKind {
        Undefined,
        LastWeek,
        PastMonth,
        PastYear,
        SpecifiedDates
    }
}
