using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoriesEdit : VRObjectsEdit<MovieCategory> {
        bool flipLayoutSignal;
        string chartPaletteName;
        int chartMarkerSize;
        double chartMarkerAngle;
        string chartMarkerKindName;

        public MovieCategoriesEdit(MovieCategoriesEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) {
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
        }
        public new MovieCategoriesEditObject VRObjectsEditObject { get { return (MovieCategoriesEditObject)EditObject; } }
        public bool FlipLayoutSignal {
            get { return flipLayoutSignal; }
            set { SetValue<bool>("FlipLayoutSignal", ref flipLayoutSignal, value); }
        }
        public string ChartPaletteName {
            get { return chartPaletteName; }
            set { SetValue<string>("ChartPaletteName", ref chartPaletteName, value); }
        }
        public int ChartMarkerSize {
            get { return chartMarkerSize; }
            set { SetValue<int>("ChartMarkerSize", ref chartMarkerSize, value); }
        }
        public double ChartMarkerAngle {
            get { return chartMarkerAngle; }
            set { SetValue<double>("ChartMarkerAngle", ref chartMarkerAngle, value); }
        }
        public string ChartMarkerKindName {
            get { return chartMarkerKindName; }
            set { SetValue<string>("ChartMarkerKindName", ref chartMarkerKindName, value); }
        }
        protected override bool CoerceCurrentRecord(MovieCategory value) {
            if(!base.CoerceCurrentRecord(value)) return false;
            return Detail.DoValidate();
        }
        public override bool DeleteCurrentRecord() {
            bool ret = base.DeleteCurrentRecord();
            if(ret)
                SetCurrentRecord(null);
            return ret;
        }
        public void FlipLayout() {
            FlipLayoutSignal = true;
            FlipLayoutSignal = false;
        }
        internal void SetCurrentRecord(MovieCategory category) {
            CurrentRecord = category == null ? MovieCategory.GetDefaultCategory(VRObjectsEditObject.VideoRentObjects.Session) : category;
        }
        protected override void DisposeManaged() {
            LayoutManager.Current.Unsubscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
            base.DisposeManaged();
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            ChartPaletteName = layoutData.MovieCategoriesEditChartPaletteName;
            ChartMarkerSize = layoutData.MovieCategoriesEditChartMarkerSize;
            ChartMarkerKindName = layoutData.MovieCategoriesEditChartMarkerKindName;
            ChartMarkerAngle = layoutData.MovieCategoriesEditChartMarkerAngle;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            layoutData.MovieCategoriesEditChartPaletteName = ChartPaletteName;
            layoutData.MovieCategoriesEditChartMarkerSize = ChartMarkerSize;
            layoutData.MovieCategoriesEditChartMarkerAngle = ChartMarkerAngle;
            layoutData.MovieCategoriesEditChartMarkerKindName = ChartMarkerKindName;
        }
        #region Commands
        public Action<object> CommandFlipLayout { get { return DoCommandFlipLayout; } }
        void DoCommandFlipLayout(object p) { FlipLayout(); }
        #endregion
    }
}
