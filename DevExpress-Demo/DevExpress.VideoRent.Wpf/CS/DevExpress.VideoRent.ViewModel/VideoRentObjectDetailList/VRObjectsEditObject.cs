using System;
using System.ComponentModel;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IVRObjectsEditObjectParent<T> where T : VideoRentBaseObject {
        AllObjects<T> GetVideoRentObjects();
    }
    public abstract class VRObjectsEditObject<T> : EditableSubobject where T : VideoRentBaseObject {
        AllObjects<T> videoRentObjects;

        public VRObjectsEditObject(EditableObject parent) : base(parent) { }
        public IVRObjectsEditObjectParent<T> VRObjectsEditObjectParent { get { return (IVRObjectsEditObjectParent<T>)Parent; } }
        public AllObjects<T> VideoRentObjects {
            get { return videoRentObjects; }
            private set { SetValue<AllObjects<T>>("VideoRentObjects", ref videoRentObjects, value, true, RaiseVideoRentObjectsChanged); }
        }
        public event ThePropertyChangedEventHandler<AllObjects<T>> VideoRentObjectsChanged;
        protected override void UpdateOverride() {
            if(VideoRentObjects != null)
                UnsubscribeFromChanged();
            VideoRentObjects = VRObjectsEditObjectParent.GetVideoRentObjects();
            if(VideoRentObjects != null)
                SubscribeToChanged();
        }
        protected override void DisposeManaged() {
            if(VideoRentObjects != null)
                UnsubscribeFromChanged();
            VideoRentObjects = null;
            base.DisposeManaged();
        }
        void RaiseVideoRentObjectsChanged(AllObjects<T> oldValue, AllObjects<T> newValue) {
            if(VideoRentObjectsChanged != null)
                VideoRentObjectsChanged(this, new ThePropertyChangedEventArgs<AllObjects<T>>(oldValue, newValue));
        }
        protected virtual void SubscribeToChanged() {
            VideoRentObjects.ListChanged += OnVideoRentObjectsListChanged;
        }
        protected virtual void UnsubscribeFromChanged() {
            VideoRentObjects.ListChanged -= OnVideoRentObjectsListChanged;
        }
        void OnVideoRentObjectsListChanged(object sender, ListChangedEventArgs e) { RaiseChanged(); }
    }
}
