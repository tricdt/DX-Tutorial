using DevExpress.Xpf.DemoBase;
using GridDemo.Filtering;

namespace GridDemo {
    class FilteringModule : GridShowcaseBrowserModule {
        const string path = "Modules/Filtering";

        protected override IShowcaseInfo[] CreateShowcases() {
            const string Version = "?v=" + AssemblyInfo.VersionShort;
            const string PopupUri = "120529/controls-and-libraries/data-grid/filtering-and-searching/filter-dropdown/excel-style-drop-down-filter" + Version + "#";
            const string EditorUri = "7788/controls-and-libraries/data-grid/filtering-and-searching/filter-editor" + Version + "#";
            const string SearchUri = "11402/controls-and-libraries/data-grid/filtering-and-searching/search" + Version;
            const string ElementsUri = "400314/controls-and-libraries/data-grid/filtering-and-searching/filter-elements" + Version;
            const string FormatConditionFiltersUri = "401262/controls-and-libraries/data-grid/filtering-and-searching/conditional-formatting-filters" + Version;
            return new IShowcaseInfo[] {
                new ShowcaseNode("Excel-style Drop-Down Filter") {
                    Children = new IShowcaseInfo[] {
                        LoadShowcase("Custom Filter Popup Content", PopupUri + "customize-the-excel-style-drop-down-filter", path, new[] { typeof(CustomFilterPopupContent) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Customize the Operator List", PopupUri + "customize-the-operator-list", path, new[] { typeof(ColumnOperators) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Customize Operands", PopupUri + "customize-operand-template", path, new[] { typeof(ColumnOperatorTemplateSelector) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Predefined Filters", PopupUri + "predefined-filters", path, new[] { typeof(ColumnPredefinedFilters) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Group Filters", PopupUri + "group-filters", path, new[] { typeof(ColumnGroupFilters) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                    }
                },
                new ShowcaseNode("Filter Editor") {
                    Children = new IShowcaseInfo[] {
                        LoadShowcase("Customize the Field List", EditorUri + "customize-the-field-list", path, new[] { typeof(FilterEditorFields) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Customize the Operator List", EditorUri + "customize-the-operator-list", path, new[] { typeof(FilterEditorOperators) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Customize Operand Values", EditorUri + "customize-operand-values", path, new[] { typeof(FilterEditorCustomizeOperandValues) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Customize Operand Templates", EditorUri + "customize-operand-template", path, new[] { typeof(FilterEditorCustomizeOperandTemplates) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Prohibit Group Types",EditorUri + "prohibit-group-types", path, new[] { typeof(FilterEditorGroupTypes) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Prohibit Group Operations", EditorUri + "prohibit-group-operations", path, new[] { typeof(FilterEditorGroupOperations) }, showCodeBehind: true, useNewDocsSite: true),
                        LoadShowcase("Prohibit Condition Operations", EditorUri + "prohibit-removing-conditions", path, new[] { typeof(FilterEditorConditionOperations) }, showCodeBehind: true, useNewDocsSite: true),                        
                        LoadShowcase("Standalone Filter Editor", EditorUri + "standalone-filter-editor", path, new[] { typeof(FilterEditorStandalone) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                    }
                },
                new ShowcaseNode("Filter Elements") {
                    Children = new IShowcaseInfo[] {
                        LoadShowcase("Checked List", ElementsUri, path, new[] { typeof(CheckedListElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Checked List - Grid Template", ElementsUri, path, new[] { typeof(CheckedListElementGridTemplate) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Checked List - Chart Template", ElementsUri, path, new[] { typeof(CheckedListElementChartTemplate) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Radio List", ElementsUri, path, new[] { typeof(RadioListElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Checked Tree List", ElementsUri, path, new[] { typeof(CheckedTreeListElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Checked Dates Tree List", ElementsUri, path, new[] { typeof(CheckedDatesTreeListElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Calendar", ElementsUri, path, new[] { typeof(CalendarElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Date Periods", ElementsUri, path, new[] { typeof(DatePeriodsElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Date Range", ElementsUri, path, new[] { typeof(DateRangeElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Date Range - RangeControl Template", ElementsUri, path, new[] { typeof(DateRangeElementRangeControlTemplate) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Range", ElementsUri, path, new[] { typeof(RangeElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Range - RangeControl Template", ElementsUri, path, new[] { typeof(RangeElementRangeControlTemplate) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Format Condition Filters", FormatConditionFiltersUri, path, new[] { typeof(FormatConditionElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Predefined Filters", ElementsUri, path, new[] { typeof(PredefinedFilterListElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Check Box", ElementsUri, path, new[] { typeof(CheckBoxElement) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Delay Applying Filters", ElementsUri, path, new[] { typeof(DelayApplyingFilters) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                    }
                },
                new ShowcaseNode("Search Panel") {
                    Children = new IShowcaseInfo[] {
                        LoadShowcase("Filter Data", SearchUri, path, new[] { typeof(SearchPanelFilterMode) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Highlight Data", SearchUri, path, new[] { typeof(SearchPanelHighlightMode) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                        LoadShowcase("Parse Modes", SearchUri, path, new[] { typeof(SearchPanelParseMode) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                    }
                },
                LoadShowcase("Auto Filter Row", "6132/controls-and-libraries/data-grid/filtering-and-searching/automatic-filter-row" + Version, path, new[] { typeof(AutoFilterRow) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Filter Panel", "6409/controls-and-libraries/data-grid/visual-elements/common-elements/filter-panel" + Version, path, new[] { typeof(FilterPanel) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
                LoadShowcase("Incremental Search", "118017/controls-and-libraries/data-grid/filtering-and-searching/search/incremental-search" + Version, path, new[] { typeof(IncrementalSearch) }, showCodeBehind: true, codeOrder: SourceCodeOrder.XamlFirst, useNewDocsSite: true),
            };
        }
    }
}
