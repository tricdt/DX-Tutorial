using DevExpress.Mvvm.DataAnnotations;

namespace GridDemo {
    [POCOViewModel]
    public class AlignGroupSummariesByColumnsViewModel {
        protected AlignGroupSummariesByColumnsViewModel() {
            HighlightBestSelling = true;
            HighlightWorstSelling = true;
        }
        public virtual bool HighlightBestSelling { get; set; }
        public virtual bool HighlightWorstSelling { get; set; }
    }
}
