using CommonDemo.Helpers;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace ControlsDemo.TabControl.PinnedTabs {
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
            Photos[5].PinMode = TabPinMode.Right;
        }

        public virtual ObservableCollection<NaturePhoto> Photos { get; protected set; }
        public void AddNewTab(TabControlTabAddingEventArgs e) {
            e.Item = NaturePhoto.Create("New Las Vegas Photo", ImagesHelper.GetRandomLasVegasPhoto());
        }
    }
    public class NaturePhoto {
        public static NaturePhoto Create(string title, ImageSource photo) {
            return ViewModelSource.Create(() => new NaturePhoto(title, photo));
        }
        protected NaturePhoto(string title, ImageSource photo) {
            Title = title;
            Photo = photo;
        }

        public string Title { get; private set; }
        public ImageSource Photo { get; private set; }
        public virtual TabPinMode PinMode { get; set; }
        
        public void Pin(TabPinMode mode) {
            PinMode = mode;
        }
        public bool CanPin(TabPinMode mode) {
            return !PinMode.Equals(mode);
        }
    }
}
