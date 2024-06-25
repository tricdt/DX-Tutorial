using DevExpress.Internal;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DialogsDemo.Helpers;
using DevExpress.Xpf.Core;
using System.Collections.Generic;
using DevExpress.Xpf.Dialogs;
using DevExpress.Mvvm.DataAnnotations;
using CommonDemo.Helpers;
using DevExpress.Xpf.DemoBase.Helpers;

namespace DialogsDemo {
    public class FileDialogsViewModel {
        string DefaultPhotoGalleryPath { get { return DialogsDemoHelper.GetPhotosPath("Photos"); } }
        public string AdditionalPhotosPath { get { return DialogsDemoHelper.GetPhotosPath("AdditionalPhotos"); } }
        string DefaultPhotoExtension { get { return ".jpg"; } }

        [BindableProperty]
        public virtual ObservableCollection<PhotoData> Photos { get; protected set; }
        [BindableProperty]
        public virtual PhotoData CurrentPhoto { get; set; }

        [BindableProperty]
        public virtual string DefaultExt { get; set; }
        [BindableProperty]
        public virtual string GalleryDirectory { get; protected set; }

        public Filters Filters { get { return FiltersHelper.PhotoFilters; } }
        public string[] Extensions { get; set; }

        public FileDialogsViewModel() {
            UpdateExtensions();
            UpdateGallery(DefaultPhotoGalleryPath);
        }

        void UpdateExtensions() {
            Extensions = new string[] { "JPG", "PNG", "BMP", "JPEG" };
            DefaultExt = Extensions.FirstOrDefault();
        }
        void UpdateGallery(string path) {
            GalleryDirectory = Path.GetFullPath(path);
            Photos = new ObservableCollection<PhotoData>(ImageHelper.GetJPGPhotos(GalleryDirectory));
            CurrentPhoto = Photos.FirstOrDefault();
        }

        public void ChangeGalleryDirectory() {
            var folderBrowser = GetFolderBrowserDialog();
            if(folderBrowser.ShowDialog().Value) {
                UpdateGallery(folderBrowser.FileName);
            }
        }
        public void UploadPhotos() {
            var openDialog = GetOpenDialog();
            if(openDialog.ShowDialog().Value) {
                foreach(var file in openDialog.FileNames) {
                    Photos.Add(ImageHelper.GetJPGPhoto(file));
                    CopyPhoto(file, GalleryDirectory + "\\" + Path.GetFileName(file));
                }
            }
        }

        public void DownloadPhoto() {
            var saveDialog = GetSaveDialog();
            if(saveDialog.ShowDialog().Value) {
                var file = saveDialog.FileName;
                CopyPhoto(Path.Combine(GalleryDirectory, CurrentPhoto.PhotoName + DefaultPhotoExtension), file);
            }
        }
        public bool CanDownloadPhoto() {
            return CurrentPhoto != null;
        }

        void CopyPhoto(string sourceFilePath, string destFileName) {
            try {
                File.Copy(sourceFilePath, destFileName, true);
            } catch(System.Exception e) {
                ThemedMessageBox.Show("Error", e.Message);
            }
        }

        #region dialogs
        DXOpenFileDialog GetOpenDialog() {
            return new DXOpenFileDialog() {
                InitialDirectory = AdditionalPhotosPath,
                Filter = Filters.GetFilterString(),
                Title = "DX Open Dialog",
                Multiselect = true,
            };
        }
        DXSaveFileDialog GetSaveDialog() {
            return new DXSaveFileDialog() {
                InitialDirectory = AdditionalPhotosPath,
                DefaultExt = DefaultExt,
                Title = "DX Save Dialog",
            };
        }
        DXOpenFileDialog GetFolderBrowserDialog() {
            return new DXOpenFileDialog() {
                InitialDirectory = DialogsDemoHelper.GetPhotosPath(string.Empty),
                Title = "DX Open folder Dialog",
                OpenFileDialogMode = OpenFileDialogMode.Folders,
            };
        }
        #endregion
    }
}
