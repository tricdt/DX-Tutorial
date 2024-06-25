using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using ProductsDemo.Controls;

namespace ProductsDemo.Modules {
    public class GridContactsModuleViewModel : GridViewModelBase<Contact> {
        public GridContactsModuleViewModel() {
            CheckedItemType = "Card";
            InitializeIndexBarSource();
            SelectedIndexBarString = IndexBarSource[0];
        }
        public virtual ObservableCollection<Contact> FilteredItemsSource { get; set; }
        public virtual List<string> IndexBarSource { get; set; }
        public virtual string SelectedIndexBarString { get; set; }
        public void SetTableView() { ChangeView(ViewType.TableView); }
        public void SetCardView() { ChangeView(ViewType.CardView); }
        public void GroupByAlphabet() { GroupBy("Name.FullName"); }
        public void GroupByState() { GroupBy("Address.State"); }
        protected virtual INotificationService NotificationService { get { return null; } }
        protected virtual void OnSelectedIndexBarStringChanged() {
            UpdateFilteredItemsSource();
        }
        public override void ShowNewItemWindow() { ShowNewContactWindow(); }
        void InitializeIndexBarSource() {
            List<string> source = new List<string>();
            foreach(Contact contact in ItemsSource) {
                string firstChar = contact.LastName.Substring(0, 1).ToUpper();
                if(source.Contains(firstChar)) continue;
                source.Add(firstChar);
            }
            source.Sort();
            source.Insert(0, "ALL");
            IndexBarSource = source;
        }
        void UpdateFilteredItemsSource() {
            ObservableCollection<Contact> source = new ObservableCollection<Contact>();
            foreach(Contact contact in ItemsSource) {
                if(SelectedIndexBarString == IndexBarSource[0] || (!string.IsNullOrEmpty(contact.LastName) && contact.LastName.ToUpper().StartsWith(SelectedIndexBarString)))
                    source.Add(contact);
            }
            FilteredItemsSource = source;
        }
        protected override void OnItemsSourceCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            base.OnItemsSourceCollectionChanged(sender, e);
            UpdateFilteredItemsSource();
        }

        protected override List<GridColumnModel> GetColumns() {
            List<GridColumnModel> columns = new List<GridColumnModel>();
            columns.Add(new GridColumnModel() { Name = "Prefix", DisplayName = string.Empty, AllowEditing = DefaultBoolean.False, Width = 40, FixedWidth = true });
            columns.Add(new GridColumnModel() { Name = "Name.FullName", DisplayName = "Name", AllowEditing = DefaultBoolean.False, GroupInterval = ColumnGroupInterval.Alphabetical });
            columns.Add(new GridColumnModel() { Name = "Email", AllowEditing = DefaultBoolean.False, Width = 182, FixedWidth = true });
            columns.Add(new GridColumnModel() { Name = "Address.State", DisplayName = "State", AllowEditing = DefaultBoolean.False, Width = 46, FixedWidth = true });
            columns.Add(new GridColumnModel() { Name = "Address.City", DisplayName = "City", AllowEditing = DefaultBoolean.False, Width = 80, FixedWidth = true });
            columns.Add(new GridColumnModel() { Name = "Phone", AllowEditing = DefaultBoolean.False, Mask = @"(\(\d\d\d\)) \d{1,3}-\d\d\d\d", Width = 90, FixedWidth = true });
            columns.Add(new GridColumnModel() { Name = "Activity", AllowEditing = DefaultBoolean.False, Width = 70, FixedWidth = true });
            return columns;
        }

        protected override void InitializeDefaultView() {
            ChangeView(ViewType.CardView);
            CheckedItemType = ItemType.Card;
        }

        protected override List<Contact> GetItemsSource() {
            return DataBaseHelper.Instance.Contacts;
        }
        protected override void GroupBy(string fieldName) {
            ChangeView(ViewType.TableView);
            base.GroupBy(fieldName);
        }

        public virtual Contact EditableContact { get; set; }
        public virtual ViewType CurrentView { get; set; }
        public List<string> States { get; set; }
        public List<string> Cities { get; set; }

        EditContactWindow contactWindow = null;

        void ChangeView(ViewType viewType) {
            ClearModel();
            ChangeViewCore(viewType);
        }
        void ChangeViewCore(ViewType viewType) {
            CurrentView = viewType;
        }

        void ShowEditContactWindowCore(Contact contact) {
            contactWindow = new EditContactWindow();
            EditableContact = contact;
            States = GetStates();
            Cities = GetCities();
            contactWindow.DataContext = this;
            contactWindow.ShowDialog();
        }
        bool isNew;
        public void ShowEditContactWindow() {
            if(SelectedItem == null)
                return;
            isNew = false;
            ShowEditContactWindowCore((Contact)SelectedItem.Clone());
        }
        public void ShowNewContactWindow() {
            isNew = true;
            ShowEditContactWindowCore(new Contact());
        }
        public void SaveContact() {
            if(isNew) {
                ItemsSource.Add(EditableContact);
                SelectedItem = EditableContact;
                NotificationService.CreatePredefinedNotification("Contact Created", EditableContact.Name.FullName, "", EditableContact.Photo).ShowAsync();
            } else {
                SelectedItem.Assign(EditableContact);
                NotificationService.CreatePredefinedNotification("Contact Changed", EditableContact.Name.FullName, "", EditableContact.Photo).ShowAsync();
            }
            contactWindow.Close();
        }
        public void CloseContactWindow() {
            contactWindow.Close();
        }
        public void DeleteContact() {
            NotificationService.CreatePredefinedNotification("Contact Deleted", SelectedItem.Name.FullName, "", SelectedItem.Photo).ShowAsync();
            ItemsSource.Remove(SelectedItem);
        }

        List<string> GetCities() {
            System.Collections.IEnumerable cities = (from contact in ItemsSource select contact.Address.City).OrderBy(s => s).Distinct();
            return cities.Cast<string>().ToList();
        }
        List<string> GetStates() {
            System.Collections.IEnumerable states = (from contact in ItemsSource select contact.Address.State).OrderBy(s => s).Distinct();
            return states.Cast<string>().ToList();
        }
    }
    public enum ViewType { TableView, CardView }
}
