using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public abstract class VRObjectDetailObject<T> : VRObjectBaseObject<T>, IVRObjectEditObjectParent<T> where T : VideoRentBaseObject {
        int createdObjectsCount = 0;
        bool wasCreatedNewObject = false;

        public VRObjectDetailObject(Session session, Guid? videoRentObjectOid)
            : base(AllObjects<T>.Set, session) {
            this.VideoRentObjectOid = videoRentObjectOid == null ? CreateNewObject() : (Guid)videoRentObjectOid;
        }
        public bool WasCreatedNewObject { get { return wasCreatedNewObject; } }
        public override object Key { get { return VideoRentObjectOid; } }
        public virtual T GetVideoRentObject(Guid videoRentObjectOid) {
            return SessionHelper.GetObjectByKey<T>(videoRentObjectOid, Session);
        }
        public NestedUnitOfWork GetNestedSession() {
            return Session.BeginNestedUnitOfWork();
        }
        protected Guid VideoRentObjectOid { get; private set; }
        protected abstract T CreateNewObjectOverride();
        protected override void ReloadEnd() {
            wasCreatedNewObject = false;
            base.ReloadEnd();
        }
        Guid CreateNewObject() {
            wasCreatedNewObject = true;
            int objectsCount = Session.GetObjectsToSave().Count;
            T videoRentObject = CreateNewObjectOverride();
            createdObjectsCount += Session.GetObjectsToSave().Count - objectsCount;
            return videoRentObject.Oid;
        }
    }
}
