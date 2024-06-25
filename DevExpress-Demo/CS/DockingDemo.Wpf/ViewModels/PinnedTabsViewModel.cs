using DockingDemo.Helpers;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System.Linq;
using System.Windows.Media;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Layout.Core;

namespace DockingDemo.ViewModels {
    public class PinnedTabsViewModel {
        public static PinnedTabsViewModel Create() {
            return ViewModelSource.Create(() => new PinnedTabsViewModel());
        }
        protected PinnedTabsViewModel() {
            Photos = new ObservableCollection<NaturePhoto>();
            for(int i = 0; i < 6; i++) {
                var photo = ImagesHelper.NaturePhotos.ElementAt(i);
                Photos.Add(NaturePhoto.Create("Nature Photo #" + i.ToString(), photo));
            }
            Photos[0].TogglePin();
            Photos[0].PinLocation = TabHeaderPinLocation.Far;
        }

        public virtual ObservableCollection<NaturePhoto> Photos { get; protected set; }
    }
    public class NaturePhoto {
        public static NaturePhoto Create(string title, string photo) {
            return ViewModelSource.Create(() => new NaturePhoto(title, photo));
        }
        protected NaturePhoto(string title, string photo) {
            Title = title;
            Photo = photo;
            Pinned = false;
        }
        public string Title { get; private set; }
        public string Photo { get; private set; }
        public bool Pinned { get; set; }
        public TabHeaderPinLocation PinLocation { get; set; }
        public void TogglePin() {
            Pinned = !Pinned;
        }
    }
}
