using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DialogsDemo.Helpers {
    [XmlRoot("Filters")]
    public class Filters : List<Filter> {
    }
    public class Filter {
        public string Description { get; set; }
        public string Expression { get; set; }
        public string FilterString { get { return string.Join(FiltersHelper.FilterSeparator, Description, Expression); } }
        public bool IsEnable { get; set; }
    }
    public static class FiltersHelper {
        public const string FilterSeparator = "|";
        public static readonly Filters PhotoFilters;

        static FiltersHelper() {
            using(var stream = DialogsDemoHelper.GetDataStream("PhotoFilters.xml")) {
                var serializer = new XmlSerializer(typeof(Filters));
                PhotoFilters = (Filters)serializer.Deserialize(stream);
            }
        }
        public static string GetFilterString(this Filters filters) {
            return filters.Where(x => x.IsEnable).Aggregate(string.Empty, (a, x) => string.Join(string.IsNullOrEmpty(a) ? string.Empty : FiltersHelper.FilterSeparator, a, x.FilterString));
        }
    }
}
