using System;
using System.ComponentModel;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace DevExpress.VideoRent.ViewModel {
    public interface IVRObjectMainEditObjectParent<T> where T : VideoRentBaseObject {
        T GetVideoRentObject(Guid videoRentObjectOid);
    }
    public interface IVRObjectEditObjectParent<T> : IVRObjectMainEditObjectParent<T> where T : VideoRentBaseObject { }
    public interface IAddVRObjectEditObjectParent {
        NestedUnitOfWork GetNestedSession();
    }
    public abstract class VRObjectEditObjectBase<T> : EditableSubobject where T : VideoRentBaseObject {
        T videoRentObject;

        public VRObjectEditObjectBase(EditableObject parent, Guid videoRentObjectOid) : base(parent) { }
        public T VideoRentObject {
            get { return videoRentObject; }
            protected set { SetValue<T>("VideoRentObject", ref videoRentObject, value); }
        }
        protected override void DisposeManaged() {
            VideoRentObject = null;
            base.DisposeManaged();
        }
    }
    public abstract class VRObjectMainEditObject<T> : VRObjectEditObjectBase<T> where T : VideoRentBaseObject {
        public VRObjectMainEditObject(EditableObject parent, Guid videoRentObjectOid)
            : base(parent, videoRentObjectOid) {
            VideoRentObjectOid = videoRentObjectOid;
        }
        public IVRObjectMainEditObjectParent<T> VideoRentEditObjectParent { get { return (IVRObjectMainEditObjectParent<T>)Parent; } }
        internal Guid VideoRentObjectOid { get; private set; }
        protected override void UpdateOverride() {
            if(VideoRentObject != null)
                UnsubscribeFromChanged();
            VideoRentObject = VideoRentEditObjectParent.GetVideoRentObject(VideoRentObjectOid);
            if(VideoRentObject != null)
                SubscribeToChanged();
        }
        protected virtual void SubscribeToChanged() {
            VideoRentObject.Changed += OnVideoRentObjectChanged;
            XPClassInfo classInfo = VideoRentObject.Session.GetClassInfo(VideoRentObject.GetType());
            foreach(XPMemberInfo m in classInfo.CollectionProperties) {
                ((XPBaseCollection)m.GetValue(VideoRentObject)).ListChanged += OnVideoRentObjectCollectionPropertyListChanged;
            }
        }
        protected virtual void UnsubscribeFromChanged() {
            VideoRentObject.Changed -= OnVideoRentObjectChanged;
            XPClassInfo classInfo = VideoRentObject.Session.GetClassInfo(VideoRentObject.GetType());
            foreach(XPMemberInfo m in classInfo.CollectionProperties) {
                ((XPBaseCollection)m.GetValue(VideoRentObject)).ListChanged -= OnVideoRentObjectCollectionPropertyListChanged;
            }
        }
        protected override void DisposeManaged() {
            if(VideoRentObject != null)
                UnsubscribeFromChanged();
            base.DisposeManaged();
        }
        void OnVideoRentObjectChanged(object sender, ObjectChangeEventArgs e) { RaiseChanged(); }
        void OnVideoRentObjectCollectionPropertyListChanged(object sender, ListChangedEventArgs e) { RaiseChanged(); }
    }
    public abstract class VRObjectEditObject<T> : VRObjectMainEditObject<T> where T : VideoRentBaseObject {
        public VRObjectEditObject(EditableObject parent, Guid videoRentObjectOid) : base(parent, videoRentObjectOid) { }
    }
    public abstract class AddVRObjectEditObject<T> : VRObjectEditObjectBase<T> where T : VideoRentBaseObject {
        Guid parentVRObjectOid;
        NestedUnitOfWork nestedSession;

        public AddVRObjectEditObject(EditableObject parent, Guid videoRentObjectOid)
            : base(parent, videoRentObjectOid) {
            parentVRObjectOid = videoRentObjectOid;
            Update();
        }
        public IAddVRObjectEditObjectParent AddVRObjectEditObjectParent { get { return (IAddVRObjectEditObjectParent)Parent; } }
        public Guid ParentVRObjectOid {
            get { return parentVRObjectOid; }
            private set { SetValue<Guid>("ParentVRObjectOid", ref parentVRObjectOid, value); }
        }
        protected NestedUnitOfWork NestedSession {
            get { return nestedSession; }
            private set { SetValue<NestedUnitOfWork>("NestedSession", ref nestedSession, value); }
        }
        protected override void UpdateOverride() {
            if(NestedSession != null) throw new InvalidOperationException();
            NestedSession = AddVRObjectEditObjectParent.GetNestedSession();
            VideoRentObject = CreateObjectOverride();
        }
        protected override void DisposeManaged() {
            VideoRentObject = null;
            NestedSession.Dispose();
            NestedSession = null;
            base.DisposeManaged();
        }
        protected abstract T CreateObjectOverride();
        internal virtual void SaveAndDispose() {
            SessionHelper.CommitSession(nestedSession, ExceptionProcesser.Current);
            Dispose();
        }
    }
}
