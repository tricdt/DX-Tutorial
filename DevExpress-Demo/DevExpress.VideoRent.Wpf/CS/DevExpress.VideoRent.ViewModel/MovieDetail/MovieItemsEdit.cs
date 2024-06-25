using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieItemsEdit : ModuleObjectEdit {
        ObservableCollection<MovieItem> selectedItems;
        ObservableCollection<string> shelves;
        bool allowDelete;

        public MovieItemsEdit(MovieItemsEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) {
            AllowDelete = false;
            SelectedItems = new ObservableCollection<MovieItem>();
            SelectedItems.CollectionChanged += (s, e) => UpdateAllowDelete();
            CreateShelves(); // TODO make shelves edit data
        }
        public MovieItemsEditObject MovieItemsEditObject { get { return (MovieItemsEditObject)EditObject; } }
        public ObservableCollection<string> Shelves {
            get { return shelves; }
            set { SetValue<ObservableCollection<string>>("Shelves", ref shelves, value); }
        }
        public ObservableCollection<MovieItem> SelectedItems {
            get { return selectedItems; }
            private set { SetValue<ObservableCollection<MovieItem>>("SelectedItems", ref selectedItems, value); }
        }
        public bool AllowDelete {
            get { return allowDelete; }
            set { SetValue<bool>("AllowDelete", ref allowDelete, value); }
        }
        public void Delete() {
            List<MovieItem> itemsForDelete = new List<MovieItem>();
            bool allItemsAllowDelete = true;
            foreach(MovieItem item in SelectedItems) {
                if(!item.AllowDelete)
                    allItemsAllowDelete = false;
                else
                    itemsForDelete.Add(item);
            }
            if(!allItemsAllowDelete && MessageBox.Show(ConstStrings.Get("NotAllItemsCanBeDeleted"), ConstStrings.Get("Question"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;
            foreach(MovieItem item in itemsForDelete) {
                ObjectHelper.SafeDeleteNoCommit(item, null);
            }
        }
        void OnSelectedItemsListChanged(object sender, ListChangedEventArgs e) {
            UpdateAllowDelete();
        }
        void UpdateAllowDelete() {
            foreach(MovieItem item in SelectedItems) {
                if(item.AllowDelete) {
                    AllowDelete = true;
                    return;
                }
            }
            AllowDelete = false;
        }
        void CreateShelves() {
            ObservableCollection<string> shelves = new ObservableCollection<string>();
            string namePattern = ConstStrings.Get("ShelfPattern");
            for(int i = 0; i < ReferenceData.ShelvesCount; ++i) {
                shelves.Add(string.Format(namePattern, i + 1));
            }
            Shelves = shelves;
        }
        #region Commands
        public Action<object> CommandDelete { get { return DoCommandDelete; } }
        void DoCommandDelete(object p) { Delete(); }
        #endregion
    }
}
