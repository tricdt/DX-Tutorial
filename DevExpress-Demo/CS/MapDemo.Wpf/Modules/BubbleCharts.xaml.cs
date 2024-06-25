using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace MapDemo {
    public partial class BubbleCharts : MapDemoModule {
        const double MinMag = 6.5;
        const double MaxMag = 9;

        ICollectionView dataView;
                
        public ICollectionView Data { get { return dataView; } }

        public BubbleCharts() {
            InitializeComponent();
            lbYearsFilter.SelectAll();
            DataContext = this;
            SetData();
        }
        void SetData() {
            List<EarthquakeViewModel> earthquakes = EarthquakesData.Instance.Items;
            this.dataView = CollectionViewSource.GetDefaultView(earthquakes);
            UpdateFilter(MinMag, MaxMag);
        }
        void UpdateFilter(double minMagnitude, double maxMagnitude) {
            if(Data != null)
                Data.Filter = (item) => {
                    EarthquakeViewModel earthquake = (EarthquakeViewModel)item;
                    return GetMagnitudeFilter(earthquake, minMagnitude, maxMagnitude) && GetYearFilter(earthquake, lbYearsFilter.SelectedItems);
                };
        }
        bool GetMagnitudeFilter(EarthquakeViewModel earthquake, double min, double max) {
            return earthquake.Magnitude >= min && earthquake.Magnitude <= max;
        }
        bool GetYearFilter(EarthquakeViewModel earthquake, IList<object> selectedYears) {
            foreach(ListBoxEditItem item in selectedYears) {
                int year = Convert.ToInt32(item.Tag);
                if(earthquake.Year == year)
                    return true;
            }
            return false;
        }
        void lbYearsFilter_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Update();
        }
        void tbMagnitudeFilter_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Update();
        }
        void Update(){
            TrackBarEditRange range = (TrackBarEditRange)tbMagnitudeFilter.EditValue;
            UpdateFilter(range.SelectionStart, range.SelectionEnd);
        }
    }
}
