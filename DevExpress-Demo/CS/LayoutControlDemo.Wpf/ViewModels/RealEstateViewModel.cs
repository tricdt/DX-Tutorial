using System.Collections.Generic;
using System.Linq;

namespace LayoutControlDemo {
    public class RealEstateViewModel {
        public RealEstateViewModel() {
            Source = Listings.DataSource;
            SelectedListing = Source.First();
        }
        IList<Listing> Source { get; set; }
        public virtual Listing SelectedListing { get; set; }

        public void NextListing() {
            var index = SelectedIndex();
            SelectedListing = Source[++index];
        }
        public bool CanNextListing() {
            return SelectedListing != Source.Last();
        }
        public void PreviousListing() {
            var index = SelectedIndex();
            SelectedListing = Source[--index];
        }
        public bool CanPreviousListing() {
            return SelectedListing != Source.First();
        }
        int SelectedIndex() {
            return Source.IndexOf(SelectedListing);
        }
    }
}
