using System.Linq;

namespace ChartsDemo {

    public class SelectionViewModel {
        CountriesInfo countriesData;

        public CountriesInfo CountriesData { get { return this.countriesData; } }
        public virtual CountryStatisticInfo SelectedCountry { get; set; }
        public virtual int SelectedIndex { get; protected set; }
        public virtual string SelectedCountryName { get; set; }

        public SelectionViewModel() {
            this.countriesData = CountriesInfo.Load();
            SelectedCountry = CountriesData.First();
        }

        protected void OnSelectedCountryChanged() {
            SelectedCountryName = SelectedCountry != null ? SelectedCountry.Name : string.Empty;
            SelectedIndex = CountriesData.IndexOf(SelectedCountry);
        }
        protected void OnSelectedCountryNameChanged() {
            SelectedCountry = CountriesData.FirstOrDefault(x => x.Name == SelectedCountryName);
        }
    }

}
