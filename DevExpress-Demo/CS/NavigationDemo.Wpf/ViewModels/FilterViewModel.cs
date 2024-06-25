using System.Windows;
using System.Windows.Media;
using NavigationDemo.Utils;

namespace NavigationDemo {
    public class FilterViewModel {
        static Size filterPreviewImageSize = new Size(200, 136);

        public FilterViewModel(string uri, FilterBase filter) {
            Filter = filter;
            Uri = uri;
        }
        public virtual string Uri { get; set; }
        public virtual ImageSource Image { get; protected set; }
        public virtual FilterBase Filter { get; protected set; }
        public virtual string Name { get { return Filter != null ? Filter.Name : string.Empty; } }
        public virtual void OnUriChanged() {
            Image = Filter.ApplyFilter(Uri, filterPreviewImageSize);
        }
    }
}
