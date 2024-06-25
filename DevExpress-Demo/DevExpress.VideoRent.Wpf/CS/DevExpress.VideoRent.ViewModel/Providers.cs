using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class CurrentCustomerProvider : BindingAndDisposable {
        static CurrentCustomerProvider current;
        public static CurrentCustomerProvider Current {
            get {
                if(current == null)
                    current = new CurrentCustomerProvider();
                return current;
            }
        }
        Guid? currentCustomerOid;
        Customer currentCustomer;
        UnitOfWork session;

        CurrentCustomerProvider() {
            AllObjects<Customer>.Set.Updated += OnCustomersSetUpdated;
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
        }
        public event EventHandler CurrentCustomerOidChanged;
        public event EventHandler CurrentCustomerChanged;
        public Guid? CurrentCustomerOid {
            get { return currentCustomerOid; }
            set { SetValue<Guid?>("CurrentCustomerOid", ref currentCustomerOid, value, RaiseCurrentCustomerOidChanged); }
        }
        public Customer CurrentCustomer {
            get { return currentCustomer; }
            private set { SetValue<Customer>("CurrentCustomer", ref currentCustomer, value, RaiseCurrentCustomerChanged); }
        }
        protected override void DisposeManaged() {
            LayoutManager.Current.Unsubscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
            AllObjects<Customer>.Set.Updated -= OnCustomersSetUpdated;
            CurrentCustomer = null;
            this.session = null;
            base.DisposeManaged();
        }
        void RaiseCurrentCustomerOidChanged(Guid? oldValue, Guid? newValue) {
            UpdateCustomerObject();
            if(CurrentCustomerOidChanged != null)
                CurrentCustomerOidChanged(this, EventArgs.Empty);
        }
        void RaiseCurrentCustomerChanged(Customer oldValue, Customer newValue) {
            if(CurrentCustomerChanged != null)
                CurrentCustomerChanged(this, EventArgs.Empty);
        }
        void UpdateCustomerObject() {
            this.session = LayoutManager.Current.Session;
            CurrentCustomer = SessionHelper.GetObjectByKey<Customer>(CurrentCustomerOid, this.session);
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            CurrentCustomerOid = ViewModelLayoutData.GetLayoutData().CurrentCustomerOid;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            ViewModelLayoutData.GetLayoutData().CurrentCustomerOid = CurrentCustomerOid;
        }
        void OnCustomersSetUpdated(object sender, EditableObjectEventArgs e) {
            if(CurrentCustomer != null)
                CurrentCustomer.Reload();
        }
    }
    public class CurrentEmployeeProvider : BindingAndDisposable {
        static CurrentEmployeeProvider current;
        public static CurrentEmployeeProvider Current {
            get {
                if(current == null)
                    current = new CurrentEmployeeProvider();
                return current;
            }
        }
        Employee currentEmployee;

        CurrentEmployeeProvider() {
            CurrentEmployee = VideoRentCurrentUser.GetCurrentUser(LayoutManager.Current.Session);
        }
        public Employee CurrentEmployee {
            get { return currentEmployee; }
            private set { SetValue<Employee>("CurrentEmployee", ref currentEmployee, value); }
        }
    }
}
