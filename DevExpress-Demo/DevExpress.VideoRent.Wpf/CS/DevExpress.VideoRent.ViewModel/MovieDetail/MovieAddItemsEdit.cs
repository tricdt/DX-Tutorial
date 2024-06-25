using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieAddItemsEdit : AddVRObjectEdit<MovieItem> {
        MovieItemFormatEditData movieItemFormatEditData;

        public MovieAddItemsEdit(MovieAddItemsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            MovieItemFormatEditData = new MovieItemFormatEditData();
        }
        public MovieItemFormatEditData MovieItemFormatEditData {
            get { return movieItemFormatEditData; }
            set { SetValue<MovieItemFormatEditData>("MovieItemFormatEditData", ref movieItemFormatEditData, value); }
        }
    }
}
