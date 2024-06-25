using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public abstract class VRObjectsListBase<T> : ModuleObjectDetail where T : VideoRentBaseObject {
        public VRObjectsListBase(VRObjectsListObject<T> editObject) : this(editObject, null) { }
        public VRObjectsListBase(VRObjectsListObject<T> editObject, object tag) : base(editObject, tag) { }
        public VRObjectsListObject<T> VRObjectsListObject { get { return (VRObjectsListObject<T>)EditObject; } }
        protected override YesNoCancel AskSaveChanges() { return YesNoCancel.Yes; }
        protected override void RaiseDirtyRoughChanged(bool oldValue, bool newValue) {
            base.RaiseDirtyRoughChanged(oldValue, newValue);
            if(newValue)
                SubscribeToSessionCreatorBeforeSessionCreated();
            else
                UnsubscribeFromSessionCreatorBeforeSessionCreated();
        }
        protected override void DisposeManaged() {
            UnsubscribeFromSessionCreatorBeforeSessionCreated();
            base.DisposeManaged();
        }
        void OnSessionCreatorBeforeSessionCreated(object sender, EventArgs e) {
            UnsubscribeFromSessionCreatorBeforeSessionCreated();
            Save();
        }
        void SubscribeToSessionCreatorBeforeSessionCreated() {
            SessionCreator.BeforeSessionCreated += OnSessionCreatorBeforeSessionCreated;
        }
        void UnsubscribeFromSessionCreatorBeforeSessionCreated() {
            SessionCreator.BeforeSessionCreated -= OnSessionCreatorBeforeSessionCreated;
        }
    }
    public abstract class VRObjectsList<T> : VRObjectsListBase<T> where T : VideoRentBaseObject {
        object detailsTypeKey = null;
        VRObjectsEdit<T> listEdit;
        ModuleObjectEdit viewOptionsEdit;
        bool hasDetailsToClose = false;

        public VRObjectsList(VRObjectsListObject<T> editObject) : base(editObject) { }
        #region Edits
        public VRObjectsEdit<T> ListEdit {
            get { return listEdit; }
            protected set { SetValue<VRObjectsEdit<T>>("ListEdit", ref listEdit, value, RaiseListEditChanged); }
        }
        public ModuleObjectEdit ViewOptionsEdit {
            get { return viewOptionsEdit; }
            private set { SetValue<ModuleObjectEdit>("ViewOptionsEdit", ref viewOptionsEdit, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> list = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(ListEdit != null)
                    list.Add(ListEdit);
                if(ViewOptionsEdit != null)
                    list.Add(ViewOptionsEdit);
                return list;
            }
        }
        #endregion
        public bool HasDetailsToClose {
            get { return hasDetailsToClose; }
            protected set { SetValue<bool>("HasDetailsToClose", ref hasDetailsToClose, value); }
        }
        public ModuleObjectDetailBase OpenDetail(Guid? vroOid) {
            return OpenDetail(vroOid, null);
        }
        public ModuleObjectDetailBase OpenDetail(Guid? vroOid, object parameter) {
            ModuleObjectDetailBase detail = OpenDetailOverride(vroOid, parameter);
            if(detail != null && detail is ModuleObjectDetail) {
                detailsTypeKey = ((ModuleObjectDetail)detail).GetModuleTypeKey();
                detail.AfterDispose += OnDetailAfterDispose;
                HasDetailsToClose = true;
            }
            return detail;
        }
        public virtual void CloseDetails() {
            if(detailsTypeKey != null)
                ModulesManager.Current.CloseModuleObjectDetails(detailsTypeKey);
        }
        public void ChangeViewOptions() {
            ViewOptionsEdit = CreateViewOptionsEditOverride();
            if(ViewOptionsEdit != null)
                ViewOptionsEdit.AfterDispose += OnViewOptionsEditAfterDispose;
        }
        protected virtual ModuleObjectDetailBase OpenDetailOverride(Guid? vroOid, object parameter) {
            return null;
        }
        protected virtual ModuleObjectEdit CreateViewOptionsEditOverride() {
            return null;
        }
        protected virtual void OnListEditCurrentRecordChanged(object sender, EventArgs e) { }
        void OnViewOptionsEditAfterDispose(object sender, EventArgs e) {
            ViewOptionsEdit = null;
        }
        void RaiseListEditChanged(VRObjectsEdit<T> oldValue, VRObjectsEdit<T> newValue) {
            if(oldValue != null)
                oldValue.CurrentRecordChanged -= OnListEditCurrentRecordChanged;
            if(newValue != null) {
                newValue.CurrentRecordChanged += OnListEditCurrentRecordChanged;
                OnListEditCurrentRecordChanged(newValue, EventArgs.Empty);
            }
        }
        void OnDetailAfterDispose(object sender, EventArgs e) {
            if(detailsTypeKey == null) return;
            foreach(ModuleObjectDetailBase detail in ModulesManager.Current.GetModulesForType(detailsTypeKey)) {
                if(!detail.Disposed) {
                    HasDetailsToClose = true;
                    return;
                }
            }
            HasDetailsToClose = false;
        }
        #region Commands
        public Action<object> CommandEdit { get { return DoCommandEdit; } }
        void DoCommandEdit(object p) {
            if(ListEdit.CurrentRecord == null) return;
            OpenDetail(ListEdit.CurrentRecord.Oid, p);
        }
        public Action<object> CommandAdd { get { return DoCommandAdd; } }
        void DoCommandAdd(object p) { OpenDetail(null, p); }
        public Action<object> CommandCloseDetails { get { return DoCommandCloseDetails; } }
        void DoCommandCloseDetails(object p) { CloseDetails(); }
        public Action<object> CommandChangeViewOptions { get { return DoCommandChangeViewOptions; } }
        void DoCommandChangeViewOptions(object p) { ChangeViewOptions(); }
        #endregion
    }
}
