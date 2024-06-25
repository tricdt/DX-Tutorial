using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public static class IListExtensions {
        public static IEnumerable<T> GetGenericEnumerable<T>(this IList list) {
            foreach(T item in list)
                yield return item;
        }
    }
    public interface IAttachedBindingListOwner {
        IList List { get; }
        event NotifyCollectionChangedEventHandler ListChanged;
        void AddItem(int newIndex, object item);
        void RemoveItem(int oldIndex);
        void MoveItem(int oldIndex, int newIndex);
        void ResetList(IList newList);
    }
    public class BindingListAttacher : DependencyObject {
        #region Dependency Properties
        public static readonly DependencyProperty BindingListProperty;
        static BindingListAttacher() {
            Type ownerType = typeof(BindingListAttacher);
            BindingListProperty = DependencyProperty.Register("BindingList", typeof(IList), ownerType, new PropertyMetadata(null,
                (d, e) => ((BindingListAttacher)d).RaiseBindingListChanged(e)));
        }
        #endregion
        IAttachedBindingListOwner owner;
        bool bindingListChangingInProgress = false;

        public BindingListAttacher(IAttachedBindingListOwner owner) {
            this.owner = owner;
            this.owner.ListChanged += OnOwnerListChanged;
        }
        public IList BindingList { get { return (IList)GetValue(BindingListProperty); } set { SetValue(BindingListProperty, value); } }
        public bool BindingListAllowItemMoving { get; set; }
        void RaiseBindingListChanged(DependencyPropertyChangedEventArgs e) {
            UnSubscribeFromOldValue(e.OldValue);
            SubscribeToNewValue(e.NewValue);
        }
        void UnSubscribeFromOldValue(object value) {
            if(value != null) {
                if(value is INotifyCollectionChanged) {
                    ((INotifyCollectionChanged)value).CollectionChanged -= OnCollectionChanged;
                } else {
                    if(value is IBindingList) {
                        ((IBindingList)value).ListChanged -= OnBindingListListChanged;
                    }
                }
            }

        }
        void SubscribeToNewValue(object value) {
            if(value != null) {
                if(value is INotifyCollectionChanged) {
                    SubscribeToCollectionChanged(value as INotifyCollectionChanged);
                } else {
                    if(value is IBindingList) {
                        SubscribeToListChanged(value as IBindingList);
                    }
                }
            }
        }
        void SubscribeToListChanged(IBindingList value) {
            value.ListChanged += OnBindingListListChanged;
            BindingListAllowItemMoving = !(value is XPBaseCollection);
            OnBindingListListChanged(value, new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        void SubscribeToCollectionChanged(INotifyCollectionChanged value) {
            value.CollectionChanged += OnCollectionChanged;
            BindingListAllowItemMoving = true;
            OnCollectionChanged(value, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(bindingListChangingInProgress) return;
            switch(e.Action) {
                case NotifyCollectionChangedAction.Reset: this.owner.ResetList(BindingList); break;
                case NotifyCollectionChangedAction.Add: this.owner.AddItem(e.NewStartingIndex, BindingList[e.NewStartingIndex]); break;
                case NotifyCollectionChangedAction.Remove: this.owner.RemoveItem(e.NewStartingIndex); break;
                case NotifyCollectionChangedAction.Replace: this.owner.MoveItem(e.OldStartingIndex, e.NewStartingIndex); break;
            }
        }
        void OnBindingListListChanged(object sender, ListChangedEventArgs e) {
            if(bindingListChangingInProgress) return;
            switch(e.ListChangedType) {
                case ListChangedType.Reset: this.owner.ResetList(BindingList); break;
                case ListChangedType.ItemAdded: this.owner.AddItem(e.NewIndex, BindingList[e.NewIndex]); break;
                case ListChangedType.ItemDeleted: this.owner.RemoveItem(e.NewIndex); break;
                case ListChangedType.ItemMoved: this.owner.MoveItem(e.OldIndex, e.NewIndex); break;
            }
        }
        void OnOwnerListChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(BindingList == null) return;
            bindingListChangingInProgress = true;
            switch(e.Action) {
                case NotifyCollectionChangedAction.Reset: ResetBindingList(this.owner.List); break;
                case NotifyCollectionChangedAction.Add: AddItemsToBindingList(e.NewStartingIndex, e.NewItems); break;
                case NotifyCollectionChangedAction.Remove: RemoveItemsFromBindingList(e.OldStartingIndex, e.OldItems); break;
                case NotifyCollectionChangedAction.Move: MoveItemsInBindingList(e.OldStartingIndex, e.NewStartingIndex, e.OldItems); break;
                case NotifyCollectionChangedAction.Replace: ReplaceItemsInBindingList(e.OldStartingIndex, e.OldItems, e.NewItems); break;
            }
            bindingListChangingInProgress = false;
        }
        void ResetBindingList(IList list) {
            if(BindingListAllowItemMoving) {
                BindingList.Clear();
                foreach(object item in list)
                    BindingList.Add(item);
            } else {
                bool bindingListChanged = false;
                foreach(object item in new List<object>(BindingList.GetGenericEnumerable<object>())) {
                    if(list.IndexOf(item) < 0) {
                        bindingListChanged = true;
                        BindingList.Remove(item);
                    }
                }
                foreach(object item in list) {
                    if(!BindingList.Contains(item)) {
                        bindingListChanged = true;
                        BindingList.Add(item);
                    }
                }
                if(bindingListChanged) {
                    bindingListChangingInProgress = false;
                    if(BindingList is INotifyCollectionChanged) {
                        OnCollectionChanged(BindingList, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    } else {
                        if(BindingList is IBindingList) {
                            OnBindingListListChanged(BindingList, new ListChangedEventArgs(ListChangedType.Reset, -1));
                        }
                    }
                }
            }
        }
        void AddItemsToBindingList(int newIndex, IList newItems) {
            for(int newItemsIndex = newItems.Count; --newItemsIndex >= 0; ) {
                BindingList.Insert(newIndex, newItems[newItemsIndex]);
            }
        }
        void RemoveItemsFromBindingList(int oldIndex, IList oldItems) {
            for(int i = oldItems.Count; --i >= 0; ) {
                BindingList.RemoveAt(oldIndex);
            }
        }
        void MoveItemsInBindingList(int oldIndex, int newIndex, IList items) {
            if(BindingListAllowItemMoving) {
                RemoveItemsFromBindingList(oldIndex, items);
                AddItemsToBindingList(newIndex, items);
            } else {
                if(BindingList is INotifyCollectionChanged) {
                    OnCollectionChanged(BindingList, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, items, newIndex, oldIndex));
                } else {
                    if(BindingList is IBindingList) {
                        OnBindingListListChanged(BindingList, new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
                    }
                }
            }
        }
        void ReplaceItemsInBindingList(int index, IList oldItems, IList newItems) {
            for(int itemIndex = 0; itemIndex < oldItems.Count; ++itemIndex) {
                BindingList[index + itemIndex] = newItems[itemIndex];
            }
        }
    }
}
