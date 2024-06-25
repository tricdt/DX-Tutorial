using CommonDemo.Helpers;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace ControlsDemo.TabControl.Views {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel() {
            Photos = new ObservableCollection<NaturePhoto>();
            for(int i = 0; i < 6; i++) {
                var photo = ImagesHelper.NaturePhotos.ElementAt(i);
                Photos.Add(NaturePhoto.Create("Nature Photo #" + i.ToString(), photo));
            }
        }

        public virtual ObservableCollection<NaturePhoto> Photos { get; protected set; }
        public void AddNewTab(TabControlTabAddingEventArgs e) {
            e.Item = NaturePhoto.Create("New Las Vegas Photo", ImagesHelper.GetRandomLasVegasPhoto());
        }
        public void SetIsEnabled(bool value) {
            foreach(var item in Photos)
                item.IsEnabled = value;
        }
        public void SetGlyphVisible(bool visible) {
            foreach(var item in Photos)
                item.ShowGlyph = visible;
        }
    }
    public class NaturePhoto {
        public static NaturePhoto Create(string title, ImageSource photo) {
            return ViewModelSource.Create(() => new NaturePhoto(title, photo));
        }
        protected NaturePhoto(string title, ImageSource photo) {
            Title = title;
            Photo = photo;
            IsEnabled = true;
            IsVisible = true;
        }

        public string Title { get; private set; }
        public ImageSource Photo { get; private set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool ShowGlyph { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual TabPinMode PinMode { get; set; }
        public ImageSource Glyph { get { return ShowGlyph ? ImagesHelper.GroupIcon : null; } }

        public void SetIsEnabled(bool value) {
            IsEnabled = value;
        }
        public bool CanSetIsEnabled(bool value) {
            return IsEnabled != value;
        }
        protected void OnShowGlyphChanged() {
            this.RaisePropertyChanged(x => x.Glyph);
        }
        public void Pin(TabPinMode mode) {
            PinMode = mode;
        }
        public bool CanPin(TabPinMode mode) {
            return !PinMode.Equals(mode);
        }
    }
}
