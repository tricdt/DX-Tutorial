using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Windows.Data;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class ComboBoxEditEditValueOwner : IAttachedBindingListOwner {
        ComboBoxEdit edit;
        bool raiseListChanged = true;

        public ComboBoxEditEditValueOwner(ComboBoxEdit edit) {
            this.edit = edit;
            this.edit.EditValueChanged += OnEditEditValueChanged;
        }
        public IList List {
            get {
                IList list = this.edit.EditValue as IList;
                if(list == null) {
                    list = new ArrayList();
                    this.edit.EditValue = list;
                }
                return list;
            }
        }
        public event NotifyCollectionChangedEventHandler ListChanged;
        public void AddItem(int newIndex, object item) {
            List.Insert(newIndex, item);
            UpdateEdit();
        }
        public void RemoveItem(int oldIndex) {
            List.RemoveAt(oldIndex);
            UpdateEdit();
        }
        public void MoveItem(int oldIndex, int newIndex) {
            object item = List[oldIndex];
            raiseListChanged = false;
            List.RemoveAt(oldIndex);
            List.Insert(newIndex, item);
            UpdateEdit();
            raiseListChanged = true;
            RaiseListChanged();
        }
        public void ResetList(IList newList) {
            this.edit.EditValue = new ArrayList(newList);
            UpdateEdit();
        }
        void OnEditEditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(raiseListChanged)
                RaiseListChanged();
        }
        void UpdateEdit() {
            this.edit.BeginInit();
            this.edit.EndInit();
        }
        void RaiseListChanged() {
            if(ListChanged != null)
                ListChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
    public class CustomComboBoxEdit : ComboBoxEdit {
        #region Dependency Properties
        public static readonly DependencyProperty EditCollectionProperty;
        static CustomComboBoxEdit() {
            Type ownerType = typeof(CustomComboBoxEdit);
            EditCollectionProperty = DependencyProperty.Register("EditCollection", typeof(IBindingList), ownerType, new PropertyMetadata(null));
        }
        #endregion
        bool? savedIsTextEditable;

        public CustomComboBoxEdit() {
            BindingListAttacher editCollectionAttacher = new BindingListAttacher(new ComboBoxEditEditValueOwner(this));
            BindingOperations.SetBinding(editCollectionAttacher, BindingListAttacher.BindingListProperty, new Binding("EditCollection") { Source = this, Mode = BindingMode.OneWay });
        }
        public IBindingList EditCollection { get { return (IBindingList)GetValue(EditCollectionProperty); } set { SetValue(EditCollectionProperty, value); } }
        protected override void OnIsReadOnlyChanged() {
            base.OnIsReadOnlyChanged();
            ShowBorder = !IsReadOnly;
            AllowDefaultButton = !IsReadOnly;
            if(IsReadOnly) {
                savedIsTextEditable = IsTextEditable;
                IsTextEditable = true;
            } else {
                IsTextEditable = savedIsTextEditable;
            }
        }
    }
}
