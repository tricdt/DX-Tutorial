using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo.ModuleResources {
    public class SearchPanelParseModeItem  {
        public SearchPanelParseMode Mode { get; set; }
        public string Description { get; set; }
    }
    public class SearchPanelParseModes {
        static SearchPanelParseModes() {
            Items = new List<SearchPanelParseModeItem>();
            Items.Add(new SearchPanelParseModeItem() { Mode = SearchPanelParseMode.Mixed, Description = "Search words are combined by the Or operator. The operator changes to And if you specify a column name before a search word." });
            Items.Add(new SearchPanelParseModeItem() { Mode = SearchPanelParseMode.Exact, Description = "The search engine does not split the query into individual words and thereby looks for exact matches." });
            Items.Add(new SearchPanelParseModeItem() { Mode = SearchPanelParseMode.Or, Description = "Search words are combined by the Or operator." });
            Items.Add(new SearchPanelParseModeItem() { Mode = SearchPanelParseMode.And, Description = "Search words are combined by the And operator." });
        }
        public static List<SearchPanelParseModeItem> Items { get; private set; }
    }
}
