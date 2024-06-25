using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Resources;

namespace DevExpress.VideoRent.ViewModel {
    public abstract class VRObjectDetail<T> : ModuleObjectDetail where T : VideoRentBaseObject {
        string title;
        string titleDraft;

        public VRObjectDetail(VRObjectDetailObject<T> editObject) : this(editObject, null) { }
        public VRObjectDetail(VRObjectDetailObject<T> editObject, object tag)
            : base(editObject, tag) {
        }
        public VRObjectDetailObject<T> VRObjectDetailEditObject { get { return (VRObjectDetailObject<T>)EditObject; } }
        public string Title {
            get { return title; }
            private set { SetValue<string>("Title", ref title, value); }
        }
        public string TitleDraft {
            get { return titleDraft; }
            protected set { SetValue<string>("TitleDraft", ref titleDraft, value, RaiseTitleDraftChanged); }
        }
        protected override YesNoCancel AskSaveChanges() {
            MessageBoxResult result = MessageBox.Show(ConstStrings.Get("CloseCancelFormWarning"), TitleDraft, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
            return result == MessageBoxResult.Yes ? YesNoCancel.Yes : result == MessageBoxResult.No ? YesNoCancel.No : YesNoCancel.Cancel;
        }
        protected override void RaiseDirtyRoughChanged(bool oldValue, bool newValue) {
            base.RaiseDirtyRoughChanged(oldValue, newValue);
            UpdateTitle();
        }
        void RaiseTitleDraftChanged(string oldValue, string newValue) {
            UpdateTitle();
        }
        void UpdateTitle() {
            Title = TitleDraft + (DirtyRough ? " *" : string.Empty);
        }
    }
}
