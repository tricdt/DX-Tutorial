Imports DevExpress.Xpf.DemoBase
Imports GridDemo.Filtering

Namespace GridDemo

    Friend Class FilteringModule
        Inherits GridShowcaseBrowserModule

        Const path As String = "Modules/Filtering"

        Protected Overrides Function CreateShowcases() As IShowcaseInfo()
            Const Version As String = "?v=" & AssemblyInfo.VersionShort
            Const PopupUri As String = "120529/controls-and-libraries/data-grid/filtering-and-searching/filter-dropdown/excel-style-drop-down-filter" & Version & "#"
            Const EditorUri As String = "7788/controls-and-libraries/data-grid/filtering-and-searching/filter-editor" & Version & "#"
            Const SearchUri As String = "11402/controls-and-libraries/data-grid/filtering-and-searching/search" & Version
            Const ElementsUri As String = "400314/controls-and-libraries/data-grid/filtering-and-searching/filter-elements" & Version
            Const FormatConditionFiltersUri As String = "401262/controls-and-libraries/data-grid/filtering-and-searching/conditional-formatting-filters" & Version
            Return New IShowcaseInfo() {New ShowcaseNode("Excel-style Drop-Down Filter") With {.Children = New IShowcaseInfo() {LoadShowcase("Custom Filter Popup Content", PopupUri & "customize-the-excel-style-drop-down-filter", path, {GetType(CustomFilterPopupContent)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Customize the Operator List", PopupUri & "customize-the-operator-list", path, {GetType(ColumnOperators)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Customize Operands", PopupUri & "customize-operand-template", path, {GetType(ColumnOperatorTemplateSelector)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Predefined Filters", PopupUri & "predefined-filters", path, {GetType(ColumnPredefinedFilters)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Group Filters", PopupUri & "group-filters", path, {GetType(ColumnGroupFilters)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}}, New ShowcaseNode("Filter Editor") With {.Children = New IShowcaseInfo() {LoadShowcase("Customize the Field List", EditorUri & "customize-the-field-list", path, {GetType(FilterEditorFields)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Customize the Operator List", EditorUri & "customize-the-operator-list", path, {GetType(FilterEditorOperators)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Customize Operand Values", EditorUri & "customize-operand-values", path, {GetType(FilterEditorCustomizeOperandValues)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Customize Operand Templates", EditorUri & "customize-operand-template", path, {GetType(FilterEditorCustomizeOperandTemplates)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Prohibit Group Types", EditorUri & "prohibit-group-types", path, {GetType(FilterEditorGroupTypes)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Prohibit Group Operations", EditorUri & "prohibit-group-operations", path, {GetType(FilterEditorGroupOperations)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Prohibit Condition Operations", EditorUri & "prohibit-removing-conditions", path, {GetType(FilterEditorConditionOperations)}, showCodeBehind:=True, useNewDocsSite:=True), LoadShowcase("Standalone Filter Editor", EditorUri & "standalone-filter-editor", path, {GetType(FilterEditorStandalone)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}}, New ShowcaseNode("Filter Elements") With {.Children = New IShowcaseInfo() {LoadShowcase("Checked List", ElementsUri, path, {GetType(CheckedListElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Checked List - Grid Template", ElementsUri, path, {GetType(CheckedListElementGridTemplate)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Checked List - Chart Template", ElementsUri, path, {GetType(CheckedListElementChartTemplate)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Radio List", ElementsUri, path, {GetType(RadioListElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Checked Tree List", ElementsUri, path, {GetType(CheckedTreeListElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Checked Dates Tree List", ElementsUri, path, {GetType(CheckedDatesTreeListElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Calendar", ElementsUri, path, {GetType(CalendarElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Date Periods", ElementsUri, path, {GetType(DatePeriodsElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Date Range", ElementsUri, path, {GetType(DateRangeElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Date Range - RangeControl Template", ElementsUri, path, {GetType(DateRangeElementRangeControlTemplate)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Range", ElementsUri, path, {GetType(RangeElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Range - RangeControl Template", ElementsUri, path, {GetType(RangeElementRangeControlTemplate)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Format Condition Filters", FormatConditionFiltersUri, path, {GetType(FormatConditionElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Predefined Filters", ElementsUri, path, {GetType(PredefinedFilterListElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Check Box", ElementsUri, path, {GetType(CheckBoxElement)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Delay Applying Filters", ElementsUri, path, {GetType(DelayApplyingFilters)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}}, New ShowcaseNode("Search Panel") With {.Children = New IShowcaseInfo() {LoadShowcase("Filter Data", SearchUri, path, {GetType(SearchPanelFilterMode)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Highlight Data", SearchUri, path, {GetType(SearchPanelHighlightMode)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Parse Modes", SearchUri, path, {GetType(SearchPanelParseMode)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}}, LoadShowcase("Auto Filter Row", "6132/controls-and-libraries/data-grid/filtering-and-searching/automatic-filter-row" & Version, path, {GetType(AutoFilterRow)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Filter Panel", "6409/controls-and-libraries/data-grid/visual-elements/common-elements/filter-panel" & Version, path, {GetType(FilterPanel)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True), LoadShowcase("Incremental Search", "118017/controls-and-libraries/data-grid/filtering-and-searching/search/incremental-search" & Version, path, {GetType(IncrementalSearch)}, showCodeBehind:=True, codeOrder:=SourceCodeOrder.XamlFirst, useNewDocsSite:=True)}
        End Function
    End Class
End Namespace