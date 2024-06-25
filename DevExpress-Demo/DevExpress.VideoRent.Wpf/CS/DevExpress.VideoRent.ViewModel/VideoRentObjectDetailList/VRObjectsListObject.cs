using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public abstract class VRObjectsListObject<T> : VRObjectBaseObject<T>, IVRObjectsEditObjectParent<T> where T : VideoRentBaseObject {
        EditableSubobject listEditObject;
        EditableSubobject viewOptionsEditObject;

        public VRObjectsListObject(Session session) : base(null, session) { }
        public override object Key { get { return typeof(XPCollection<T>); } }
        public virtual AllObjects<T> GetVideoRentObjects() {
            return new AllObjects<T>(Session);
        }
        protected virtual EditableSubobject CreateListEditObject() {
            return null;
        }
        protected virtual EditableSubobject CreateViewOptionsEditObject() {
            return null;
        }
        protected override void ReloadBegin() {
            SessionHelper.CommitSession(Session, ExceptionProcesser.Current);
            base.ReloadBegin();
        }
        #region Subobjects
        internal EditableSubobject ListEditObject {
            get {
                if(listEditObject == null)
                    listEditObject = CreateListEditObject();
                return listEditObject;
            }
        }
        internal EditableSubobject ViewOptionsEditObject {
            get {
                if(viewOptionsEditObject == null)
                    viewOptionsEditObject = CreateViewOptionsEditObject();
                return viewOptionsEditObject;
            }
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                if(ListEditObject != null)
                    list.Add(ListEditObject);
                if(ViewOptionsEditObject != null)
                    list.Add(ViewOptionsEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == listEditObject) {
                listEditObject = null;
                return true;
            }
            if(editableSubobject == viewOptionsEditObject) {
                viewOptionsEditObject = null;
                return true;
            }
            return false;
        }
        #endregion
    }
}
