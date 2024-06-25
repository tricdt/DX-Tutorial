using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistsEdit : VRObjectsEdit<Artist> {
        CountryEditData countryEditData;
        PersonGenderEditData personGenderEditData;
        GridUIOptions gridUIOptions;
        int currentArtistPicturesCount;

        public ArtistsEdit(ArtistsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            PersonGenderEditData = new PersonGenderEditData();
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
        }
        public new ArtistsEditObject VRObjectsEditObject { get { return (ArtistsEditObject)EditObject; } }
        public CountryEditData CountryEditData {
            get { return countryEditData; }
            private set { SetValue<CountryEditData>("CountryEditData", ref countryEditData, value, true); }
        }
        public PersonGenderEditData PersonGenderEditData {
            get { return personGenderEditData; }
            private set { SetValue<PersonGenderEditData>("PersonGenderEditData", ref personGenderEditData, value); }
        }
        public GridUIOptions GridUIOptions {
            get { return gridUIOptions; }
            set { SetValue<GridUIOptions>("GridUIOptions", ref gridUIOptions, value); }
        }
        public int CurrentArtistPicturesCount {
            get { return currentArtistPicturesCount; }
            set { SetValue<int>("CurrentArtistPicturesCount", ref currentArtistPicturesCount, value); }
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            CountryEditData = new CountryEditData(VRObjectsEditObject.VideoRentObjects.Session);
        }
        protected override void DisposeManaged() {
            CountryEditData = null;
            base.DisposeManaged();
        }
        protected override void RaiseCurrentRecordChangedOverride(Artist oldValue, Artist newValue) {
            base.RaiseCurrentRecordChangedOverride(oldValue, newValue);
            if(oldValue != null)
                oldValue.Pictures.ListChanged -= OnPicturesListChanged;
            if(newValue != null) {
                newValue.Pictures.ListChanged += OnPicturesListChanged;
                CurrentArtistPicturesCount = newValue.Pictures.Count;
            } else {
                CurrentArtistPicturesCount = 0;
            }
        }
        void OnPicturesListChanged(object sender, System.ComponentModel.ListChangedEventArgs e) {
            CurrentArtistPicturesCount = CurrentRecord == null ? 0 : CurrentRecord.Pictures.Count;
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            GridUIOptions = layoutData.ArtistsEditGridUIOptions;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
        }
    }
}
