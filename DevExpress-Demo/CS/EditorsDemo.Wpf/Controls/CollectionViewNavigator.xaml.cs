using DevExpress.Mvvm;
using DevExpress.Xpf.Utils;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DXDemo.Controls {
    public partial class CollectionViewNavigator : UserControl {
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty =
            DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", typeof(bool), typeof(CollectionViewNavigator), new UIPropertyMetadata(true));
        public static readonly DependencyProperty CollectionViewProperty =
            DependencyPropertyManager.Register("CollectionView", typeof(ICollectionView), typeof(CollectionViewNavigator), new UIPropertyMetadata(null));
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemEditorVisibilityProperty =
            DependencyPropertyManager.Register("IsSynchronizedWithCurrentItemEditorVisibility", typeof(Visibility), typeof(CollectionViewNavigator), new UIPropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty EditableCollectionViewVisibilityProperty =
            DependencyPropertyManager.Register("EditableCollectionViewVisibility", typeof(Visibility), typeof(CollectionViewNavigator), new UIPropertyMetadata(Visibility.Visible));

        IList directions = new List<ListSortDirection>() { ListSortDirection.Ascending, ListSortDirection.Descending };
        IList fields = new List<string>() { "JobTitle", "FirstName", "LastName", "BirthDate" };
        public IList Directions { get { return directions; } }
        public IList Fields { get { return fields; } }

        public bool IsSynchronizedWithCurrentItem {
            get { return (bool)GetValue(IsSynchronizedWithCurrentItemProperty); }
            set { SetValue(IsSynchronizedWithCurrentItemProperty, value); }
        }
        public ICollectionView CollectionView {
            get { return (ICollectionView)GetValue(CollectionViewProperty); }
            set { SetValue(CollectionViewProperty, value); }
        }
        public Visibility IsSynchronizedWithCurrentItemEditorVisibility {
            get { return (Visibility)GetValue(IsSynchronizedWithCurrentItemEditorVisibilityProperty); }
            set { SetValue(IsSynchronizedWithCurrentItemEditorVisibilityProperty, value); }
        }
        public Visibility EditableCollectionViewVisibility {
            get { return (Visibility)GetValue(EditableCollectionViewVisibilityProperty); }
            set { SetValue(EditableCollectionViewVisibilityProperty, value); }
        }

        public int CurrentSortDescription { get; set; }
        public int CurrentGroupDescription { get; set; }
        public string CurrentGroupFieldName { get; set; }
        public string CurrentSortFieldName { get; set; }
        public ListSortDirection CurrentSortDirection { get; set; }

        public ICommand DeleteGroup { get; private set; }
        public ICommand DeleteSort { get; private set; }
        public ICommand AddGroup { get; private set; }
        public ICommand AddSort { get; private set; }

        public CollectionViewNavigator() {
            InitializeComponent();
            root.DataContext = this;
            DeleteGroup = new DelegateCommand<object>(OnDeleteGroup, CanDeleteGroup);
            DeleteSort = new DelegateCommand<object>(OnDeleteSort, CanDeleteSort);
            AddGroup = new DelegateCommand<object>(OnAddGroup);
            AddSort = new DelegateCommand<object>(OnAddSort);
        }
        void OnDeleteGroup(object parameter) {
            if(CurrentGroupDescription >= 0)
                CollectionView.GroupDescriptions.RemoveAt(CurrentGroupDescription);
        }
        void OnDeleteSort(object parameter) {
            if(CurrentSortDescription >= 0) {
                CollectionView.GroupDescriptions.Remove(FindGroupDescription(CurrentSortDescription));
                CollectionView.SortDescriptions.RemoveAt(CurrentSortDescription);
            }
        }
        void OnAddGroup(object parameter) {
            if(ContainsGroupDescription(CurrentGroupFieldName))
                return;
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription(CurrentGroupFieldName));
            if(!ContainsSortDescription(CurrentGroupFieldName))
                CollectionView.SortDescriptions.Add(new SortDescription(CurrentGroupFieldName, ListSortDirection.Ascending));
        }
        void OnAddSort(object parameter) {
            if(ContainsSortDescription(CurrentSortFieldName))
                return;
            CollectionView.SortDescriptions.Add(new SortDescription(CurrentSortFieldName, CurrentSortDirection));
        }
        public bool CanDeleteGroup(object parameter) {
            return CurrentGroupDescription >= 0;
        }
        public bool CanDeleteSort(object parameter) {
            return CurrentSortDescription >= 0;
        }
        bool ContainsGroupDescription(string fieldName) {
            foreach(PropertyGroupDescription desc in CollectionView.GroupDescriptions)
                if(desc.PropertyName == fieldName)
                    return true;
            return false;
        }
        PropertyGroupDescription FindGroupDescription(int index) {
            string name = CollectionView.SortDescriptions[CurrentSortDescription].PropertyName;
            foreach(PropertyGroupDescription desc in CollectionView.GroupDescriptions)
                if(desc.PropertyName == name)
                    return desc;
            return null;
        }
        bool ContainsSortDescription(string fieldName) {
            foreach(SortDescription desc in CollectionView.SortDescriptions)
                if(desc.PropertyName == fieldName)
                    return true;
            return false;
        }

    }
}
