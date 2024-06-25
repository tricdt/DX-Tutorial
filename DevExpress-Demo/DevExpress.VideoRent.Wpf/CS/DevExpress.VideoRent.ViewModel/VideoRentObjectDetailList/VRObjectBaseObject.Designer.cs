using System;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace DevExpress.VideoRent.ViewModel {
    public class AllObjects<T> : XPCollection<T> where T : VideoRentBaseObject {
        public static readonly EditableObjectSet Set = new EditableObjectSet();

        bool disposed = false;
        T excludedObject;

        public AllObjects(Session session, CriteriaOperator theCriteria, params SortProperty[] sortProperties)
            : this(null, session, theCriteria, sortProperties) {
        }
        public AllObjects(T excludedObject, Session session, CriteriaOperator theCriteria, params SortProperty[] sortProperties)
            : base(session, theCriteria, sortProperties) {
            this.excludedObject = excludedObject;
            SubscribeToSetUpdated();
            if(this.excludedObject != null)
                this.Remove(this.excludedObject);
        }
        public AllObjects(Session session) : this(session, null) { }
        public new bool Disposed { get { return disposed; } }
        protected override void Dispose(bool disposing) {
            if(disposing) {
                UnsubscribeFromSetUpdated();
            }
            disposed = true;
            base.Dispose(disposing);
        }
        void SubscribeToSetUpdated() {
            Set.Updated -= OnEditableObjectSetUpdated;
            Set.Updated += OnEditableObjectSetUpdated;
        }
        void UnsubscribeFromSetUpdated() {
            Set.Updated -= OnEditableObjectSetUpdated;
        }
        void OnEditableObjectSetUpdated(object sender, EditableObjectEventArgs e) {
            Guid oid = (Guid)e.UpdatedObject.Key;
            T updatedObject = Session.GetObjectByKey<T>(oid);
            if(updatedObject == null || updatedObject == this.excludedObject) return;
            ReloadObject(updatedObject);
            Add(updatedObject);
        }
        void ReloadObject(T obj) {
            obj.Reload();
            XPClassInfo classInfo = Session.GetClassInfo<T>();
            foreach(XPMemberInfo m in classInfo.CollectionProperties) {
                ((XPBaseCollection)m.GetValue(obj)).Reload();
            }
        }
    }
    public static class SessionCreator {
#if DEBUG
        public class DebugSession : UnitOfWork {
            public DebugSession(IDataLayer dataLayer) : base(dataLayer) { }
            public bool IsDisposed { get; private set; }
#if !SL
            protected override void Dispose(bool disposing) {
                IsDisposed = true;
                base.Dispose(disposing);
            }
#endif
        }
#endif
        public static EventHandler BeforeSessionCreated;
        public static UnitOfWork CreateSession(IDataLayer dataLayer) {
            RaiseBeforeSessionCreated();
#if DEBUG
            return new DebugSession(dataLayer);
#else
            return new UnitOfWork(dataLayer);
#endif
        }
        static void RaiseBeforeSessionCreated() {
            if(BeforeSessionCreated != null)
                BeforeSessionCreated(typeof(SessionCreator), EventArgs.Empty);
        }
    }
    public abstract class VRObjectBaseObject<T> : EditableObject where T : VideoRentBaseObject {
        UnitOfWork oldSession;

        public VRObjectBaseObject(EditableObjectSet set, Session session)
            : base(set) {
            Session = SessionCreator.CreateSession(session.DataLayer);
        }
        public override bool Dirty { get { return Session != null && (HasObjectToSave || Session.GetObjectsToDelete().Count > 0); } }
        protected UnitOfWork Session { get; private set; }
        protected override void SaveOverride() {
            SessionHelper.CommitSession(Session, ExceptionProcesser.Current);
        }
        protected override void ReloadBegin() {
            oldSession = Session;
            Session = SessionCreator.CreateSession(oldSession.DataLayer);
        }
        protected override void ReloadEnd() {
            oldSession.Dispose();
        }
        protected override void DisposeManaged() {
            Session.Dispose();
            Session = null;
            base.DisposeManaged();
        }
        bool HasObjectToSave { get { return Session.GetObjectsToSave().Count != 0; } }
    }
}
