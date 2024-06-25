using System;
using System.Collections;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.VideoRent.ViewModel.ViewModelBase {
    public abstract class LayoutData : VideoRentBaseObject {
        Employee employee;

        public LayoutData(Session session) : base(session) { }
        [Indexed(Unique = false)]
        public Employee Employee {
            get { return employee; }
            set { SetPropertyValue<Employee>("Employee", ref employee, value); }
        }
    }
    public class LayoutManager {
        static LayoutManager current;
        public static LayoutManager Current {
            get {
                if(current == null)
                    current = new LayoutManager();
                return current;
            }
        }

        Session originalSession;

        public XPCollection<LayoutData> LayoutData { get; private set; }
        public UnitOfWork Session { get; private set; }
        public bool Login(string login, string password) { return Login(login, password, null); }
        public bool Login(string login, string password, Session session) {
            Logout();
            this.originalSession = session != null ? session : DevExpress.Xpo.Session.DefaultSession;
            if(!VideoRentCurrentUser.Login(this.originalSession, login, password)) return false;
            LoadLayoutData(false);
            return true;
        }
        public void Logout() {
            if(LayoutData == null) return;
            SaveLayoutData(false);
        }
        public void ClearLayoutData<T>() where T : LayoutData {
            foreach(LayoutData layoutData in new ArrayList(GetLayoutData<T>())) {
                layoutData.Delete();
            }
            SaveLayoutData(true);
            LoadLayoutData(true);
        }
        public XPCollection<T> GetLayoutData<T>() where T : LayoutData {
            return new XPCollection<T>(LayoutManager.Current.LayoutData.Session, LayoutManager.Current.LayoutData.OfType<T>(), null);
        }
        public void Subscribe(SaveLoadLayoutDataEventHandler onAfterLoad, SaveLoadLayoutDataEventHandler onBeforeSave) {
            afterLoad += onAfterLoad;
            beforeSave += onBeforeSave;
            RaiseAfterLoad(onAfterLoad, false);
        }
        public void Unsubscribe(SaveLoadLayoutDataEventHandler onAfterLoad, SaveLoadLayoutDataEventHandler onBeforeSave) {
            RaiseBeforeSave(onBeforeSave, false);
            afterLoad -= onAfterLoad;
            beforeSave -= onBeforeSave;
        }
        SaveLoadLayoutDataEventHandler beforeSave;
        SaveLoadLayoutDataEventHandler afterLoad;
        void LoadLayoutData(bool clearing) {
            Session = new UnitOfWork(this.originalSession.DataLayer);
            Employee currentUser = VideoRentCurrentUser.GetCurrentUser(Session);
            LayoutData = new XPCollection<LayoutData>(Session, CriteriaOperator.Parse("Employee = ?", currentUser));
            RaiseAfterLoad(this.afterLoad, clearing);
        }
        void SaveLayoutData(bool clearing) {
            RaiseBeforeSave(this.beforeSave, clearing);
            SessionHelper.CommitSession(Session, ExceptionProcesser.Current);
            LayoutData = null;
            Session = null;
        }
        void RaiseBeforeSave(SaveLoadLayoutDataEventHandler beforeSave, bool clearing) {
            if(LayoutData != null && beforeSave != null)
                beforeSave(this, new SaveLoadLayoutDataEventArgs(clearing));
        }
        void RaiseAfterLoad(SaveLoadLayoutDataEventHandler afterLoad, bool clearing) {
            if(LayoutData != null && afterLoad != null)
                afterLoad(this, new SaveLoadLayoutDataEventArgs(clearing));
        }
    }
}
