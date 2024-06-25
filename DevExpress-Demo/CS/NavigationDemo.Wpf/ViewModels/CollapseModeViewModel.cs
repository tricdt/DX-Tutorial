using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace NavigationDemo {
    public class CollapseModeViewModel {

        public ReadOnlyCollection<FilterGroup> FiltersData { get; private set; }

        public CollapseModeViewModel() {
            FiltersData = new ReadOnlyCollection<FilterGroup>(CreateFilters());
        }

        FilterGroup[] CreateFilters() {
            var experienceItems = new FilterItem[] {
                new FilterItem("> 24 years", "(DateDiffYear([HireDate], Today()) > 24)"),
                new FilterItem("> 19 and <= 24 years", "(DateDiffYear([HireDate], Today()) > 19 AND DateDiffYear([HireDate], Today()) <= 24)"),
                new FilterItem("> 14 and <= 19 years", "(DateDiffYear([HireDate], Today()) > 14 AND DateDiffYear([HireDate], Today()) <= 19)"),
                new FilterItem("<= 14 years", "(DateDiffYear([HireDate], Today()) <= 14)"),
            };
            var factory = ViewModelSource.Factory((string name, string filterString, bool showInCollapsedMode, byte[] icon) 
                => new FilterItem(name, filterString, showInCollapsedMode, icon));
            
            var regionItems = EmployeesWithPhotoData.DataSource.Select(x => x.CountryRegionName)
                .Distinct()
                .Select(x => factory(x, string.Format("([CountryRegionName] = '{0}')", x), false, GetFlag(x))   ).ToArray();
            regionItems.Take(4).ToList().ForEach(x => x.ShowInCollapsedMode = true);

            return new FilterGroup[] {
                new FilterGroup("Experience", experienceItems),
                new FilterGroup("Region", regionItems.ToArray())
            };
        }
        static byte[] GetFlag(string country) {
            var countryInfo = CountriesData.DataSource.FirstOrDefault(x => object.Equals(x.ActualName, country));
            return countryInfo == null ? null : countryInfo.Flag;
        }
    }

    public class FilterGroup {
        public string Name { get; private set; }
        public List<FilterItem> FilterItems { get; private set; }

        public FilterGroup(string name, FilterItem[] filterItems) {
            Name = name;
            FilterItems = filterItems.ToList();
        }
        public override string ToString() {
            return Name;
        }
    }
    public class FilterItem {
        public string Name { get; private set; }
        public string FilterString { get; private set; }
        public bool ShowInCollapsedMode { get; set; }
        [BindableProperty]
        public virtual bool CanSelect { get; set; }
        public object Icon { get; private set; }

        public FilterItem(string name, string filterString, bool showInCollapsedMode = false, byte[] icon = null) {
            Name = name;
            FilterString = filterString;
            ShowInCollapsedMode = showInCollapsedMode;
            Icon = icon;
            CanSelect = true;
        }
        public override string ToString() {
            return Name;
        }
    }
}
