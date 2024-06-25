using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieAddItemsEditObject : AddVRObjectEditObject<MovieItem> {
        int itemsCount;

        public MovieAddItemsEditObject(EditableObject parent, Guid parentVRObjectOid) : base(parent, parentVRObjectOid) {
            ItemsCount = 1;
        }
        public int ItemsCount {
            get { return itemsCount; }
            set { SetValue<int>("ItemsCount", ref itemsCount, value); }
        }
        protected override MovieItem CreateObjectOverride() {
            return new MovieItem(SessionHelper.GetObjectByKey<Movie>(ParentVRObjectOid, NestedSession));
        }
        internal override void SaveAndDispose() {
            VideoRentObject.Location = string.Format(ConstStrings.Get("ShelfPattern"), VideoRentObject.Movie.MovieId % ReferenceData.ShelvesCount + 1);
            for(int i = 0; i < itemsCount - 1; ++i) {
                MovieItem item = new MovieItem(VideoRentObject.Movie, VideoRentObject.Format, VideoRentObject.SellingPrice);
                item.AvailableForSell = VideoRentObject.AvailableForSell;
                item.Location = VideoRentObject.Location;
            }
            base.SaveAndDispose();
        }
    }
}
