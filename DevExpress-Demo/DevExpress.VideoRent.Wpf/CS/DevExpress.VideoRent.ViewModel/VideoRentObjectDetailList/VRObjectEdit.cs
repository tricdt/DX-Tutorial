using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Resources;

namespace DevExpress.VideoRent.ViewModel {
    public abstract class VRObjectEdit<T> : ModuleObjectEdit where T : VideoRentBaseObject {
        public VRObjectEdit(VRObjectEditObject<T> editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public VRObjectEditObject<T> VRObjectEditObject { get { return (VRObjectEditObject<T>)EditObject; } }
        protected virtual bool DeleteObject(VideoRentBaseObject objectToDelete, string message) {
            if(objectToDelete == null) return false;
            return ObjectHelper.SafeDeleteNoCommit(objectToDelete, message) != null;
        }
    }
    public abstract class AddVRObjectEdit<T> : ModuleObjectEdit where T : VideoRentBaseObject {
        public AddVRObjectEdit(AddVRObjectEditObject<T> editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public AddVRObjectEditObject<T> AddVRObjectEditObject { get { return (AddVRObjectEditObject<T>)EditObject; } }
        public virtual bool SaveAndDispose() {
            if(!DoValidate()) return false;
            AddVRObjectEditObject.SaveAndDispose();
            Dispose();
            return true;
        }
        #region Commands
        public Action<object> CommandSaveAndDispose { get { return DoCommandSaveAndDispose; } }
        void DoCommandSaveAndDispose(object p) { SaveAndDispose(); }
        #endregion
    }
}
