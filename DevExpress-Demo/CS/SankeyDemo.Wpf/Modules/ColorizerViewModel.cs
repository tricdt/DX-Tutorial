using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts.Sankey;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SankeyDemo {
    public class ColorizerViewModel : BindableBase {
        public static ColorizerViewModel Create() {
            return ViewModelSource.Create(() => new ColorizerViewModel());
        }

        bool isAsiaChecked = true;
        bool isAustraliaChecked = true;
        bool isEuropeChecked = true;
        bool isNorthAmericaChecked = true;
        bool isSouthAmericaChecked = true;
        Dictionary<string, List<string>> continentCountriesPairs;

        public virtual bool IsAsiaChecked {
            get { return isAsiaChecked; }
            set {
                isAsiaChecked = value;
                RefreshData();
            }
        }
        public virtual bool IsAustraliaChecked {
            get { return isAustraliaChecked; }
            set {
                isAustraliaChecked = value;
                RefreshData();
            }
        }
        public virtual bool IsEuropeChecked {
            get { return isEuropeChecked; }
            set {
                isEuropeChecked = value;
                RefreshData();
            }
        }
        public virtual bool IsNorthAmericaChecked {
            get { return isNorthAmericaChecked; }
            set {
                isNorthAmericaChecked = value;
                RefreshData();
            }
        }
        public virtual bool IsSouthAmericaChecked {
            get { return isSouthAmericaChecked; }
            set {
                isSouthAmericaChecked = value;
                RefreshData();
            }
        }
        public virtual Dictionary<string, List<string>> ContinentCountriesPairs {
            get { return continentCountriesPairs; }
        }
        public virtual List<Export> DataSource { get; set; }

        protected ColorizerViewModel() {
            continentCountriesPairs = ContinentCountries.GetContinentCountriesPairs_ColorizerDemo();
            RefreshData();
        }
        void RefreshData() {
            List<Export> data = LargestExportPartners.GetData();
            Func<Export, string, bool> removeFunction = (Export e, string country) => {
                return continentCountriesPairs[country].Contains(e.Importer) || continentCountriesPairs[country].Contains(e.Exporter);
            };
            if(!isNorthAmericaChecked)
                data.RemoveAll(x => removeFunction(x, "North America"));
            if(!isSouthAmericaChecked)
                data.RemoveAll(x => removeFunction(x, "South America"));
            if(!isAsiaChecked)
                data.RemoveAll(x => removeFunction(x, "Asia"));
            if(!isAustraliaChecked)
                data.RemoveAll(x => removeFunction(x, "Australia"));
            if(!isEuropeChecked)
                data.RemoveAll(x => removeFunction(x, "Europe"));
            DataSource = data;
        }
    }
}
