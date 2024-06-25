using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using DevExpress.Internal;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using NavigationDemo.Utils;

namespace NavigationDemo {
    public class PhotoStudioViewModel {

        UniversalFilter universalFilter;
        ContrastFilter contrastFilter;
        bool allowUpdateColors = true;
        bool allowUpdateBrightness = true;
        bool allowUpdateContrast = true;

        public PhotoStudioViewModel() {
            CreatePhotos();
            universalFilter = new UniversalFilter();
            contrastFilter = new ContrastFilter();
        }

        public virtual void OnLoaded() {
            SelectedPhoto = Photos.First();
            FilteredImage = SelectedPhoto.Image;
            ClearAllFilters();
        }
        public void OnUnloaded() {
        }

        public virtual IEnumerable<PhotoInfo> Photos { get; set; }

        public virtual PhotoInfo SelectedPhoto { get; set; }
        public virtual ImageSource FilteredImage { get; set; }

        public virtual IEnumerable<FilterViewModel> Filters { get; set; }
        public virtual FilterViewModel SelectedFilter { get; set; }

        public virtual int Brightness { get; set; }
        public virtual int Contrast { get; set; }

        [BindableProperty(OnPropertyChangedMethodName = "OnColorChanged")]
        public virtual int RColor { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnColorChanged")]
        public virtual int GColor { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnColorChanged")]
        public virtual int BColor { get; set; }

        protected void OnSelectedFilterChanged() {
            UpdateFilteredImage();
        }
        protected void OnSelectedPhotoChanged() {
            if(SelectedPhoto == null)
                return;
            UpdateFilters();
            ClearAllFilters();
            UpdateFilteredImage();
        }

        void UpdateFilteredImage() {
            if(SelectedFilter == null) {
                FilteredImage = SelectedPhoto.Image;
                return;
            }
            ClearContrast();
            ClearColors();
            ClearBrightness();
            FilteredImage = SelectedFilter.Filter.ApplyFilter(SelectedPhoto.Uri);
            return;
        }
        protected void OnBrightnessChanged() {
            if(!allowUpdateBrightness)
                return;
            ClearContrast();
            ClearColors();
            SelectedFilter = null;
            FilteredImage = universalFilter.ApplyFilter(SelectedPhoto.Uri, Brightness, Brightness, Brightness);
        }
        protected void OnContrastChanged() {
            if(!allowUpdateContrast)
                return;
            ClearBrightness();
            ClearColors();
            SelectedFilter = null;
            FilteredImage = contrastFilter.ApplyFilter(SelectedPhoto.Uri, Contrast);
        }

        protected void OnColorChanged() {
            if(!allowUpdateColors)
                return;
            ClearContrast();
            ClearBrightness();
            SelectedFilter = null;
            FilteredImage = universalFilter.ApplyFilter(SelectedPhoto.Uri, RColor, GColor, BColor);
        }
        void ClearAllFilters() {
            ClearBrightness();
            ClearContrast();
            ClearColors();
            SelectedFilter = null;
        }
        void ClearBrightness() {
            allowUpdateBrightness = false;
            Brightness = 1;
            allowUpdateBrightness = true;
        }
        void ClearContrast() {
            allowUpdateContrast = false;
            Contrast = 1;
            allowUpdateContrast = true;
        }
        void ClearColors() {
            allowUpdateColors = false;
            RColor = 1;
            GColor = 1;
            BColor = 1;
            allowUpdateColors = true;
        }
        void UpdateFilters() {
            var uri = SelectedPhoto.Uri;
            if(Filters != null) {
                Filters.ForEach(x => x.Uri = uri);
                return;
            }
            var imageFactory = ViewModelSource.Factory((string uriString, FilterBase filter) => new FilterViewModel(uriString, filter));
            Filters = new FilterViewModel[] {
                imageFactory(uri, new PolaroidFilter()),
                imageFactory(uri, new GBRFilter()),
                imageFactory(uri, new GrayScaleFilter()),
                imageFactory(uri, new BGRFilter()),
                imageFactory(uri, new SepiaFilter()),
                imageFactory(uri, new NegativeFilter())
            };
        }
        void CreatePhotos() {
            var random = new Random();

            Func<string, string> getDirectoryPath = photosFolderName => DataDirectoryHelper.GetFile(@"Photos\" + photosFolderName, DataDirectoryHelper.DataFolderName);
            Func<string, string[]> getPhotoPaths = directoryPath => Directory.GetFiles(directoryPath, "*.jpg");
            var paths = getPhotoPaths(getDirectoryPath("Photos")).Concat(getPhotoPaths(getDirectoryPath("AdditionalPhotos")));

            var photoFactory = ViewModelSource.Factory((string uriString, int rating) => new PhotoInfo(uriString, rating));
            Photos = paths.Select(path => photoFactory(path, random.Next(5)));
        }
    }
}
