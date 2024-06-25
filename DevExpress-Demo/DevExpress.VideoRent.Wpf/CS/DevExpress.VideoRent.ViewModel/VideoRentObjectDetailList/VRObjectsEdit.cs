using System;
using System.Threading;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
#if SL
using DevExpress.Xpf.Drawing;
#else
using System.Drawing;
#endif

namespace DevExpress.VideoRent.ViewModel {
    public abstract class VRObjectsEdit<T> : ModuleObjectEdit where T : VideoRentBaseObject {
        T currentRecord;
        bool allowDelete;
        volatile bool coerceCurrentRecordInProgress = false;

        public VRObjectsEdit(VRObjectsEditObject<T> editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
        }
        public VRObjectsEditObject<T> VRObjectsEditObject { get { return (VRObjectsEditObject<T>)EditObject; } }
        public T CurrentRecord {
            get { return currentRecord; }
            set { SetValue<T>("CurrentRecord", ref currentRecord, value, RaiseCurrentRecordChanged); }
        }
        public bool AllowDelete {
            get { return allowDelete; }
            set { SetValue<bool>("AllowDelete", ref allowDelete, value); }
        }
        public event EventHandler CurrentRecordChanged;
        public virtual bool DeleteCurrentRecord() {
            return DeleteObject(CurrentRecord, string.Format(ConstStrings.Get("DeleteRecord"), CurrentRecord.ToString()), (o) => { CurrentRecord = null; });
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            if(CurrentRecord == null && VRObjectsEditObject.VideoRentObjects.Count != 0)
                CurrentRecord = VRObjectsEditObject.VideoRentObjects[0];
        }
        protected virtual bool DeleteObject(VideoRentBaseObject objectToDelete, string message, Action<object> action) {
            if(objectToDelete == null) return false;
            return ObjectHelper.SafeDelete(objectToDelete, message, ExceptionProcesser.Current, action);
        }
        protected override void DisposeManaged() {
            CurrentRecord = null;
            base.DisposeManaged();
        }
        void RaiseCurrentRecordChanged(T oldValue, T newValue) {
            bool coerceSuccess = true;
            if(!coerceCurrentRecordInProgress) {
                coerceSuccess = CoerceCurrentRecord(newValue);
                if(!coerceSuccess) {
                    BackgroundHelper.DoInBackground((ThreadStart)(() =>
                    {
                        coerceCurrentRecordInProgress = true;
                        CurrentRecord = oldValue;
                        coerceCurrentRecordInProgress = false;
                    }));
                }
            }
            if(coerceSuccess)
                RaiseCurrentRecordChangedOverride(oldValue, newValue);
        }
        protected virtual bool CoerceCurrentRecord(T value) { return true; }
        protected virtual void RaiseCurrentRecordChangedOverride(T oldValue, T newValue) {
            AllowDelete = CurrentRecord != null && CurrentRecord.AllowDelete;
            if(CurrentRecordChanged != null) {
                CurrentRecordChanged(this, EventArgs.Empty);
            }
        }
        #region Commands
        public Action<object> CommandDeleteCurrentRecord { get { return DoCommandDeleteCurrentRecord; } }
        void DoCommandDeleteCurrentRecord(object p) { DeleteCurrentRecord(); }
        #endregion
    }
    public abstract class VRObjectPicturesEdit<T> : ModuleObjectEdit where T : VideoRentBaseObject {
        Picture currentPicture;

        public VRObjectPicturesEdit(VRObjectMainEditObject<T> editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public VRObjectMainEditObject<T> VRObjectMainEditObject { get { return (VRObjectMainEditObject<T>)EditObject; } }
        public Picture CurrentPicture {
            get { return currentPicture; }
            set { SetValue<Picture>("CurrentPicture", ref currentPicture, value); }
        }
        public void AddPicture() {
            Image newImage = OpenFileDialog.View.OpenImage();
            if(newImage == null) return;
            AddPicture(newImage);
        }
        public void DeleteCurrentPicture() {
            DeleteObject(CurrentPicture, ConstStrings.Get("DeletePictureMessage"));
        }
        protected abstract void AddPicture(Image picture);
        protected virtual bool DeleteObject(VideoRentBaseObject objectToDelete, string message) {
            if(objectToDelete == null) return false;
            return ObjectHelper.SafeDelete(objectToDelete, message, ExceptionProcesser.Current);
        }
        #region Commands
        public Action<object> CommandAddPicture { get { return DoCommandAddPicture; } }
        void DoCommandAddPicture(object p) { AddPicture(); }
        public Action<object> CommandDeleteCurrentPicture { get { return DoCommandDeleteCurrentPicture; } }
        void DoCommandDeleteCurrentPicture(object p) { DeleteCurrentPicture(); }
        #endregion
    }
}
