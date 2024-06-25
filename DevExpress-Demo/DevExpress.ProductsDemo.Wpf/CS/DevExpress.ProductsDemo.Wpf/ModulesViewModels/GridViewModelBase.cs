using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using DevExpress.Data;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;

namespace ProductsDemo.Modules {
    public class ItemTypeToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string itemType = parameter as string;
            string newValue = value as string;
            if(itemType == null || value == null)
                return false;
            return itemType == newValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if((bool)value)
                return parameter;
            return Binding.DoNothing;
        }
    }
    public class ItemType {
        public const string None = "None";
        public const string List = "List";
        public const string ToDoListItem = "ToDoListItem";
        public const string CompletedItem = "CompletedItem";
        public const string TodayItem = "TodayItem";
        public const string PrioritizedItem = "PrioritizedItem";
        public const string OverdueItem = "OverdueItem";
        public const string SimpleListItem = "SimpleListItem";
        public const string DeferredItem = "DeferredItem";
        public const string Print = "Print";
        public const string Card = "Card";
        public const string State = "State";
        public const string Alphabet = "Alphabet";
    }

    public abstract class GridViewModelBase<T> : GridViewModelBase, ISupportNavigation where T : class {
        public virtual ObservableCollection<T> ItemsSource { get; protected set; }
        protected virtual void OnItemsSourceChanging(ObservableCollection<T> newValue) {
            SelectedItem = null;
            if(ItemsSource != null)
                ItemsSource.CollectionChanged -= OnItemsSourceCollectionChanged;
        }
        protected virtual void OnItemsSourceChanged(ObservableCollection<T> oldValue) {
            if(ItemsSource != null)
                ItemsSource.CollectionChanged += OnItemsSourceCollectionChanged;
        }
        protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) { }
        protected sealed override void InitializeItemsSource() {
            ItemsSource = new ObservableCollection<T>(GetItemsSource());
        }

        public virtual T SelectedItem { get; set; }

        protected virtual void OnSelectedItemChanged(T oldValue) {
            if(SelectedItemChanged != null)
                SelectedItemChanged(this, new ValueChangedEventArgs<T>(oldValue, SelectedItem));
        }

        protected abstract List<T> GetItemsSource();
        public virtual string CheckedItemType { get; set; }
        protected GridModuleNavigationParameter NavigationParameter { get; private set; }
        public event EventHandler<ValueChangedEventArgs<T>> SelectedItemChanged;
        protected virtual void OnNavigatedFrom() { }
        protected virtual void OnNavigatedTo() {
            if(NavigationParameter == GridModuleNavigationParameter.NewItem)
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(ShowNewItemWindow));
        }
        public abstract void ShowNewItemWindow();
        #region ISupportNavigation
        void ISupportNavigation.OnNavigatedFrom() { OnNavigatedFrom(); }
        void ISupportNavigation.OnNavigatedTo() { OnNavigatedTo(); }
        object ISupportParameter.Parameter {
            get { return NavigationParameter; }
            set { NavigationParameter = value == null ? GridModuleNavigationParameter.Default : (GridModuleNavigationParameter)value; }
        }
        #endregion
    }
    public enum GridModuleNavigationParameter { Default, NewItem }
    [POCOViewModel]
    public abstract class GridViewModelBase {
        public GridViewModelBase() {
            InitializeData();
            InitializeItemsSource();
            InitializeColumns();
            InitializeDefaultView();
        }

        protected abstract List<GridColumnModel> GetColumns();

        protected abstract void InitializeItemsSource();

        protected virtual void InitializeData() { }

        void InitializeColumns() {
            Columns = GetColumns();
        }

        protected virtual void GroupBy(string fieldName) {
            Columns.First(c => c.Name == fieldName).IsGrouped = true;
        }
        protected void SortBy(string fieldName, ColumnSortOrder order) {
            Columns.First(c => c.Name == fieldName).SortOrder = order;
        }
        protected void ClearModel() {
            Ungroup();
            ClearSorting();
            FilterString = null;
            BeginUpdate();
            AssignIndexes();
            EndUpdate();
        }

        void AssignIndexes() {
            for(int i = 0; i < Columns.Count; i++) {
                Columns[i].Index = i;
            }
        }
        void Ungroup() {
            foreach(var column in Columns)
                column.IsGrouped = false;
        }
        void ClearSorting() {
            foreach(var column in Columns)
                column.SortOrder = ColumnSortOrder.None;
        }

        protected virtual void InitializeDefaultView() { }

        public List<GridColumnModel> Columns { get; private set; }
        public virtual string FilterString { get; set; }

        [Command(Name = "PrintCommand")]
        public void DoPrint() {
            if(Print != null)
                Print(this, EventArgs.Empty);
        }

        protected void BeginUpdate() {
            if(IsLoadingChanged != null)
                IsLoadingChanged(this, new IsLoadingEventArgs() { IsLoading = true });
        }
        protected void EndUpdate() {
            if(IsLoadingChanged != null)
                IsLoadingChanged(this, new IsLoadingEventArgs() { IsLoading = false });
        }

        public event EventHandler<IsLoadingEventArgs> IsLoadingChanged;
        public event EventHandler Print;
    }

    public class IsLoadingEventArgs : EventArgs {
        public bool IsLoading { get; set; }
    }

}
